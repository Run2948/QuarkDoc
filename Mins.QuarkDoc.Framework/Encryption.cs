using System;
using System.Security.Cryptography;
using System.Text;

namespace Mins.QuarkDoc.Framework
{
    public class Encryption
    {
        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            MD5 md5 = MD5.Create(); 
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
    }
}
