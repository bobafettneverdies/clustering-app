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
            SqlDataReader testReader = cmd.ExecuteReader();
            Assert.AreEqual(true, testReader.Read()); //тест на наличие тегов в базе после выполнения алгоритма машинного обучения
            long Tag_ID = (long)testReader[0];
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT TagInDoc_ID, Doc_ID FROM TagInDoc WHERE Tag_ID=@tid", con);
            cmd.Parameters.AddWithValue("@tid", Tag_ID);
            testReader = cmd.ExecuteReader();
            Assert.AreEqual(true, testReader.Read()); //тест на наличие записей в таблице TagInDoc
            long Doc_ID = (long) testReader[1];
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT IsMarked FROM Doc WHERE Doc_ID=@did", con);
            cmd.Parameters.AddWithValue("@did", Doc_ID);
            testReader = cmd.ExecuteReader();
            if(testReader.Read()) Assert.AreEqual(true, (bool)testReader[0]); //тест значения флага покрытости документа тегом
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
            SqlDataReader testReader = cmd.ExecuteReader();
            Assert.AreEqual(true, testReader.Read());
            long Cluster_ID = (long) testReader[0]; 
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT Doc_ID FROM Doc WHERE Cluster_ID=@cid", con);
            cmd.Parameters.AddWithValue("@cid", Cluster_ID);
            testReader = cmd.ExecuteReader();
            Assert.AreEqual(true, testReader.Read());
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT Cluster_ID FROM Cluster WHERE N=0 OR W=0 OR S=0", con);
            testReader = cmd.ExecuteReader();
            Assert.AreEqual(false, testReader.Read()); //тест на отсутствие пустых кластеров
            con.Close();

            TestDBHelper.RestoreAfterTest();
        }
    }
}
