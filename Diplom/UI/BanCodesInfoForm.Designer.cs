namespace Diplom.UI
{
    partial class BanCodesInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewBanCodes;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewBanCodes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBanCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBanCodes
            // 
            this.dataGridViewBanCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBanCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBanCodes.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBanCodes.Name = "dataGridViewBanCodes";
            this.dataGridViewBanCodes.ReadOnly = true;
            this.dataGridViewBanCodes.RowHeadersWidth = 51;
            this.dataGridViewBanCodes.Size = new System.Drawing.Size(663, 300);
            this.dataGridViewBanCodes.TabIndex = 0;
            // 
            // BanCodesInfoForm
            // 
            this.ClientSize = new System.Drawing.Size(663, 300);
            this.Controls.Add(this.dataGridViewBanCodes);
            this.Name = "BanCodesInfoForm";
            this.Text = "Коды блокировок и их значения";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBanCodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}