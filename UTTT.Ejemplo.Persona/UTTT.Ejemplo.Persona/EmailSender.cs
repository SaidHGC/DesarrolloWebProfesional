using System;
using System.Net;
using System.Net.Mail;

namespace UTTT.Ejemplo.Persona
{
    public class EmailSender
    {
        private String correoElectronico;

        public EmailSender(String correo)
        {
            correoElectronico = correo;
        }
        public void sendMessage(String message)
        {
            var fromAddress = new MailAddress("saidhgc02@gmail.com", "SaidH Cuevas");
            var toAddress = new MailAddress(correoElectronico);
            const string fromPassword = "SGC0311S";
            const string subject = "Problemas en el aplicativo";
            var body = "Se encontraron las siguientes excepciones " + message;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var mensaje = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(mensaje);
            }
        }
    }
}