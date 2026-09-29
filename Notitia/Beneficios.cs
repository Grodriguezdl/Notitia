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
    public partial class Beneficios : Form
    {
        public Beneficios()
        {
            InitializeComponent();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Extrafinanciamiento_en_efectivo().ShowDialog();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cero_riesgo().ShowDialog();
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

        private void rjButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Dividelo_todo().ShowDialog();
        }
    }
}
