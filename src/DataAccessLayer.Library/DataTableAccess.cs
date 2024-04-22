using System.Data;
using System.Reflection;

namespace DataAccessLayer.Library
{

    // TODO: evitare duplicati
    public class DataTableAccess<T>
    {
        private readonly string XmlExtension = ".xml";
        private readonly string ClassType = typeof(T).Name;
        private PropertyInfo[] Properties => typeof(T).GetProperties();

        //public PropertyInfo[] GetProperties() => Properties;
        public string GetClassType() => ClassType;

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
            if (File.Exists(ClassType + XmlExtension))
            {
                dataSet.ReadXml(ClassType + XmlExtension);
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

        public List<T> ConvertDataSetToList(DataSet dataSet)
        {
            List<T> items = new List<T>();

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
