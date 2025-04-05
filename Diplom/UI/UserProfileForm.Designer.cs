using System;
using System.Windows.Forms;
using System.Drawing;

namespace Diplom.UI
{
    partial class UserProfileForm : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserProfileForm));
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblTotalWorkTime = new System.Windows.Forms.Label();
            this.lblSessionsHeader = new System.Windows.Forms.Label();
            this.lblViolationsHeader = new System.Windows.Forms.Label();
            this.dataGridWorkSessions = new System.Windows.Forms.DataGridView();
            this.dataGridViolations = new System.Windows.Forms.DataGridView();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridWorkSessions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViolations)).BeginInit();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblWelcome.Location = new System.Drawing.Point(20, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(500, 37);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Добро пожаловать, [Имя]!";
            // 
            // lblFullName
            // 
            this.lblFullName.BackColor = System.Drawing.Color.Transparent;
            this.lblFullName.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblFullName.Location = new System.Drawing.Point(20, 85);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(350, 20);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "ФИО: ";
            // 
            // lblTotalWorkTime
            // 
            this.lblTotalWorkTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalWorkTime.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblTotalWorkTime.Location = new System.Drawing.Point(20, 110);
            this.lblTotalWorkTime.Name = "lblTotalWorkTime";
            this.lblTotalWorkTime.Size = new System.Drawing.Size(350, 20);
            this.lblTotalWorkTime.TabIndex = 3;
            this.lblTotalWorkTime.Text = "Общее рабочее время: ";
            // 
            // lblSessionsHeader
            // 
            this.lblSessionsHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblSessionsHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSessionsHeader.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblSessionsHeader.Location = new System.Drawing.Point(20, 140);
            this.lblSessionsHeader.Name = "lblSessionsHeader";
            this.lblSessionsHeader.Size = new System.Drawing.Size(300, 25);
            this.lblSessionsHeader.TabIndex = 4;
            this.lblSessionsHeader.Text = "История рабочих сессий:";
            // 
            // lblViolationsHeader
            // 
            this.lblViolationsHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblViolationsHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblViolationsHeader.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblViolationsHeader.Location = new System.Drawing.Point(20, 320);
            this.lblViolationsHeader.Name = "lblViolationsHeader";
            this.lblViolationsHeader.Size = new System.Drawing.Size(300, 25);
            this.lblViolationsHeader.TabIndex = 6;
            this.lblViolationsHeader.Text = "Нарушения режима:";
            // 
            // dataGridWorkSessions
            // 
            this.dataGridWorkSessions.AllowUserToAddRows = false;
            this.dataGridWorkSessions.AllowUserToDeleteRows = false;
            this.dataGridWorkSessions.ColumnHeadersHeight = 29;
            this.dataGridWorkSessions.Location = new System.Drawing.Point(20, 170);
            this.dataGridWorkSessions.Name = "dataGridWorkSessions";
            this.dataGridWorkSessions.ReadOnly = true;
            this.dataGridWorkSessions.RowHeadersWidth = 51;
            this.dataGridWorkSessions.Size = new System.Drawing.Size(640, 150);
            this.dataGridWorkSessions.TabIndex = 5;
            // 
            // dataGridViolations
            // 
            this.dataGridViolations.AllowUserToAddRows = false;
            this.dataGridViolations.AllowUserToDeleteRows = false;
            this.dataGridViolations.ColumnHeadersHeight = 29;
            this.dataGridViolations.Location = new System.Drawing.Point(20, 350);
            this.dataGridViolations.Name = "dataGridViolations";
            this.dataGridViolations.ReadOnly = true;
            this.dataGridViolations.RowHeadersWidth = 51;
            this.dataGridViolations.Size = new System.Drawing.Size(640, 150);
            this.dataGridViolations.TabIndex = 7;
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(20, 506);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(640, 35);
            this.btnExportPDF.TabIndex = 8;
            this.btnExportPDF.Text = "Экспортировать в PDF";
            // 
            // topMenu
            // 
            this.topMenu.BackColor = System.Drawing.Color.Transparent;
            this.topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(780, 28);
            this.topMenu.TabIndex = 0;
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProfile,
            this.menuSettings,
            this.menuLogout});
            this.menuOptions.ForeColor = System.Drawing.SystemColors.Control;
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
            // UserProfileForm
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(780, 580);
            this.Controls.Add(this.topMenu);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblTotalWorkTime);
            this.Controls.Add(this.lblSessionsHeader);
            this.Controls.Add(this.dataGridWorkSessions);
            this.Controls.Add(this.lblViolationsHeader);
            this.Controls.Add(this.dataGridViolations);
            this.Controls.Add(this.btnExportPDF);
            this.MainMenuStrip = this.topMenu;
            this.Name = "UserProfileForm";
            this.Text = "Личный кабинет";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridWorkSessions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViolations)).EndInit();
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        // ===== Поля формы =====
        private Label lblWelcome;
        private Label lblFullName;
        private Label lblTotalWorkTime;
        private Label lblSessionsHeader;
        private Label lblViolationsHeader;
        private DataGridView dataGridWorkSessions;
        private DataGridView dataGridViolations;
        private Button btnExportPDF;

        private MenuStrip topMenu;
        private ToolStripMenuItem menuOptions;
        private ToolStripMenuItem menuProfile;
        private ToolStripMenuItem menuSettings;
        private ToolStripMenuItem menuLogout;
    }
}
