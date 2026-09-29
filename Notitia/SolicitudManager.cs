using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public class SolicitudManager
    {
        private readonly string connectionString = "DATABASE=Dbexpo";

        public void CrearSolicitud(string idUsuario, string nombreCompleto, string numeroTelefono, string correoElectronico, string solicitudDe)
        {
            string idSolCuenta = ObtenerNuevoID(); // Método para obtener el nuevo ID

            string query = "INSERT INTO Solicitud_Productos_Cuenta (IDSolCuenta, IDUsuario, Nombre_Completo, Numero_de_telefono, Correo_Electronico, Solicitud_de) " +
                           "VALUES (@idSolCuenta, @idUsuario, @nombreCompleto, @numeroTelefono, @correoElectronico, @solicitudDe)";

            try
            {
                using (var connection = new DB2Connection(connectionString))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@idSolCuenta", idSolCuenta);
                        command.Parameters.Add("@idUsuario", idUsuario);
                        command.Parameters.Add("@nombreCompleto", nombreCompleto);
                        command.Parameters.Add("@numeroTelefono", numeroTelefono);
                        command.Parameters.Add("@correoElectronico", correoElectronico);
                        command.Parameters.Add("@solicitudDe", solicitudDe);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Solicitud creada con éxito.");
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show($"Error de DB2: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private string ObtenerNuevoID()
        {
            string lastId = "";
            string query = "SELECT MAX(IDSolCuenta) FROM Solicitud_Productos_Cuenta";

            try
            {
                using (var connection = new DB2Connection(connectionString))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            lastId = result.ToString();
                            // Aquí debes convertir el ID a un número y sumar 1
                            int newId = int.Parse(lastId) + 1;
                            return newId.ToString();
                        }
                        else
                        {
                            return "1"; // Si no hay registros, empieza con 1
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show($"Error de DB2: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return null; // Manejo en caso de error
        }
    }
}