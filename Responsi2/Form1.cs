using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace Responsi2
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        string connstring="Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=responsi2";
        public DataTable dt;
        public static NpgsqlCommand cmd = null;
        private string sql = null;
        private DataGridViewRow r;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            LoadData();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            try
            {
                conn.Open();
                dgvEmp.DataSource = null;
                sql = @"select * from emp_selectjoin()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvEmp.DataSource = dt;
                conn.Close();
            }
            catch  (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "FAIL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if ( r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang ingin diubah", "Info", MessageBoxButtons.OK);
                return;
            }
            try
            {
                conn.Open();
                sql = @"select emp_insert(:_name, :_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_name", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", tbDept.Text);
                if ((bool)cmd.ExecuteScalar() == true)
                {
                    MessageBox.Show("Data karyawan telah diupdate", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "FAIL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if( r == null)
            {
                MessageBox.Show("Klik pada baris yang akan dihapus");
                return;
            }
            /*if (MessageBox.Show("Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }*/
            try
            {
                conn.Open();
                sql = @"select emp_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", Employee.clicked.id);
                if((bool)cmd.ExecuteScalar() == true)
                {
                    MessageBox.Show("Data karyawan telah dihapus", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "FAIL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void dgvEmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dgvEmp.Rows[e.RowIndex];
                tbNama.Text = r.Cells["_nama"].Value.ToString();
                tbDept.Text = r.Cells["_nama_dep"].Value.ToString();
                //getEmp(tbNama.Text, tbDept.Text);
            }
        }

        private void getEmp(string nama, int id_Dep)
        {
            // harusnya ini nge set dari yg di klik ke interface clickedEmp
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Klik pada baris yang akan diubah");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select emp_update(:_id, :_name, :_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", Employee.clicked.id);
                cmd.Parameters.AddWithValue("_name", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", tbDept.Text);
                if ((bool)cmd.ExecuteScalar() == true)
                {
                    MessageBox.Show("Data karyawan telah diubah", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "FAIL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }
    }
}