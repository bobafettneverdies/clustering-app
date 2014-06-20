using System.Data.SqlClient;

namespace ClusterisationApp
{
    class MachineLearningArray
    {
        private short[] LearningArray;
        public MachineLearningArray(){
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();
            var cmd = new SqlCommand("SELECT MAX([Doc_ID]) FROM [Doc]", con);
            SqlDataReader datareader = cmd.ExecuteReader();
            if (datareader.Read())
                LearningArray = new short[(long)datareader[0]];
            con.Close();
        }

        public int ArrayModify(long ID, long mintagindoccount)
        {
            if (this.LearningArray[ID - 1] >= mintagindoccount) return 0;
            else
            {
                this.LearningArray[ID - 1] += 1;
                if (this.LearningArray[ID - 1] < mintagindoccount) return 0;
                else
                {
                    SqlConnection con = new SqlConnection(DBCon.Con);
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
