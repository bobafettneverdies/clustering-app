using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClusterisationApp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT Cluster_ID[Идентификатор], N[Число документов в кластере], W[Ширина], S[Площадь] from Cluster", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, "Cluster");

            ClustersView.DataSource = ds.Tables[0];
            con.Close();

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Cluster_ID from Cluster", con);
            SqlDataReader reader;

            reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cluster_ID", typeof(string));
            dt.Load(reader);

            comboBox1.ValueMember = "Cluster_ID";
            comboBox1.DisplayMember = "Cluster_ID";
            comboBox1.DataSource = dt;

            con.Close();

           
        }

        public void ClustRefresh(){
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT Cluster_ID[Идентификатор], N[Число документов в кластере], W[Ширина], S[Площадь] from Cluster", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, "Cluster");

            ClustersView.DataSource = ds.Tables[0];
            con.Close();

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Cluster_ID from Cluster", con);
            SqlDataReader reader;

            reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cluster_ID", typeof(string));
            dt.Load(reader);

            comboBox1.ValueMember = "Cluster_ID";
            comboBox1.DisplayMember = "Cluster_ID";
            comboBox1.DataSource = dt;

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MachineLearningForm f5 = new MachineLearningForm();
            f5.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RepulsionForm f4 = new RepulsionForm();
            f4.Show();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            ShowTagsForm f3 = new ShowTagsForm();
            f3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void добавитьНовыйФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDocForm f2 = new AddDocForm();
            f2.Show();
        }

        private void стеретьДанныеОКластеризацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Вы Уверены?", "Удаление разбиения документов на кластеры", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(DBCon.Con);
                con.Open();
                var cmd = new SqlCommand("TRUNCATE TABLE Cluster TRUNCATE TABLE TagInCluster", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("UPDATE Doc SET Cluster_ID=NULL", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void очиститьДанныеОВыделенныхТегахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Вы Уверены?", "Удаление выделенных тегов", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(DBCon.Con);
                con.Open();
                var cmd = new SqlCommand("TRUNCATE TABLE Tag TRUNCATE TABLE TagInDoc TRUNCATE TABLE TagLearningInDoc TRUNCATE TABLE TagLearning", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT Cluster_ID[Идентификатор], N[Число документов в кластере], W[Ширина], S[Площадь] from Cluster", con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, "Cluster");

            ClustersView.DataSource = ds.Tables[0];
            con.Close();
        }

        private void вызватьСправкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Программа \"Система кластеризации текстовых документов\" \nРазработчик: Комяков Дмитрий Сергеевич \nММиКН-454", "Помощь", MessageBoxButtons.OK); 
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Программа \"Система кластеризации текстовых документов\" \nРазработчик: Комяков Дмитрий Сергеевич \nММиКН-454", "О программе", MessageBoxButtons.OK);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(DBCon.Con);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT DocBody[Текст документа], Doc_ID[Идентификатор], DocName[Название документа] from Doc WHERE Cluster_ID=@id", con);

            da.SelectCommand.Parameters.AddWithValue("@id", Convert.ToInt64(comboBox1.Text.ToString()));
            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, "Doc");

            DocsView.DataSource = ds.Tables[0];
            
            
            con.Close();
        }

       
    }
}
