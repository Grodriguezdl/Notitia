using RJCodeAdvance.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Notitia
{
    public partial class Carga : Form
    {
        public Carga()
        {
            InitializeComponent();
            if (Environment.GetCommandLineArgs().Contains("--skipLoading"))
            {
                // Salta el formulario de carga y muestra el formulario de inicio de sesión
                this.Hide();
                Login formularioLogin = new Login();
                formularioLogin.Show();
            }
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Carga_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2CircleProgressBar1.Value < guna2CircleProgressBar1.Maximum)
            {

                guna2CircleProgressBar1.Value++;
            }
            else
            {
                timer1.Stop();
                this.Hide();
                new Login().ShowDialog();
            }
        }
    }
    }
