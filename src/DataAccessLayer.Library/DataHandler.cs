
using Model.Library;
using System.Data;

namespace DataAccessLayer.Library
{
    public class DataHandler<T> : ICrud<T> where T : DataObject
    {
        private readonly DataTableAccess<T> Table;

        public DataHandler(DataTableAccess<T> table)
        {
            Table = table;
        }

        public void Add(T item)
        {
            DataTable table = Table.AddItemToDataTable(item);
            table.WriteXml(Table.XMLFileName, XmlWriteMode.WriteSchema);
        }

        public bool Delete(T item)
        {
            DataTable table = Table.ReadDataTableFromFile(Table.XMLFileName);
            var itemFound = Get(item);
            var row = table.Rows.Find(itemFound.Id);
            if (row != null)
            {
                table.Rows.Remove(row);
                table.WriteXml(Table.XMLFileName, XmlWriteMode.WriteSchema);
                return true;
            }
            return false;
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
