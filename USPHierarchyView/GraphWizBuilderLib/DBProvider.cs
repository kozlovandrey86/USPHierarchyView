using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizBuilderLib
{
    class DBProvider
    {
        string _connString;
        public DBProvider(string dbname=null)
        {
            var name = dbname ?? "DB";
            _connString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public IEnumerable<T> ExecQuery<T>(string query)
        {
            using (var connection = new SqlConnection(_connString))
            {
                return connection.Query<T>(query);
            }
        }
    }
}
