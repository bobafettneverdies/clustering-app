using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClusterisationApp.Forms
{
    public partial class AddDocForm : Form
    {
        public AddDocForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            var cmd = new SqlCommand("INSERT INTO [Doc] ([DocName], [DocBody],[IsMarked]) VALUES (@name, @body, @mark)", con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@body", richTextBox1.Text);
            cmd.Parameters.AddWithValue("@mark", "False");
            cmd.ExecuteNonQuery();

            con.Close();

            Close();
        }
    }
    
}
