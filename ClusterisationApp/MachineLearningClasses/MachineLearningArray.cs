using System;
using System.Data.SqlClient;

namespace ClusterisationApp.MachineLearningClasses
{
    public class MachineLearningArray
    {
        private short[] LearningArray;
        private long[] DocIDs;
        public MachineLearningArray(string connectionstring)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            var cmd = new SqlCommand("SELECT COUNT([Doc_ID]) FROM [Doc]", con);
            SqlDataReader datareader = cmd.ExecuteReader();
            if (datareader.Read())
            {
                LearningArray = new short[(int)datareader[0]];
                DocIDs = new long[(int)datareader[0]];
            }
            con.Close();

            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT [Doc_ID] FROM [Doc]", con);
            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                DocIDs[i] = (long)datareader[0];
                i++;
            }
            con.Close();
        }

        public int ArrayModify(long ID, long mintagindoccount, string connectionstring)
        {
            int i = Array.IndexOf(DocIDs, ID, 0);
            if (this.LearningArray[i] >= mintagindoccount) return 0;
            else
            {
                this.LearningArray[i] += 1;
                if (this.LearningArray[i] < mintagindoccount) return 0;
                else
                {
                    SqlConnection con = new SqlConnection(connectionstring);
                    con.Open();
                    var cmd = new SqlCommand("UPDATE [Doc] Set [IsMarked]='True' WHERE [Doc_ID]=@id", con);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 1;
                }
            }
        }
    }
}
