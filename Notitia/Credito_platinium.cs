using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public partial class Credito_platinium : Form
    {
        public Credito_platinium()
        {
            InitializeComponent();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Tarjetas_Credito().ShowDialog();
        }
    }
}
