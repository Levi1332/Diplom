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
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
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
            this.topMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViolations)).BeginInit();
            this.SuspendLayout();
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.BackColor = System.Drawing.Color.Transparent;
            this.scrollPanel.Controls.Add(this.topMenu);
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
            this.scrollPanel.Size = new System.Drawing.Size(800, 603);
            this.scrollPanel.TabIndex = 0;
            // 
            // topMenu
            // 
            this.topMenu.BackColor = System.Drawing.Color.Transparent;
            this.topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(779, 28);
            this.topMenu.TabIndex = 0;
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProfile,
            this.menuSettings,
            this.menuLogout});
            this.menuOptions.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(79, 24);
            this.menuOptions.Text = "≡ Меню";
            // 
            // menuProfile
            // 
            this.menuProfile.Name = "menuProfile";
            this.menuProfile.Size = new System.Drawing.Size(245, 26);
            this.menuProfile.Text = "🏠 Главный экран";
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(245, 26);
            this.menuSettings.Text = "⚙ Настройки";
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(245, 26);
            this.menuLogout.Text = "🚪 Выйти из аккаунта";
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblWelcome.Location = new System.Drawing.Point(12, 44);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(700, 37);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Добро пожаловать!";
            // 
            // lblFullName
            // 
            this.lblFullName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFullName.Location = new System.Drawing.Point(12, 89);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(500, 20);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "ФИО: ";
            // 
            // lblTotalWorkTime
            // 
            this.lblTotalWorkTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalWorkTime.Location = new System.Drawing.Point(12, 114);
            this.lblTotalWorkTime.Name = "lblTotalWorkTime";
            this.lblTotalWorkTime.Size = new System.Drawing.Size(500, 20);
            this.lblTotalWorkTime.TabIndex = 3;
            this.lblTotalWorkTime.Text = "Общее рабочее время: ";
            // 
            // lblSessionsHeader
            // 
            this.lblSessionsHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSessionsHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSessionsHeader.Location = new System.Drawing.Point(12, 144);
            this.lblSessionsHeader.Name = "lblSessionsHeader";
            this.lblSessionsHeader.Size = new System.Drawing.Size(300, 25);
            this.lblSessionsHeader.TabIndex = 4;
            this.lblSessionsHeader.Text = "Календарь рабочих дней:";
            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrevMonth.Location = new System.Drawing.Point(12, 174);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(40, 30);
            this.btnPrevMonth.TabIndex = 5;
            this.btnPrevMonth.Text = "<";
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextMonth.Location = new System.Drawing.Point(272, 174);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(40, 30);
            this.btnNextMonth.TabIndex = 6;
            this.btnNextMonth.Text = ">";
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMonthYear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMonthYear.Location = new System.Drawing.Point(62, 174);
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
            this.lblViolationsHeader.Location = new System.Drawing.Point(12, 866);
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
            this.dataGridViolations.Location = new System.Drawing.Point(12, 896);
            this.dataGridViolations.Name = "dataGridViolations";
            this.dataGridViolations.ReadOnly = true;
            this.dataGridViolations.RowHeadersWidth = 51;
            this.dataGridViolations.Size = new System.Drawing.Size(705, 200);
            this.dataGridViolations.TabIndex = 10;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(12, 1106);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(705, 40);
            this.btnExportPDF.TabIndex = 11;
            this.btnExportPDF.Text = "Экспортировать в PDF";
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // flowCalendar
            // 
            this.flowCalendar.BackColor = System.Drawing.Color.Transparent;
            this.flowCalendar.Location = new System.Drawing.Point(12, 214);
            this.flowCalendar.Name = "flowCalendar";
            this.flowCalendar.Size = new System.Drawing.Size(705, 649);
            this.flowCalendar.TabIndex = 8;
            // 
            // UserProfileForm
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(800, 603);
            this.Controls.Add(this.scrollPanel);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.topMenu;
            this.Name = "UserProfileForm";
            this.Text = "Личный кабинет";
            this.scrollPanel.ResumeLayout(false);
            this.scrollPanel.PerformLayout();
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
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
        private MenuStrip topMenu;
        private ToolStripMenuItem menuOptions;
        private ToolStripMenuItem menuProfile;
        private ToolStripMenuItem menuSettings;
        private ToolStripMenuItem menuLogout;
        private DoubleBufferedFlowLayoutPanel flowCalendar;
        private Button btnPrevMonth;
        private Button btnNextMonth;
        private Label lblMonthYear;
    }
}
