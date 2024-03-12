using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Food_XYZ.Helper;

namespace Food_XYZ
{
    public partial class KelolaUser : Form
    {
        SqlConnection conn = ConnectionSql.GetConnection();
        private int id;
        string title = "Kelola User";


        public KelolaUser(int idUser)
        {
            InitializeComponent();
            id = idUser;
        }

        private void clear()
        {
            cmbTpUser.Text = "";
            tbNama.Text = "";
            tbTlpn.Text = "";
            tbAlmt.Text = "";
            tbUsrnme.Text = "";
            tbPswrd.Text = "";
        }


        private void loadUser()
        {
            Classes cls = new Classes();
            DataTable dt = cls.listUsers;
            dataGridView1.DataSource = dt;
        }

        private bool Val()
        {
            var strgs = new List<string>()
            {
                cmbTpUser.Text, 
                tbNama.Text, 
                tbTlpn.Text,
                tbAlmt.Text,
                tbUsrnme.Text,
                tbPswrd.Text
            };

            var strVal = Validation.ValidationStirng(strgs);
            if (!strVal)
            {
                Snipetmbox.Error(title, "Must fill all field");
                return false;
            }
            return true;
        }


        private void KelolaUser_Load(object sender, EventArgs e)
        {
            cmbTpUser.Items.Add("kasir");
            cmbTpUser.Items.Add("gudang");
            loadUser();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Classes cls = new Classes();
            cls.idUser = id;
            cls.statusLog = "Logout";
            cls.insLogin();
            Snipetmbox.Information(title, "Succses Logout!!");
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ConnectionSql.OpenConnection();
            var valid = Val();
            if (!valid) return;
            Action action = new Action();
            int id = Convert.ToInt32(action.loadUpdate[6]);


            DialogResult result = MessageBox.Show("apakah kamu ingin update ?", title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
            SqlCommand cmd = new SqlCommand("Update tbl_user set tipe_user = @tpUsr, nama = @nm, alamat = @almt, telpon = @tlp, username = @usrnm, password = @pswrd where id_user = @id", conn);
            cmd.Parameters.AddWithValue("@tpUsr", VarInput.tipeUser);
            cmd.Parameters.AddWithValue("@nm", VarInput.nama);
            cmd.Parameters.AddWithValue("@tlp", VarInput.alamat);
            cmd.Parameters.AddWithValue("@almt", VarInput.telepon);
            cmd.Parameters.AddWithValue("@usrnm", VarInput.username);
            cmd.Parameters.AddWithValue("@pswrd", VarInput.password);
                cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            loadUser();
            clear();
            }
            else
            {
                clear();
            }
        }

        private void btnTmbh_Click(object sender, EventArgs e)
        {

            var valid = Val();
            if (!valid) return;
            DialogResult result = MessageBox.Show("Are you ure want to add?", title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Action input = new Action();
                input.addUser(VarInput.nama);
                Snipetmbox.Information(title, "Succsess Input");
                loadUser();
            }
            else
            {
                clear();
            }

        }

        private void cmbTpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            VarInput.tipeUser = cmbTpUser.Text;
        }

        private void tbNama_TextChanged(object sender, EventArgs e)
        {
            VarInput.nama = tbNama.Text;
        }

        private void tbTlpn_TextChanged(object sender, EventArgs e)
        {
            VarInput.telepon = tbTlpn.Text;
        }

        private void tbAlmt_TextChanged(object sender, EventArgs e)
        {
            VarInput.alamat = tbAlmt.Text;
        }

        private void tbUsrnme_TextChanged(object sender, EventArgs e)
        {
            VarInput.username = tbUsrnme.Text;
        }

        private void tbPswrd_TextChanged(object sender, EventArgs e)
        {
            VarInput.password = tbPswrd.Text;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell.Selected = true;

            int idUser = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id_user"].Value);
            Action.idSelected = idUser;
            Action action = new Action();
            object[] valueFilds = action.loadUpdate;  

            cmbTpUser.Text = Convert.ToString(valueFilds[0]);
            tbNama.Text = Convert.ToString(valueFilds[1]);
            tbTlpn.Text = Convert.ToString(valueFilds[2]);
            tbAlmt.Text = Convert.ToString(valueFilds[3]);
            tbUsrnme.Text = Convert.ToString(valueFilds[4]);
            tbPswrd.Text = Convert.ToString(valueFilds[5]); 
        }

        private void Search(string dataToFind)
        {
            ConnectionSql.OpenConnection();
            


            SqlDataAdapter sda = new SqlDataAdapter("select id_user, tipe_user, nama, alamat, telpon from tbl_user where concat(id_user, tipe_user, nama, alamat, telpon) like '%'+ @srch +'%'", conn); 
            sda.SelectCommand.Parameters.AddWithValue("@srch", dataToFind);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void tbCari_TextChanged(object sender, EventArgs e)
        {
            Search(tbCari.Text);
        }
    }
}
