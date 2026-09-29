using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Notitia
{
    public partial class Login : Form
    {
        private SerialPort serialPort;
        private bool showpassword = false;
        int fallos = 0;
        private Timer timer;
        public Login()
        {
            InitializeComponent();
            ConfigurarPuertoSerial();
            timer = new Timer();
            timer.Interval = 30000; // 30 segundos
            timer.Tick += Timer_Tick;
            // Establecer el layout inicial para el PictureBox
            guna2PictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void ConfigurarPuertoSerial()
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close(); // Cierra el puerto si está abierto
                }

                serialPort = new SerialPort("COM8", 9600); // Asegúrate de que COM6 es el puerto correcto
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort.Open();
            }
            catch (UnauthorizedAccessException)
            {
                // Maneja la excepción según sea necesario, pero sin mostrar mensajes
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones según sea necesario
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine();
                if (data.StartsWith("UID:"))
                {
                    string uid = data.Substring(4).Trim();
                    this.Invoke(new Action(() => VerificarUID(uid)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer datos del puerto: " + ex.Message);
            }
        }

        private void VerificarUID(string uid)
        {
            string idUsuario = null;
            string idClasificacion = null;
            string nombreUsuario = null;

            string query = "SELECT IDUsuario, IDClasificacion, Nombre_de_Usuario FROM Usuarios WHERE Codigo_tarjeta = @uid";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@uid", uid);
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idUsuario = reader["IDUsuario"].ToString();
                                idClasificacion = reader["IDClasificacion"].ToString();
                                nombreUsuario = reader["Nombre_de_Usuario"].ToString();
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(idUsuario) && !string.IsNullOrEmpty(idClasificacion))
                {
                    Sesion.UsuarioActual = idUsuario;
                    Sesion.IDUsuarioActual = idUsuario;
                    Sesion.NombreUsuarioActual = nombreUsuario;
                    Sesion.IDClasificacionActual = idClasificacion;

                    Resumen formularioResumen = new Resumen();
                    formularioResumen.MostrarSegunClasificacion(idClasificacion);
                    this.Hide();
                    formularioResumen.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("Código de tarjeta no válido.");
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            rjButton1.Enabled = true; // Habilitar el botón después de 30 segundos
            MessageBox.Show("Puedes intentar nuevamente.");
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Tips().ShowDialog();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            string usuario = TxtUsuario.Texts.Trim();
            string contraseña = TxtContrasena.Texts.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, completa ambos campos.");
                return;
            }

            var (idUsuario, idClasificacion, nombreUsuario) = VerificarCredenciales(usuario, contraseña);

            if (!string.IsNullOrEmpty(idClasificacion) && !string.IsNullOrEmpty(idUsuario))
            {
                Sesion.UsuarioActual = idUsuario;
                Sesion.IDUsuarioActual = idUsuario;
                Sesion.NombreUsuarioActual = nombreUsuario;
                Sesion.IDClasificacionActual = idClasificacion;

                Resumen formularioResumen = new Resumen();
                formularioResumen.MostrarSegunClasificacion(idClasificacion);
                this.Hide();
                formularioResumen.ShowDialog();
    
            }
            else
            {
                fallos++;
                MessageBox.Show("Usuario o contraseña incorrectos.");
                if (fallos >= 3)
                {
                    rjButton1.Enabled = false;
                    timer.Start();
                }
            }
        }

        private (string IDUsuario, string IDClasificacion, string nombreUsuario) VerificarCredenciales(string usuario, string contrasena)
        {
            string idUsuario = null;
            string idClasificacion = null;
            string nombreUsuario = null;
            string estado = null;

            string query = "SELECT IDusuario, IDClasificacion, Nombre_de_Usuario, Estado FROM Usuarios WHERE Nombre_de_Usuario = @usuario AND Contrasena = @contraseña";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@usuario", usuario);
                        command.Parameters.Add("@contraseña", contrasena);

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idUsuario = reader["IDusuario"].ToString();
                                idClasificacion = reader["IDClasificacion"].ToString();
                                nombreUsuario = reader["Nombre_de_Usuario"].ToString();
                                estado = reader["Estado"].ToString();

                                // Verificar si el usuario está habilitado
                                if (estado == "Deshabilitado")
                                {
                                    MessageBox.Show("Tu cuenta ha sido deshabilitada. Contacta al administrador.");
                                    return (null, null, null);
                                }
                            }
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return (idUsuario, idClasificacion, nombreUsuario);
        }




        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Registro().ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void rjTextBox1_Enter(object sender, EventArgs e)
        {
            if (TxtUsuario.Texts == "Ingrese su usuario")
            {
                TxtUsuario.Texts = "";
                TxtUsuario.ForeColor = Color.Black;
            } 
        }

        private void rjTextBox1_Leave(object sender, EventArgs e)
        {
            if (TxtUsuario.Texts == "")
            {
                TxtUsuario.Texts = "Ingrese su usuario";
                TxtUsuario.ForeColor = Color.Silver;
            }
        }

        private void rjTextBox2_Enter(object sender, EventArgs e)
        {
            if (TxtContrasena.Texts == "Ingrese su contraseña")
            {
                TxtContrasena.Texts = "";
                TxtContrasena.ForeColor = Color.Black;
            }
        }

        private void rjTextBox2_Leave(object sender, EventArgs e)
        {
            if (TxtContrasena.Texts == "")
            {
                TxtContrasena.Texts = "Ingrese su contraseña";
                TxtContrasena.ForeColor = Color.Silver;
            }
        }

        private void rjButton1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void rjButton1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Rbtn1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Rbtn2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            new Recuperar_contraseña().ShowDialog();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            showpassword = !showpassword;
            if (showpassword)
            {
                TxtContrasena.PasswordChar = false;
                pictureBox3.BackgroundImage = Properties.Resources.mostrar_contrasena;
            }
            else if (!showpassword)
            {
                TxtContrasena.PasswordChar= true;
                pictureBox3.BackgroundImage = Properties.Resources.oculto;
            }

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Cerrar el puerto COM si está abierto
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                }

                // Reiniciar la aplicación con el argumento para omitir el formulario de carga
                Application.Exit();
                System.Diagnostics.Process.Start(Application.ExecutablePath, "--skipLoading");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reiniciar la aplicación: " + ex.Message);
            }
        }

    }
}
