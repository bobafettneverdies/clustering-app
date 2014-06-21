namespace ClusterisationApp.Forms
{
    partial class MachineLearningForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mintagcountBox = new System.Windows.Forms.TextBox();
            this.mintagindoccountBox = new System.Windows.Forms.TextBox();
            this.wordsindoccountBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mintagcountBox
            // 
            this.mintagcountBox.Location = new System.Drawing.Point(326, 12);
            this.mintagcountBox.Name = "mintagcountBox";
            this.mintagcountBox.Size = new System.Drawing.Size(212, 20);
            this.mintagcountBox.TabIndex = 0;
            // 
            // mintagindoccountBox
            // 
            this.mintagindoccountBox.Location = new System.Drawing.Point(326, 38);
            this.mintagindoccountBox.Name = "mintagindoccountBox";
            this.mintagindoccountBox.Size = new System.Drawing.Size(212, 20);
            this.mintagindoccountBox.TabIndex = 1;
            // 
            // wordsindoccountBox
            // 
            this.wordsindoccountBox.Location = new System.Drawing.Point(326, 64);
            this.wordsindoccountBox.Name = "wordsindoccountBox";
            this.wordsindoccountBox.Size = new System.Drawing.Size(212, 20);
            this.wordsindoccountBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Начать алгоритм машинного обучения";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Количество слов, выделяемых в документе";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Минимальное количество документов покрываемых тегом";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(267, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Минимальное число тегов покрывающих документ";
            // 
            // MachineLearningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 139);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wordsindoccountBox);
            this.Controls.Add(this.mintagindoccountBox);
            this.Controls.Add(this.mintagcountBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MachineLearningForm";
            this.Text = "Введите параметры алгоритма машинного обучения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mintagcountBox;
        private System.Windows.Forms.TextBox mintagindoccountBox;
        private System.Windows.Forms.TextBox wordsindoccountBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}