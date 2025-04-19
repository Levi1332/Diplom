using System;
using System.Windows.Forms;

namespace Diplom.UI
{
    public partial class PdfTemplateForm : Form
    {
        public string Template { get; private set; }

        public PdfTemplateForm(string currentTemplate)
        {
            InitializeComponent();
            txtTemplate.Text = currentTemplate;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string input = txtTemplate.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Введите шаблон имени файла.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Template = input;
            DialogResult = DialogResult.OK;
            Close();
        }

      
        
    }
}
