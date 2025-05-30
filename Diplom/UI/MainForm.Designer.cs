namespace Diplom
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnStartWork;
        private System.Windows.Forms.Button btnStopWork;
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuProfile;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;

        // Настройки: группы и пункты
        private System.Windows.Forms.ToolStripMenuItem settingsWorkGroup;
        private System.Windows.Forms.ToolStripMenuItem settingsLunchTime;
        private System.Windows.Forms.ToolStripMenuItem settingsLunchSession;

        private System.Windows.Forms.ToolStripMenuItem settingsPdfGroup;
        private System.Windows.Forms.ToolStripMenuItem settingsPdfTemplate;
        private System.Windows.Forms.ToolStripMenuItem settingsDefaultPath;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnStartWork = new System.Windows.Forms.Button();
            this.btnStopWork = new System.Windows.Forms.Button();
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsWorkGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsLunchTime = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsLunchSession = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsPdfGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsPdfTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsDefaultPath = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.статистикаСотрудниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.BackColor = System.Drawing.Color.Transparent;
            this.lblTimer.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTimer.Location = new System.Drawing.Point(265, 152);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(268, 70);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "00:00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartWork
            // 
            this.btnStartWork.AutoSize = true;
            this.btnStartWork.Font = new System.Drawing.Font("Arial", 14F);
            this.btnStartWork.Location = new System.Drawing.Point(226, 324);
            this.btnStartWork.Name = "btnStartWork";
            this.btnStartWork.Size = new System.Drawing.Size(345, 50);
            this.btnStartWork.TabIndex = 1;
            this.btnStartWork.Text = "▶️ Начать рабочий день";
            this.btnStartWork.Click += new System.EventHandler(this.btnStartWork_Click);
            // 
            // btnStopWork
            // 
            this.btnStopWork.AutoSize = true;
            this.btnStopWork.Font = new System.Drawing.Font("Arial", 14F);
            this.btnStopWork.Location = new System.Drawing.Point(226, 391);
            this.btnStopWork.Name = "btnStopWork";
            this.btnStopWork.Size = new System.Drawing.Size(345, 50);
            this.btnStopWork.TabIndex = 2;
            this.btnStopWork.Text = "⏸️ Остановить рабочий день";
            this.btnStopWork.Click += new System.EventHandler(this.btnStopWork_Click);
            // 
            // topMenu
            // 
            this.topMenu.BackColor = System.Drawing.Color.Transparent;
            this.topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(800, 28);
            this.topMenu.TabIndex = 3;
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProfile,
            this.menuSettings,
            this.menuLogout,
            this.adminToolStripMenuItem,
            this.статистикаСотрудниковToolStripMenuItem});
            this.menuOptions.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(79, 24);
            this.menuOptions.Text = "≡ Меню";
            // 
            // menuProfile
            // 
            this.menuProfile.BackColor = System.Drawing.Color.Transparent;
            this.menuProfile.Name = "menuProfile";
            this.menuProfile.Size = new System.Drawing.Size(284, 26);
            this.menuProfile.Text = "🏠 Личный кабинет";
            this.menuProfile.Click += new System.EventHandler(this.menuProfile_Click);
            this.menuProfile.Paint += new System.Windows.Forms.PaintEventHandler(this.RemoveBackground);
            // 
            // menuSettings
            // 
            this.menuSettings.BackColor = System.Drawing.Color.Transparent;
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsWorkGroup,
            this.settingsPdfGroup});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(284, 26);
            this.menuSettings.Text = "⚙ Настройки";
            this.menuSettings.Paint += new System.Windows.Forms.PaintEventHandler(this.RemoveBackground);
            // 
            // settingsWorkGroup
            // 
            this.settingsWorkGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsLunchTime,
            this.settingsLunchSession});
            this.settingsWorkGroup.Name = "settingsWorkGroup";
            this.settingsWorkGroup.Size = new System.Drawing.Size(223, 26);
            this.settingsWorkGroup.Text = "🕓 Рабочее время";
            // 
            // settingsLunchTime
            // 
            this.settingsLunchTime.Name = "settingsLunchTime";
            this.settingsLunchTime.Size = new System.Drawing.Size(259, 26);
            this.settingsLunchTime.Text = "🍽️ Время обеда";
            this.settingsLunchTime.Click += new System.EventHandler(this.settingsLunchTime_Click);
            // 
            // settingsLunchSession
            // 
            this.settingsLunchSession.Name = "settingsLunchSession";
            this.settingsLunchSession.Size = new System.Drawing.Size(259, 26);
            this.settingsLunchSession.Text = "⏱ Длительность обеда";
            this.settingsLunchSession.Click += new System.EventHandler(this.settingsLunchSession_Click);
            // 
            // settingsPdfGroup
            // 
            this.settingsPdfGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsPdfTemplate,
            this.settingsDefaultPath});
            this.settingsPdfGroup.Name = "settingsPdfGroup";
            this.settingsPdfGroup.Size = new System.Drawing.Size(223, 26);
            this.settingsPdfGroup.Text = "📄 PDF и экспорт";
            // 
            // settingsPdfTemplate
            // 
            this.settingsPdfTemplate.Name = "settingsPdfTemplate";
            this.settingsPdfTemplate.Size = new System.Drawing.Size(246, 26);
            this.settingsPdfTemplate.Text = "Шаблон имени файла";
            this.settingsPdfTemplate.Click += new System.EventHandler(this.settingsPdfTemplate_Click);
            // 
            // settingsDefaultPath
            // 
            this.settingsDefaultPath.Name = "settingsDefaultPath";
            this.settingsDefaultPath.Size = new System.Drawing.Size(246, 26);
            this.settingsDefaultPath.Text = "Путь по умолчанию";
            this.settingsDefaultPath.Click += new System.EventHandler(this.settingsDefaultPath_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.BackColor = System.Drawing.Color.Transparent;
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(284, 26);
            this.menuLogout.Text = "🚪 Выйти из аккаунта";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            this.menuLogout.Paint += new System.Windows.Forms.PaintEventHandler(this.RemoveBackground);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(284, 26);
            this.adminToolStripMenuItem.Text = "🛠️Админ панель";
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // статистикаСотрудниковToolStripMenuItem
            // 
            this.статистикаСотрудниковToolStripMenuItem.Name = "статистикаСотрудниковToolStripMenuItem";
            this.статистикаСотрудниковToolStripMenuItem.Size = new System.Drawing.Size(284, 26);
            this.статистикаСотрудниковToolStripMenuItem.Text = "📈 Статистика сотрудников";
            this.статистикаСотрудниковToolStripMenuItem.Click += new System.EventHandler(this.статистикаСотрудниковToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.btnStartWork);
            this.Controls.Add(this.btnStopWork);
            this.Controls.Add(this.topMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.topMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рабочий стол";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void RemoveBackground(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            ((System.Windows.Forms.ToolStripMenuItem)sender).BackColor = System.Drawing.Color.Transparent;
        }
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаСотрудниковToolStripMenuItem;
    }
}
