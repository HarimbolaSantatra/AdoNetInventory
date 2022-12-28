using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppInventaire.Utils
{
    public class Operation
    {
        public static string Sha1Hash(string input)
        {
            // Hash User input password. Sha1 is the algorithm used in MySql table
            SHA1 mySha1 = SHA1.Create();
            // Convert LoginPassword string to Byte Array
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            Byte[] hashed_inputBytes = mySha1.ComputeHash(inputBytes);
            // Reconvert to string
            string hashed_input = BitConverter.ToString(hashed_inputBytes).Replace("-", String.Empty).ToLower();

            mySha1.Dispose();

            return hashed_input;
        }
    }
}