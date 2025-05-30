using System.Drawing;
using System.Windows.Forms;

namespace Diplom.UI
{
    partial class UserProfileForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblTotalWorkTime = new System.Windows.Forms.Label();
            this.lblSessionsHeader = new System.Windows.Forms.Label();
            this.btnPrevMonth = new System.Windows.Forms.Button();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.lblViolationsHeader = new System.Windows.Forms.Label();
            this.dataGridViolations = new System.Windows.Forms.DataGridView();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.flowCalendar = new DoubleBufferedFlowLayoutPanel();
            this.scrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViolations)).BeginInit();
            this.SuspendLayout();
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.BackColor = System.Drawing.Color.Transparent;
            this.scrollPanel.Controls.Add(this.lblWelcome);
            this.scrollPanel.Controls.Add(this.lblFullName);
            this.scrollPanel.Controls.Add(this.lblTotalWorkTime);
            this.scrollPanel.Controls.Add(this.lblSessionsHeader);
            this.scrollPanel.Controls.Add(this.btnPrevMonth);
            this.scrollPanel.Controls.Add(this.btnNextMonth);
            this.scrollPanel.Controls.Add(this.lblMonthYear);
            this.scrollPanel.Controls.Add(this.flowCalendar);
            this.scrollPanel.Controls.Add(this.lblViolationsHeader);
            this.scrollPanel.Controls.Add(this.dataGridViolations);
            this.scrollPanel.Controls.Add(this.btnExportPDF);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Size = new System.Drawing.Size(1700, 603);
            this.scrollPanel.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWelcome.Location = new System.Drawing.Point(12, 46);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(700, 37);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Добро пожаловать!";
            // 
            // lblFullName
            // 
            this.lblFullName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFullName.Location = new System.Drawing.Point(16, 99);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(500, 20);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "ФИО: ";
            // 
            // lblTotalWorkTime
            // 
            this.lblTotalWorkTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalWorkTime.Location = new System.Drawing.Point(16, 136);
            this.lblTotalWorkTime.Name = "lblTotalWorkTime";
            this.lblTotalWorkTime.Size = new System.Drawing.Size(500, 20);
            this.lblTotalWorkTime.TabIndex = 3;
            this.lblTotalWorkTime.Text = "Общее рабочее время: ";
            // 
            // lblSessionsHeader
            // 
            this.lblSessionsHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSessionsHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSessionsHeader.Location = new System.Drawing.Point(14, 181);
            this.lblSessionsHeader.Name = "lblSessionsHeader";
            this.lblSessionsHeader.Size = new System.Drawing.Size(300, 25);
            this.lblSessionsHeader.TabIndex = 4;
            this.lblSessionsHeader.Text = "Календарь рабочих дней:";
            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrevMonth.Location = new System.Drawing.Point(14, 211);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(40, 30);
            this.btnPrevMonth.TabIndex = 5;
            this.btnPrevMonth.Text = "<";
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextMonth.Location = new System.Drawing.Point(274, 211);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(40, 30);
            this.btnNextMonth.TabIndex = 6;
            this.btnNextMonth.Text = ">";
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMonthYear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMonthYear.Location = new System.Drawing.Point(64, 211);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(200, 30);
            this.lblMonthYear.TabIndex = 7;
            this.lblMonthYear.Text = "Месяц Год";
            this.lblMonthYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblViolationsHeader
            // 
            this.lblViolationsHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblViolationsHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblViolationsHeader.Location = new System.Drawing.Point(729, 211);
            this.lblViolationsHeader.Name = "lblViolationsHeader";
            this.lblViolationsHeader.Size = new System.Drawing.Size(300, 25);
            this.lblViolationsHeader.TabIndex = 9;
            this.lblViolationsHeader.Text = "Нарушения режима:";
            // 
            // dataGridViolations
            // 
            this.dataGridViolations.AllowUserToAddRows = false;
            this.dataGridViolations.AllowUserToDeleteRows = false;
            this.dataGridViolations.ColumnHeadersHeight = 40;
            this.dataGridViolations.Location = new System.Drawing.Point(734, 251);
            this.dataGridViolations.Name = "dataGridViolations";
            this.dataGridViolations.ReadOnly = true;
            this.dataGridViolations.RowHeadersWidth = 51;
            this.dataGridViolations.Size = new System.Drawing.Size(753, 601);
            this.dataGridViolations.TabIndex = 10;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(14, 871);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(1584, 40);
            this.btnExportPDF.TabIndex = 11;
            this.btnExportPDF.Text = "Экспортировать в PDF";
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // flowCalendar
            // 
            this.flowCalendar.BackColor = System.Drawing.Color.Transparent;
            this.flowCalendar.Location = new System.Drawing.Point(14, 251);
            this.flowCalendar.Name = "flowCalendar";
            this.flowCalendar.Size = new System.Drawing.Size(705, 601);
            this.flowCalendar.TabIndex = 8;
            // 
            // UserProfileForm
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1700, 603);
            this.WindowState = FormWindowState.Maximized;
            this.Controls.Add(this.scrollPanel);
            this.DoubleBuffered = true;
            this.Name = "UserProfileForm";
            this.Text = "Личный кабинет";
            this.scrollPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViolations)).EndInit();
            this.ResumeLayout(false);

        }

        private Panel scrollPanel;
        private Label lblWelcome;
        private Label lblFullName;
        private Label lblTotalWorkTime;
        private Label lblSessionsHeader;
        private Label lblViolationsHeader;
        private DataGridView dataGridViolations;
        private Button btnExportPDF;
        private DoubleBufferedFlowLayoutPanel flowCalendar;
        private Button btnPrevMonth;
        private Button btnNextMonth;
        private Label lblMonthYear;
    }
}
