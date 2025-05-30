using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom
{
    public partial class WorkTimeSettingsForm : Form
    {
        public TimeSpan LunchTime { get; private set; }
        public TimeSpan LunchDuration { get; private set; }

        public WorkTimeSettingsForm(TimeSpan currentLunch, TimeSpan currentDuration, bool lunchTime, bool lunchSession)
        {
            InitializeComponent();

            timePickerLunch.Value = DateTime.Today.Add(currentLunch);

            cmbLunchDuration.Items.Clear();
            cmbLunchDuration.Items.AddRange(new object[] {
                TimeSpan.FromMinutes(10),
                TimeSpan.FromMinutes(20),
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(40),
                TimeSpan.FromMinutes(50),
                TimeSpan.FromMinutes(60)
            });

            if (currentDuration == TimeSpan.FromMinutes(10)) cmbLunchDuration.SelectedIndex = 0;
            else if (currentDuration == TimeSpan.FromMinutes(20)) cmbLunchDuration.SelectedIndex = 1;
            else if (currentDuration == TimeSpan.FromMinutes(30)) cmbLunchDuration.SelectedIndex = 2;
            else if (currentDuration == TimeSpan.FromMinutes(40)) cmbLunchDuration.SelectedIndex = 3;
            else if (currentDuration == TimeSpan.FromMinutes(50)) cmbLunchDuration.SelectedIndex = 4;
            else if (currentDuration == TimeSpan.FromMinutes(60)) cmbLunchDuration.SelectedIndex = 5;
            else cmbLunchDuration.SelectedIndex = 2;

            lblLunch.Visible = lunchTime;
            lblLunchDuration.Visible = lunchSession;

            timePickerLunch.Visible = lunchTime;
            cmbLunchDuration.Visible = lunchSession;
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            LunchTime = timePickerLunch.Value.TimeOfDay;

            switch (cmbLunchDuration.SelectedIndex)
            {
                case 0: LunchDuration = TimeSpan.FromMinutes(10); break;
                case 1: LunchDuration = TimeSpan.FromMinutes(20); break;
                case 2: LunchDuration = TimeSpan.FromMinutes(30); break;
                case 3: LunchDuration = TimeSpan.FromMinutes(40); break;
                case 4: LunchDuration = TimeSpan.FromMinutes(50); break;
                case 5: LunchDuration = TimeSpan.FromMinutes(60); break;
                default: LunchDuration = TimeSpan.FromMinutes(30); break;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
