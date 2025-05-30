using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Diplom.UI
{
    public partial class BanReasonForm : Form
    {
        public int SelectedBanReasonId { get; private set; }
        public string AdminNote { get; private set; }

        public BanReasonForm(List<BanReasonItem> reasons)
        {
            InitializeComponent();
            comboBoxReasons.DataSource = reasons;
            comboBoxReasons.DisplayMember = "Reason";
            comboBoxReasons.ValueMember = "Id";
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SelectedBanReasonId = (int)comboBoxReasons.SelectedValue;
            AdminNote = txtAdminNote.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            BanCodesInfoForm form = new BanCodesInfoForm();
            form.ShowDialog();
        }
    }

    public class BanReasonItem
    {
        public int Id { get; set; }
        public string Reason { get; set; }

        public override string ToString() => Reason;
    }

}

