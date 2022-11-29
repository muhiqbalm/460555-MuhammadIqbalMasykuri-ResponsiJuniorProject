using Npgsql;
using System.Data;
using System.Diagnostics.Metrics;

namespace ResponsiJunpro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        departemen Dep = new departemen();
        karyawan Karyawan = new karyawan();

        private NpgsqlConnection conn;
        string connstring = "Host=localhost; Port=2022; Username=postgres; Password=informatika; Database=responsi";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                sql = @"select * from st_insert(:_nama,:_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", cbDepartemen.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil ditambahkan");
                    tbNama.Text = cbDepartemen.Text = null;
                    dgvData.DataSource = null;
                    sql = "select * from karyawan";
                    cmd = new NpgsqlCommand(sql, conn);
                    dt = new DataTable();
                    NpgsqlDataReader rd = cmd.ExecuteReader();
                    dt.Load(rd);
                    dgvData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadData()
        {
            conn = new NpgsqlConnection(connstring);
            conn.Open();

            sql = @"select * from karyawan";
            cmd = new NpgsqlCommand(sql, conn);
            dt = new DataTable();
            NpgsqlDataReader rd = cmd.ExecuteReader();
            dt.Load(rd);
            dgvData.DataSource = dt;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan diupdate!", "Good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                sql = @"select * from st_update(:_id_karyawan,:_nama,:_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["id_karyawan"].Value);
                cmd.Parameters.AddWithValue("_nama", tbNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", cbDepartemen.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Users Berhasil diupdate!", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadData();
                    tbNama.Text = cbDepartemen.Text = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dgvData.Rows[e.RowIndex];
                tbNama.Text = r.Cells["nama"].Value.ToString();
                cbDepartemen.Text = r.Cells["id_dep"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan didelete!", "Good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Apakah benar anda ingin menghapus data " + r.Cells["nama"].Value.ToString() + " ?", "Hapus data terkonfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)

                try
                {
                    sql = @"select * from st_delete(:_id)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("id_karyawan", r.Cells["id_karyawan"].Value.ToString());
                    if ((int)cmd.ExecuteScalar() == 1)
                    {
                        MessageBox.Show("Data Users Berhasil dihapus!", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                        tbNama.Text = cbDepartemen.Text = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "DELETE FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
    }
}