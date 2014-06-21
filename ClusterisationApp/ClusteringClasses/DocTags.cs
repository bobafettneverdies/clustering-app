using System.Data.SqlClient;

namespace ClusterisationApp.ClusteringClasses
{
    class DocTags //список тегов покрывающих документ
    {
        private long _docId; //идентификатор документа
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private long _clusterId = 0; //идентификатор кластера к которому относится документ, если NULL, то = 0
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private long[] TagIDs; //список идентификаторов тегов покрывающих документ

        public DocTags(long ID, string connectionstring)
        {
            _docId = ID;
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            var cmd = new SqlCommand("SELECT COUNT([Tag_ID]) FROM [TagInDoc] WHERE [Doc_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            SqlDataReader datareader = cmd.ExecuteReader();
            if (datareader.Read())
                TagIDs = new long[(int)datareader[0]];
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT [Cluster_ID] FROM [Doc] WHERE [Doc_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            datareader = cmd.ExecuteReader();
            if (datareader.Read())
            {
                if (datareader.IsDBNull(0)) _clusterId = 0;
                else _clusterId = (long)datareader[0];
            }
            con.Close();

            con.Open();
            cmd = new SqlCommand("SELECT [Tag_ID] FROM [TagInDoc] WHERE [Doc_ID]=@id", con);
            cmd.Parameters.AddWithValue("@id", ID);
            datareader = cmd.ExecuteReader();
            long i = 0;
            while (datareader.Read()) TagIDs[i++] = (long)datareader[0];

            con.Close();
        }

        public long[] GetTagIDs() { return this.TagIDs; } //получить список идентификаторов тегов, покрывающих документ
        public long GetClusterId() { return this._clusterId; } //получить идентификато кластера к которому относится документ
    }
}
