using System;
using System.Drawing;
using System.Windows.Forms;
using Diplom.UI;
using Diplom.Services.Calendar;
using Diplom.Core.Service;
using Diplom.Properties;
using Diplom.Core.Setting;
using System.Data;
using Diplom.Helpers;
using Diplom.Core.AntiCheat.Analyzers;
using Diplom.Core.AntiCheat.Interfaces;
using Diplom.Core.AntiCheat.Monitors;
using Diplom.Core.AntiCheat.Services;
using System.Collections.Generic;

namespace Diplom
{
    public partial class MainForm : Form
    {
        private string connectionString = DatabaseConfig.connectionString;
        private WorkSessionManager workSession;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private InactivityMonitor inactivityMonitor;
        private ApplicationTracker appTracker;
        private WorkTimeSettings _settings;
        private ToolStripMenuItem openPdfAfterExportMenuItem;
        private HiddenAntiCheatService _antiCheatService;
        private DateTime lastLunchDay = LunchPersistenceHelper.LoadLunchDay();
        private int userId;
        private string _role; 
        private bool lunchPauseActive = false;
        private bool isLunchBreak = false;

        public MainForm(int userId, string role)
        {
            InitializeComponent();
            InitializeTrayIcon();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
            this.userId = userId;
            _role = role;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadUserId();
            InitializeWorkSession();
          

            _settings = SettingsManager.LoadSettings();
            ThemeManager.ApplyTheme(this, _settings.Theme, menuOptions, lblTimer, btnStartWork, btnStopWork);

            InitInterfaceMenu();

            InitSettingsMenu();
            InitNotificationsMenu();

            ApplySavedFont();

            timer1.Interval = 60000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();


            adminToolStripMenuItem.Visible = (_role == "admin");
           
        }

        private void SetTheme(string theme, ToolStripMenuItem system, ToolStripMenuItem light)
        {
            system.Checked = (theme == "system");
            light.Checked = (theme == "light");
            

            _settings.Theme = theme;
            SettingsManager.SaveSettings(_settings);

            ThemeManager.ApplyTheme(this, theme, menuOptions, lblTimer, btnStartWork, btnStopWork);

        }

        private void InitInterfaceMenu()
        {
            var interfaceMenu = new ToolStripMenuItem("🎨 Интерфейс");

            var themeMenu = new ToolStripMenuItem("Тема");

            var systemThemeItem = new ToolStripMenuItem("Системная") { CheckOnClick = true };
            var lightThemeItem = new ToolStripMenuItem("Светлая") { CheckOnClick = true };

            systemThemeItem.Click += (s, e) => SetTheme("system", systemThemeItem, lightThemeItem);
            lightThemeItem.Click += (s, e) => SetTheme("light", systemThemeItem, lightThemeItem);

            themeMenu.DropDownItems.Add(systemThemeItem);
            themeMenu.DropDownItems.Add(lightThemeItem);

            string currentTheme = _settings.Theme;
            systemThemeItem.Checked = currentTheme == "system";
            lightThemeItem.Checked = currentTheme == "light";

           
            var fontSizeMenu = new ToolStripMenuItem("Размер шрифта");

            var timerFontSizeItem = new ToolStripMenuItem("Размер таймера");
            timerFontSizeItem.Click += (s, e) =>
            {
                using (FontDialog fontDialog = new FontDialog())
                {
                    fontDialog.Font = new Font(_settings.FontFamily, _settings.TimerFontSize);
                    fontDialog.MinSize = 14;
                    fontDialog.MaxSize = 72;

                    if (fontDialog.ShowDialog() == DialogResult.OK)
                    {
                        Font selectedFont = fontDialog.Font;
                        lblTimer.Font = selectedFont;

                        _settings.FontFamily = selectedFont.FontFamily.Name;
                        _settings.TimerFontSize = selectedFont.Size;
                        SettingsManager.SaveSettings(_settings);
                        ReloadUI();
                    }
                }
            };

            var buttonFontSizeItem = new ToolStripMenuItem("Размер кнопок");
            buttonFontSizeItem.Click += (s, e) =>
            {
                using (FontDialog fontDialog = new FontDialog())
                {
                    fontDialog.Font = new Font(_settings.FontFamily, _settings.ButtonFontSize);
                    fontDialog.MinSize = 10;
                    fontDialog.MaxSize = 36;

                    if (fontDialog.ShowDialog() == DialogResult.OK)
                    {
                        Font selectedFont = fontDialog.Font;
                        btnStartWork.Font = selectedFont;
                        btnStopWork.Font = selectedFont;

                        _settings.FontFamily = selectedFont.FontFamily.Name;
                        _settings.ButtonFontSize = selectedFont.Size;
                        SettingsManager.SaveSettings(_settings);
                        ReloadUI();
                    }
                }
            };
            var resetSettingsItem = new ToolStripMenuItem("🔄 Сбросить до базовых");
            resetSettingsItem.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Вы уверены, что хотите сбросить настройки интерфейса?", "Сброс настроек", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    _settings.FontFamily = "Arial";
                    _settings.TimerFontSize = 36f;
                    _settings.ButtonFontSize = 14f;
                    _settings.Theme = "system";

                    SettingsManager.SaveSettings(_settings);
                    ThemeManager.ApplyTheme(this, _settings.Theme, menuOptions, lblTimer, btnStartWork, btnStopWork);

                    ApplySavedFont();
                    ReloadUI();  
                }
            };

