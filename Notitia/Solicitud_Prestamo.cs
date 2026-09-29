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
    public partial class Solicitud_Prestamo : Form
    {
        private int clickop = 0;
        private static int lastSolicitudPrestamoId;
        string[] Zona = new string[28];
        string[] departamentos = {
    "Guatemala", "Baja Verapaz", "Alta Verapaz", "El Progreso",
    "Izabal", "Zacapa", "Chimaltenango", "Sacatepéquez",
    "Escuintla", "Santa Rosa", "Sololá", "Totonicapán",
    "Quetzaltenango", "Suchitepéquez", "Retalhuleu", "San Marcos",
    "Huehuetenango", "Quiché", "Baja Verapaz", "Alta Verapaz",
    "El Progreso", "Izabal", "Zacapa", "Chimaltenango",
    "Sacatepéquez", "Escuintla", "Santa Rosa", "Sololá",
    "Totonicapán", "Quetzaltenango", "Suchitepéquez", "Retalhuleu",
    "San Marcos", "Huehuetenango", "Quiché"
};
        public Solicitud_Prestamo()
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
        private void Solicitud_Prestamo_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            for (int f = 0; f <= 27; f++)
            {
                Zona[f] = "Z." + f.ToString();
            }
            CmbZona.Items.AddRange(Zona);
            CmbConsolidar.Items.Add("Si");
            CmbConsolidar.Items.Add("No");
            CmbSalbanc.Items.Add("Si");
            CmbSalbanc.Items.Add("No");
            for (int f2 = 12; f2 <= 60; f2 += 12)
            {
                CmbPlazo.Items.Add(f2.ToString());
            }
            CmbDep.Items.AddRange(departamentos);
            lastSolicitudPrestamoId = ObtenerUltimoIdSolicitudPrestamo() + 1;
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
            this.Invalidate();
            this.Update();
            ocultar();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:" + DateTime.Now.ToString("hh:mm:ss");
        }

        private void Btnpresacep_Click(object sender, EventArgs e)
        {
            bool sonCamposValidos = !string.IsNullOrWhiteSpace(Txtnombrecompleto.Texts)
                            && !string.IsNullOrWhiteSpace(TxtCorreo.Texts)
                            && !string.IsNullOrWhiteSpace(TxtNumero_telefono.Texts)
                            && !string.IsNullOrWhiteSpace(TxtDPI.Texts)
                            && !string.IsNullOrWhiteSpace(TxtNit.Texts)
                            && !string.IsNullOrWhiteSpace(CmbDep.SelectedItem?.ToString())
                            && !string.IsNullOrWhiteSpace(CmbZona.SelectedItem?.ToString())
                            && !string.IsNullOrWhiteSpace(TxtEmpresa_donde_laboras.Texts)
                            && !string.IsNullOrWhiteSpace(TxtIngmens.Texts)
                            && !string.IsNullOrWhiteSpace(TxtMontdes.Texts)
                            && !string.IsNullOrWhiteSpace(TxtFechadenacimiento.Texts)
                            && !string.IsNullOrWhiteSpace(TxtAntiguedadLaboral.Texts)
                            && CmbPlazo.SelectedIndex != -1
                            && CmbConsolidar.SelectedIndex != -1
                            && CmbSalbanc.SelectedIndex != -1;

            bool checkBoxMarcado = guna2CustomCheckBox1.Checked;

            if (sonCamposValidos && checkBoxMarcado)
            {
                // Obtener los valores de los campos
                string idUsuario = Sesion.IDUsuarioActual;
                string nombreCompleto = Txtnombrecompleto.Texts;
                string correoElectronico = TxtCorreo.Texts;
                string numeroTelefonico = TxtNumero_telefono.Texts;
                string dpi = TxtDPI.Texts;
                string nit = TxtNit.Texts;
                string departamento = CmbDep.SelectedItem?.ToString();
                string zonaResidencia = CmbZona.SelectedItem?.ToString();
                string empresaLabora = TxtEmpresa_donde_laboras.Texts;
                string antiguedadLaboral = TxtAntiguedadLaboral.Texts;
                decimal ingresosMensuales = decimal.Parse(TxtIngmens.Texts);
                decimal montoDeseado = decimal.Parse(TxtMontdes.Texts);
                DateTime fechaNacimiento = DateTime.Parse(TxtFechadenacimiento.Texts);
                int plazoPagar = int.Parse(CmbPlazo.SelectedItem?.ToString());
                string consolidarDeudas = CmbConsolidar.SelectedItem?.ToString();
                string recibeSalarioBanco = CmbSalbanc.SelectedItem?.ToString();

                // Obtener IDSolicitudPrestamo
                int nuevoIdSolicitud = ObtenerUltimoIdSolicitudPrestamo() + 1;

                // Guardar la solicitud de préstamo en la base de datos
                GuardarSolicitudPrestamoEnDB(nuevoIdSolicitud, idUsuario, nombreCompleto, correoElectronico, numeroTelefonico, dpi, nit, departamento, zonaResidencia, empresaLabora, antiguedadLaboral, ingresosMensuales, montoDeseado, fechaNacimiento, plazoPagar, consolidarDeudas, recibeSalarioBanco);

                // Mostrar mensaje de confirmación
                MessageBox.Show("Su solicitud será enviada a revisión y posteriormente será aprobada o denegada por un administrador. Gracias por su confianza.", "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos del formulario
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos requeridos y marque la casilla de confirmación para continuar.", "Campos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActivarBotonPresacep()
        {
            // Verificar que todos los campos requeridos estén llenos
            bool camposLlenos = !string.IsNullOrWhiteSpace(Txtnombrecompleto.Texts)
                                && !string.IsNullOrWhiteSpace(TxtCorreo.Texts)
                                && !string.IsNullOrWhiteSpace(TxtNumero_telefono.Texts)
                                && !string.IsNullOrWhiteSpace(TxtDPI.Texts)
                                && !string.IsNullOrWhiteSpace(TxtNit.Texts)
                                && !string.IsNullOrWhiteSpace(CmbDep.SelectedItem?.ToString())
                                && !string.IsNullOrWhiteSpace(CmbZona.SelectedItem?.ToString())
                                && !string.IsNullOrWhiteSpace(TxtEmpresa_donde_laboras.Texts)
                                && !string.IsNullOrWhiteSpace(TxtIngmens.Texts)
                                && !string.IsNullOrWhiteSpace(TxtMontdes.Texts)
                                && !string.IsNullOrWhiteSpace(TxtFechadenacimiento.Texts)
                                && !string.IsNullOrWhiteSpace(TxtAntiguedadLaboral.Texts)
                                && CmbPlazo.SelectedIndex != -1
                                && CmbConsolidar.SelectedIndex != -1
                                && CmbSalbanc.SelectedIndex != -1;

            bool checkBoxMarcado = guna2CustomCheckBox1.Checked;
            Btnpresacep.Enabled = camposLlenos && checkBoxMarcado;
        }

        private void LimpiarCampos()
        {
            Txtnombrecompleto.Texts = "";
            TxtCorreo.Texts = "";
            TxtNumero_telefono.Texts = "";
            TxtDPI.Texts = "";
            TxtNit.Texts = "";
            TxtEmpresa_donde_laboras.Texts = "";
            TxtIngmens.Texts = "";
            TxtMontdes.Texts = "";
            TxtFechadenacimiento.Texts = "";
            TxtAntiguedadLaboral.Texts = "";
            CmbDep.SelectedIndex = -1;
            CmbZona.SelectedIndex = -1;
            CmbPlazo.SelectedIndex = -1;
            CmbConsolidar.SelectedIndex = -1;
            CmbSalbanc.SelectedIndex = -1;
            guna2CustomCheckBox1.Checked = false;
        }


        private int ObtenerUltimoIdSolicitudPrestamo()
        {
            // Método para obtener el último ID de solicitud de préstamo desde la base de datos
            int ultimoId = 0;
            try
            {
                string connectionString = Conexion.conexion;
                using (var connection = new DB2Connection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT MAX(IDSolicitudPrestamo) FROM Solicitudes_Prestamo";
                    using (var cmd = new DB2Command(sql, connection))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                        {
                            ultimoId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el último ID de solicitud de préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ultimoId;
        }


        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void Txtnombrecompleto__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void CmbConsolidar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtCorreo__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtNumero_telefono__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtFechadenacimiento__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtDPI__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtNit__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void CmbDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void CmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtEmpresa_donde_laboras__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtAntiguedadLaboral__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtIngmens__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void TxtMontdes__TextChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void CmbPlazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void CmbSalbanc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivarBotonPresacep();
        }

        private void GuardarSolicitudPrestamoEnDB(int idSolicitud, string idUsuario, string nombreCompleto, string correoElectronico, string numeroTelefonico, string dpi, string nit, string departamento, string zonaResidencia, string empresaLabora, string antiguedadLaboral, decimal ingresosMensuales, decimal montoDeseado, DateTime fechaNacimiento, int plazoPagar, string consolidarDeudas, string recibeSalarioBanco)
        {
            try
            {
                string connectionString = Conexion.conexion; // Usar la conexión definida en tu clase Conexion
                using (var connection = new DB2Connection(connectionString))
                {
                    connection.Open();

                    // Preparar la consulta SQL parametrizada para insertar en la tabla Solicitudes_Prestamo
                    string sql = @"INSERT INTO Solicitudes_Prestamo (IDSolicitudPrestamo, IDUsuario, Nombre_Completo, Correo_Electronico, Numero_Telefonico, Fecha_de_Nacimiento, DPI, NIT, Departamento, Zona_de_Residencia, Empresa_donde_labora, Antiguedad_Laboral, Ingresos_Mensuales, Monto_Deseado, Plazo_a_Pagar, Consolidar_Deudas, Recibe_salario_de_nuestro_banco)
                           VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                    using (var cmd = new DB2Command(sql, connection))
                    {
                        // Asignar los parámetros
                        cmd.Parameters.Add("IDSolicitudPrestamo", DB2Type.VarChar).Value = idSolicitud; // Asegúrate de que este ID sea único y válido
                        cmd.Parameters.Add("IDUsuario", DB2Type.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("Nombre_Completo", DB2Type.VarChar).Value = nombreCompleto;
                        cmd.Parameters.Add("Correo_Electronico", DB2Type.VarChar).Value = correoElectronico;
                        cmd.Parameters.Add("Numero_Telefonico", DB2Type.VarChar).Value = numeroTelefonico;
                        cmd.Parameters.Add("Fecha_de_Nacimiento", DB2Type.Date).Value = fechaNacimiento;
                        cmd.Parameters.Add("DPI", DB2Type.VarChar).Value = dpi;
                        cmd.Parameters.Add("NIT", DB2Type.VarChar).Value = nit;
                        cmd.Parameters.Add("Departamento", DB2Type.VarChar).Value = departamento;
                        cmd.Parameters.Add("Zona_de_Residencia", DB2Type.VarChar).Value = zonaResidencia;
                        cmd.Parameters.Add("Empresa_donde_labora", DB2Type.VarChar).Value = empresaLabora;
                        cmd.Parameters.Add("Antiguedad_Laboral", DB2Type.VarChar).Value = antiguedadLaboral;
                        cmd.Parameters.Add("Ingresos_Mensuales", DB2Type.Decimal).Value = ingresosMensuales;
                        cmd.Parameters.Add("Monto_Deseado", DB2Type.Decimal).Value = montoDeseado;
                        cmd.Parameters.Add("Plazo_a_Pagar", DB2Type.Integer).Value = plazoPagar;
                        cmd.Parameters.Add("Consolidar_Deudas", DB2Type.VarChar).Value = consolidarDeudas;
                        cmd.Parameters.Add("Recibe_salario_de_nuestro_banco", DB2Type.VarChar).Value = recibeSalarioBanco;

                        // Ejecutar la consulta
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verificar si se insertó correctamente
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Solicitud de préstamo registrada correctamente en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hubo un problema al intentar registrar la solicitud de préstamo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar guardar la solicitud de préstamo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
