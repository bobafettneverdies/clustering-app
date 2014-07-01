using System.Data.SqlClient;

namespace ClusterisationApp.MachineLearningClasses
{
    public class Document
    {
        private long _docid; //идентификатор документа
        private string _docbody; //текст документа

        public Document(long ID, string connectionstring)
        {

            _docid = ID;

            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();

            var cmd = new SqlCommand("SELECT [DocBody] FROM [Doc] WHERE [Doc_ID]=@ID", con);
            cmd.Parameters.AddWithValue("@ID", ID);

            SqlDataReader datareader = cmd.ExecuteReader();

            while (datareader.Read())
                _docbody = datareader[0].ToString();

            con.Close();

        }

        public void Normalise() //нормализовать текст документа
        {
            this._docbody = this._docbody.ToLower();

            foreach (var word in new Consts().InsignificantWords)
            {
                this._docbody = this._docbody.Replace(word, " ");
            }
        }

        public string Getbody() { return this._docbody; } //получить текст документа
    }
}
