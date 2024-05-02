namespace BusinessLogic.Library.Interfaces
{
    internal interface ICrud<T>
    {
        public bool Add(T item);
        public IEnumerable<T> Get(T item);
        public T? GetById(Guid id);
        public IEnumerable<T> GetAll();
        public bool Update(T item);
        public bool Delete(T item);
        public void Save();
    }
}
