﻿using System.Data;
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
        private PropertyInfo[] Properties => typeof(T).GetProperties();

        public void AddItemToDataTable(T item, DataTable table)
        {
            PopulateOrCreate(table);
            CreateRows(item, table);
        }

        public void AddListToDataTable(List<T> items, DataTable table)
        {
            PopulateOrCreate(table);
            CreateRows(items, table);
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

        private void PopulateOrCreate(DataTable dataTable)
        {
            dataTable.TableName = ClassType;

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
                string pkFieldString = Properties.FirstOrDefault(p => p.Name == "Id").Name ?? string.Empty;
                if (pkFieldString != string.Empty)
                {
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[pkFieldString] };
                }
            }
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

        public void ReadDataTableFromFile(string path, DataTable table)
        {
            table.ReadXml(path);
        }
    }
}
