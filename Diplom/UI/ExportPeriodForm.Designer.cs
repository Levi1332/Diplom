using System.Drawing;

namespace Diplom.UI
{
    partial class ExportPeriodForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.radioWeek = new System.Windows.Forms.RadioButton();
            this.radioMonth = new System.Windows.Forms.RadioButton();
            this.radioYear = new System.Windows.Forms.RadioButton();
            this.radioCustom = new System.Windows.Forms.RadioButton();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // radioWeek
            // 
            this.radioWeek.AutoSize = true;
            this.radioWeek.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radioWeek.Location = new System.Drawing.Point(20, 20);
            this.radioWeek.Name = "radioWeek";
            this.radioWeek.Size = new System.Drawing.Size(85, 24);
            this.radioWeek.TabIndex = 0;
            this.radioWeek.TabStop = true;
            this.radioWeek.Text = "Неделя";
            this.radioWeek.UseVisualStyleBackColor = true;

            // 
            // radioMonth
            // 
            this.radioMonth.AutoSize = true;
            this.radioMonth.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radioMonth.Location = new System.Drawing.Point(20, 50);
            this.radioMonth.Name = "radioMonth";
            this.radioMonth.Size = new System.Drawing.Size(76, 24);
            this.radioMonth.TabIndex = 1;
            this.radioMonth.TabStop = true;
            this.radioMonth.Text = "Месяц";
            this.radioMonth.UseVisualStyleBackColor = true;

            // 
            // radioYear
            // 
            this.radioYear.AutoSize = true;
            this.radioYear.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radioYear.Location = new System.Drawing.Point(20, 80);
            this.radioYear.Name = "radioYear";
            this.radioYear.Size = new System.Drawing.Size(59, 24);
            this.radioYear.TabIndex = 2;
            this.radioYear.TabStop = true;
            this.radioYear.Text = "Год";
            this.radioYear.UseVisualStyleBackColor = true;

            // 
            // radioCustom
            // 
            this.radioCustom.AutoSize = true;
            this.radioCustom.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radioCustom.Location = new System.Drawing.Point(20, 110);
            this.radioCustom.Name = "radioCustom";
            this.radioCustom.Size = new System.Drawing.Size(183, 24);
            this.radioCustom.TabIndex = 3;
            this.radioCustom.TabStop = true;
            this.radioCustom.Text = "Пользовательский";
            this.radioCustom.UseVisualStyleBackColor = true;
            this.radioCustom.CheckedChanged += new System.EventHandler(this.radioCustom_CheckedChanged); // Обработчик изменения

            // 
            // dateFrom
            // 
            this.dateFrom.Enabled = false;
            this.dateFrom.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFrom.Location = new System.Drawing.Point(220, 110);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(120, 27);
            this.dateFrom.TabIndex = 4;

            // 
            // dateTo
            // 
            this.dateTo.Enabled = false;
            this.dateTo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTo.Location = new System.Drawing.Point(360, 110);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(120, 27);
            this.dateTo.TabIndex = 5;

            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnSelectPath.BackColor = System.Drawing.Color.FromArgb(0, 122, 204); // Цвет кнопки
            this.btnSelectPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectPath.ForeColor = Color.White;
            this.btnSelectPath.Location = new System.Drawing.Point(20, 150);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(160, 40);
            this.btnSelectPath.TabIndex = 6;
            this.btnSelectPath.Text = "Выбрать путь";
            this.btnSelectPath.UseVisualStyleBackColor = false;
            this.btnSelectPath.FlatAppearance.BorderSize = 0;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);

            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); // Цвет кнопки
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = Color.White;
            this.btnExport.Location = new System.Drawing.Point(200, 150);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(160, 40);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Экспорт";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPath.ForeColor = Color.Gray;
            this.lblPath.Location = new System.Drawing.Point(20, 200);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(133, 20);
            this.lblPath.TabIndex = 8;
            this.lblPath.Text = "Путь не выбран";

            // 
            // ExportPeriodForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.radioCustom);
            this.Controls.Add(this.radioYear);
            this.Controls.Add(this.radioMonth);
            this.Controls.Add(this.radioWeek);
            this.Name = "ExportPeriodForm";
            this.Text = "Экспорт данных";
            this.Load += new System.EventHandler(this.ExportPeriodForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RadioButton radioWeek;
        private System.Windows.Forms.RadioButton radioMonth;
        private System.Windows.Forms.RadioButton radioYear;
        private System.Windows.Forms.RadioButton radioCustom;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblPath;
    }
}
