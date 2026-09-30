using System;
using System.Collections.Generic;
using System.Text;

namespace Sharnaka_Dev.Data
{
    public static class DbPaths
    {
        public static string ConnectionString
        {
            get
            {
                var DbDir = Path.Combine(AppContext.BaseDirectory, "Data");
                Directory.CreateDirectory(DbDir);
                return $"Data Source={Path.Combine(DbDir, "Sharnaka_Dev.db")}";
            }

        }
    }
}
