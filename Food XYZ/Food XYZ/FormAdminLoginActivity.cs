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
    public partial class FormAdminLoginActivity : Form
    {
        private int id;
        string title = "Log Actifity";
        SqlConnection conn = ConnectionSql.GetConnection();
        public FormAdminLoginActivity(int idUser)
        {
            InitializeComponent();
            id = idUser;
        }
         

        private void FormAdminLoginActivity_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            Classes cls = new Classes();
            DataTable dt = cls.activity;
            dataGridView1.DataSource = dt;
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

        private void button1_Click(object sender, EventArgs e)
        {
            /*MessageBox.Show("jam" + dtpckr1);*/
            DateTime dtpckr1 = dateTimePicker1.Value;
            SqlDataAdapter sda = new SqlDataAdapter("select * from tbl_logg where cast(waktu as date) = @wktu", conn);
            sda.SelectCommand.Parameters.AddWithValue("@wktu", dtpckr1.Date);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnKelolaLaporan_Click(object sender, EventArgs e)
        {

        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {
            KelolaUser kelolaUser = new KelolaUser(id);
            kelolaUser.Show();
            this.Hide();
        }
    }
}
