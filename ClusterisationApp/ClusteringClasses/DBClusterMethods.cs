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

        static public void UpdateTagInClusterOccPlus1(long Tag_ID, long Cluster_ID, string connectionstring)
        {
            SqlConnection ticcon = new SqlConnection(connectionstring);
            ticcon.Open();

            var ticcmd = new SqlCommand("SELECT [TagInCl_ID] FROM [TaginCluster] WHERE [Tag_ID]=@tid AND [Cluster_ID]=@cid", ticcon);
            ticcmd.Parameters.AddWithValue("@tid", Tag_ID);
            ticcmd.Parameters.AddWithValue("@cid", Cluster_ID);
            SqlDataReader ticReader = ticcmd.ExecuteReader();
            if (ticReader.Read())
            {
                SqlConnection ticcon1 = new SqlConnection(connectionstring);
                ticcon1.Open();
                var ticcmd1 = new SqlCommand("UPDATE [TaginCluster] SET [Occ]=Occ+1 WHERE [TagInCl_ID]=@id", ticcon1);
                ticcmd1.Parameters.AddWithValue("@id", (long)ticReader[0]);
                ticcmd1.ExecuteNonQuery();
                ticcon1.Close();
            }
            else
            {
                SqlConnection ticcon1 = new SqlConnection(connectionstring);
                ticcon1.Open();
                var ticcmd1 = new SqlCommand("INSERT INTO [TagInCluster] ([Tag_ID], [Cluster_ID], [Occ]) VALUES (@tid, @cid, 1)", ticcon1);
                ticcmd1.Parameters.AddWithValue("@tid", Tag_ID);
                ticcmd1.Parameters.AddWithValue("@cid", Cluster_ID);
                ticcmd1.ExecuteNonQuery();
                ticcon1.Close();
            }
            ticcon.Close();
        }
    }
}
