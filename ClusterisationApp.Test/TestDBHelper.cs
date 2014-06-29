using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace ClusterisationApp.Test
{
    public class TestDBHelper
    {
        public static void ExecScript(String scriptName)
        {
            string sqlConnectionString = "Data Source=HOME; Initial Catalog=ClusteringAppTestDB; Integrated Security=True;";
            FileInfo file = new FileInfo(Path.Combine("SQL", scriptName));
            string script = file.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(script);
        }

        public static void BackupBeforeTest()
        {
            TestDBHelper.ExecScript("BackupBeforeTest.sql");
        }

        public static void RestoreAfterTest()
        {
            TestDBHelper.ExecScript("RestoreAfterTest.sql");
        }
    }
}
