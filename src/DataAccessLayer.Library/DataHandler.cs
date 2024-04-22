
using System.Data;

namespace DataAccessLayer.Library
{
    public class DataHandler<T> : ICrud<T>
    {
        private readonly DataTableAccess<T> Table = new();

        public void Add(T item)
        {
            DataTable table = Table.AddItemToDataTable(item);
            table.WriteXml(Table.XMLFileName, XmlWriteMode.WriteSchema);
        }

        public bool Delete(T item)
        {
            throw new NotImplementedException();
        }

        public T? Get(T item) => GetAll().Where(i => i.Equals(item)).FirstOrDefault();

        public IEnumerable<T> GetAll()
        {
            DataTable dataTable = Table.ReadDataTableFromFile(Table.XMLFileName);
            var result = Table.ConvertDataTableToList(dataTable);
            return result;
        }

        public bool Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
