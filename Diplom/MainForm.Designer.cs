namespace Diplom
{
    partial class MainForm
    {
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnStartWork;
        private System.Windows.Forms.Button btnStopWork;
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuProfile;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;

        /// <summary>
        /// Освобождает используемые ресурсы
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }

        #region Код, сгенерированный дизайнером формы

        /// <summary>
        /// Метод для инициализации компонентов формы
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnStartWork = new System.Windows.Forms.Button();
            this.btnStopWork = new System.Windows.Forms.Button();
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.Color.Transparent;
            this.lblTimer.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTimer.Location = new System.Drawing.Point(200, 150);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(400, 80);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "00:00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStartWork
            // 
            this.btnStartWork.Font = new System.Drawing.Font("Arial", 14F);
            this.btnStartWork.Location = new System.Drawing.Point(251, 321);
            this.btnStartWork.Name = "btnStartWork";
            this.btnStartWork.Size = new System.Drawing.Size(300, 50);
            this.btnStartWork.TabIndex = 1;
            this.btnStartWork.Text = "▶️ Начать рабочий день";
            this.btnStartWork.Click += new System.EventHandler(this.btnStartWork_Click);
            // 
            // btnStopWork
            // 
            this.btnStopWork.Font = new System.Drawing.Font("Arial", 14F);
            this.btnStopWork.Location = new System.Drawing.Point(251, 391);
            this.btnStopWork.Name = "btnStopWork";
            this.btnStopWork.Size = new System.Drawing.Size(300, 50);
            this.btnStopWork.TabIndex = 2;
            this.btnStopWork.Text = "⏸️ Остановить рабочий день";
            this.btnStopWork.Click += new System.EventHandler(this.btnStopWork_Click);
            // 
            // topMenu
            // 
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
            this.menuLogout});
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(79, 24);
            this.menuOptions.Text = "≡ Меню";
            // 
            // menuProfile
            // 
            this.menuProfile.Name = "menuProfile";
            this.menuProfile.Size = new System.Drawing.Size(245, 26);
            this.menuProfile.Text = "🏠 Личный кабинет";
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
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