            interfaceMenu.DropDownItems.Add(new ToolStripSeparator());
            interfaceMenu.DropDownItems.Add(resetSettingsItem);

            fontSizeMenu.DropDownItems.Add(timerFontSizeItem);
            fontSizeMenu.DropDownItems.Add(buttonFontSizeItem);

            interfaceMenu.DropDownItems.Add(themeMenu);
            interfaceMenu.DropDownItems.Add(fontSizeMenu);

            menuSettings.DropDownItems.Add(interfaceMenu);
        }

        private void InitNotificationsMenu()
        {
            var notificationsMenu = new ToolStripMenuItem("🔔 Уведомления");

            var notifyStart = new ToolStripMenuItem("Оповещать о начале дня") { CheckOnClick = true, Checked = _settings.NotifyStartDay };
            notifyStart.CheckedChanged += (s, e) =>
            {
                _settings.NotifyStartDay = notifyStart.Checked;
                SettingsManager.SaveSettings(_settings);
            };

            var notifyEnd = new ToolStripMenuItem("Оповещать об окончании дня") { CheckOnClick = true, Checked = _settings.NotifyEndDay };
            notifyEnd.CheckedChanged += (s, e) =>
            {
                _settings.NotifyEndDay = notifyEnd.Checked;
                SettingsManager.SaveSettings(_settings);
            };

            var notifyViolations = new ToolStripMenuItem("Оповещать о нарушениях") { CheckOnClick = true, Checked = _settings.NotifyViolations };
            notifyViolations.CheckedChanged += (s, e) =>
            {
                _settings.NotifyViolations = notifyViolations.Checked;
                SettingsManager.SaveSettings(_settings);
            };

            var notifyLogin = new ToolStripMenuItem("Оповещение о входе в аккаунт") { CheckOnClick = true, Checked = _settings.NotifyLogin };
            notifyLogin.CheckedChanged += (s, e) =>
            {
                _settings.NotifyLogin = notifyLogin.Checked;
                SettingsManager.SaveSettings(_settings);
            };

            var notifyLogout = new ToolStripMenuItem("Оповещение о выходе с аккаунта") { CheckOnClick = true, Checked = _settings.NotifyLogout };
            notifyLogout.CheckedChanged += (s, e) =>
            {
                _settings.NotifyLogout = notifyLogout.Checked;
                SettingsManager.SaveSettings(_settings);
            };

            notificationsMenu.DropDownItems.Add(notifyStart);
            notificationsMenu.DropDownItems.Add(notifyEnd);
            notificationsMenu.DropDownItems.Add(notifyViolations);
            notificationsMenu.DropDownItems.Add(notifyLogin);
            notificationsMenu.DropDownItems.Add(notifyLogout);

            menuSettings.DropDownItems.Add(notificationsMenu);
        }

        public void ReloadUI()
        {
            Controls.Clear();
            InitializeComponent();
            MainForm_Load(this, EventArgs.Empty);
        }

        private void ApplySavedFont()
        {
            var timerFont = new Font(_settings.FontFamily, _settings.TimerFontSize);
            var buttonFont = new Font(_settings.FontFamily, _settings.ButtonFontSize);

            lblTimer.Font = timerFont;
            btnStartWork.Font = buttonFont;
            btnStopWork.Font = buttonFont;

            lblTimer.AutoSize = true;
            btnStartWork.AutoSize = true;
            btnStopWork.AutoSize = true;

            MainForm_Resize(this, EventArgs.Empty); 
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now.TimeOfDay;
            var today = DateTime.Today;
            var lunchStart = _settings.LunchTime;
            var lunchEnd = lunchStart + _settings.LunchDuration;

            if (lastLunchDay != today)
            {
                lastLunchDay = today;
                lunchPauseActive = false;
                isLunchBreak = false;
            }

            if (!lunchPauseActive && now >= lunchStart && now <= lunchEnd)
            {
                lunchPauseActive = true;
                isLunchBreak = true;

                workSession.Disable();
                workSession.Pause();
                inactivityMonitor.PauseMonitoring();

                lblTimer.Text = "⏳ Сейчас обеденный перерыв";
                lblTimer.ForeColor = Color.OrangeRed;
                lblTimer.AutoSize = true;
                lblTimer.Location = new Point((this.ClientSize.Width - lblTimer.Width) / 2, lblTimer.Location.Y);

                ShowNotification("Обеденный перерыв", $"Начался обеденный перерыв и продлится до {lunchEnd} ");
                LunchPersistenceHelper.SaveLunchDay(DateTime.Today);

            }
            else if (lunchPauseActive && now > lunchEnd && isLunchBreak)
            {
                isLunchBreak = false;

                workSession.Resume();
                inactivityMonitor.ResetInactivityFlag();
                workSession.AddLunchBonus(_settings.LunchDuration);
                workSession.Enable();

                inactivityMonitor.ResumeMonitoring();
               

                lblTimer.ForeColor = Color.White;
                ReloadUI();
            }
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
            inactivityMonitor = new InactivityMonitor(workSession, this, userId);

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

            if (_settings.NotifyStartDay)
            {
                ShowNotification("Рабочий день начат", "Вы начали рабочую сессию.");
            }

            var monitors = new List<IInputMonitor>
            {
                new MouseMonitor()
            };
                    var analyzers = new List<IActivityAnalyzer>
            {
                new MousePatternAnalyzer()
            };

            _antiCheatService = new HiddenAntiCheatService(monitors, analyzers, userId);
            _antiCheatService.Start();

            AntiCheatService.Start(_antiCheatService);
        }

        private void btnStopWork_Click(object sender, EventArgs e)
        {
            workSession.Stop();
            appTracker.StopTracking();
            AntiCheatService.Stop();
            _antiCheatService.Stop();
            if (_settings.NotifyEndDay)
            {
                ShowNotification("Рабочий день завершён", "Вы завершили рабочую сессию.");
            }

        }

        private void ShowNotification(string title, string message)
        {
            var notifyIcon = new NotifyIcon
            {
                Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico"),
                Visible = true,
                BalloonTipTitle = title,
                BalloonTipText = message
            };

            notifyIcon.ShowBalloonTip(300);

            Timer timer = new Timer { Interval = 40 };
            timer.Tick += (s, e) =>
            {
                notifyIcon.Dispose();
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
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
                if (workSession != null && workSession.IsRunning)
                {
                    e.Cancel = true;
                    Hide();
                    trayIcon.Visible = true;
                    trayIcon.ShowBalloonTip(3000, "Программа свернута", "Приложение продолжает работать в фоновом режиме", ToolTipIcon.Info);
                }
                else
                {
                    trayIcon.Visible = false;
                    trayIcon.Dispose();
                    Application.Exit();
                }
            }
            else
            {
                trayIcon.Visible = false;
            }
        }

        private void InitSettingsMenu()
        {
            _settings = SettingsManager.LoadSettings();

            openPdfAfterExportMenuItem = new ToolStripMenuItem("Открывать PDF после экспорта");
            openPdfAfterExportMenuItem.CheckOnClick = true;
            openPdfAfterExportMenuItem.Checked = _settings.OpenPdfAfterExport;

            openPdfAfterExportMenuItem.CheckedChanged += (s, e) =>
            {
                _settings.OpenPdfAfterExport = openPdfAfterExportMenuItem.Checked;
                SettingsManager.SaveSettings(_settings);
            };

            if (settingsPdfGroup != null && !settingsPdfGroup.DropDownItems.Contains(openPdfAfterExportMenuItem))
            {
                settingsPdfGroup.DropDownItems.Add(openPdfAfterExportMenuItem);
            }
            var languageGroup = new ToolStripMenuItem("🌍 Язык");

            var langRu = new ToolStripMenuItem("Русский") { Checked = _settings.Language == "ru", CheckOnClick = true };
            var langEn = new ToolStripMenuItem("English") { Checked = _settings.Language == "en", CheckOnClick = true };

            langRu.Click += (s, e) => ChangeLanguage("ru");
            langEn.Click += (s, e) => ChangeLanguage("en");

            languageGroup.DropDownItems.Add(langRu);
            languageGroup.DropDownItems.Add(langEn);

            menuSettings.DropDownItems.Add(languageGroup);

        }

        private void menuProfile_Click(object sender, EventArgs e)
        {

            IUserDataService userDataService = new UserDataService(connectionString, userId);
            IWorkSessionService workSessionService = new WorkSessionService(connectionString);
            IViolationService violationService = new ViolationService(connectionString, userId);
            IOvertimeService overtimeService = new OvertimeService(connectionString);
            ICalendarRenderer renderer = new CalendarRenderer();
            IPdfReportService pdfReportService = new PdfReportService(workSessionService, violationService, overtimeService);

            UserProfileForm userProfileForm = new UserProfileForm(userDataService, violationService, workSessionService, renderer, pdfReportService, userId);
            userProfileForm.Show();

        }

        private void settingsLunchTime_Click(object sender, EventArgs e)
        {
            var settings = SettingsManager.LoadSettings();


            var form = new WorkTimeSettingsForm(settings.LunchTime, TimeSpan.Zero, true, false);

            if (form.ShowDialog() == DialogResult.OK)
            {
                settings.LunchTime = form.LunchTime;
                SettingsManager.SaveSettings(settings);
                _settings = settings;
            }
        }
        private void settingsLunchSession_Click(object sender, EventArgs e)
        {
            var settings = SettingsManager.LoadSettings();

            var form = new WorkTimeSettingsForm(TimeSpan.Zero, settings.LunchDuration, false, true);

            if (form.ShowDialog() == DialogResult.OK)
            {
                settings.LunchDuration = form.LunchDuration;
                SettingsManager.SaveSettings(settings);
                _settings = settings;
            }
        }

        private void settingsPdfTemplate_Click(object sender, EventArgs e)
        {
            var settings = SettingsManager.LoadSettings();

            var form = new PdfTemplateForm(settings.PdfFileNameTemplate);
            if (form.ShowDialog() == DialogResult.OK)
            {
                settings.PdfFileNameTemplate = form.Template;
                SettingsManager.SaveSettings(settings);
                _settings = settings;
            }
        }

        private void settingsDefaultPath_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите папку для сохранения PDF по умолчанию";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    var settings = SettingsManager.LoadSettings();
                    settings.DefaultExportPath = folderDialog.SelectedPath;

                    SettingsManager.SaveSettings(settings);
                    _settings = settings;

                    MessageBox.Show("Путь по умолчанию успешно сохранён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void ChangeLanguage(string lang)
        {
            _settings.Language = lang;
            SettingsManager.SaveSettings(_settings);

            MessageBox.Show("Изменения языка вступят в силу после перезапуска приложения.", "Язык изменён", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
           var logout = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (logout == DialogResult.Yes)
            {
                if (_settings.NotifyLogout)
                    ShowNotification("Выход","Вы вышли из учетки");
                Application.Restart();
            }
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
        }
        public bool IsLunchBreak => isLunchBreak;
    }
}