using System.Data;
using System.Reflection;

namespace DataAccessLayer.Library
{

    // TODO: evitare duplicati
    // TODO: valutare il senso di avere un dataset, datatable dovrebbe bastare
    public class DataTableAccess<T>
    {
        public string Extension => ".xml";
        public string ClassType => typeof(T).Name;
        public string XMLFileName => $"{ClassType}{Extension}";

        private List<string> ExcludedProperties = ["RoleEnum"];
        private PropertyInfo[] Properties => typeof(T).GetProperties()
            .Where(item => !ExcludedProperties.Contains(item.Name))
            .Cast<PropertyInfo>()
            .ToArray();

        public DataTable AddItemToDataTable(T item)
        {
            var table = CreateTable();
            CreateRows(item, table);

            return table;
        }

        public DataTable AddListToDataTable(List<T> items)
        {
            var table = CreateTable();
            CreateRows(items, table);

            return table;
        }

        private void CreateRows(List<T> items, DataTable table)
        {
            foreach (var item in items)
            {
                DataRow? newRow = table?.NewRow();
                foreach (var property in Properties)
                {
                    newRow[property.Name] = property.GetValue(item);
                }
                table?.Rows.Add(newRow);
            }
        }

        private void CreateRows(T item, DataTable table)
        {
            DataRow? newRow = table?.NewRow();
            foreach (var property in Properties)
            {
                newRow[property.Name] = property.GetValue(item);
            }
            table?.Rows.Add(newRow);
        }

        private DataTable CreateTable()
        {
            DataTable dataTable = new DataTable(ClassType);

            if (File.Exists(XMLFileName))
            {
                dataTable.ReadXml(XMLFileName);
            }
            else
            {
                foreach (var property in Properties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
            }

            return dataTable;
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
            DataTable table = new();
            table.ReadXml(path);
            return table;
        }
    }
}
