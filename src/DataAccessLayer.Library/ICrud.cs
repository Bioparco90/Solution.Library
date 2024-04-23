using System.Data;

namespace DataAccessLayer.Library
{
    internal interface ICrud<T>
    {
        public void Add(T item);
        public T? Get(T item);
        public IEnumerable<T> GetAll();
        public bool Update(T item);
        public bool Delete(T item);
        public void Save();
    }
}
