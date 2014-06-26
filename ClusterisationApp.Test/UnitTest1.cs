using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClusterisationApp.ClusteringClasses;
using ClusterisationApp.MachineLearningClasses;
using ClusterisationApp.Forms;
using ClusterisationApp;
using NUnit.Framework;
using System.Data.SqlClient;


using Assert = NUnit.Framework.Assert;

namespace ClusterisationApp.Test
{
    [TestClass]
    public class ClusteringTests
    {
        private const string TestConnection = "Data Source=HOME; Initial Catalog=ClusteringAppTestDB; Integrated Security=True;";
        
        [TestMethod]
        public void TestCreateEmptyCluster()
        {
            DBClusterMethods.CreateEmptyCluster(TestConnection);
            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("SElECT [Cluster_ID] FROM [Cluster] WHERE W=0", con);
            SqlDataReader DataReader = cmd.ExecuteReader();
            Assert.AreEqual(true, DataReader.Read());
            con.Close();
        }

        [TestMethod]
        public void TestDeletAllEmptyClusters()
        {
            DBClusterMethods.DeleteAllEmptyClustersFromDataBase(TestConnection);
            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("SElECT [Cluster_ID] FROM [Cluster] WHERE W=0", con);
            SqlDataReader DataReader = cmd.ExecuteReader();
            Assert.AreEqual(false, DataReader.Read());
            con.Close();
        }

        [TestMethod]
        public void TestUpdateClusterIDInDoc()
        {
            DBClusterMethods.UpdateClusterIDInDoc(1, 1, TestConnection);
        }

        [TestMethod]
        public void TestGetClustercharacteristics()
        {
            Cluster cl = new Cluster(1, TestConnection);
            Assert.AreEqual(1, cl.getID());
            Assert.AreEqual(1, cl.getN());
            Assert.AreEqual(15, cl.getS());
            Assert.AreEqual(15, cl.getW());
        }

        [TestMethod]
        public void TestDeleteAllEmptyTagInCluster()
        {
            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO TagInCluster ([Tag_ID], [Cluster_ID],[Occ]) VALUES (16, 1, 0)", con);
            cmd.ExecuteNonQuery();
            con.Close();
            
            Cluster cl = new Cluster(1, TestConnection);
            cl.DeleteAllEmptyTagInCluster(TestConnection);

            con.Open();
            cmd = new SqlCommand("SElECT [Cluster_ID] FROM [TagInCluster] WHERE Occ=0", con);
            SqlDataReader DataReader = cmd.ExecuteReader();
            Assert.AreEqual(false, DataReader.Read());
            con.Close();
        }

        [TestMethod]
        public void TestConvertDBFields()
        {
            Cluster cl = new Cluster(1, TestConnection);
            cl.ConvertDBFields(TestConnection);
            Assert.AreEqual(1, cl.getID());
            Assert.AreEqual(1, cl.getN());
            Assert.AreEqual(15, cl.getS());
            Assert.AreEqual(15, cl.getW());
        }

        [TestMethod]
        public void TestDocTags()
        {
            DocTags dt = new DocTags(1, TestConnection);
            Assert.AreEqual(1, dt.GetClusterId());
            long[] tagids = dt.GetTagIDs();
            for(long i=1; i<16; i++) Assert.AreEqual(i, tagids[i-1]);
        }
    }

}
