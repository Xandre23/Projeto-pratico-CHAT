using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ChatUni9.Security
{
    public class Hash
    {
        internal string GenerateHashSHA512(string input)
        {          

            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = new SHA512Managed())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var saltBytes = new byte[8];
                byte[] hashWithSaltBytes = new byte[hashedInputBytes.Length + saltBytes.Length];

                for (int i = 0; i < hashedInputBytes.Length; i++)
                    hashWithSaltBytes[i] = hashedInputBytes[i];

                for (int i = 0; i < saltBytes.Length; i++)
                    hashWithSaltBytes[hashedInputBytes.Length + i] = saltBytes[i];
               
                string hashValue = Convert.ToBase64String(hashWithSaltBytes);
                
                return hashValue;
            }
        }
    }
}
