using IBM.Data.DB2;
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
    public partial class Agregar_cuenta_bancaria : Form
    {
        public event EventHandler CuentaCreada;
        public Agregar_cuenta_bancaria()
        {
            InitializeComponent();
        }

        private void TxtNombredecuenta__TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDPI__TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtConfirmarcontraseña__TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnCrearcuenta_Click(object sender, EventArgs e)
        {
            // Verificar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(TxtNombredecuenta.Texts) ||
                string.IsNullOrWhiteSpace(TxtDPI.Texts) ||
                string.IsNullOrWhiteSpace(TxtConfirmarcontraseña.Texts))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Validar DPI y contraseña
                var usuarioValidado = ValidarDPIyContrasena(connection, TxtDPI.Texts, TxtConfirmarcontraseña.Texts);

                if (usuarioValidado == null)
                {
                    MessageBox.Show("DPI o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el siguiente IDCuenta
                int nuevoIdCuenta = ObtenerSiguienteIdCuenta(connection);

                // Generar un número de cuenta aleatorio
                string nuevoNumeroCuenta = GenerarNumeroCuentaAleatorio(connection);

                // Insertar la nueva cuenta en la base de datos
                InsertarNuevaCuenta(connection, nuevoIdCuenta, usuarioValidado.IDUsuario, TxtNombredecuenta.Texts, nuevoNumeroCuenta, 0.00m);

                MessageBox.Show("Cuenta creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CuentaCreada?.Invoke(this, EventArgs.Empty); // Disparar el evento
                this.Close();
            }
        }
        private Usuario ValidarDPIyContrasena(DB2Connection connection, string dpi, string contrasena)
        {
            string queryUsuario = "SELECT IDUsuario, Contrasena FROM Usuarios WHERE DPI = @dpi";
            using (var command = new DB2Command(queryUsuario, connection))
            {
                command.Parameters.Add(new DB2Parameter("@dpi", dpi));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var idUsuario = reader.GetString(0);
                        var contrasenaAlmacenada = reader.GetString(1);

                        // Verificar que el IDUsuario corresponde al usuario actual
                        if (contrasenaAlmacenada == contrasena && idUsuario == Sesion.UsuarioActual)
                        {
                            return new Usuario { IDUsuario = idUsuario };
                        }
                    }
                }
            }
            return null;
        }

        private int ObtenerSiguienteIdCuenta(DB2Connection connection)
        {
            try
            {
                string queryMaxId = "SELECT COALESCE(MAX(CAST(IDCuenta AS INTEGER)), 0) FROM Cuentas_Bancarias";
                using (var command = new DB2Command(queryMaxId, connection))
                {
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result) + 1; // Asumiendo que los IDs son numéricos
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show("Error al obtener el siguiente ID de cuenta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // Rethrow the exception after logging it
            }
        }

        private void InsertarNuevaCuenta(DB2Connection connection, int idCuenta, string idUsuario, string nombreCuenta, string numeroCuenta, decimal saldo)
        {
            try
            {
                string queryInsertar = "INSERT INTO Cuentas_Bancarias (IDCuenta, IDUsuario, Nombre_de_Cuenta, Numero_de_Cuenta, Saldo) VALUES (@idCuenta, @idUsuario, @nombreCuenta, @numeroCuenta, @saldo)";
                using (var command = new DB2Command(queryInsertar, connection))
                {
                    command.Parameters.Add(new DB2Parameter("@idCuenta", DB2Type.VarChar) { Value = idCuenta.ToString() });
                    command.Parameters.Add(new DB2Parameter("@idUsuario", DB2Type.VarChar) { Value = idUsuario });
                    command.Parameters.Add(new DB2Parameter("@nombreCuenta", DB2Type.VarChar) { Value = nombreCuenta });
                    command.Parameters.Add(new DB2Parameter("@numeroCuenta", DB2Type.VarChar) { Value = numeroCuenta });
                    command.Parameters.Add(new DB2Parameter("@saldo", DB2Type.Decimal) { Value = saldo }); // Saldo inicial

                    command.ExecuteNonQuery();
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show("Error al insertar la nueva cuenta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // Rethrow the exception after logging it
            }
        }

        private string GenerarNumeroCuentaAleatorio(DB2Connection connection)
        {
            Random random = new Random();
            string numeroCuenta;

            do
            {
                numeroCuenta = random.Next(10000000, 99999999).ToString(); // Genera un número entre 10000000 y 99999999
            } while (NumeroCuentaExistente(connection, numeroCuenta)); // Verifica que no exista

            return numeroCuenta;
        }

        private bool NumeroCuentaExistente(DB2Connection connection, string numeroCuenta)
        {
            string queryVerificar = "SELECT COUNT(*) FROM Cuentas_Bancarias WHERE Numero_de_Cuenta = @numeroCuenta";
            using (var command = new DB2Command(queryVerificar, connection))
            {
                command.Parameters.Add(new DB2Parameter("@numeroCuenta", numeroCuenta));
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0; // Devuelve true si existe
            }
        }
    }

    // Clase auxiliar para representar un usuario
    public class Usuario
    {
        public string IDUsuario { get; set; }
    }


}

