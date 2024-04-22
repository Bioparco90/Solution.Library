using System.Data;
using System.Reflection;

namespace DataAccessLayer.Library
{

    // TODO: evitare duplicati
    public class DataTableAccess<T>
    {
        public string XmlExtension => ".xml";
        public string ClassType => typeof(T).Name;
        public string XMLFileName => $"{ClassType}{XmlExtension}";
        private PropertyInfo[] Properties => typeof(T).GetProperties();

        public DataSet AddItemToDataSet(T item)
        {
            DataSet dataSet = new();
            CreateTable(dataSet);
            PopulateDataSet(item, dataSet);

            return dataSet;
        }

        public DataSet AddListToDataSet(List<T> items)
        {
            DataSet dataSet = new();
            CreateTable(dataSet);
            CreateDataSet(items, dataSet);

            return dataSet;
        }

        private void CreateDataSet(List<T> items, DataSet dataSet)
        {
            foreach (var item in items)
            {
                DataRow? newRow = dataSet?.Tables[ClassType]?.NewRow();
                foreach (var property in Properties)
                {
                    newRow[property.Name] = property.GetValue(item);
                }
                dataSet?.Tables[ClassType]?.Rows.Add(newRow);
            }
        }

        private void PopulateDataSet(T item, DataSet dataSet)
        {
            DataRow? newRow = dataSet?.Tables[ClassType]?.NewRow();
            foreach (var property in Properties)
            {
                newRow[property.Name] = property.GetValue(item);
            }
            dataSet?.Tables[ClassType]?.Rows.Add(newRow);
        }

        private void CreateTable(DataSet dataSet)
        {
            if (File.Exists(XMLFileName))
            {
                dataSet.ReadXml(XMLFileName);
            }
            else
            {
                DataTable dataTable = new DataTable(ClassType);
                foreach (var property in Properties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
                dataSet.Tables.Add(dataTable);
            }
        }

        public IEnumerable<T> ConvertDataSetToList(DataSet dataSet)
        {
            List<T> items = new();

            if (dataSet.Tables.Contains(ClassType))
            {
                DataTable dataTable = dataSet.Tables[ClassType];


                foreach (DataRow row in dataTable.Rows)
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var property in Properties)
                    {
                        property.SetValue(obj, row[property.Name]);
                    }
                    items.Add(obj);
                }
            }

            return items;
        }
        public DataSet ReadDataSetFromFile(string path)
        {
            DataSet dataSet = new();
            dataSet.ReadXml(path);

            return dataSet;
        }
    }
}
