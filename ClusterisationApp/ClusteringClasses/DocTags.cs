using System.Data.SqlClient;

namespace ClusterisationApp
{
    class DocTags //список тегов покрывающих документ
    {
        private long DocId; //идентификатор документа
        private long ClusterID=0; //идентификатор кластера к которому относится документ, если NULL, то = 0
        private long[] TagIDs; //список идентификаторов тегов покрывающих документ

        public DocTags(long ID)
        {
            DocId = ID;
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();
            var cmd = new SqlCommand("SELECT COUNT([Tag_ID]) FROM [TagInDoc] WHERE [Doc_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            SqlDataReader datareader = cmd.ExecuteReader();
            if (datareader.Read())
                TagIDs = new long [(int)datareader[0]];
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT [Cluster_ID] FROM [Doc] WHERE [Doc_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            datareader = cmd.ExecuteReader();
            if (datareader.Read())
            {
                if(datareader.IsDBNull(0)) ClusterID = 0;
                else ClusterID = (long)datareader[0];
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT [Tag_ID] FROM [TagInDoc] WHERE [Doc_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            datareader = cmd.ExecuteReader();
            long i=0;
            while (datareader.Read()) TagIDs[i++] = (long)datareader[0];

            con.Close();
        }

        public long[] GetTagIDs() { return this.TagIDs; } //получить список идентификаторов тегов, покрывающих документ
        public long GetClusterID() { return this.ClusterID; } //получить идентификато кластера к которому относится документ
    }
}
