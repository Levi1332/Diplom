using System.Windows.Forms;

namespace Diplom
{
    partial class WorkTimeSettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblLunch;
        private DateTimePicker timePickerLunch;
        private Button btnSave;

        private Label lblLunchDuration;
        private ComboBox cmbLunchDuration;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLunch = new Label();
            this.timePickerLunch = new DateTimePicker();
            this.lblLunchDuration = new Label();
            this.cmbLunchDuration = new ComboBox();
            this.btnSave = new Button();
            this.SuspendLayout();
            // 
            // lblLunch
            // 
            this.lblLunch.Location = new System.Drawing.Point(12, 20);
            this.lblLunch.Name = "lblLunch";
            this.lblLunch.Size = new System.Drawing.Size(130, 23);
            this.lblLunch.Text = "Время начала обеда:";
            // 
            // timePickerLunch
            // 
            this.timePickerLunch.Format = DateTimePickerFormat.Time;
            this.timePickerLunch.Location = new System.Drawing.Point(170, 20);
            this.timePickerLunch.Name = "timePickerLunch";
            this.timePickerLunch.ShowUpDown = true;
            this.timePickerLunch.Size = new System.Drawing.Size(120, 22);
            // 
            // lblLunchDuration
            // 
            this.lblLunchDuration.Location = new System.Drawing.Point(12, 60);
            this.lblLunchDuration.Name = "lblLunchDuration";
            this.lblLunchDuration.Size = new System.Drawing.Size(130, 23);
            this.lblLunchDuration.Text = "Длительность:";
            // 
            // cmbLunchDuration
            // 
            this.cmbLunchDuration.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLunchDuration.Location = new System.Drawing.Point(170, 60);
            this.cmbLunchDuration.Name = "cmbLunchDuration";
            this.cmbLunchDuration.Size = new System.Drawing.Size(120, 24);
            this.cmbLunchDuration.Items.AddRange(new object[] {
                "30 минут", "45 минут", "1 час"
            });
            this.cmbLunchDuration.SelectedIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(90, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // WorkTimeSettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(320, 160);
            this.Controls.Add(this.lblLunch);
            this.Controls.Add(this.timePickerLunch);
            this.Controls.Add(this.lblLunchDuration);
            this.Controls.Add(this.cmbLunchDuration);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkTimeSettingsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Настройки обеда";
            this.ResumeLayout(false);
        }
    }
}
