using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBudget
{
    internal static class Constants
    {
        public const int VERSION = 1;
        public const string CATEGORY_FILE = "categories.csv";
        public const int DAYS_PER_MONTH = 30;
        public const string DATABASE = "data.sqlite";
        public const int SQLITE_VERSION = 3;
        public static readonly string CONNECTION_STRING = "Data Source=" + Constants.DATABASE + ";Version=" + SQLITE_VERSION;
    }
}
