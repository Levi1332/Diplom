using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Diplom.Security;

namespace Diplom.UI
{
    public partial class BanCodesInfoForm : Form
    {
        private readonly string _connectionString = DatabaseConfig.connectionString;

        public BanCodesInfoForm()
        {
            InitializeComponent();
            LoadBanCodes();
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");
        }

        private void LoadBanCodes()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT BanCode AS [Код], Description AS [Описание] FROM BanCodes";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridViewBanCodes.DataSource = table;

                dataGridViewBanCodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dataGridViewBanCodes.Columns[0].Width = 150;  
                dataGridViewBanCodes.Columns[1].Width = 380;  
            }
        }

    }
}
