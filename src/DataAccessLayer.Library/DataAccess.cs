using Humanizer;
using System.Reflection;

namespace DataAccessLayer.Library
{
    public class DataAccess<T>
    {
        private readonly string ClassType = typeof(T).Name;
        private PropertyInfo[] Properties => typeof(T).GetProperties();

        // creare metodi per dataset e datatable. Generics?
        public PropertyInfo[] GetProperties() => Properties;
        public string GetClassType() => ClassType;


    }
}
