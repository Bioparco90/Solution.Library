using System.Data;
using System.Reflection;

namespace DataAccessLayer.Library
{

    // TODO: evitare duplicati
    public class DataAccess<T>
    {
        private readonly string ClassType = typeof(T).Name;
        private PropertyInfo[] Properties => typeof(T).GetProperties();

        // creare metodi per dataset e datatable. Generics?
        public PropertyInfo[] GetProperties() => Properties;
        public string GetClassType() => ClassType;

        public DataSet ConvertListToDataSet(List<T> items)
        {
            DataSet dataSet = new DataSet();

            if (File.Exists(ClassType + ".xml"))
            {
                dataSet.ReadXml(ClassType + ".xml");
            }
            else
            {
                // Creazione di una tabella nel DataSet corrispondente alla classe T
                DataTable dataTable = new DataTable(ClassType);
                foreach (var property in Properties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
                dataSet.Tables.Add(dataTable);
            }


            // Aggiunta degli oggetti alla tabella nel DataSet
            foreach (var item in items)
            {
                DataRow newRow = dataSet.Tables[ClassType].NewRow();
                foreach (var property in Properties)
                {
                    newRow[property.Name] = property.GetValue(item);
                }
                dataSet?.Tables[ClassType]?.Rows.Add(newRow);
            }

            return dataSet;
        }
    }
}
