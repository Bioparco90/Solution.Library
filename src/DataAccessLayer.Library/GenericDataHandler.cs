using Model.Library;
using System.Data;

namespace DataAccessLayer.Library
{
    public abstract class GenericDataHandler<T> : ICrud<T> where T : DataObject
    {
        protected readonly DataTableAccess<T> DataAccess;
        protected DataTable Table;

        public GenericDataHandler(DataTableAccess<T> dataAccess)
        {
            DataAccess = dataAccess;
            Table = File.Exists(dataAccess.XMLFileName)
                ? dataAccess.ReadDataTableFromFile(dataAccess.XMLFileName)
                : dataAccess.PopulateOrCreate();
        }

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
        public bool DeleteAll()
        {
            try
            {
                Table.Rows.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual T? Get(T item) => GetAll().FirstOrDefault(i => i.Equals(item));
        public virtual T? GetById(Guid id) => GetAll().FirstOrDefault(i => i.Id == id);
        public virtual IEnumerable<T> GetAll() => DataAccess.ConvertDataTableToList(Table);

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
                var value = property.GetValue(item);
                if (!(property.Name == "Id") && (value != default))
                {
                    row[property.Name] = value;
                }
            }
            row.EndEdit();

            return true;
        }

        public virtual void Save() => Table.WriteXml(DataAccess.XMLFileName, XmlWriteMode.WriteSchema);


    }
}
