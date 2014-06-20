using System;
using System.Windows.Forms;

namespace ClusterisationApp
{
    public partial class RepulsionForm : Form
    {
        public RepulsionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                float r=0;
                r = float.Parse(RepC.Text);
                //DialogResult goodresult;
                //goodresult = MessageBox.Show("Начало алгоритма кластеризации", "Сообщение", MessageBoxButtons.OK);
                Clustering clustering = new Clustering();
                clustering.StartClusteringAlg(r);
                Close();
            }

            catch (FormatException)
            {
                DialogResult badresult; 
                badresult = MessageBox.Show("Неправильно введен коэффициент отталкивания", "Repulsion Error", MessageBoxButtons.OK);
            }
        }
    }
}
