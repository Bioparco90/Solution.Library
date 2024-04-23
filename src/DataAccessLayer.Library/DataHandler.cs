
using Model.Library;
using System.Data;

namespace DataAccessLayer.Library
{
    public class DataHandler<T> : ICrud<T> where T : DataObject
    {
        private readonly DataTableAccess<T> DataAccess;
        private DataTable Table;

        public DataHandler(DataTableAccess<T> dataAccess, DataTable table)
        {
            DataAccess = dataAccess;
            Table = table;
        }

        public void Add(T item)
        {
            DataAccess.AddItemToDataTable(item, Table);
        }


        public bool Delete(T item)
        {
            var itemFound = Get(item);
            var row = Table.Rows.Find(itemFound.Id);
            if (row != null)
            {
                Table.Rows.Remove(row);
                return true;
            }
            return false;
        }

        public T? Get(T item) => GetAll().Where(i => i.Equals(item)).FirstOrDefault();

        public IEnumerable<T> GetAll()
        {
            DataAccess.ReadDataTableFromFile(DataAccess.XMLFileName, Table);
            var result = DataAccess.ConvertDataTableToList(Table);
            return result;
        }


        public bool Update(T item)
        {
            T? itemFound = Get(item);
            var row = Table.Rows.Find(itemFound?.Id);
            if (row is null)
            {
                return false;
            }

            row.BeginEdit();
            foreach (var property in typeof(T).GetProperties())
            {
                if (!(property.Name == "Id"))
                {
                    row[property.Name] = property.GetValue(item);
                }
            }
            row.EndEdit();
            return true;
        }

        public void Save()
        {
            Table.WriteXml(DataAccess.XMLFileName, XmlWriteMode.WriteSchema);
        }
    }
}
