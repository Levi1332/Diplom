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
    public partial class LoginForm : Form
    {
        private Image originalImage;
        private Image newImage;

        public LoginForm()
        {
            InitializeComponent();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
            this.MaximizeBox = false;
            originalImage = Properties.Resources.eye_closed;
            newImage = Properties.Resources.eye_open;
            this.AcceptButton = btnLogin;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            CLogin cLogin = new CLogin();
            var result = cLogin.Login(login, password);

            if (!result.Success)
            {
                MessageBox.Show(result.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();
            MainForm mainForm = new MainForm(result.UserId, result.Role);
            mainForm.Show();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = newImage;
            textBoxPassword.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = originalImage;
            if (textBoxPassword.Text != "Введите пароль")
            {
                this.textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text!="Введите пароль")
            {
               this.textBoxPassword.UseSystemPasswordChar = true;
            }
          
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;  
            if (textBox != null)
            {
                if (textBox.Text == "Введите логин" || textBox.Text == "Введите пароль")
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.White;
                    if (textBox == textBoxPassword)
                    {
                        textBox.UseSystemPasswordChar = true;
                    }
                }
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;  
            if (textBox != null)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox == textBoxLogin)
                    {
                        textBox.Text = "Введите логин";
                    }
                    else if (textBox == textBoxPassword)
                    {
                        textBox.Text = "Введите пароль";
                        textBox.UseSystemPasswordChar = false;
                    }
                    textBox.ForeColor = Color.Gray;
                }
            }
        }
    }
}
