using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom.Core.Setting
{
    internal static class ThemeManager
    {
        public static void ApplyTheme(
            Form form,
            string theme,
            ToolStripMenuItem menuOptions,
            Label lblTimer,
            Button btnStartWork,
            Button btnStopWork)
        {
            switch (theme)
            {
                case "light":
                    form.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Resources\light_theme_background.png");
                    form.BackgroundImageLayout = ImageLayout.Stretch;
                    form.BackColor = Color.WhiteSmoke;

                    menuOptions.BackColor = Color.Transparent;
                    menuOptions.ForeColor = Color.Black;

                    lblTimer.ForeColor = Color.Black;
                    lblTimer.BackColor = Color.Transparent;

                    btnStartWork.BackColor = Color.White;
                    btnStopWork.BackColor = Color.White;
                    break;

                case "system":
                default:
                    form.BackgroundImage = Properties.Resources.Back_main;
                    form.BackgroundImageLayout = ImageLayout.Stretch;
                    form.BackColor = SystemColors.Control;

                    menuOptions.BackColor = Color.Transparent;
                    menuOptions.ForeColor = Color.White;

                    lblTimer.ForeColor = Color.White;
                    lblTimer.BackColor = Color.Transparent;

                    btnStartWork.BackColor = Color.White;
                    btnStopWork.BackColor = Color.White;
                    break;
            }
        }
    }
}
