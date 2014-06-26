using System.Data.SqlClient;

namespace ClusterisationApp.ClusteringClasses
{
    public class DBClusterMethods
    {
        static public void DeleteAllEmptyClustersFromDataBase(string connectionstring)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM Cluster WHERE NOT EXISTS(SELECT * from Doc Where Doc.Cluster_ID=Cluster.Cluster_ID)", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        static public void CreateEmptyCluster(string connectionstring)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            var cmd = new SqlCommand("INSERT INTO [Cluster] ([N], [W],[S]) VALUES (0, 0, 0)", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        static public void UpdateClusterIDInDoc(long Cluster_ID, long Doc_ID, string connectionstring)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            var cmd = new SqlCommand("UPDATE [Doc] SET [Cluster_ID]=@cid WHERE [Doc_ID]=@did", con);
            cmd.Parameters.AddWithValue("@cid", Cluster_ID);
            cmd.Parameters.AddWithValue("@did", Doc_ID);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
