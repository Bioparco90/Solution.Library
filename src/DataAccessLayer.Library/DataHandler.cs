
using System.Data;

namespace DataAccessLayer.Library
{
    public class DataHandler<T> : ICrud<T>
    {
        private readonly DataTableAccess<T> Table = new();

        public bool Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T item)
        {
            throw new NotImplementedException();
        }

        public T? Get(T item) => GetAll().Where(i => i.Equals(item)).FirstOrDefault();

        public IEnumerable<T> GetAll()
        {
            DataSet dataset = Table.ReadDataSetFromFile(Table.XMLFileName);
            var result = Table.ConvertDataSetToList(dataset);
            return result;
        }

        public bool Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
