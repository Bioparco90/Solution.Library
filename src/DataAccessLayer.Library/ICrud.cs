namespace DataAccessLayer.Library
{
    internal interface ICrud<T>
    {
        public bool Add(T item);
        public T? Get(T item);
        public IEnumerable<T> GetAll();
        public bool Update(T item);
        public bool Delete(T item);
    }
}
