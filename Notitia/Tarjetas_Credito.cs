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
    public partial class Tarjetas_Credito : Form
    {
        public Tarjetas_Credito()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Información().ShowDialog();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Credito_clasica().ShowDialog();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Credito_gold().ShowDialog();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Credito_platinium().ShowDialog();
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Credito_black().ShowDialog();
        }
    }
}
