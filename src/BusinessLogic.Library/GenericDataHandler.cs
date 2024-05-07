using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;
using System.Data;

namespace BusinessLogic.Library
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

        public virtual IEnumerable<T> Get(T item) => GetAll().Where(i => i.Equals(item));
        public virtual T? GetById(Guid id) => GetAll().FirstOrDefault(i => i.Id == id);
        public virtual IEnumerable<T> GetAll() => DataAccess.ConvertDataTableToList(Table);

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

        public virtual bool Update(T item)
        {
            var row = Table.Rows.Find(item.Id);
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

        public virtual bool Delete(T item)
        {
            var row = Table.Rows.Find(item.Id);
            if (row == null)
            {
                return false;
            }

            Table.Rows.Remove(row);
            return true;
        }

        public virtual void Save() => Table.WriteXml(DataAccess.XMLFileName, XmlWriteMode.WriteSchema);

        public T? GetSingleOrNull(T item)
        {
            var itemsFound = Get(item).ToList();
            return itemsFound.Count == 1 ? itemsFound[0] : null;
        }
    }
}
