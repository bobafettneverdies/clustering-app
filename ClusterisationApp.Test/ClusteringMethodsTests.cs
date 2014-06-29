using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClusterisationApp.ClusteringClasses;
using NUnit.Framework;
using System.Data.SqlClient;


using Assert = NUnit.Framework.Assert;

namespace ClusterisationApp.Test
{
    [TestClass]
    public class ClusteringMethodsTests
    {
        private const string TestConnection = "Data Source=HOME; Initial Catalog=ClusteringAppTestDB; Integrated Security=True;";
        /*
        //DBClusterMethods
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
            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("SElECT [Cluster_ID] FROM [Doc] WHERE [Doc_ID]=1", con);
            SqlDataReader DataReader = cmd.ExecuteReader();
            if(DataReader.Read()) Assert.AreEqual(1, (long)DataReader[0]);
            con.Close();
        }

        [TestMethod]
        public void TestUpdateTagInClusterOccPlus1()
        {
            DBClusterMethods.UpdateTagInClusterOccPlus1(16, 46, TestConnection);
            SqlConnection con = new SqlConnection(TestConnection);
            con.Open();
            var cmd = new SqlCommand("SElECT [TagInCl_ID] FROM [TagInCluster] WHERE [Cluster_ID]=46 And [Tag_ID]=16", con);
            SqlDataReader DataReader = cmd.ExecuteReader();
            Assert.AreEqual(true, DataReader.Read());
            con.Close();
            con.Open();
            cmd = new SqlCommand("DELETE FROM [TagInCluster] WHERE [Cluster_ID]=46", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Cluster
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

        //DocTags
        [TestMethod]
        public void TestDocTags()
        {
            DocTags dt = new DocTags(1, TestConnection);
            Assert.AreEqual(1, dt.GetClusterId());
            long[] tagids = dt.GetTagIDs();
            for(long i=1; i<16; i++) Assert.AreEqual(i, tagids[i-1]);
        }

        //Profit
        [TestMethod]
        public void TestProfit()
        {
            Profit pf = new Profit();
            Assert.AreEqual(0,pf.GetProfit());
            pf.Profitmodify(1);
            Assert.AreEqual(1, pf.GetProfit());
        }

        [TestMethod]
        public void TestDeltaAddAndDeltaSub()
        {
            Profit pf = new Profit();
            Cluster cl = new Cluster(1, TestConnection);
            DocTags dt = new DocTags(1, TestConnection);
            Assert.AreEqual(true, pf.DeltaAdd(cl, dt, 1, TestConnection) > 0);
            Assert.AreEqual(false, pf.DeltaSub(cl, dt, 1, TestConnection) > 0);
        }*/
    }
}
