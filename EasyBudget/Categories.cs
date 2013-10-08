using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace EasyBudget
{
    internal static class Categories
    {
        public static bool Add(string Name, double Capacity)
        {
            return Form1.execute_db_command(string.Format
            ("insert into category (name, capacity) values (\'{0}\', {1})", Name, Capacity));
        }

        public static IEnumerable<string> AllCategoryStrings
        { get { return GetField<string>("name"); } }

        internal static IEnumerable<T> GetField<T>(string FieldName)
        {
            return query<T>("select {0} from category", FieldName);
        }

        internal static IEnumerable<T> GetField<T>(string FieldName, string Condition)
        {
            return query<T>("select {0} from category where ({1})", FieldName, Condition);
        }

        internal static IEnumerable<T> GetField<T>(string FieldName, string Condition, int Limit)
        {
            return query<T>("select {0} from category where ({1}) limit {2}", FieldName, Condition, Limit);
        }

        internal static System.Data.SQLite.SQLiteDataReader GetRows(string Condition = "1 = 1")
        {
            return query("select * from category where ({0})", Condition);
        }

        private static System.Data.SQLite.SQLiteDataReader query(string cmd, params object[] options)
        {
            return Form1.execute_db_query(string.Format(cmd, options));
        }

        private static IEnumerable<T> query<T>(string cmd, params object[] options)
        {
            List<T> list = new List<T>();
            var q = Form1.execute_db_query(string.Format(cmd, options));
            while (q.Read())
            {
                list.Add((T)q[0]);
            }
            q.Close();
            return list;
        }
    }
}
