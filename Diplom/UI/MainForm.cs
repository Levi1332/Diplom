using System;
using System.Drawing;
using System.Windows.Forms;
using Diplom.UI;


namespace Diplom
{
    public partial class MainForm : Form
    {
        private string connectionString = DatabaseConfig.connectionString;
        private WorkSessionManager workSession;
        private int userId;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private InactivityMonitor inactivityMonitor;
        private ApplicationTracker appTracker;

        public MainForm(int userId)
        {
            InitializeComponent();
            InitializeTrayIcon();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
            this.userId = userId;
            this.FormClosing += OnFormClosing; 

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadUserId();
            InitializeWorkSession();
        }

        private void LoadUserId()
        {
            if (userId == -1)
            {
                MessageBox.Show("Ошибка: Не удалось загрузить данные пользователя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void InitializeWorkSession()
        {
            workSession = new WorkSessionManager(userId, lblTimer);
            inactivityMonitor = new InactivityMonitor(workSession);

            IApplicationRepository repository = new ApplicationRepository(connectionString);
            IBlockedAppsRepository blockedAppsRepo = new BlockedAppsRepository(connectionString);

            IViolationRepository violationRepo = new ViolationRepository(connectionString);
            IApplicationBlocker applicationBlocker = new ApplicationBlocker(userId, violationRepo);

            appTracker = new ApplicationTracker(userId, repository, blockedAppsRepo, applicationBlocker);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            lblTimer.Location = new System.Drawing.Point((this.ClientSize.Width - lblTimer.Width) / 2, 150);
            btnStartWork.Location = new System.Drawing.Point((this.ClientSize.Width - btnStartWork.Width) / 2, 300);
            btnStopWork.Location = new System.Drawing.Point((this.ClientSize.Width - btnStopWork.Width) / 2, 370);
        }

        private void btnStartWork_Click(object sender, EventArgs e)
        {
            workSession.Start();
            appTracker.StartTracking();
        }

        private void btnStopWork_Click(object sender, EventArgs e)
        {
            workSession.Stop();
            appTracker.StopTracking();
        }

        private void InitializeTrayIcon()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Открыть", null, ShowMainForm);
            trayMenu.Items.Add("Выход", null, ExitApplication);

            trayIcon = new NotifyIcon
            {
                Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico"),
                ContextMenuStrip = trayMenu,
                Text = "Программа работает в фоновом режиме",
                Visible = false
            };
            trayIcon.DoubleClick += ShowMainForm;
        }

        private void ShowMainForm(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (workSession.IsRunning)
                {
                    e.Cancel = true;
                    Hide();
                    trayIcon.Visible = true;
                    trayIcon.ShowBalloonTip(3000, "Программа свернута", "Приложение продолжает работать в фоновом режиме", ToolTipIcon.Info);
                }
                else
                {
                    ExitApplication(sender, e); 
                }
            }
        }

        private void menuProfile_Click(object sender, EventArgs e)
        {
            
            IUserDataService userDataService = new UserDataService(connectionString,userId);
            IWorkSessionService workSessionService = new WorkSessionService(connectionString,userId);
            IViolationService violationService = new ViolationService(connectionString,userId);

            UserProfileForm userProfileForm = new UserProfileForm(userDataService, workSessionService, violationService);
            userProfileForm.Show();

        }
    }
}
