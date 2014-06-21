using System;
using System.Windows.Forms;
using ClusterisationApp.MachineLearningClasses;

namespace ClusterisationApp.Forms
{
    public partial class MachineLearningForm : Form
    {
        public MachineLearningForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long wordsindoccount=0, mintagcount=0, mintagindoccount=0;
                wordsindoccount = long.Parse(wordsindoccountBox.Text);
                mintagcount = long.Parse(mintagcountBox.Text);
                mintagindoccount = long.Parse(mintagindoccountBox.Text);
                //DialogResult goodresult;
                //goodresult = MessageBox.Show("Начало алгоритма кластеризации", "Сообщение", MessageBoxButtons.OK);
                MachineLearning ml = new MachineLearning();
                ml.StartLearningProcess(wordsindoccount, mintagcount, mintagindoccount, DBCon.Con);
                Close();
            }

            catch (FormatException)
            {
                DialogResult badresult;
                badresult = MessageBox.Show("Неправильно введены параметры алгоритма машинного обучения", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
