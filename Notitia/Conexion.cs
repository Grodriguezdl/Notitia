using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notitia
{
    internal class Conexion
    {
        public static string conexion = "DATABASE = Dbexpo";
        public static string conexionBancovir = "DATABASE = Bancovir";
    }
    public class TuClaseDondeEstaVerificarCredenciales
    {
        private string VerificarCredenciales(string usuario, string contraseña)
        {
            string IDClasificacion = "";

            string query = "SELECT IDClasificacion FROM Usuarios WHERE Nombre_de_Usuario = @usuario AND Contrasena = @contraseña";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@usuario", usuario);
                        command.Parameters.Add("@contraseña", contraseña);

                        connection.Open();

                        var result = command.ExecuteScalar();

                        if (result != null)
                        {
                            IDClasificacion = result.ToString();
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                // Manejo de excepciones de DB2
                Console.WriteLine($"Error de DB2: {ex.Message}");
                throw;  // Puedes manejar esta excepción de acuerdo a tus necesidades
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                Console.WriteLine($"Error: {ex.Message}");
                throw;  // Puedes manejar esta excepción de acuerdo a tus necesidades
            }

            return IDClasificacion;
        }
    }
}
