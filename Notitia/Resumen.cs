using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBM.Data.DB2;

namespace Notitia
{   
   
    public partial class Resumen : Form
    {
        int clickbtn1 = 0;
        int clickbtn2 = 0;
        int clickop = 0;
        private string usuarioActual;
        public Resumen()
        {
            InitializeComponent();
            MostrarSegunClasificacion(Sesion.IDClasificacionActual);
        }
        private void CargarDatos()
        {
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Crear DataTable combinado
                var dtCombinado = new DataTable();
                dtCombinado.Columns.Add("ID");
                dtCombinado.Columns.Add("Tipo");
                dtCombinado.Columns.Add("Descripcion");
                dtCombinado.Columns.Add("Monto", typeof(decimal));

                decimal balanceTotal = 0;

                // Cargar cuentas bancarias
                string queryCuentas = "SELECT IDCuenta, Nombre_de_Cuenta, Numero_de_Cuenta, Saldo FROM Cuentas_Bancarias WHERE IDUsuario = @usuario";
                using (var commandCuentas = new DB2Command(queryCuentas, connection))
                {
                    commandCuentas.Parameters.Add(new DB2Parameter("@usuario", Sesion.UsuarioActual));

                    using (var reader = commandCuentas.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string idCuenta = reader.GetString(0);
                            string descripcion = $"{reader.GetString(1)} ({reader.GetString(2)})";
                            decimal saldo = reader.GetDecimal(3);
                            balanceTotal += saldo;

                            dtCombinado.Rows.Add(idCuenta, "Cuenta Bancaria", descripcion, saldo);
                        }
                    }
                }

                // Cargar préstamos
                string queryPrestamos = "SELECT IDPrestamo, Monto FROM Prestamos WHERE IDUsuario = @usuario";
                using (var commandPrestamos = new DB2Command(queryPrestamos, connection))
                {
                    commandPrestamos.Parameters.Add(new DB2Parameter("@usuario", Sesion.UsuarioActual));

                    using (var reader = commandPrestamos.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string idPrestamo = reader.GetString(0);
                            decimal monto = reader.GetDecimal(1);
                            balanceTotal -= monto;

                            dtCombinado.Rows.Add(idPrestamo, "Préstamo", "Préstamo", -monto); // Mostrar préstamos como valores negativos
                        }
                    }
                }

                // Asignar el DataTable combinado al DataGridView
                DgvProductos.DataSource = dtCombinado;

                // Mostrar balance total
                lbl1.Text = "El balance actual del usuario es: " + balanceTotal.ToString("C2");
            }
        }


        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string url = "https://docs.google.com/forms/d/e/1FAIpQLSd8v1Il3EAmvqdOk7wKimSV3EiSvkU8S_6DFFhBmYWyPJHNcw/viewform?vc=0&c=0&w=1&flr=0&pli=1";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        public void MostrarSegunClasificacion(string clasificacion)
        {

            // Hacer visible el botón solo si es Administrador (1) o Empleado (2)
            BtnVistade.Visible = clasificacion == "1" || clasificacion == "2";
            LblUsuario.Text = Sesion.NombreUsuarioActual; // Mostrar nombre de usuario en el label
        }
        private void Resumen_Load(object sender, EventArgs e)
        {

            CargarDatos();
            pnlbalprod.Visible = false;
            PnlProductos.Visible = true;
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible=false;
            DgvProductos.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            clickbtn1++;
            if (clickbtn1 % 2 == 0)
            {
                pnlbalprod.Visible = false;
            }
            else
            {
                pnlbalprod.Visible = true;
            }
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            clickbtn2++;
            if (clickbtn2 % 2 == 0)
            {
                PnlProductos.Visible = true;
            }
            else
            {
                PnlProductos.Visible = false;
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new A_otras_cuentas().ShowDialog();
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
            this.Invalidate();
            this.Update();
            ocultar();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
        }

        private void BtnTransferir_Click(object sender, EventArgs e)
        {
            mostrar(Pnltransferir);
        }

        private void BtnSolicituddeproductos_Click(object sender, EventArgs e)
        {
            mostrar(Pnlsolicituddeproductos);
        }

        private void BtnVistade_Click(object sender, EventArgs e)
        {
            mostrar(Pnladminempleado);
        }

        private void Btnotrascuentas_Click(object sender, EventArgs e)
        {
            this.Hide();
            new A_otras_cuentas().ShowDialog();
            ocultar();
        }

        private void BtnHaciaotropais_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnHaciaotrosbancos_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Hacia_otros_bancos().ShowDialog();
            ocultar();
        }

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Información().ShowDialog();
            ocultar();
        }

        private void BtnCuenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cuenta().ShowDialog();
            ocultar();
        }

        private void BtnPrestamo_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_Prestamo().ShowDialog();
            ocultar();
        }

        private void BtnDivisa_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambio_de_divisa().ShowDialog();
            ocultar();
        }

        private void BtnTransacciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Historial_transacciones().ShowDialog();
            ocultar();
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Usuarios_vista().ShowDialog();
            ocultar();
        }

        private void BtnSolprod_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_de_productos().ShowDialog();
            ocultar();
        }

        private void BtnPrestamos_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitudes_prestamo().ShowDialog();
            ocultar();
        }

        private void Resumen_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:"+DateTime.Now.ToString("hh:mm:ss");
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void LblUsuario_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LblUsuario_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void rjCircularPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void rjCircularPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void rjDatePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rjDatePicker1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lblHoraactual_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lblHoraactual_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void rjDatePicker1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnCrearCuentanueva_Click(object sender, EventArgs e)
        {
            var agregarCuentaForm = new Agregar_cuenta_bancaria();
            agregarCuentaForm.CuentaCreada += (s, args) => CargarDatos(); // Suscribirse al evento
            agregarCuentaForm.ShowDialog();
        }

        private void DgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }
