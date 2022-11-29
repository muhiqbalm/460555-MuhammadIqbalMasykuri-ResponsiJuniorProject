using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ResponsiJunpro
{
    public class karyawan : departemen
    {
        private string _nama;
        private string _idkaryawan;
        private string _id_dep;

        public string Nama
        {
            get { return _nama; }
            set { _nama = value; }
        }

        public string Id_karyawan
        {
            get { return _idkaryawan; }
        }

        private NpgsqlConnection conn;
        string connstring = "Host=localhost; Port=2022; Username=postgres; Password=informatika; Database=responsi";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;

        /*public void add(string _nama, string _id_dep)
        {
            conn = new NpgsqlConnection(connstring);
            conn.Open();
            sql = @"select * from st_insert(:_nama,:_id_dep)";

            _nama = Nama; ;
            _country = Country;
            _loginusername = loginusername;

            cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("_city", _city);
            cmd.Parameters.AddWithValue("_country", _country);
            cmd.Parameters.AddWithValue("_owner", _loginusername);
            if ((int)cmd.ExecuteScalar() == 1)
            {
                MessageBox.Show("Lokasi berhasil ditambahkan");
            }
        }*/
    }
}
