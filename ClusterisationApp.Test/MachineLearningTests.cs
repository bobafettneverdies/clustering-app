using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClusterisationApp.ClusteringClasses;
using ClusterisationApp.MachineLearningClasses;
using NUnit.Framework;
using System.Data.SqlClient;

using Assert = NUnit.Framework.Assert;

namespace ClusterisationApp.Test
{
    [TestClass]
    public class MachineLearningTests
    {
        private const string TestConnection = "Data Source=HOME; Initial Catalog=ClusteringAppTestDB; Integrated Security=True;";

        [TestMethod]
        public void TestMachineLearningAlgortithm()
        {
            TestDBHelper.BackupBeforeTest();
            MachineLearning ml = new MachineLearning();
            ml.StartLearningProcess(15, 1, 1, TestConnection);
            
            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("SELECT Tag_ID FROM Tag", con);
            SqlDataReader datareader = cmd.ExecuteReader();
            Assert.AreEqual(true, datareader.Read());
            con.Close();
            
            TestDBHelper.RestoreAfterTest();
        }

        [TestMethod]
        public void TestClusteringAlgorithm()
        {
            TestDBHelper.BackupBeforeTest();
            MachineLearning ml = new MachineLearning();
            ml.StartLearningProcess(15, 1, 1, TestConnection);
            Clustering cling = new Clustering();
            cling.StartClusteringAlg(1.2F, TestConnection);

            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("SELECT Cluster_ID FROM Cluster", con);
            SqlDataReader datareader = cmd.ExecuteReader();
            Assert.AreEqual(true, datareader.Read());
            con.Close();
            
            TestDBHelper.RestoreAfterTest();
        }
    }
}
