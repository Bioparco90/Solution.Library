Console.WriteLine("Don't delete me");

// TODO: Gestire la differenziazione dei poteri tra utente admin e utente user (compreso rilvevamento dell'utente che compie le azioni come il prestito o la restituzione, un sistema di pseudo-sessione al login potrebbe essere adeguato)
// TODO: Gestire limitazioni alle Reservation "Il libro è da considerarsi non più disponibile ad altre prenotazioni nel momento in cui esiste un numero di prenotazioni uguale alla quantità di copie in biblioteca aventi tutte un EndDate maggiore rispetto al momento in cui si fa richiesta. All’atto della restituzione, il sistema dovrà farsi carico di aggiornare l’EndDate della prenotazione con la data della restituzione stessa"
// TODO: Implementare metodo di restituzione del libro (ricordarsi di aggiornare la property EndDate e verificare specifiche
// TODO: Patchare l'eliminazione del libro, controllare le specifiche relative e chiedere eventuali spiegazioni (probabilmente va gestita l'eliminazione di TUTTE LE COPIE, quindi valutare la creazione di un metodo per l'eliminazione della singola copia)
// TODO: Implementare un metodo GetByProperties anche per le Reservation
// TODO: Rivalutazione della business logic secondo la sezione "note varie" delle specifiche, ci sono campi required di cui tenere conto.