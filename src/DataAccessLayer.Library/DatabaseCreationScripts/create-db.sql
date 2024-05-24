CREATE DATABASE Library;

GO

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
    CONSTRAINT FK_BookReservation FOREIGN KEY (BookId) REFERENCES Books(ID) ON DELETE CASCADE
);

GO

CREATE VIEW [dbo].[ActiveReservations]
AS
SELECT ID, UserId, BookId, StartDate, EndDate
FROM Reservations
WHERE EndDate >= GETDATE();

GO

CREATE VIEW [dbo].[ActiveReservationsCross]
AS
SELECT Books.ID as BookId, Reservations.ID, Username, Title, StartDate, EndDate 
FROM Reservations
  JOIN Books ON BookId = Books.ID
  JOIN Users ON UserId = Users.ID
WHERE EndDate >= GETDATE();

GO

SELECT
  Books.Title,
  Users.Username,
  Reservations.StartDate,
  Reservations.EndDate,
  CASE
    WHEN Reservations.EndDate >= GETDATE() THEN 'Active'
    ELSE 'Expired'
  END AS Status
FROM Reservations
JOIN Books ON Reservations.BookId = Books.ID
JOIN Users ON Reservations.UserId = Users.ID

GO

-- Trigger book deletion
CREATE TRIGGER TR_BookDelete
ON Books
INSTEAD OF DELETE
AS
BEGIN
    -- Check if there are active reservations for the books to be deleted
    IF EXISTS (
        SELECT 1
        FROM [dbo].[ActiveReservations] ar
        INNER JOIN deleted d ON ar.BookId = d.ID
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

GO

-- Creazione del trigger per controllare le prenotazioni attive
CREATE TRIGGER [dbo].[PreventDuplicateReservations]
ON [dbo].[Reservations]
INSTEAD OF INSERT
AS
BEGIN
    -- Imposta il NOCOUNT per evitare la restituzione del conteggio delle righe
    SET NOCOUNT ON;

    -- Controlla se esiste già una prenotazione attiva per ciascuna riga inserita
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN [dbo].[ActiveReservations] ar
        ON i.UserId = ar.UserId AND i.BookId = ar.BookId
    )
    BEGIN
        -- Se esiste una prenotazione attiva, genera un errore e annulla l'inserimento
        RAISERROR ('L''utente ha già una prenotazione attiva per questo libro.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
    ELSE
    BEGIN
        -- Inserisci i dati nella tabella di destinazione
        INSERT INTO Reservations (UserId, BookId, StartDate, EndDate)
        SELECT UserId, BookId, StartDate, EndDate FROM inserted;
    END;
END;


GO

INSERT INTO Users (ID, Username, Password, Role) VALUES 
('6e4547a1-4838-42ad-bb1d-088160722496', 'user1', 'pippo', 2),
('e9bf87da-8344-40f4-b65f-35b7c345bbb4', 'bob', 'securepwd456', 2),
('53a74000-3b0c-46ec-a660-4347bd5bbeb0', 'grace', 'password123', 2),
('b200c745-1cba-4ac0-b28e-504a7a1860e7', 'charlie', 'p@ssw0rd', 2),
('1277e918-ac18-429f-b723-69806538593a', 'alice', 'password123', 2),
('044ce480-18fd-4379-b335-7479215bb719', 'admin', '12345', 1),
('43e4e698-22d9-4aaf-a1ff-845f1ff3c940', 'dave', 'letmein', 2),
('06715319-ac0a-4724-90a9-bb61a16e61f1', 'eve', 'qwerty', 2),
('662e38b0-9d4f-47b1-b762-edbb2c1a5517', 'user2', 'franco', 2),
('c009db55-7820-435d-bc62-fa9284b1e292', 'frank', 'abc123', 2);

INSERT INTO Books (ID, Title, AuthorName, AuthorSurname, PublishingHouse, Quantity) VALUES 
('ab579a40-a6e2-4018-a4ca-14bf0cd702d7', 'Harry Potter e il Principe Mezzosangue', 'J.K.', 'Rowling', 'Bloomsbury', 9),
('9bcae3b4-1485-42a2-a4da-2872bacba41e', 'Il Codice da Vinci', 'Dan', 'Brown', 'Mondadori', 3),
('fd25463b-5b45-465e-afd9-45e37d8a0576', 'Il Signore degli Anelli', 'J.R.R.', 'Tolkien', 'Bompiani', 5),
('d3a11f69-d5bb-4c29-a84c-4a0733fdc91c', 'Harry Potter e il Calice di Fuoco', 'J.K.', 'Rowling', 'Bloomsbury', 6),
('209a7079-c559-4c36-8377-656667bfd247', 'L''Ultimo Giorno', 'Glenn', 'Cooper', 'Longanesi', 2),
('c88159f7-f441-463e-8b80-6ee2edc7ab45', 'Harry Potter e il Prigioniero di Azkaban', 'J.K.', 'Rowling', 'Bloomsbury', 1),
('9b1541e6-1d55-4fb1-91da-6f41843dbfde', 'Harry Potter e l''Ordine della Fenice', 'J.K.', 'Rowling', 'Bloomsbury', 7),
('41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', 'Il Marchio del Diavolo', 'Glenn', 'Cooper', 'Longanesi', 3),
('42ebe098-d50e-4059-a18a-8277aa01db24', 'Shining', 'Stephen', 'King', 'Sperling & Kupfer', 5),
('646519b1-c149-49ec-bfb9-8e23108c1ced', 'Angeli e Demoni', 'Dan', 'Brown', 'Mondadori', 4),
('6556dfc9-ca30-4056-988f-93b0e1e3aa82', 'Harry Potter e la Pietra Filosofale', 'J.K.', 'Rowling', 'Bloomsbury', 5),
('4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', 'Harry Potter e i Doni della Morte', 'J.K.', 'Rowling', 'Bloomsbury', 4),
('4b8acca4-52a6-4201-8016-a890cde873b5', 'Doctor Sleep', 'Stephen', 'King', 'Sperling & Kupfer', 4),
('98c1e100-771b-48bc-b8c4-a99fae77a36d', 'Origin', 'Dan', 'Brown', 'Mondadori', 3),
('aaaa4807-0367-4ab6-9212-bfa1f966fc99', 'Inferno', 'Dan', 'Brown', 'Mondadori', 3),
('19d1c54b-99e9-4c37-99ff-e3a6c1e73c2b', 'Harry Potter e la Camera dei Segreti', 'J.K.', 'Rowling', 'Bloomsbury', 2),
('0487d4be-b16f-4cbc-9297-f946f8d03353', 'It', 'Stephen', 'King', 'Sperling & Kupfer', 4);

INSERT INTO Reservations(ID, UserId, BookId, StartDate, EndDate) VALUES('03705782-595d-4a98-81bd-0282f5a4b6e4', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', '41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', '07/01/2024', '05/02/2024')
,('2b89f0c8-80fb-4c0d-b5fd-087a683746a1', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '4b8acca4-52a6-4201-8016-a890cde873b5', '13/11/2023', '12/12/2023')
,('631ff20e-f182-4727-afd3-0a2f8865d790', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', '42ebe098-d50e-4059-a18a-8277aa01db24', '18/12/2023', '17/01/2024')
,('1bd37751-d8b5-400a-b46d-0d13a9ba4ef3', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', '98c1e100-771b-48bc-b8c4-a99fae77a36d', '18/11/2023', '17/12/2023')
,('f28504b1-5a10-473e-a9ff-0ed81f195f5d', '044ce480-18fd-4379-b335-7479215bb719', '6556dfc9-ca30-4056-988f-93b0e1e3aa82', '26/08/2023', '25/09/2023')
,('8844bce1-b0d3-4506-98a8-16d337cb5953', '6e4547a1-4838-42ad-bb1d-088160722496', 'ab579a40-a6e2-4018-a4ca-14bf0cd702d7', '10/05/2023', '09/06/2023')
,('5322dd95-81b1-4520-ab1a-20cd6c685669', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', '646519b1-c149-49ec-bfb9-8e23108c1ced', '01/05/2024', '30/05/2024')
,('8a0bd078-4d2e-41af-a5ad-21558c1c5db7', '1277e918-ac18-429f-b723-69806538593a', '98c1e100-771b-48bc-b8c4-a99fae77a36d', '22/03/2024', '20/04/2024')
,('8355af0f-bd02-4681-9f48-241c0afa18bf', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', '42ebe098-d50e-4059-a18a-8277aa01db24', '05/06/2023', '04/07/2023')
,('a13e57f1-30ec-4885-9f2f-26943c1d71b8', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', '4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', '31/08/2023', '30/09/2023')
,('242d8405-8257-4c28-b222-2d64595e73ab', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', 'ab579a40-a6e2-4018-a4ca-14bf0cd702d7', '06/02/2024', '06/03/2024')
,('efcfb5b9-6985-45ed-a5c8-2ddca912d2cb', '1277e918-ac18-429f-b723-69806538593a', '19d1c54b-99e9-4c37-99ff-e3a6c1e73c2b', '28/11/2023', '27/12/2023')
,('087dbb18-3c9c-4821-b3e0-2fba30edd24c', '1277e918-ac18-429f-b723-69806538593a', '4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', '07/03/2024', '05/04/2024')
,('732c1a0b-80d6-4d04-bd9d-3400f939df94', 'c009db55-7820-435d-bc62-fa9284b1e292', '646519b1-c149-49ec-bfb9-8e23108c1ced', '23/12/2023', '22/01/2024')
,('c9c35df3-b33d-4cbe-9c32-410af3dd8813', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', 'd3a11f69-d5bb-4c29-a84c-4a0733fdc91c', '25/05/2023', '24/06/2023')
,('9abe5d48-07be-4f0a-9f68-46319de97ef7', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '19d1c54b-99e9-4c37-99ff-e3a6c1e73c2b', '24/09/2023', '23/10/2023')
,('e50b9fde-5081-4d0f-b19b-468a43599976', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '42ebe098-d50e-4059-a18a-8277aa01db24', '21/02/2024', '20/03/2024')
,('92c1c8bb-2903-4665-b4c6-48ce40effb4e', '044ce480-18fd-4379-b335-7479215bb719', '0487d4be-b16f-4cbc-9297-f946f8d03353', '03/12/2023', '02/01/2024')
,('966441ce-1713-4cc3-8eb3-49cbc01af201', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', '19d1c54b-99e9-4c37-99ff-e3a6c1e73c2b', '27/01/2024', '25/02/2024')
,('a3fc3e41-74cd-4329-9792-4a63d8536f3d', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '9bcae3b4-1485-42a2-a4da-2872bacba41e', '15/05/2023', '14/06/2023')
,('115d9765-c3c0-4c6d-848c-4c57f0eb3960', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', 'ab579a40-a6e2-4018-a4ca-14bf0cd702d7', '04/10/2023', '03/11/2023')
,('fe212784-8875-41d9-a184-4dc9d9a68025', '6e4547a1-4838-42ad-bb1d-088160722496', 'd3a11f69-d5bb-4c29-a84c-4a0733fdc91c', '03/08/2023', '02/09/2023')
,('38859449-abaf-4dc7-a9f9-571e0d6a8d03', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', '42ebe098-d50e-4059-a18a-8277aa01db24', '17/08/2023', '16/09/2023')
,('5d023c80-e7c3-4a5f-ada3-5a8baf3caf3d', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', 'ab579a40-a6e2-4018-a4ca-14bf0cd702d7', '11/04/2024', '10/05/2024')
,('77d9cdb8-3ed1-412f-80ef-5bdf6c82e836', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', 'fd25463b-5b45-465e-afd9-45e37d8a0576', '20/05/2023', '19/06/2023')
,('892e14f3-d54f-4ff0-888c-5d014e46ce09', '06715319-ac0a-4724-90a9-bb61a16e61f1', '0487d4be-b16f-4cbc-9297-f946f8d03353', '06/04/2024', '05/05/2024')
,('5e610aca-ac7d-4a7b-ba07-5d4d1254cd53', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', 'c88159f7-f441-463e-8b80-6ee2edc7ab45', '13/08/2023', '12/09/2023')
,('4a45f3d0-1b0f-4a42-9414-6148e899a35a', '6e4547a1-4838-42ad-bb1d-088160722496', 'aaaa4807-0367-4ab6-9212-bfa1f966fc99', '19/09/2023', '18/10/2023')
,('548f9fe3-5fc1-47f8-a540-62c7252dcfbf', 'c009db55-7820-435d-bc62-fa9284b1e292', '4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', '18/06/2023', '17/07/2023')
,('d3df5466-b3d0-42c5-9dfa-643d43a92781', '1277e918-ac18-429f-b723-69806538593a', '646519b1-c149-49ec-bfb9-8e23108c1ced', '21/08/2023', '20/09/2023')
,('166b7c82-57d3-4ddb-9cb9-6682eb9fcd8c', '044ce480-18fd-4379-b335-7479215bb719', 'c88159f7-f441-463e-8b80-6ee2edc7ab45', '01/06/2023', '30/06/2023')
,('faacf64a-f995-493e-ba97-687acce88de9', '6e4547a1-4838-42ad-bb1d-088160722496', '41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', '08/11/2023', '07/12/2023')
,('4d927655-86d3-42ff-8478-6bd4c36cc3f2', '06715319-ac0a-4724-90a9-bb61a16e61f1', 'ab579a40-a6e2-4018-a4ca-14bf0cd702d7', '20/07/2023', '19/08/2023')
,('469d09a5-6142-4b8a-b28b-6d5bc1cfbd3d', '06715319-ac0a-4724-90a9-bb61a16e61f1', '646519b1-c149-49ec-bfb9-8e23108c1ced', '10/06/2023', '09/07/2023')
,('33569e62-e7af-4250-b07f-6fc17f06c836', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', '42ebe098-d50e-4059-a18a-8277aa01db24', '19/10/2023', '18/11/2023')
,('60649aa2-d2a0-455e-b6ef-70095688c1d5', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', '02/01/2024', '31/01/2024')
,('a5fb2dc8-2ced-4d8f-a81c-7017975c697f', '1277e918-ac18-429f-b723-69806538593a', '209a7079-c559-4c36-8377-656667bfd247', '30/05/2023', '29/06/2023')
,('715c3ab8-a424-4328-8701-75bc1c5c9af1', '1277e918-ac18-429f-b723-69806538593a', '4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', '11/05/2024', '09/06/2024')
,('e2b46515-02e4-4336-8a2f-774fdb568ea3', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', '19d1c54b-99e9-4c37-99ff-e3a6c1e73c2b', '01/04/2024', '30/04/2024')
,('e34bfc73-31ee-4d41-a993-79154c222179', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '209a7079-c559-4c36-8377-656667bfd247', '08/08/2023', '07/09/2023')
,('aa6e5c47-4ef5-48b8-b132-81415bcd9ec2', '1277e918-ac18-429f-b723-69806538593a', '19d1c54b-99e9-4c37-99ff-e3a6c1e73c2b', '10/07/2023', '09/08/2023')
,('5e95f1a6-96a4-4201-9a60-814aa688174d', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', '98c1e100-771b-48bc-b8c4-a99fae77a36d', '01/07/2023', '31/07/2023')
,('c3ca74d7-6245-4442-984c-82773da7fb13', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', '6556dfc9-ca30-4056-988f-93b0e1e3aa82', '02/03/2024', '31/03/2024')
,('53638558-0af4-402a-a7cb-85beb659f616', '044ce480-18fd-4379-b335-7479215bb719', '41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', '12/03/2024', '10/04/2024')
,('77a8520b-3056-4b54-a136-88290ed41dca', 'c009db55-7820-435d-bc62-fa9284b1e292', '9bcae3b4-1485-42a2-a4da-2872bacba41e', '11/02/2024', '11/03/2024')
,('4ffa547c-8e6f-4c34-8942-8cf3721a927b', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '4b8acca4-52a6-4201-8016-a890cde873b5', '28/06/2023', '27/07/2023')
,('bb63c0e3-c842-4363-b4c7-8db0154262e8', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', '6556dfc9-ca30-4056-988f-93b0e1e3aa82', '29/10/2023', '28/11/2023')
,('66879ed8-01a4-4c5b-bbe7-90184c17eb58', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', '6556dfc9-ca30-4056-988f-93b0e1e3aa82', '06/05/2024', '04/06/2024')
,('606e0b0c-2e06-4118-8ad8-9046c30e8702', '044ce480-18fd-4379-b335-7479215bb719', '0487d4be-b16f-4cbc-9297-f946f8d03353', '15/07/2023', '14/08/2023')
,('b0eee25a-0ca7-4a72-a574-912833d5c170', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', '4b8acca4-52a6-4201-8016-a890cde873b5', '09/09/2023', '08/10/2023')
,('1f62dab9-555b-44cb-8269-92465df38040', '6e4547a1-4838-42ad-bb1d-088160722496', '41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', '22/06/2023', '21/07/2023')
,('31e20697-6ce0-4371-bf41-960cb4683861', 'c009db55-7820-435d-bc62-fa9284b1e292', '98c1e100-771b-48bc-b8c4-a99fae77a36d', '14/09/2023', '13/10/2023')
,('00698ed9-17e1-42a6-a625-9b2963b71f3c', '1277e918-ac18-429f-b723-69806538593a', '98c1e100-771b-48bc-b8c4-a99fae77a36d', '17/01/2024', '15/02/2024')
,('1dbff42d-dbbb-4d76-853e-b50725729b50', '06715319-ac0a-4724-90a9-bb61a16e61f1', '0487d4be-b16f-4cbc-9297-f946f8d03353', '01/02/2024', '29/02/2024')
,('d256bf37-ada4-4ace-863a-bbef0a372451', '044ce480-18fd-4379-b335-7479215bb719', '41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', '16/05/2024', '14/06/2024')
,('6e53b143-6939-4c78-b61e-bc37e3973e84', '044ce480-18fd-4379-b335-7479215bb719', 'aaaa4807-0367-4ab6-9212-bfa1f966fc99', '27/03/2024', '25/04/2024')
,('d7f67991-1a23-4741-9421-bdde9c790e91', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', 'aaaa4807-0367-4ab6-9212-bfa1f966fc99', '06/07/2023', '05/08/2023')
,('288c7716-9bfc-4564-9d64-bdf1cf644cb8', '1277e918-ac18-429f-b723-69806538593a', '9bcae3b4-1485-42a2-a4da-2872bacba41e', '09/10/2023', '08/11/2023')
,('3ccc5bbd-c0c5-4b87-82e9-c18a91dffee9', 'c009db55-7820-435d-bc62-fa9284b1e292', '4e2a0116-c1d5-4e8d-9f18-9d2cd6fbb5d7', '03/11/2023', '02/12/2023')
,('7e2bc799-6906-4c74-a329-c5378a35443e', '6e4547a1-4838-42ad-bb1d-088160722496', 'fd25463b-5b45-465e-afd9-45e37d8a0576', '21/04/2024', '20/05/2024')
,('39823963-1639-4631-85ed-c65f9dafe077', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', 'aaaa4807-0367-4ab6-9212-bfa1f966fc99', '23/11/2023', '22/12/2023')
,('c364d2b0-e684-4c7d-9394-c7e015ac8d77', '6e4547a1-4838-42ad-bb1d-088160722496', 'fd25463b-5b45-465e-afd9-45e37d8a0576', '16/02/2024', '16/03/2024')
,('2b5035d3-4112-479e-ad49-cadad777f456', '044ce480-18fd-4379-b335-7479215bb719', 'aaaa4807-0367-4ab6-9212-bfa1f966fc99', '22/01/2024', '20/02/2024')
,('0ba7f738-f4fc-41ae-beb3-cbd3de9a049e', 'c009db55-7820-435d-bc62-fa9284b1e292', '9bcae3b4-1485-42a2-a4da-2872bacba41e', '16/04/2024', '15/05/2024')
,('36f34f80-5fd2-4f6d-a3a2-d0b0ea5cb06e', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', 'ab579a40-a6e2-4018-a4ca-14bf0cd702d7', '08/12/2023', '07/01/2024')
,('e628863f-71c3-423a-80e2-d1818c8d27c6', '06715319-ac0a-4724-90a9-bb61a16e61f1', '646519b1-c149-49ec-bfb9-8e23108c1ced', '24/10/2023', '23/11/2023')
,('953ceec5-c1c6-4de5-84f7-d7383bdedc0e', '044ce480-18fd-4379-b335-7479215bb719', 'fd25463b-5b45-465e-afd9-45e37d8a0576', '14/10/2023', '13/11/2023')
,('755e35bc-c269-4cbd-9ba5-d7cdc56c3958', 'b200c745-1cba-4ac0-b28e-504a7a1860e7', '4b8acca4-52a6-4201-8016-a890cde873b5', '12/01/2024', '10/02/2024')
,('a36c8048-a447-4744-9551-e020ca71b5c3', '06715319-ac0a-4724-90a9-bb61a16e61f1', '41f75b2b-2fcf-4eed-a5dc-81b9ec5799f1', '05/09/2023', '04/10/2023')
,('d9488aff-6d3e-4cdb-aa28-e6080646dcdd', '6e4547a1-4838-42ad-bb1d-088160722496', '6556dfc9-ca30-4056-988f-93b0e1e3aa82', '28/12/2023', '27/01/2024')
,('7075cd70-ce65-41eb-9752-e789126217e1', '53a74000-3b0c-46ec-a660-4347bd5bbeb0', '646519b1-c149-49ec-bfb9-8e23108c1ced', '26/02/2024', '25/03/2024')
,('81d6a02f-9212-4d5b-ae66-eaba52cf06a0', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', '6556dfc9-ca30-4056-988f-93b0e1e3aa82', '12/06/2023', '11/07/2023')
,('b431f402-3fb6-4751-9231-ee1208001b2b', '43e4e698-22d9-4aaf-a1ff-845f1ff3c940', '4b8acca4-52a6-4201-8016-a890cde873b5', '17/03/2024', '15/04/2024')
,('1d7a9a0b-0f7d-421c-b466-f53fb7660cac', '662e38b0-9d4f-47b1-b762-edbb2c1a5517', '9bcae3b4-1485-42a2-a4da-2872bacba41e', '25/07/2023', '24/08/2023')
,('ccd7654c-893f-482c-940f-fbf86cb15c82', 'e9bf87da-8344-40f4-b65f-35b7c345bbb4', '42ebe098-d50e-4059-a18a-8277aa01db24', '26/04/2024', '25/05/2024')
,('793ff1ea-143d-4a71-bb50-fe20c0f976c6', 'c009db55-7820-435d-bc62-fa9284b1e292', 'fd25463b-5b45-465e-afd9-45e37d8a0576', '30/07/2023', '29/08/2023');
