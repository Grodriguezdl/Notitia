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
    public partial class Cuentav2 : Form
    {
        public static int opcionrecibida { get; set; }
        string tipo;
        public Cuentav2()
        {
            InitializeComponent();
        }

        private void Cuentav2_Load(object sender, EventArgs e)
        {
            if (opcionrecibida == 1)
            {
                tipo = "Ahorrar";
            }
            if (opcionrecibida == 2)
            {
                tipo = "Depositar";
            }
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro de cancelar la solicitud?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                new Resumen().ShowDialog();
            }
        }

        private void TxtNombre__TextChanged(object sender, EventArgs e)
        {
            
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            
        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {
           
        }

        private void BtnCrearsolicitud_Click(object sender, EventArgs e)
        {
            string nombreCompleto = TxtNombreCompleto.Texts;
            string numeroTelefono = TxtCelular.Texts;
            string correoElectronico = TxtCorreoElectronico.Texts;
            string solicitudDe = tipo; // tipo contiene "Ahorrar" o "Depositar"
            string idUsuario = Sesion.IDUsuarioActual; // Asegúrate de tener este valor en la sesión

            // Validar que los campos no estén vacíos
            if (!string.IsNullOrEmpty(nombreCompleto) && !string.IsNullOrEmpty(numeroTelefono) && !string.IsNullOrEmpty(correoElectronico))
            {
                SolicitudManager solicitudManager = new SolicitudManager();
                solicitudManager.CrearSolicitud(idUsuario, nombreCompleto, numeroTelefono, correoElectronico, solicitudDe);
                MessageBox.Show("Su solicitud se ha enviado, será revisada por un administrador y se le notificará cuando se apruebe o rechace.", "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                new Resumen().ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Por favor, completa todos los campos.");
            }
            
        }
    }
}
