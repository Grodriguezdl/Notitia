using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Notitia
{
    public class DCorreoSoporte: DRecuperarPassword

    {
        public DCorreoSoporte()
        {
            remitenteCorreo = "notitiabanco@gmail.com"; 
            password= "duni uqln jzzh qlhi";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            using (var message = new MailMessage(remitenteCorreo, destinatario))
            {
                message.Subject = asunto;
                message.Body = cuerpo;
                message.IsBodyHtml = true; // Si deseas enviar el cuerpo en HTML

                using (var smtpClient = new SmtpClient(host, port))
                {
                    smtpClient.Credentials = new NetworkCredential(remitenteCorreo, password);
                    smtpClient.EnableSsl = ssl;

                    smtpClient.Send(message);
                }
            }
        }
    }
}
