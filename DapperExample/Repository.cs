using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;

namespace DapperExample
{
    class Repository
    {
        public List<Author> ReadAll()
        {

            //using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            using (IDbConnection db = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString()))
            {
                return //db.Query<Author>("Select * From Author").ToList();
                db.Query<Author>("SP_Authors", CommandType.StoredProcedure).ToList();
            }

        }
    }
}
