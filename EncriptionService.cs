using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class EncriptionService
    {
        private readonly byte[] _key;
         
        public EncriptionService(string key)
        {
            using var sha256 = SHA256.Create();
            _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            Console.WriteLine($"Geerated key : { Convert.ToHexString(_key)}");
        }
        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;

            aes.GenerateIV();
        }
    }
}
