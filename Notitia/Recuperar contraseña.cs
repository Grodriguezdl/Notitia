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
    public partial class Recuperar_contraseña : Form
    {
        public Recuperar_contraseña()
        {
            InitializeComponent();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            var user = new NRecuperarPassword();
            var result = user.recoveryPassword(TxtUser.Texts);
            lblResult.Text = result;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
        }
    }
}
