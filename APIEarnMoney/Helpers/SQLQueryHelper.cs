using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace APIEarnMoney.Helpers
{
    public static class SQLQueryHelper
    {
        public static string SelectAll<T>() where T : class
        {
            string tableName = typeof(T).Name;
            string query = $"SELECT * FROM {tableName}";
            return query;
        }
        public static string Insert<T>(T entity) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder columns = new StringBuilder();
            StringBuilder parameters = new StringBuilder();

            foreach (PropertyInfo property in properties)
            {
                // Exclude properties with Key attribute and properties that are null
                var dbGenerate = property.GetCustomAttribute<DatabaseGeneratedAttribute>();
                if ((property.GetCustomAttribute<KeyAttribute>() == null || (dbGenerate != null && dbGenerate!.DatabaseGeneratedOption == DatabaseGeneratedOption.None)) && property.GetValue(entity) != null)
                {
                    if (columns.Length > 0)
                    {
                        columns.Append(", ");
                        parameters.Append(", ");
                    }

                    columns.Append(property.Name);
                    parameters.Append($"@{property.Name}");
                }
            }

            string tableName = type.Name;
            string query = $"INSERT INTO {tableName}{(tableName.EndsWith("s") ? "" : "s")} ({columns}) VALUES ({parameters})";
            return query;
        }

        public static string Update<T>(Expression<Func<T, object>> keySelector, params Expression<Func<T, object>>[] propertySelectors) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            StringBuilder setValues = new StringBuilder();
            string keyPropertyName = ((MemberExpression)keySelector.Body).Member.Name;

            foreach (PropertyInfo property in properties)
            {
                if (property.Name.Equals(keyPropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    // Skip the key property
                    continue;
                }

                if (propertySelectors.Any(selector => ((MemberExpression)selector.Body).Member.Name.Equals(property.Name)))
                {
                    if (setValues.Length > 0)
                    {
                        setValues.Append(", ");
                    }

                    setValues.Append($"{property.Name} = @{property.Name}");
                }
            }

            string tableName = type.Name;
            string query = $"UPDATE {tableName} SET {setValues} WHERE {keyPropertyName} = @{keyPropertyName}";
            return query;
        }

        public static string Delete<T>(Expression<Func<T, object>> keySelector) where T : class
        {
            Type type = typeof(T);
            string keyPropertyName = ((MemberExpression)keySelector.Body).Member.Name;

            string tableName = type.Name;
            string query = $"DELETE FROM {tableName} WHERE {keyPropertyName} = @{keyPropertyName}";
            return query;
        }
    }
}
