using Model.Library;
using System.Data;

namespace DataAccessLayer.Library
{
    public abstract class DataHandler<T> : ICrud<T> where T : DataObject
    {
        protected readonly DataTableAccess<T> DataAccess;
        protected DataTable Table;

        public DataHandler(DataTableAccess<T> dataAccess)
        {
            DataAccess = dataAccess;
            Table = File.Exists(dataAccess.XMLFileName)
                ? dataAccess.ReadDataTableFromFile(dataAccess.XMLFileName)
                : dataAccess.PopulateOrCreate();
        }

        // TODO: Risolvere questo schifo temporaneo
        public virtual bool Add(T item)
        {
            try
            {
                DataAccess.AddItemToDataTable(item, Table);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Delete(T item)
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

        public virtual T? Get(T item) => GetAll().FirstOrDefault(i => i.Equals(item));
        public virtual T? GetById(Guid id) => GetAll().FirstOrDefault(i => i.Id == id);

        public virtual IEnumerable<T> GetAll()
        {
            //DataTable table = DataAccess.ReadDataTableFromFile(DataAccess.XMLFileName);
            var result = DataAccess.ConvertDataTableToList(Table);
            return result;
        }

        public virtual bool Update(T item)
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

        public virtual void Save()
        {
            Table.WriteXml(DataAccess.XMLFileName, XmlWriteMode.WriteSchema);
        }
    }
}
