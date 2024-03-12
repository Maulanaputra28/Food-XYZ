using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_XYZ
{
    class Action
    {
        SqlConnection conn = ConnectionSql.GetConnection();

        public static int idSelected;
         public void addUser(string tes)
        {
            MessageBox.Show("output Varinput : "+tes);
        }

        public object[] loadUpdate
        {
            get
            {
                ConnectionSql.OpenConnection();
                int id = 0;
                string tp_usr = "";
                string nama = "";
                string tlpn = "";
                string almt = "";
                string usrnm = "";
                string pswrd = "";
                SqlCommand cmd = new SqlCommand("select * from tbl_user where id_user = @id", conn);
                cmd.Parameters.AddWithValue("@id", idSelected);

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        id = read.GetInt32(read.GetOrdinal("id_user"));
                        tp_usr = read["tipe_user"].ToString();
                        nama = read["nama"].ToString();
                        tlpn = read["telpon"].ToString();
                        almt = read["alamat"].ToString();
                        usrnm = read["username"].ToString();
                        pswrd = read["password"].ToString();
                    }
                    read.Close();
                }

                object[] valueFields = { tp_usr, nama, tlpn, almt, usrnm, pswrd, id };
                return valueFields;
            }
        }
    }
}

            /*SqlCommand cmd = new SqlCommand("insert into tbl_user(tipe_user, nama, telpon, alamat, username, password) values (@tpusr, @nm, @tlp, @almt, @usrnm, @pswrd)", conn);
            cmd.Parameters.AddWithValue("@tpusr", VarInput.tipeUser);
            cmd.Parameters.AddWithValue("@nm", VarInput.nama);
            cmd.Parameters.AddWithValue("@tlp", VarInput.telepon);
            cmd.Parameters.AddWithValue("@almt", VarInput.alamat);
            cmd.Parameters.AddWithValue("@usrnm", VarInput.username);
            cmd.Parameters.AddWithValue("@pswrd", VarInput.password);
            cmd.ExecuteNonQuery();*/