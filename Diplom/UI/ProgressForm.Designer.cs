using System.Drawing;
using System.Windows.Forms;

namespace Diplom.UI
{
    partial class ProgressForm
    {
        private System.ComponentModel.IContainer components = null;
        private ProgressBar progressBar;
        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.progressBar = new ProgressBar();
            this.lblStatus = new Label();

            // ProgressForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 90);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Создание отчета...";
            this.BackColor = Color.White;

            // lblStatus
            this.lblStatus.Text = "Пожалуйста, подождите. Экспорт выполняется...";
            this.lblStatus.Font = new Font("Segoe UI", 10F);
            this.lblStatus.AutoSize = false;
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStatus.Dock = DockStyle.Top;
            this.lblStatus.Height = 40;
            this.lblStatus.Padding = new Padding(0, 10, 0, 0);

            // progressBar
            this.progressBar.Dock = DockStyle.Bottom;
            this.progressBar.Height = 25;
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.Value = 0;
            this.progressBar.ForeColor = Color.FromArgb(76, 175, 80);

            // Add controls
            this.Controls.Add(lblStatus);
            this.Controls.Add(progressBar);
        }
    }
}
