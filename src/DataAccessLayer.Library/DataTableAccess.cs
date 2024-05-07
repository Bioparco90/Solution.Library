using Model.Library;
using System.Data;
using System.Reflection;

namespace DataAccessLayer.Library
{
    public class DataTableAccess<T>
    {
        public string Extension => ".xml";
        public string ClassType => typeof(T).Name;
        public string XMLFileName => $"{ClassType}{Extension}";
        private PropertyInfo[] Properties => typeof(T).GetProperties();

        public void AddItemToDataTable(T item, DataTable table)
        {
            CreateRows(item, table);
        }

        public void AddListToDataTable(List<T> items, DataTable table)
        {
            CreateRows(items, table);
        }

        private void CreateRows(List<T> items, DataTable table)
        {
            foreach (var item in items)
            {
                DataRow newRow = table.NewRow();
                foreach (var property in Properties)
                {
                    newRow[property.Name] = property.GetValue(item);
                }
                table.Rows.Add(newRow);
            }
        }

        private void CreateRows(T item, DataTable table)
        {
            DataRow newRow = table.NewRow();
            foreach (var property in Properties)
            {
                newRow[property.Name] = property.GetValue(item);
            }
            table.Rows.Add(newRow);
        }

        public DataTable PopulateOrCreate()
        {
            DataTable table = new DataTable();
            table.TableName = ClassType;

            foreach (var property in Properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            string pkFieldString = Properties.FirstOrDefault(p => p.Name == "Id").Name ?? string.Empty;
            if (pkFieldString != string.Empty)
            {
                table.PrimaryKey = new DataColumn[] { table.Columns[pkFieldString] };
            }

            if (typeof(T) == typeof(User))
            {
                UniqueConstraint unique = new(new DataColumn[] { table.Columns["Username"] });
                table.Constraints.Add(unique);
            }

            if (typeof(T) == typeof(Book))
            {
                UniqueConstraint unique = new(new DataColumn[]
                {
                    table.Columns["Title"],
                    table.Columns["AuthorName"],
                    table.Columns["AuthorSurname"],
                    table.Columns["PublishingHouse"]
                });
                table.Constraints.Add(unique);
            }

            return table;
        }

        public IEnumerable<T> ConvertDataTableToList(DataTable table)
        {
            List<T> items = new();

            foreach (DataRow row in table.Rows)
            {
                T obj = Activator.CreateInstance<T>();
                foreach (var property in Properties)
                {
                    property.SetValue(obj, row[property.Name]);
                }
                items.Add(obj);
            }

            return items;
        }

        public DataTable ReadDataTableFromFile(string path)
        {
            DataTable table = new DataTable();
            if (File.Exists(path))
            {
                table.ReadXml(path);
            }

            return table;
        }
    }
}
