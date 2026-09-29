using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{

    public partial class Cambiar_Contraseña : Form
    {
        int clickop = 0;
        private bool click = false;
        private string generatedCode;
        private bool codigoEnviado = false;
        public Cambiar_Contraseña()
        {
            InitializeComponent();
            MostrarSegunClasificacion(Sesion.IDClasificacionActual);

        }
        public void MostrarSegunClasificacion(string clasificacion)
        {

            // Hacer visible el botón solo si es Administrador (1) o Empleado (2)
            BtnVistade.Visible = clasificacion == "1" || clasificacion == "2";
            LblUsuario.Text = Sesion.NombreUsuarioActual; // Mostrar nombre de usuario en el label
        }


        private void ocultar()
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
        }
        private void visible()
        {
            if (Pnltransferir.Visible == true)
                Pnltransferir.Visible = false;
            if (Pnlsolicituddeproductos.Visible == true)
                Pnlsolicituddeproductos.Visible = false;
            if (Pnladminempleado.Visible == true)
                Pnladminempleado.Visible = false;
        }

        private void mostrar(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                visible();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private void btncont_Click(object sender, EventArgs e)
        {
            string contrasenaActual = TxtConact.Texts.Trim();
            if (!ValidarContrasenaActual(contrasenaActual))
            {
                MessageBox.Show("La contraseña actual es incorrecta.");
                return;
            }

            string nuevaContrasena = TxtNuevacont.Texts.Trim();
            string confirmacionContrasena = Txtconfnewcont.Texts.Trim();

            if (nuevaContrasena != confirmacionContrasena)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            if (!ValidarRequisitosContrasena(nuevaContrasena))
            {
                MessageBox.Show("La nueva contraseña no cumple con los requisitos.");
                return;
            }

            CambiarContrasena(nuevaContrasena);
        }

        private bool ValidarContrasenaActual(string contrasena)
        {
            string connectionString = Conexion.conexion;
            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT Contrasena FROM Usuarios WHERE IDUsuario = @IDUsuario";
                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = Sesion.IDUsuarioActual;

                try
                {
                    connection.Open();
                    string contrasenaActualBD = command.ExecuteScalar()?.ToString();
                    // Aquí se debe comparar usando un método de hash, por ejemplo, BCrypt
                    return contrasenaActualBD == contrasena; // Reemplaza con lógica de hash
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la contraseña: " + ex.Message);
                    return false;
                }
            }
        }

        private bool ValidarRequisitosContrasena(string contrasena)
        {
            if (contrasena.Length < 8)
                return false;
            if (!contrasena.Any(char.IsUpper))
                return false;
            if (!contrasena.Any(char.IsLower))
                return false;
            if (!contrasena.Any(char.IsDigit))
                return false;
            if (!contrasena.Any(ch => "!@#$%^&*()_+[]{}|;:,.<>?".Contains(ch)))
                return false;
            return true;
        }

        private void VerificarContraseñaActual()
        {
            string connectionString = Conexion.conexion;
            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT Contrasena FROM Usuarios WHERE IDUsuario = @IDUsuario";
                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = Sesion.IDUsuarioActual;

                try
                {
                    connection.Open();
                    string contrasenaActualBD = command.ExecuteScalar()?.ToString();

                    // Aquí se debe usar el método de hash para comparar
                    if (contrasenaActualBD == TxtConact.Texts) // Asegúrate de hashear la contraseña ingresada
                    {
                        string nuevaContrasena = TxtNuevacont.Texts;
                        string confirmacionContrasena = Txtconfnewcont.Texts;

                        if (nuevaContrasena != confirmacionContrasena)
                        {
                            MessageBox.Show("La nueva contraseña y la confirmación no coinciden.");
                            return;
                        }

                        if (!ValidarRequisitosContrasena(nuevaContrasena))
                        {
                            MessageBox.Show("La nueva contraseña no cumple con los requisitos.");
                            return;
                        }

                        CambiarContrasena(nuevaContrasena); // Llama a CambiarContrasena con la nueva contraseña
                    }
                    else
                    {
                        MessageBox.Show("La contraseña actual es incorrecta.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar la contraseña: " + ex.Message);
                }
            }
        }

        private void CambiarContrasena(string nuevaContrasena)
        {
            string connectionString = Conexion.conexion;
            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "UPDATE Usuarios SET Contrasena = @NuevaContrasena WHERE IDUsuario = @IDUsuario";
                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@NuevaContrasena", DB2Type.VarChar).Value = nuevaContrasena; // Asegúrate de hashear la contraseña aquí
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = Sesion.IDUsuarioActual;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Contraseña cambiada exitosamente.");
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cambiar la contraseña: " + ex.Message);
                }
            }
        }

        private void LimpiarCampos()
        {
            
            TxtConact.Texts = "";
            Txtconfnewcont.Texts = "";
            TxtNuevacont.Texts = "";
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
           
           
                DialogResult result = MessageBox.Show("¿Esta seguro que desea salir?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Hide();
                    new Resumen().ShowDialog();
                }
  
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void BtnResumen_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Resumen().ShowDialog(); ocultar();
        }

        private void Cambiar_Contraseña_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string url = "https://docs.google.com/forms/d/e/1FAIpQLSd8v1Il3EAmvqdOk7wKimSV3EiSvkU8S_6DFFhBmYWyPJHNcw/viewform?vc=0&c=0&w=1&flr=0&pli=1";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void rjCircularPictureBox1_Click(object sender, EventArgs e)
        {
            clickop++;
            if (clickop % 2 == 0)
            {
                PnlOpcionusuario.Visible = false;
            }
            else
            {
                PnlOpcionusuario.Visible = true;
            }
        }

        private void LblUsuario_Click(object sender, EventArgs e)
        {
            clickop++;
            if (clickop % 2 == 0)
            {
                PnlOpcionusuario.Visible = false;
            }
            else
            {
                PnlOpcionusuario.Visible = true;
            }
        }

        private void btnActualizacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambiar_datos().ShowDialog();
        }

        private void Btncontra_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
        }

        private void BtnTransferir_Click(object sender, EventArgs e)
        {
            mostrar(Pnltransferir);
        }

        private void Btnotrascuentas_Click(object sender, EventArgs e)
        {
            this.Hide();
            new A_otras_cuentas().ShowDialog(); ocultar();
        }

        private void BtnHaciaotropais_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnHaciaotrosbancos_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Hacia_otros_bancos().ShowDialog(); ocultar();
        }

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Información().ShowDialog(); ocultar();
        }

        private void BtnSolicituddeproductos_Click(object sender, EventArgs e)
        {
            mostrar(Pnlsolicituddeproductos);
        }

        private void BtnCuenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cuenta().ShowDialog(); ocultar();
        }

        private void BtnPrestamo_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_Prestamo().ShowDialog(); ocultar();
        }

        private void BtnDivisa_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambio_de_divisa().ShowDialog(); ocultar();
        }

        private void BtnTransacciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Historial_transacciones().ShowDialog(); ocultar();
        }

        private void BtnVistade_Click(object sender, EventArgs e)
        {
            mostrar(Pnladminempleado);
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Usuarios_vista().ShowDialog(); ocultar();
        }

        private void BtnSolprod_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_de_productos().ShowDialog(); ocultar();
        }

        private void BtnPrestamos_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitudes_prestamo().ShowDialog(); ocultar();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog(); ocultar();
        }

        private void Pictureboxcontraseña_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:" + DateTime.Now.ToString("hh:mm:ss");
        }

        private void TxtConact__TextChanged(object sender, EventArgs e)
        {

        }
    }
}
