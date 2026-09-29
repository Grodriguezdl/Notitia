using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public class DRecuperarPassword
    {
        private SmtpClient smtpClient;
        protected string remitenteCorreo {  get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }

        protected void initializeSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(remitenteCorreo, password);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }
        public void sendMail(string subject, string body, string contraseña, List<string> destinatarioCorreo)
        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(remitenteCorreo);
                foreach (string mail in destinatarioCorreo)
                {
                    mailMessage.To.Add(mail);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Body = body + "\nSu contraseña es: " + contraseña;
                mailMessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje de error en un cuadro de diálogo
                MessageBox.Show("Error al enviar correo: " + ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
        public string recoveryPasswod(string usuarioSolicitando)
        {
            using (var connection = new DB2Connection())
            {
                connection.ConnectionString = Conexion.conexion;
                connection.Open();
                using(var command = new DB2Command())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Usuarios where Nombre_de_Usuario=@Nombre_de_Usuario or Correo_Electronico=@Correo_Electronico";
                    command.Parameters.Add("@Nombre_de_Usuario", DB2Type.VarChar).Value = usuarioSolicitando; 
                    command.Parameters.Add("@Correo_Electronico", DB2Type.VarChar).Value = usuarioSolicitando; 
                    command.CommandType = System.Data.CommandType.Text;
                    using (DB2DataReader reader = command.ExecuteReader())
                        if (reader.Read() == true)
                    {
                        string nombreUsuario = reader.GetString(8);
                        string correoUsuario = reader.GetString(6);
                        string contraseña = reader.GetString(9);

                            var mailService = new DCorreoSoporte();
                            mailService.sendMail(
                               subject: "Sistema de Notitia: Solicitud de recuperación de contraseña",
                               body: "Hola, " + nombreUsuario + "\nUsted solicitó recuperar su contraseña." +
                                     "\nSin embargo, le pedimos que cambie su contraseña inmediatamente una vez que ingrese al sistema...",
                               contraseña: contraseña,
                               destinatarioCorreo: new List<string> { correoUsuario });

                            return "Hola, " + nombreUsuario + "\nUsted solicitó recuperar su contraseña. " +
                                   "Por favor revise su correo: " + correoUsuario +
                                   "\nSin embargo, le pedimos que cambie su contraseña inmediatamente una vez que ingrese al sistema...";

                        }
                    else
                        return "Lo sentimos, no tiene una cuenta con ese correo o nombre del usuario";
                }
            }
        } 
    }
}
