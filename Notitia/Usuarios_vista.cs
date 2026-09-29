using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public partial class Usuarios_vista : Form
    {
        int clickop = 0;
        public Usuarios_vista()
        {
            InitializeComponent();
            MostrarSegunClasificacion(Sesion.IDClasificacionActual);
        }
        public void MostrarSegunClasificacion(string clasificacion)
        {
            // Hacer visible el botón solo si es Administrador (1) o Empleado (2)
            BtnVistade.Visible = clasificacion == "1" || clasificacion == "2";

            // Mostrar nombre de usuario en el label
            LblUsuario.Text = Sesion.NombreUsuarioActual;

            // Ocultar controles si la clasificación es "2"
            bool esEmpleado = clasificacion == "2";
            BtnEditar.Visible = !esEmpleado;
            BtnEliminar.Visible = !esEmpleado;
            BtnActivar.Visible = !esEmpleado;
            TxtId.Visible = !esEmpleado;
            label1.Visible = !esEmpleado;
            Txtcontrasena.Visible = !esEmpleado;
            label4.Visible = !esEmpleado;
            label5.Visible = !esEmpleado;
            Txtidclasificacion.Visible = !esEmpleado;
            TxtTarjeta.Visible = !esEmpleado;
            label6.Visible = !esEmpleado;
            if (esEmpleado)
            {
                BtnBuscar.Location = new Point(301, 589);
            }
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
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Usuarios_vista_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            UsuariosVista();
            DgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void UsuariosVista()
        {
            string query = "SELECT * FROM Usuarios";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        connection.Open();

                        using (var adapter = new DB2DataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Asignar la tabla como origen de datos del DataGridView
                            DgvUsuarios.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                Console.WriteLine($"Error de DB2: {ex.Message}");
                MessageBox.Show("Hubo un error al cargar las solicitudes de préstamo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                MessageBox.Show("Hubo un error al cargar las solicitudes de préstamo.");
            }
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

        private void Btncontra_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambiar_Contraseña().ShowDialog();
        }

        private void btnActualizacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambiar_datos().ShowDialog();
        }

        private void BtnResumen_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Resumen().ShowDialog(); ocultar();
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
            this.Invalidate();
            this.Update();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:" + DateTime.Now.ToString("hh:mm:ss");
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {

        }

        private void rjTextBox1__TextChanged_1(object sender, EventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string idUsuario = TxtId.Texts;
            if (string.IsNullOrEmpty(idUsuario))
            {
                MessageBox.Show("Por favor, ingrese un ID de usuario.");
                return;
            }

            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Deshabilitar el usuario
                string disableUserQuery = "UPDATE Usuarios SET Estado = 'Deshabilitado' WHERE IDUsuario = ?";
                using (var command = new DB2Command(disableUserQuery, connection))
                {
                    command.Parameters.Add(new DB2Parameter("IDUsuario", idUsuario));
                    command.ExecuteNonQuery();
                }

                // Deshabilitar las cuentas asociadas al usuario
                string disableAccountsQuery = "UPDATE Cuentas_Bancarias SET Estado = 'Deshabilitado' WHERE IDUsuario = ?";
                using (var command = new DB2Command(disableAccountsQuery, connection))
                {
                    command.Parameters.Add(new DB2Parameter("IDUsuario", idUsuario));
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Usuario y cuentas deshabilitados exitosamente.");
            }
        }
        private void Actualizar()
        {
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "SELECT * FROM Usuarios"; // Ajusta según lo que necesites
                using (var command = new DB2Command(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        DgvUsuarios.DataSource = dt; // Actualiza el DataGridView
                    }
                }
            }
        }
        private void TxtBuscar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = TxtNombreusuario.Texts; // Usar TxtNombreusuario para buscar

            // Código para buscar y cargar datos en DgvUsuarios
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "SELECT * FROM Usuarios WHERE Nombre_de_Usuario LIKE ?";
                using (var command = new DB2Command(query, connection))
                {
                    command.Parameters.Add(new DB2Parameter("NombreUsuario", "%" + nombreUsuario + "%"));
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        DgvUsuarios.DataSource = dt;
                    }
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            string idUsuario = TxtId.Texts;
            string nombreUsuario = TxtNombreusuario.Texts;
            string contrasena = Txtcontrasena.Texts;
            string idClasificacion = Txtidclasificacion.Texts;
            string tarjeta = TxtTarjeta.Texts;

            if (string.IsNullOrEmpty(idUsuario))
            {
                MessageBox.Show("Por favor, ingrese un ID de usuario.");
                return;
            }

            // Código para actualizar el registro en la base de datos, excluyendo el ID
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "UPDATE Usuarios SET Nombre_de_Usuario = ?, Contrasena = ?, IDClasificacion = ?, Codigo_tarjeta = ? WHERE IDUsuario = ?";
                using (var command = new DB2Command(query, connection))
                {
                    command.Parameters.Add(new DB2Parameter("NombreUsuario", nombreUsuario));
                    command.Parameters.Add(new DB2Parameter("Contrasena", contrasena));
                    command.Parameters.Add(new DB2Parameter("IDClasificacion", idClasificacion));
                    command.Parameters.Add(new DB2Parameter("CodigoTarjeta", tarjeta));
                    command.Parameters.Add(new DB2Parameter("IDUsuario", idUsuario));  // El ID se utiliza solo para buscar
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Usuario editado exitosamente.");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un usuario con ese ID.");
                    }
                }
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Banco_afiliado().ShowDialog();
        }

        private void TxtActivar_Click(object sender, EventArgs e)
        {
            string idUsuario = TxtId.Texts;
            if (string.IsNullOrEmpty(idUsuario))
            {
                MessageBox.Show("Por favor, ingrese un ID de usuario.");
                return;
            }

            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Deshabilitar el usuario
                string disableUserQuery = "UPDATE Usuarios SET Estado = 'Habilitado' WHERE IDUsuario = ?";
                using (var command = new DB2Command(disableUserQuery, connection))
                {
                    command.Parameters.Add(new DB2Parameter("IDUsuario", idUsuario));
                    command.ExecuteNonQuery();
                }

                // Deshabilitar las cuentas asociadas al usuario
                string disableAccountsQuery = "UPDATE Cuentas_Bancarias SET Estado = 'Habilitado' WHERE IDUsuario = ?";
                using (var command = new DB2Command(disableAccountsQuery, connection))
                {
                    command.Parameters.Add(new DB2Parameter("IDUsuario", idUsuario));
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Usuario y cuentas activadas exitosamente.");
            }
        }
    }
}

