using DBHelper;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExample
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TestDBHelper();
            stopwatch.Stop();
            logger.Info("Processing time of TestDBHelper:{0}", stopwatch.Elapsed);
            Console.WriteLine("Processing time of TestDBHelper:{0}",stopwatch.Elapsed);
            stopwatch.Start();
            Repository repo = new Repository();
            repo.ReadAll().ForEach(a => Console.WriteLine(a.FirstName));
            stopwatch.Stop();
            Console.WriteLine("Processing time of Dapper:{0}", stopwatch.Elapsed);
        }

        static void TestDBHelper()
        {
            IDBManager idbManager;
            using(idbManager = new DBManager(DataProvider.SqlServer))
            {
                idbManager.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                idbManager.Open();
                idbManager.ExecuteReader(System.Data.CommandType.StoredProcedure,"SP_Authors");
                while (idbManager.DataReader.Read())
                {
                    Console.WriteLine(idbManager.DataReader["FirstName"]);
                }
            }
        }
    }
}
