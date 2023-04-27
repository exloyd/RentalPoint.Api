using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RentalPoint.Authentication
{
    public static class AuthenticationOptions
    {
        public const string Issuer = "RentalPoint.Api";
        public const string Audience = "RentalPoint.UI";
        private const string Key = "wT13xJqAIssCweeJpCpUqiFAKA4yrUiw";
        public const int Lifetime = 60;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}