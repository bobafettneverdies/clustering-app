using System;
using System.Data.SqlClient;

namespace ClusterisationApp
{
    class Profit
    {
        private float profit=0;
        
        public float DeltaAdd(Cluster C, DocTags t, float r) //подсчет прироста Profit(C,r) при добавлении документа t в кластер С
        {
            long S_new = C.getS() + (t.GetTagIDs()).Length;
            long W_new = C.getW();

            long[] tagarray = t.GetTagIDs();
            
            for (long i = 0; i < tagarray.Length; i++)
            {
                SqlConnection con = new SqlConnection(DBCon.Con);
                con.Open();
                var cmd = new SqlCommand("SELECT [Occ] FROM [TagInCluster] WHERE [Cluster_ID]=@clid AND [Tag_ID]=@tid", con);
                cmd.Parameters.AddWithValue("@clid", C.getID());
                cmd.Parameters.AddWithValue("@tid", tagarray[i]);
                SqlDataReader datareader = cmd.ExecuteReader();
                if (datareader.Read())
                {
                    if ((long)datareader[0] == 0 || datareader.IsDBNull(0)) { W_new++; }
                }
                else { W_new++; }
                con.Close();
            }

            float prev;
            if (C.getW() == 0) prev = 0;
            else prev = (float)C.getS() * (float)C.getN() / (float)Math.Pow((float)C.getW(), (float)r);
            
            return (float)S_new*((float)C.getN()+1)/(float)Math.Pow((float)W_new, (float)r) - prev;
        }

        public float DeltaSub(Cluster C, DocTags t, float r) //подсчет прироста Profit(C,r) при удалении документа t из кластера С
        {
            long S_new = C.getS() - (t.GetTagIDs()).Length;
            long W_new = C.getW();
            if (W_new == 0 || S_new == 0) return 0;

            long[] tagarray = t.GetTagIDs();
            for (long i = 0; i < tagarray.Length; i++)
            {
                SqlConnection con = new SqlConnection("Data Source=HOME; Initial Catalog=DocsDataBase; Integrated Security=True;");
                con.Open();
                var cmd = new SqlCommand("SELECT [Occ] FROM [TagInCluster] WHERE [Cluster_ID]=@clid AND [Tag_ID]=@tid", con);
                cmd.Parameters.AddWithValue("@clid", C.getID());
                cmd.Parameters.AddWithValue("@tid", tagarray[i]);
                SqlDataReader datareader = cmd.ExecuteReader();
                if (datareader.Read())
                    if ((long)datareader[0] == 1) { W_new--; }
                con.Close();
            }
            return (float)S_new * ((float)C.getN() - 1) / (float)Math.Pow((float)W_new, (float)r) - (float)C.getS() * (float)C.getN() / (float)Math.Pow((float)C.getW(), (float)r);
        }

        public void profitmodify(float delta) { this.profit += delta; }
        public float GetProfit() { return profit; }
    }
}
