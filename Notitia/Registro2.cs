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
    public partial class Registro2 : Form
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
        public Registro2()
        {
            InitializeComponent();
        }

        private void Registro2_Load(object sender, EventArgs e)
        {

        }

        private void btnben_Click(object sender, EventArgs e)
        {
            var registro = new Registro
            {
                PrimerNombre = this.PrimerNombre,
                SegundoNombre = this.SegundoNombre,
                PrimerApellido = this.PrimerApellido,
                SegundoApellido = this.SegundoApellido,
                CorreoElectronico = this.CorreoElectronico,
                DPI = this.DPI,
                Direccion = this.Direccion,
                EstadoCivil = this.EstadoCivil,
                Profesion = this.Profesion,
                Telefono = this.Telefono
            };
            this.Hide();
            registro.ShowDialog();

        }

        private void Finalizar_Click(object sender, EventArgs e)
        {
            string usuarioNuevo = TxtUsuarionuevo.Texts;
            string contraseña = Txtcontraseña.Texts;
            string confirmarContraseña = TxtConfirmarcontrasena.Texts;

            if (contraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (NombreUsuarioExiste(usuarioNuevo))
                {
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor, elija otro nombre de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CrearUsuario(usuarioNuevo, contraseña);
                MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                new Login().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool NombreUsuarioExiste(string nombreUsuario)
        {
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre_de_Usuario = @nombreUsuario";
                using (var command = new DB2Command(query, connection))
                {
                    command.Parameters.Add("@nombreUsuario", nombreUsuario);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void CrearUsuario(string usuarioNuevo, string contraseña)
        {
            using (var connection = new DB2Connection(Conexion.conexion))
            {
                connection.Open();

                // Crear y obtener nuevo IDUsuario
                string idUsuario = ObtenerNuevoID(connection);

                // Insertar usuario
                InsertarUsuario(connection, idUsuario, usuarioNuevo, contraseña);

                // Insertar dirección
                InsertarDireccion(connection, idUsuario);

                // Crear cuenta bancaria
                CrearCuentaBancaria(connection, idUsuario);
            }
        }

        private string ObtenerNuevoID(DB2Connection connection)
        {
            string query = "SELECT COALESCE(MAX(IDUsuario), 0) + 1 FROM Usuarios";
            using (var command = new DB2Command(query, connection))
            {
                return command.ExecuteScalar().ToString();
            }
        }

        private void InsertarUsuario(DB2Connection connection, string idUsuario, string usuarioNuevo, string contraseña)
        {
            string query = "INSERT INTO Usuarios (IDUsuario, Nombre_de_Usuario, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Correo_Electronico, DPI, Estado_Civil, Profesion_Oficio, Número_de_telefono, IDClasificacion, Estado) " +
                "VALUES (@idUsuario, @usuarioNuevo, @contraseña, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @CorreoElectronico, @DPI, @EstadoCivil, @Profesion, @Telefono, '3', 'Activo')";
            using (var command = new DB2Command(query, connection))
            {
                command.Parameters.Add("@idUsuario", idUsuario);
                command.Parameters.Add("@usuarioNuevo", usuarioNuevo);
                command.Parameters.Add("@contraseña", contraseña);
                command.Parameters.Add("@PrimerNombre", PrimerNombre);
                command.Parameters.Add("@SegundoNombre", SegundoNombre);
                command.Parameters.Add("@PrimerApellido", PrimerApellido);
                command.Parameters.Add("@SegundoApellido", SegundoApellido);
                command.Parameters.Add("@CorreoElectronico", CorreoElectronico);
                command.Parameters.Add("@DPI", DPI);
                command.Parameters.Add("@EstadoCivil", EstadoCivil);
                command.Parameters.Add("@Profesion", Profesion);
                command.Parameters.Add("@Telefono", Telefono);
                command.ExecuteNonQuery();
            }
        }

        private void InsertarDireccion(DB2Connection connection, string idUsuario)
        {
            string idDireccion = ObtenerNuevoIDDireccion(connection);
            string query = "INSERT INTO Direccion (IDDireccion, IDUsuario, Direccion, Departamento, Zona, Municipio) VALUES (@idDireccion, @idUsuario, @Direccion, '-', '-', '-')";
            using (var command = new DB2Command(query, connection))
            {
                command.Parameters.Add("@idDireccion", idDireccion);
                command.Parameters.Add("@idUsuario", idUsuario);
                command.Parameters.Add("@Direccion", Direccion);
                command.ExecuteNonQuery();
            }
        }

        private string ObtenerNuevoIDDireccion(DB2Connection connection)
        {
            string query = "SELECT COALESCE(MAX(IDDireccion), 0) + 1 FROM Direccion";
            using (var command = new DB2Command(query, connection))
            {
                return command.ExecuteScalar().ToString();
            }
        }

        private void CrearCuentaBancaria(DB2Connection connection, string idUsuario)
        {
            string idCuenta = ObtenerNuevoIDCuenta(connection);

            string query = "INSERT INTO Cuentas_Bancarias (IDCuenta, IDUsuario, Nombre_de_Cuenta, Numero_de_Cuenta, Saldo, Estado) " +
                           "VALUES (@idCuenta, @idUsuario, 'Mi primera cuenta', @NumeroCuenta, 100, 'Activo')";
            using (var command = new DB2Command(query, connection))
            {
                command.Parameters.Add("@idCuenta", idCuenta);
                command.Parameters.Add("@idUsuario", idUsuario);
                command.Parameters.Add("@NumeroCuenta", GenerarNumeroCuenta());
                command.ExecuteNonQuery();
            }
        }

        private string ObtenerNuevoIDCuenta(DB2Connection connection)
        {
            string query = "SELECT COALESCE(MAX(CAST(IDCuenta AS INTEGER)), 0) + 1 FROM Cuentas_Bancarias";
            using (var command = new DB2Command(query, connection))
            {
                var result = command.ExecuteScalar();
                int nuevoId = Convert.ToInt32(result);
                return nuevoId.ToString(); // Convertir a string para que sea un VARCHAR
            }
        }

        private string GenerarNumeroCuenta()
        {
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString();
        }

    }
}