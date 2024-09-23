using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace KoneksiDatabase
{
    public partial class FrmMain : Form
    {
        private readonly MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private readonly DataSet ds = new DataSet();
        private string alamat, query;

        public FrmMain()
        {
            alamat = "server=localhost; database=db_pomodoro; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = "select * from tbl_pengguna";
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].HeaderText = "ID Pengguna";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].HeaderText = "Username";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[2].HeaderText = "Password";
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[3].HeaderText = "Nama Pengguna";
                dataGridView1.Columns[4].Width = 120;
                dataGridView1.Columns[4].HeaderText = "Level";

                ClearForm();
                EnableButtons(false, false, true, false, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtNama.Clear();
            txtPassword.Clear();
            txtUsername.Clear();
            txtID.Focus();
        }

        private void EnableButtons(bool update, bool delete, bool save, bool clear, bool search)
        {
            btnUpdate.Enabled = update;
            btnDelete.Enabled = delete;
            btnSave.Enabled = save;
            btnClear.Enabled = clear;
            btnSearch.Enabled = search;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "")
                {
                    query = $"select * from tbl_pengguna where username = '{txtUsername.Text}'";
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txtID.Text = kolom["id_pengguna"].ToString();
                            txtPassword.Text = kolom["password"].ToString();
                            txtNama.Text = kolom["nama_pengguna"].ToString();
                            CBLevel.Text = kolom["level"].ToString();
                        }
                        txtUsername.Enabled = false;
                        dataGridView1.DataSource = ds.Tables[0];
                        EnableButtons(true, true, false, true, false);
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada !!");
                        FrmMain_Load(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && txtNama.Text != "")
                {
                    query = $"insert into tbl_pengguna values ('{txtID.Text}','{txtUsername.Text}','{txtPassword.Text}','{txtNama.Text}','{CBLevel.Text}')";
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Insert Data Sukses ...");
                        FrmMain_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal insert Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != "" && txtNama.Text != "" && txtUsername.Text != "" && txtID.Text != "")
                {
                    query = $"update tbl_pengguna set password = '{txtPassword.Text}', nama_pengguna = '{txtNama.Text}', level = '{CBLevel.Text}' where id_pengguna = '{txtID.Text}'";
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Update Data Sukses ...");
                        FrmMain_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal Update Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text != "")
                {
                    query = $"delete from tbl_pengguna where id_pengguna = '{txtID.Text}'";
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Delete Data Sukses ...");
                        FrmMain_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Gagal Hapus Data . . . ");
                    }
                }
                else
                {
                    MessageBox.Show("ID Tidak Ditemukan !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            EnableButtons(false, false, true, false, true);
        }
    }
}
