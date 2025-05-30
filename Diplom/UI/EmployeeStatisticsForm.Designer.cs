namespace Diplom.UI
{
    partial class EmployeeStatisticsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.DateTimePicker dateTimeTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView dataGridViewStats;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimeTo = new System.Windows.Forms.DateTimePicker();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dataGridViewStats = new System.Windows.Forms.DataGridView();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.BtnExportAllPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStats)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(14, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(343, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "📊 Статистика сотрудников";
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Location = new System.Drawing.Point(40, 48);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(228, 22);
            this.dateTimeFrom.TabIndex = 2;
            // 
            // dateTimeTo
            // 
            this.dateTimeTo.Location = new System.Drawing.Point(320, 48);
            this.dateTimeTo.Name = "dateTimeTo";
            this.dateTimeTo.Size = new System.Drawing.Size(228, 22);
            this.dateTimeTo.TabIndex = 4;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(17, 53);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(19, 16);
            this.labelFrom.TabIndex = 1;
            this.labelFrom.Text = "С:";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(286, 53);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(28, 16);
            this.labelTo.TabIndex = 3;
            this.labelTo.Text = "По:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.ForeColor = System.Drawing.Color.Gray;
            this.textBoxSearch.Location = new System.Drawing.Point(571, 48);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(205, 22);
            this.textBoxSearch.TabIndex = 5;
            this.textBoxSearch.Text = "Поиск по имени...";
            this.textBoxSearch.Enter += new System.EventHandler(this.TextBox_Enter);
            this.textBoxSearch.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(789, 47);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(91, 27);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // dataGridViewStats
            // 
            this.dataGridViewStats.AllowUserToAddRows = false;
            this.dataGridViewStats.AllowUserToDeleteRows = false;
            this.dataGridViewStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStats.Location = new System.Drawing.Point(17, 85);
            this.dataGridViewStats.MultiSelect = false;
            this.dataGridViewStats.Name = "dataGridViewStats";
            this.dataGridViewStats.ReadOnly = true;
            this.dataGridViewStats.RowHeadersWidth = 51;
            this.dataGridViewStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStats.Size = new System.Drawing.Size(1344, 320);
            this.dataGridViewStats.TabIndex = 7;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(17, 416);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(137, 32);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "Экспорт в PDF";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.BtnExportPdf_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(1224, 416);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(137, 32);
            this.buttonRefresh.TabIndex = 9;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // BtnExportAllPdf
            // 
            this.BtnExportAllPdf.Location = new System.Drawing.Point(170, 416);
            this.BtnExportAllPdf.Name = "BtnExportAllPdf";
            this.BtnExportAllPdf.Size = new System.Drawing.Size(166, 32);
            this.BtnExportAllPdf.TabIndex = 10;
            this.BtnExportAllPdf.Text = "Экспорт всех отчетов";
            this.BtnExportAllPdf.UseVisualStyleBackColor = true;
            this.BtnExportAllPdf.Click += new System.EventHandler(this.BtnExportAllPdf_Click);
            // 
            // EmployeeStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 469);
            this.Controls.Add(this.BtnExportAllPdf);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.dateTimeFrom);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.dateTimeTo);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dataGridViewStats);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonRefresh);
            this.Name = "EmployeeStatisticsForm";
            this.Text = "Статистика сотрудников";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStats)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnExportAllPdf;
    }
}
