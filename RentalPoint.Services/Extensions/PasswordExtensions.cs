using System;
using System.Security.Cryptography;
using System.Text;

namespace RentalPoint.Services.Extensions
{
    public static class PasswordExtensions
    {
        public static string GetHash(this string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
 
            return Convert.ToBase64String(hash);
        }
    }
}