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
    public partial class A_otras_cuentas : Form
    {
        int clickop = 0;

        public A_otras_cuentas()
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Agregar().ShowDialog();
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

        private void A_otras_cuentas_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            CmbTipodeejecucion.Items.Add("Inmediata");
            CmbTipodeejecucion.Items.Add("Programada");
            CmbTipodeejecucion.Items.Add("Recurrente");
            CargarCuentasBancarias();
            CargarCuentasPropias();
        }


        private void CargarCuentasBancarias()
        {
            string usuarioActual = Sesion.UsuarioActual;
            string connectionString = Conexion.conexion; // Obtener la cadena de conexión desde tu clase Conexion

            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT IDCuenta, Nombre_de_Cuenta || ' - ' || Numero_de_Cuenta AS NombreCompleto FROM Cuentas_Bancarias WHERE IDUsuario = @IDUsuario";

                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = usuarioActual;

                DB2DataAdapter adapter = new DB2DataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    CmbCuentasprop.DisplayMember = "NombreCompleto";
                    CmbCuentasprop.ValueMember = "IDCuenta";
                    CmbCuentasprop.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las cuentas bancarias: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
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
            new Resumen().ShowDialog();
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

        private void Btnotrascuentas_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Resumen().ShowDialog();
            ocultar();
        }

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Información().ShowDialog();
            ocultar();
        }

        private void BtnSolicituddeproductos_Click(object sender, EventArgs e)
        {
            mostrar(Pnlsolicituddeproductos);
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

        private void BtnVistade_Click(object sender, EventArgs e)
        {
            mostrar(Pnladminempleado);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:" + DateTime.Now.ToString("hh:mm:ss");
        }

        private void guna2Button1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void guna2Button1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void btnVolver_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void btnVolver_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void rjDatePicker1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void lblHoraactual_DragLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void rjCircularPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void LblUsuario_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void rjDatePicker1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lblHoraactual_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void rjCircularPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void LblUsuario_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lblHoraactual_Click(object sender, EventArgs e)
        {

        }

        private void lblHoraactual_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void RbtnTodas_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Rbtncuentaspropias_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtCuentasbancariaspropias.Checked)
            {
               
                CargarCuentasPropias();
            }
        }

        private void RbtnTerceros_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Rbtnotros_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnAfiliadas.Checked)
            {
                CargarAfiliadas();
            }
        }

        private void CargarCuentasPropias()
        {
            string usuarioActual = Sesion.UsuarioActual;
            string connectionString = Conexion.conexion;

            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT IDCuenta, Nombre_de_Cuenta || ' - ' || Numero_de_Cuenta AS NombreCompleto FROM Cuentas_Bancarias WHERE IDUsuario = @IDUsuario";

                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = usuarioActual;

                DB2DataAdapter adapter = new DB2DataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    CmbCuentasdepos.DataSource = dataTable;
                    CmbCuentasdepos.DisplayMember = "NombreCompleto";
                    CmbCuentasdepos.ValueMember = "IDCuenta";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las cuentas bancarias: " + ex.Message);
                }
            }
        }
        private void CargarAfiliadasNotitia()
        {
            string usuarioActual = Sesion.UsuarioActual;
            string connectionString = Conexion.conexion;

            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT IDAfiliadas_not, Nombre_cuenta || ' - ' || Numero_de_cuenta AS NombreCompleto, Saldo FROM Afiliadas_notitia WHERE IDUsuario = @IDUsuario";

                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = usuarioActual;

                DB2DataAdapter adapter = new DB2DataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    CmbCuentasdepos.DataSource = dataTable;
                    CmbCuentasdepos.DisplayMember = "NombreCompleto";
                    CmbCuentasdepos.ValueMember = "IDAfiliadas_not";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las cuentas afiliadas notitia: " + ex.Message);
                }
            }
        }
        private void CargarAfiliadas()
        {
            string usuarioActual = Sesion.UsuarioActual;
            string connectionString = Conexion.conexion;

            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT IDAfiliada, Numero_de_cuenta || ' - ' || Banco AS NombreCompleto FROM Afiliadas WHERE IDUsuario = @IDUsuario";

                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = usuarioActual;

                DB2DataAdapter adapter = new DB2DataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    CmbCuentasdepos.DataSource = dataTable;
                    CmbCuentasdepos.DisplayMember = "NombreCompleto";
                    CmbCuentasdepos.ValueMember = "IDAfiliada";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las cuentas afiliadas: " + ex.Message);
                }
            }
        }
        private void btncont_Click(object sender, EventArgs e)
        {
            if (CmbCuentasprop.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una cuenta bancaria.");
                return;
            }

            decimal monto;
            if (!decimal.TryParse(TxtMonto.Texts, out monto))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            string cuentaSeleccionada = CmbCuentasprop.SelectedValue.ToString();
            decimal saldoCuenta = ObtenerSaldoCuenta(cuentaSeleccionada);

            if (monto >= saldoCuenta)
            {
                MessageBox.Show("La cantidad ingresada es mayor o igual al saldo de la cuenta. No se puede realizar la transferencia.");
                return;
            }
            if (monto <= 0)
            {
                MessageBox.Show("La cantidad ingresada no es valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tipoDeEjecucion = CmbTipodeejecucion.SelectedItem.ToString();
            string descripcion = string.IsNullOrEmpty(TxtDes.Text) ? "-" : TxtDes.Text;

            using (DB2Connection connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Obtener el nuevo IDTransferencia
                string queryIdTransferencia = "SELECT COALESCE(MAX(IDTransferencia), 0) + 1 FROM Transferencias";
                int nuevoIdTransferencia;

                using (DB2Command commandId = new DB2Command(queryIdTransferencia, connection))
                {
                    nuevoIdTransferencia = Convert.ToInt32(commandId.ExecuteScalar());
                }

                // Insertar la nueva transferencia
                string queryTransferencia = @"
            INSERT INTO Transferencias (IDTransferencia, IDUsuario, Monto, Fecha, IDCuenta, Tipo_de_ejecucion, Descripcion)
            VALUES (@IDTransferencia, @IDUsuario, @Monto, CURRENT_DATE, @IDCuenta, @Tipo_de_ejecucion, @Descripcion);";

                using (DB2Command commandTransferencia = new DB2Command(queryTransferencia, connection))
                {
                    commandTransferencia.Parameters.Add("@IDTransferencia", DB2Type.VarChar).Value = nuevoIdTransferencia.ToString();
                    commandTransferencia.Parameters.Add("@IDUsuario", DB2Type.VarChar).Value = Sesion.IDUsuarioActual;
                    commandTransferencia.Parameters.Add("@Monto", DB2Type.Decimal).Value = monto;
                    commandTransferencia.Parameters.Add("@IDCuenta", DB2Type.VarChar).Value = cuentaSeleccionada;
                    commandTransferencia.Parameters.Add("@Tipo_de_ejecucion", DB2Type.VarChar).Value = tipoDeEjecucion;
                    commandTransferencia.Parameters.Add("@Descripcion", DB2Type.VarChar).Value = descripcion;

                    commandTransferencia.ExecuteNonQuery();
                }

                // Actualizar saldo de la cuenta de débito
                string queryDebito = "UPDATE Cuentas_Bancarias SET Saldo = Saldo - @Monto WHERE IDCuenta = @IDCuentaDebito";
                using (DB2Command commandDebito = new DB2Command(queryDebito, connection))
                {
                    commandDebito.Parameters.Add("@Monto", DB2Type.Decimal).Value = monto;
                    commandDebito.Parameters.Add("@IDCuentaDebito", DB2Type.VarChar).Value = cuentaSeleccionada;
                    commandDebito.ExecuteNonQuery();
                }

                // Actualizar saldo de la cuenta de crédito
                string cuentaDepositoSeleccionada = CmbCuentasdepos.SelectedValue.ToString();
                string queryCredito = "UPDATE Cuentas_Bancarias SET Saldo = Saldo + @Monto WHERE IDCuenta = @IDCuentaCredito";
                using (DB2Command commandCredito = new DB2Command(queryCredito, connection))
                {
                    commandCredito.Parameters.Add("@Monto", DB2Type.Decimal).Value = monto;
                    commandCredito.Parameters.Add("@IDCuentaCredito", DB2Type.VarChar).Value = cuentaDepositoSeleccionada;
                    commandCredito.ExecuteNonQuery();
                }

                // Actualizar saldo en Bancovir
                using (DB2Connection connectionBancovir = new DB2Connection(Conexion.conexionBancovir))
                {
                    connectionBancovir.Open();
                    string numeroCuentaAfiliada = ObtenerNumeroCuentaAfiliada(cuentaDepositoSeleccionada);
                    string queryActualizarSaldo = "UPDATE Cuentas_Bancarias SET Saldo = Saldo + @Monto WHERE Numero_de_Cuenta = @NumeroDeCuenta";

                    using (DB2Command commandActualizarSaldo = new DB2Command(queryActualizarSaldo, connectionBancovir))
                    {
                        commandActualizarSaldo.Parameters.Add("@Monto", DB2Type.Decimal).Value = monto;
                        commandActualizarSaldo.Parameters.Add("@NumeroDeCuenta", DB2Type.VarChar).Value = numeroCuentaAfiliada;
                        commandActualizarSaldo.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Transferencia realizada exitosamente.");
        }
        private string ObtenerNumeroCuentaAfiliada(string idAfiliada)
        {
            string numeroCuenta = string.Empty;
            string connectionString = Conexion.conexion; // Asegúrate de que esta conexión es la correcta.

            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT Numero_de_cuenta FROM Afiliadas WHERE IDAfiliada = @IDAfiliada";

                using (DB2Command command = new DB2Command(query, connection))
                {
                    command.Parameters.Add("@IDAfiliada", DB2Type.VarChar).Value = idAfiliada;

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        numeroCuenta = result.ToString();
                    }
                }
            }
            return numeroCuenta;
        }

        private decimal ObtenerSaldoCuenta(string idCuenta)
        {
            string connectionString = Conexion.conexion; // Obtener la cadena de conexión desde tu clase Conexion
            decimal saldo = 0;

            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                string query = "SELECT Saldo FROM Cuentas_Bancarias WHERE IDCuenta = @IDCuenta";

                DB2Command command = new DB2Command(query, connection);
                command.Parameters.Add("@IDCuenta", DB2Type.VarChar).Value = idCuenta;

                try
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    saldo = result != null ? (decimal)result : 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el saldo de la cuenta: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return saldo;
        }

        private void RbtnAfiliadasnotitia_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnAfiliadasnotitia.Checked)
            {

                CargarAfiliadasNotitia();
            }
        }
    }
}
    

