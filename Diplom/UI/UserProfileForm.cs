using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom.UI
{
    public partial class UserProfileForm : Form
    {
        
        private readonly IUserDataService _userDataService;
        private readonly IWorkSessionService _workSessionService;
        private readonly IViolationService _violationService;

        public UserProfileForm(
            IUserDataService userDataService,
            IWorkSessionService workSessionService,
            IViolationService violationService)
        {
            InitializeComponent();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");

            _userDataService = userDataService;
            _workSessionService = workSessionService;
            _violationService = violationService;
           
            LoadUserData();
            LoadWorkSessions();
            LoadViolations();
            
        }

        private void LoadUserData()
        {
            lblFullName.Text = $"ФИО: {_userDataService.GetFullName()}";
            lblTotalWorkTime.Text = $"Общее рабочее время: {_userDataService.GetTotalWorkTime()}";
        }

        private void LoadWorkSessions()
        {
            dataGridWorkSessions.DataSource = _workSessionService.GetWorkSessions();
        }

        private void LoadViolations()
        {
            dataGridViolations.DataSource = _violationService.GetViolations();
        }
    }
}
