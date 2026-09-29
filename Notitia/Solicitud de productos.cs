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
    public partial class Solicitud_de_productos : Form
    {
        int clickop = 0;
        public Solicitud_de_productos()
        {
            InitializeComponent();
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
        private void Solicitud_de_productos_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            CargarSolicitudesProductos();
            DgvSolprod.DefaultCellStyle.ForeColor = Color.Black;
            LblUsuario.Text = Sesion.NombreUsuarioActual;
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

        private void BtnSolprod_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
            ocultar();
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

        private void BtnSolicituddeproductos_Click(object sender, EventArgs e)
        {
            mostrar(Pnlsolicituddeproductos);
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
        private void CargarSolicitudesProductos()
        {
            string query = "SELECT * FROM Solicitud_Productos_Cuenta";

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
                            DgvSolprod.DataSource = dataTable;
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

        private void ActualizarDgvSolprod()
        {
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "SELECT * FROM Solicitud_Productos_Cuenta"; // Ajusta según lo que necesites
                using (var command = new DB2Command(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        DgvSolprod.DataSource = dt; // Actualiza el DataGridView
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string idSolCuenta = TxtId.Texts;
            if (string.IsNullOrEmpty(idSolCuenta))
            {
                MessageBox.Show("Por favor, ingrese un ID de Solicitud de Cuenta.");
                return;
            }


            // Código para eliminar el registro de la base de datos
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "DELETE FROM Solicitud_Productos_Cuenta WHERE IDSolCuenta = ?";
                using (var command = new DB2Command(query, connection))
                {
                    command.Parameters.Add(new DB2Parameter("IDSolCuenta", idSolCuenta));
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Solicitud eliminada exitosamente.");
                        ActualizarDgvSolprod(); // Llama a método para actualizar el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una solicitud con ese ID.");
                    }
                }
            }
        }
    }
}
