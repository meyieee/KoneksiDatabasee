using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace KoneksiDatabase
{
    private MySqlConnection koneksi;
    private MySqlDataAdapter adapter;
    private MySqlCommand perintah;
    private DataSet ds = new DataSet();
    private string alamat, query;
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            alamat = "server=localhost; database=db_pomodoro; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load_1(object sender, EventArgs e)
        {

        }
    }
}
