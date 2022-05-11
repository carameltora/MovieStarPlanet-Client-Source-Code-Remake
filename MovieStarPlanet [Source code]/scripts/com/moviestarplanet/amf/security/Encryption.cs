using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace com.movieStarPlanet.security
{
    internal class Encryption
    {
        public static string DecryptPayload(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("YxOFRrDt7PoUlnTuNjZ8sGmrpr1sP7uR");
            byte[] bytes2 = Encoding.ASCII.GetBytes("8816891831636569");
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = bytes,
                IV = bytes2,
                Mode = CipherMode.CBC
            };
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.KeySize = 256;
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(message));
            CryptoStream stream2 = new CryptoStream(stream, rijndaelManaged.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Read);
            return new StreamReader(stream2).ReadToEnd();
        }

        public static string EncryptPayload(string message)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes("YxOFRrDt7PoUlnTuNjZ8sGmrpr1sP7uR");
                byte[] bytes2 = Encoding.ASCII.GetBytes("8816891831636569");
                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = bytes,
                    IV = bytes2,
                    Mode = CipherMode.CBC
                };
                rijndaelManaged.BlockSize = 128;
                rijndaelManaged.KeySize = 256;
                byte[] bytes3 = Encoding.UTF8.GetBytes(message);
                return Convert.ToBase64String(rijndaelManaged.CreateEncryptor(bytes, bytes2).TransformFinalBlock(bytes3, 0, bytes3.Length));
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public static string DecryptSessionIdFile(string contents)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("SessionGodsFuckWannabesrpr1sP7uR");
            byte[] bytes2 = Encoding.ASCII.GetBytes("5894760551482354");
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = bytes,
                IV = bytes2,
                Mode = CipherMode.CBC
            };
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.KeySize = 256;
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(contents));
            CryptoStream stream2 = new CryptoStream(stream, rijndaelManaged.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Read);
            return new StreamReader(stream2).ReadToEnd();
        }

        public static string HashSHA256(string data)
        {
            SHA256Managed sHA256Managed = new SHA256Managed();
            return BitConverter.ToString(sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }
}