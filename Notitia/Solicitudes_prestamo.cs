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
    public partial class Solicitudes_prestamo : Form
    {
        int clickop = 0;
        public Solicitudes_prestamo()
        {
            InitializeComponent();
            MostrarSegunClasificacion(Sesion.IDClasificacionActual);
           
        }
       

            public void MostrarSegunClasificacion(string clasificacion)
            {
                // Hacer visible el botón BtnVistade solo si es Administrador (1) o Empleado (2)
                BtnVistade.Visible = clasificacion == "1" || clasificacion == "2";

                // Ocultar BtnAprobar si la clasificación del usuario es "2"
                BtnAprobar.Visible = clasificacion != "2";

                // Mostrar nombre de usuario en el label
                LblUsuario.Text = Sesion.NombreUsuarioActual;
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
        private void Solicitudes_prestamo_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            CargarSolicitudesPrestamo();
            DgvSolicitudesprestamo.DefaultCellStyle.ForeColor = Color.Black;
            
        }


        private void CargarSolicitudesPrestamo()
        {
            string query = "SELECT * FROM SOLICITUDES_PRESTAMO";

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
                            DgvSolicitudesprestamo.DataSource = dataTable;
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

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnActualizacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambiar_datos().ShowDialog();
        }

        private void Btncontra_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambiar_Contraseña().ShowDialog();
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
            this.Invalidate();
            this.Update();
            ocultar();
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

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string idSolicitudPrestamo = TxtId.Texts; // Asegúrate de que TxtId es el campo correcto para la tabla Solicitudes_Prestamo
            if (string.IsNullOrEmpty(idSolicitudPrestamo))
            {
                MessageBox.Show("Por favor, ingrese un ID de solicitud de préstamo.");
                return;
            }

            // Código para eliminar el registro de la base de datos
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "DELETE FROM Solicitudes_Prestamo WHERE IDSolicitudPrestamo = @idSolicitudPrestamo";
                using (var command = new DB2Command(query, connection))
                {
                    command.Parameters.Add("@idSolicitudPrestamo", DB2Type.VarChar).Value = idSolicitudPrestamo;
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Solicitud de préstamo eliminada exitosamente.");
                        // Actualizar el DataGridView después de eliminar
                        ActualizarDgvSolicitudesprestamo();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una solicitud de préstamo con ese ID.");
                    }
                }
            }
        }
        private void ActualizarDgvSolicitudesprestamo()
        {
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "SELECT * FROM Solicitudes_Prestamo";
                using (var command = new DB2Command(query, connection))
                {
                    using (var adapter = new DB2DataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        DgvSolicitudesprestamo.DataSource = dataTable;
                    }
                }
            }
        }

        private void BtnAprobar_Click(object sender, EventArgs e)
        {
            string idSolicitudPrestamo = TxtId.Texts;
            if (string.IsNullOrEmpty(idSolicitudPrestamo))
            {
                MessageBox.Show("Por favor, ingrese un ID de solicitud de préstamo.");
                return;
            }

            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Obtener los datos de la solicitud de préstamo
                string querySelect = "SELECT * FROM Solicitudes_Prestamo WHERE IDSolicitudPrestamo = ?";
                SolicitudPrestamo solicitud = null;

                using (var commandSelect = new DB2Command(querySelect, connection))
                {
                    commandSelect.Parameters.Add(new DB2Parameter("IDSolicitudPrestamo", idSolicitudPrestamo));
                    using (var reader = commandSelect.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            solicitud = new SolicitudPrestamo
                            {
                                IDSolicitudPrestamo = reader["IDSolicitudPrestamo"].ToString(),
                                IDUsuario = reader["IDUsuario"].ToString(),
                                Nombre_Completo = reader["Nombre_Completo"].ToString(),
                                Correo_Electronico = reader["Correo_Electronico"].ToString(),
                                Numero_Telefonico = reader["Numero_Telefonico"].ToString(),
                                Fecha_de_Nacimiento = Convert.ToDateTime(reader["Fecha_de_Nacimiento"]),
                                DPI = reader["DPI"].ToString(),
                                NIT = reader["NIT"].ToString(),
                                Departamento = reader["Departamento"].ToString(),
                                Zona_de_Residencia = reader["Zona_de_Residencia"].ToString(),
                                Empresa_donde_labora = reader["Empresa_donde_labora"].ToString(),
                                Antiguedad_Laboral = reader["Antiguedad_Laboral"].ToString(),
                                Ingresos_Mensuales = Convert.ToDecimal(reader["Ingresos_Mensuales"]),
                                Monto_Deseado = Convert.ToDecimal(reader["Monto_Deseado"]),
                                Plazo_a_Pagar = Convert.ToInt32(reader["Plazo_a_Pagar"]),
                                Consolidar_Deudas = reader["Consolidar_Deudas"].ToString(),
                                Recibe_salario_de_nuestro_banco = reader["Recibe_salario_de_nuestro_banco"].ToString()
                            };
                        }
                    }
                }

                if (solicitud == null)
                {
                    MessageBox.Show("No se encontró una solicitud de préstamo con ese ID.");
                    return;
                }

                // Obtener el IDPrestamo más alto y sumar 1
                string queryMaxId = "SELECT MAX(IDPrestamo) FROM Prestamos";
                string newIdPrestamo = "1";

                using (var commandMaxId = new DB2Command(queryMaxId, connection))
                {
                    var result = commandMaxId.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        newIdPrestamo = (Convert.ToInt32(result) + 1).ToString();
                    }
                }

                // Insertar el nuevo registro en la tabla Prestamos
                string queryInsert = "INSERT INTO Prestamos (IDPrestamo, IDUsuario, Monto, Plazo) VALUES (?, ?, ?, ?)";

                using (var commandInsert = new DB2Command(queryInsert, connection))
                {
                    commandInsert.Parameters.Add(new DB2Parameter("IDPrestamo", newIdPrestamo));
                    commandInsert.Parameters.Add(new DB2Parameter("IDUsuario", solicitud.IDUsuario));
                    commandInsert.Parameters.Add(new DB2Parameter("Monto", solicitud.Monto_Deseado));
                    commandInsert.Parameters.Add(new DB2Parameter("Plazo", solicitud.Plazo_a_Pagar));

                    commandInsert.ExecuteNonQuery();
                }

                // Eliminar la solicitud de préstamo
                string queryDelete = "DELETE FROM Solicitudes_Prestamo WHERE IDSolicitudPrestamo = ?";

                using (var commandDelete = new DB2Command(queryDelete, connection))
                {
                    commandDelete.Parameters.Add(new DB2Parameter("IDSolicitudPrestamo", idSolicitudPrestamo));
                    commandDelete.ExecuteNonQuery();
                }

                MessageBox.Show("Préstamo aprobado y solicitud eliminada exitosamente.");
            }
        }

        private class SolicitudPrestamo
        {
            public string IDSolicitudPrestamo { get; set; }
            public string IDUsuario { get; set; }
            public string Nombre_Completo { get; set; }
            public string Correo_Electronico { get; set; }
            public string Numero_Telefonico { get; set; }
            public DateTime Fecha_de_Nacimiento { get; set; }
            public string DPI { get; set; }
            public string NIT { get; set; }
            public string Departamento { get; set; }
            public string Zona_de_Residencia { get; set; }
            public string Empresa_donde_labora { get; set; }
            public string Antiguedad_Laboral { get; set; }
            public decimal Ingresos_Mensuales { get; set; }
            public decimal Monto_Deseado { get; set; }
            public int Plazo_a_Pagar { get; set; }
            public string Consolidar_Deudas { get; set; }
            public string Recibe_salario_de_nuestro_banco { get; set; }
        }
    }
}
