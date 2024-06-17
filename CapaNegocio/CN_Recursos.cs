using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
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

    }
}
