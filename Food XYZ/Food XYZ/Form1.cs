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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Food_XYZ
{
    public partial class Form1 : Form
    {
        SqlConnection conn = ConnectionSql.GetConnection();
        string title = "Form Login";
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void reset()
        {
            tbPassword.Text = "";
            tbUsername.Text = "";

        }

        private void login()
        {
            Classes cls = new Classes();
            cls.username = tbUsername.Text;
            cls.password = tbPassword.Text;

            object[] dataUsers = cls.dataUserLogin;

            int id = Convert.ToInt32(dataUsers[0]);
            string usrnme = Convert.ToString(dataUsers[1]);
            string pswrd = Convert.ToString(dataUsers[2]);
            string tpUsers = Convert.ToString(dataUsers[3]);

            cls.idUser = Convert.ToInt32(dataUsers[0]);

            if (tbUsername.Text == "" && tbPassword.Text == "")
            {
                Snipetmbox.Warning(title, "semua harus terisi");
            } 
            else if (tbUsername.Text == usrnme && tbPassword.Text == pswrd)
            {
                if (tpUsers == "admin")
                {
                    cls.statusLog = "login";
                    cls.insLogin();
                    Snipetmbox.Information(title, "Sucsess Login!!");
                    FormAdminLoginActivity admin = new FormAdminLoginActivity(id);
                    admin.Show();
                    this.Hide();
                }
                else if (tpUsers == "kasir")
                {
                    Snipetmbox.Information(title, "Kasir");
                }
            }
            else
            {
                Snipetmbox.Warning(title, "username atau password yang anda masukkan tidak sesuai");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
