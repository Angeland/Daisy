using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Daisy.Interfaces
{
    class Hashers
    {
        public static UInt64 ComputeSimpleHash(string input)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < input.Length; i++)
            {
                hashedValue += input[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }

        public static string ComputeHashSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                for (int a = 0; a < hash.Length; a++)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(hash[a].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
