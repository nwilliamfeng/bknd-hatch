using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;

namespace BkndHatch.Data
{
    public abstract class RepositoryBase
    {

        public const string CONN_STRING = "dbConn";

        public abstract string TableName { get; }

        public virtual bool ClearTable()
        {
            using (var conn = this.CreateConnection())
            {
              return  conn.Execute($"delete from {TableName}")>0;
            }
        }

        


        protected IDbConnection CreateConnection() =>
            new SQLiteConnection(ConfigurationManager.ConnectionStrings[CONN_STRING].ConnectionString);
    }
}
