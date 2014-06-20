using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClusterisationApp
{
    public partial class ShowTagsForm : Form
    {
        public ShowTagsForm()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select Tag_ID[Идентификатор], TagWord [Теговое слово], [IDF] from Tag", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, "Tag");

            dataGridTags.DataSource = ds.Tables[0]; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
