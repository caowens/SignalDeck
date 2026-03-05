using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignalDeck.Api.Utils
{
    public static class SecurityUtils
    {
        public static string HashKey(string plainKey)
        {
            var bytes = Encoding.UTF8.GetBytes(plainKey);
            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }
}