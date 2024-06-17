using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace CapaNegocio
{
    public class CN_Recursos
    {

        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }


        // encriptacion de texto en sha256
        public static string ConvertirSha256(string texto)
        {
           
            using (SHA256 hash = SHA256.Create())
            {
               
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder sb = new StringBuilder();
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static bool EnviarEmail(string email, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("juanjo.mdt@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                // Configurar el servidor SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("juanjo.mdt@gmail.com", "mmrgfdbgzhrzsbya"),
                    EnableSsl = true
                };

                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, loguear el error)
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }

            return resultado;
        }
    }
}
