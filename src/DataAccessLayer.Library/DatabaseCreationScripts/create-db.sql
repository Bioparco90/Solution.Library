CREATE DATABASE Library;

GO;

USE Library;

-- Tables creation
CREATE TABLE Users (
    ID uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    Role INT DEFAULT 2
);

CREATE TABLE Books (
    ID uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(255) NOT NULL,
    AuthorName NVARCHAR(100) NOT NULL,
    AuthorSurname NVARCHAR(100) NOT NULL,
    PublishingHouse NVARCHAR(255) NOT NULL,
    Quantity INT NOT NULL,
    CONSTRAINT UC_Book UNIQUE (Title, AuthorName, AuthorSurname, PublishingHouse)
);

CREATE TABLE Reservations (
    ID uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    UserId uniqueidentifier NOT NULL,
    BookId uniqueidentifier NOT NULL,
    StartDate DATE NOT NULL DEFAULT GETDATE(),
    EndDate DATE NOT NULL DEFAULT DATEADD(DAY, 30, GETDATE()),
    CONSTRAINT FK_UserReservation FOREIGN KEY (UserId) REFERENCES Users(ID),
    CONSTRAINT FK_BookReservation FOREIGN KEY (BookId) REFERENCES Books(ID)
);

GO;

-- Trigger book deletion
CREATE TRIGGER TR_BookDelete
ON Books
INSTEAD OF DELETE
AS
BEGIN
    -- Check if there are active reservations for the books to be deleted
    IF EXISTS (
        SELECT 1
        FROM Reservations r
        INNER JOIN deleted d ON r.BookId = d.ID
        WHERE r.EndDate > GETDATE()
    )
    BEGIN
        -- If there are active reservations, cancel the deletion
        RAISERROR ('Cannot delete the book because there are active reservations.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- If there are no active reservations, delete associated reservations and then the book
        DELETE FROM Reservations
        WHERE BookId IN (SELECT ID FROM deleted);

        DELETE FROM Books
        WHERE ID IN (SELECT ID FROM deleted);
    END
END;

GO;

-- Creazione del trigger per controllare le prenotazioni attive
CREATE TRIGGER PreventDuplicateReservations
ON Reservations
FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UserId uniqueidentifier;
    DECLARE @BookId uniqueidentifier;

    -- Ottieni l'ID utente e l'ID libro dall'inserted table
    SELECT @UserId = UserId, @BookId = BookId FROM inserted;

    -- Controlla se esiste una prenotazione attiva per l'utente e il libro inseriti
    IF EXISTS (
        SELECT 1
        FROM Reservations
        WHERE UserId = @UserId
        AND BookId = @BookId
        AND StartDate <= GETDATE()
        AND EndDate >= GETDATE()
    )
    BEGIN
        -- Se esiste una prenotazione attiva, genera un errore e annulla l'inserimento
        RAISERROR ('L''utente ha già una prenotazione attiva per questo libro.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
