namespace ClusterisationApp.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MachLearn = new System.Windows.Forms.Button();
            this.ClustAlg = new System.Windows.Forms.Button();
            this.ShowTags = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьНовыйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стеретьДанныеОКластеризацииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьДанныеОВыделенныхТегахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вызватьСправкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClustersView = new System.Windows.Forms.DataGridView();
            this.DocsView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Refresh = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClustersView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocsView)).BeginInit();
            this.SuspendLayout();
            // 
            // MachLearn
            // 
            this.MachLearn.Location = new System.Drawing.Point(12, 469);
            this.MachLearn.Name = "MachLearn";
            this.MachLearn.Size = new System.Drawing.Size(170, 35);
            this.MachLearn.TabIndex = 0;
            this.MachLearn.Text = "Запустить алгоритм машинного обучения";
            this.MachLearn.UseVisualStyleBackColor = true;
            this.MachLearn.Click += new System.EventHandler(this.button1_Click);
            // 
            // ClustAlg
            // 
            this.ClustAlg.Location = new System.Drawing.Point(188, 469);
            this.ClustAlg.Name = "ClustAlg";
            this.ClustAlg.Size = new System.Drawing.Size(170, 35);
            this.ClustAlg.TabIndex = 2;
            this.ClustAlg.Text = "Запустить алгоритм кластеризации";
            this.ClustAlg.UseVisualStyleBackColor = true;
            this.ClustAlg.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ShowTags
            // 
            this.ShowTags.Location = new System.Drawing.Point(706, 469);
            this.ShowTags.Name = "ShowTags";
            this.ShowTags.Size = new System.Drawing.Size(170, 35);
            this.ShowTags.TabIndex = 1;
            this.ShowTags.Text = "Показать выделенные теги";
            this.ShowTags.UseVisualStyleBackColor = true;
            this.ShowTags.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(888, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьНовыйФайлToolStripMenuItem,
            this.стеретьДанныеОКластеризацииToolStripMenuItem,
            this.очиститьДанныеОВыделенныхТегахToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // добавитьНовыйФайлToolStripMenuItem
            // 
            this.добавитьНовыйФайлToolStripMenuItem.Name = "добавитьНовыйФайлToolStripMenuItem";
            this.добавитьНовыйФайлToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.добавитьНовыйФайлToolStripMenuItem.Text = "Добавить новый файл в базу данных";
            this.добавитьНовыйФайлToolStripMenuItem.Click += new System.EventHandler(this.добавитьНовыйФайлToolStripMenuItem_Click);
            // 
            // стеретьДанныеОКластеризацииToolStripMenuItem
            // 
            this.стеретьДанныеОКластеризацииToolStripMenuItem.Name = "стеретьДанныеОКластеризацииToolStripMenuItem";
            this.стеретьДанныеОКластеризацииToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.стеретьДанныеОКластеризацииToolStripMenuItem.Text = "Стереть результаты кластеризации";
            this.стеретьДанныеОКластеризацииToolStripMenuItem.Click += new System.EventHandler(this.стеретьДанныеОКластеризацииToolStripMenuItem_Click);
            // 
            // очиститьДанныеОВыделенныхТегахToolStripMenuItem
            // 
            this.очиститьДанныеОВыделенныхТегахToolStripMenuItem.Name = "очиститьДанныеОВыделенныхТегахToolStripMenuItem";
            this.очиститьДанныеОВыделенныхТегахToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.очиститьДанныеОВыделенныхТегахToolStripMenuItem.Text = "Стереть выделенные теги";
            this.очиститьДанныеОВыделенныхТегахToolStripMenuItem.Click += new System.EventHandler(this.очиститьДанныеОВыделенныхТегахToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вызватьСправкуToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.помощьToolStripMenuItem.Text = "Справка";
            // 
            // вызватьСправкуToolStripMenuItem
            // 
            this.вызватьСправкуToolStripMenuItem.Name = "вызватьСправкуToolStripMenuItem";
            this.вызватьСправкуToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.вызватьСправкуToolStripMenuItem.Text = "Помощь";
            this.вызватьСправкуToolStripMenuItem.Click += new System.EventHandler(this.вызватьСправкуToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // ClustersView
            // 
            this.ClustersView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClustersView.Location = new System.Drawing.Point(12, 78);
            this.ClustersView.Name = "ClustersView";
            this.ClustersView.Size = new System.Drawing.Size(409, 385);
            this.ClustersView.TabIndex = 4;
            // 
            // DocsView
            // 
            this.DocsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DocsView.Location = new System.Drawing.Point(427, 118);
            this.DocsView.Name = "DocsView";
            this.DocsView.Size = new System.Drawing.Size(449, 346);
            this.DocsView.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Кластеры";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(427, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Документы входящие в кластер";
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(364, 470);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(170, 34);
            this.Refresh.TabIndex = 12;
            this.Refresh.Text = "Обновить данные в таблицах";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(427, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 21);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Выберите идентификатор кластера:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 516);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DocsView);
            this.Controls.Add(this.ClustersView);
            this.Controls.Add(this.ShowTags);
            this.Controls.Add(this.ClustAlg);
            this.Controls.Add(this.MachLearn);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Система кластеризации текстовых документов";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClustersView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MachLearn;
        private System.Windows.Forms.Button ClustAlg;
        private System.Windows.Forms.Button ShowTags;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьНовыйФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem стеретьДанныеОКластеризацииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьДанныеОВыделенныхТегахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вызватьСправкуToolStripMenuItem;
        private System.Windows.Forms.DataGridView ClustersView;
        private System.Windows.Forms.DataGridView DocsView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
    }
}

