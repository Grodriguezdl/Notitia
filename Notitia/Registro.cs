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
    public partial class Registro : Form
    {
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string DPI { get; set; }
        public string Direccion { get; set; }
        public string EstadoCivil { get; set; }
        public string Profesion { get; set; }
        public string Telefono { get; set; }
        public Registro()
        {
            InitializeComponent();
        }

        private void btnben_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro de cancelar el registro?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                new Login().ShowDialog();
            }
        }

        private void rjTextBox5__TextChanged(object sender, EventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtPrimerNombre.Texts) ||
                string.IsNullOrWhiteSpace(TxtPrimerapellido.Texts) ||
                string.IsNullOrWhiteSpace(TxtCorreoElectronico.Texts) ||
                string.IsNullOrWhiteSpace(TxtDpi.Texts) ||
                string.IsNullOrWhiteSpace(TxtDireccion.Texts) ||
                string.IsNullOrWhiteSpace(TxtEstadoCivil.Texts) ||
                string.IsNullOrWhiteSpace(TxtProfesionuoficio.Texts) ||
                string.IsNullOrWhiteSpace(TxtTelefono.Texts))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Registro2 registro2 = new Registro2
            {
                PrimerNombre = TxtPrimerNombre.Texts,
                SegundoNombre = TxtSegundonombre.Texts,
                PrimerApellido = TxtPrimerapellido.Texts,
                SegundoApellido = TxtSegundoApellido.Texts,
                CorreoElectronico = TxtCorreoElectronico.Texts,
                DPI = TxtDpi.Texts,
                Direccion = TxtDireccion.Texts,
                EstadoCivil = TxtEstadoCivil.Texts,
                Profesion = TxtProfesionuoficio.Texts,
                Telefono = TxtTelefono.Texts
            };

            this.Hide();
            registro2.ShowDialog();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            TxtPrimerNombre.Texts = PrimerNombre;
            TxtSegundonombre.Texts = SegundoNombre;
            TxtPrimerapellido.Texts = PrimerApellido;
            TxtSegundoApellido.Texts = SegundoApellido;
            TxtCorreoElectronico.Texts = CorreoElectronico;
            TxtDpi.Texts = DPI;
            TxtDireccion.Texts = Direccion;
            TxtEstadoCivil.Texts = EstadoCivil;
            TxtProfesionuoficio.Texts = Profesion;
            TxtTelefono.Texts = Telefono;
        }
    }
}
