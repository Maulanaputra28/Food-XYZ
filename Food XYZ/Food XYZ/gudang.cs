using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_XYZ
{
    public partial class gudang : Form
    {
        public gudang()
        {
            InitializeComponent();
        }

        private void gudang_Load(object sender, EventArgs e)
        {
            Classes cls = new Classes();
            DataTable dt = cls.barang;

            dataGridView1.DataSource = dt;
        }
    }
}
