using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class MainForm : Form
    {
        private WorkTimer workTimer;

        public MainForm()
        {
            InitializeComponent();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            lblTimer.Location = new System.Drawing.Point((this.ClientSize.Width - lblTimer.Width) / 2, 150);
            btnStartWork.Location = new System.Drawing.Point((this.ClientSize.Width - btnStartWork.Width) / 2, 300);
            btnStopWork.Location = new System.Drawing.Point((this.ClientSize.Width - btnStopWork.Width) / 2, 370);
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeTimer();
        } 

        private void InitializeTimer()
        {
            workTimer = new WorkTimer(lblTimer); 
        }

        private void btnStartWork_Click(object sender, EventArgs e)
        {
            workTimer.Start(); 
        }

        private void btnStopWork_Click(object sender, EventArgs e)
        {
            workTimer.Stop(); 
        }

    }
}
