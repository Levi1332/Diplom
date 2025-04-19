using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom.UI
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }

        public void SetProgress(int value)
        {
            progressBar.Value = Math.Min(Math.Max(value, 0), 100);
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
        }
    }
}
