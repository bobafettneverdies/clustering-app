﻿using System.Data.SqlClient;

namespace ClusterisationApp
{
    class Cluster
    {
        private long clustid;
        private long N;
        private long W;
        private long S;

        public Cluster(long ID)
        {
            clustid = ID;
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();
            var cmd = new SqlCommand("SELECT [N],[W],[S] FROM [Cluster] WHERE [Cluster_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            SqlDataReader datareader = cmd.ExecuteReader();
            if (datareader.Read())
            {
                N = (long)datareader[0];
                W = (long)datareader[1];
                S = (long)datareader[2];
            }
            con.Close();
        }

        public void DeleteAllEmptyTagInDocs()
        {
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();
            var cmd = new SqlCommand("DELETE FROM [TagInCluster] WHERE [Occ]=0 AND [Cluster_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", clustid);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ConvertDBFields()
        {
            long Nnew = 0, Wnew = 0, Snew = 0;
            
            SqlConnection con = new SqlConnection(DBCon.Con);

            con.Open();
            var cmd = new SqlCommand("SELECT COUNT([Doc_ID]) FROM [Doc] WHERE Cluster_ID=@cid", con);
            cmd.Parameters.AddWithValue("@cid", clustid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                Nnew = (long)((int)reader[0]);
            con.Close();
            
            con.Open();
            cmd = new SqlCommand("SELECT COUNT([Tag_ID]) FROM [TagInCluster] WHERE Cluster_ID=@cid", con);
            cmd.Parameters.AddWithValue("@cid", clustid);
            reader = cmd.ExecuteReader();
            if (reader.Read())
                Wnew = (long)((int)reader[0]);
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT [Occ] FROM [TagInCluster] WHERE Cluster_ID=@cid", con);
            cmd.Parameters.AddWithValue("@cid", clustid);
            reader = cmd.ExecuteReader();
            while (reader.Read())
                Snew += (long)reader[0];
            con.Close();

            con.Open();
            cmd = new SqlCommand("UPDATE [Cluster] SET [N]=@n, [W]=@w, [S]=@s WHERE [Cluster_ID]=@id", con);
            cmd.Parameters.AddWithValue("@n", Nnew);
            cmd.Parameters.AddWithValue("@w", Wnew);
            cmd.Parameters.AddWithValue("@s", Snew);
            cmd.Parameters.AddWithValue("@id", clustid);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public long getID() { return clustid; } //получить идентификатор кластеру
        public long getN() { return N; } //получить число документов в кластере
        public long getW() { return W; } //получить ширину кластера
        public long getS() { return S; } //получить площадь кластера
    }
}
