using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_XYZ
{

    class Classes
    {
        SqlConnection conn = ConnectionSql.GetConnection();
        public string username { get; set; }

        public string password { get; set; }

        public int idUser;

        public string statusLog;
        public object[] dataUserLogin
        {
            get
            {
                ConnectionSql.OpenConnection();
                string nme = "";
                string pswrd = "";
                int id = 0;
                string tpUser = "";
                SqlCommand cmd = new SqlCommand("select * from tbl_user where username = @usrnme and password = @pswrd", conn);
                cmd.Parameters.AddWithValue("@usrnme", username);
                cmd.Parameters.AddWithValue("@pswrd", password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id_user"));
                        nme = reader["username"].ToString();
                        pswrd = reader["password"].ToString();
                        tpUser = reader["tipe_user"].ToString();
                    }
                    reader.Close();
                }
                 object[] dataUser = { id, nme, pswrd, tpUser};
                return dataUser;
            }
        }
        
        public DataTable barang
        {
            get
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from tbl_barang", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
        }

        public DataTable activity
        {
            get
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from tbl_logg", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
        }

        public DataTable listUsers
        {
            get
            {
                SqlDataAdapter sda = new SqlDataAdapter("select id_user, tipe_user, nama, alamat, telpon from tbl_user where tipe_user in('kasir','gudang')", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
            
        }

        public void insLogin()
        {
            DateTime date = DateTime.Now;
            SqlCommand cmd2 = new SqlCommand("insert into tbl_logg( aktivitas, id_user) values( @aktivitas, @id_user)", conn);
            cmd2.Parameters.AddWithValue("@aktivitas", statusLog);
            cmd2.Parameters.AddWithValue("@id_user", idUser);
            cmd2.ExecuteNonQuery();
        }
    }
}
