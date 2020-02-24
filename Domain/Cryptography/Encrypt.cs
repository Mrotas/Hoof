using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Cryptography
{
    public class Encrypt
    {
        public static string EncryptPassword(string password)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
