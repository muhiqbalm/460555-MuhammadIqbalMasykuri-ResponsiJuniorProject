using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace ResponsiJunpro
{
    public class departemen
    {
        protected int _id_dep;
        protected string _nama_dep;

        public int id_dep
        {
            get { return _id_dep; }
            set { _id_dep = value; }
        }

        public string nama_dep
        {
            get { return _nama_dep; }
            set { _nama_dep = value; }
        }

        public NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=responsi";
        public static NpgsqlCommand cmd;
        public string sql = null;

        public void getNamaDepartemen()
        {
            conn = new NpgsqlConnection(connstring);
            conn.Open();
            sql = @"select nama_dep from departemen";
            cmd = new NpgsqlCommand(sql, conn);
        }
    }
}
