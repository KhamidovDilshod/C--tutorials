using System.Security.Cryptography;
using System.Text;

namespace C__tutorials.Utils
{
#pragma warning disable
    public class Util
    {
        public static byte[] Encode(string password)
        {
            using var hmac = new HMACSHA512();
            Console.WriteLine(password);
            var hashed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            Console.WriteLine(hashed);
            Console.WriteLine(hmac.Key);
            return hashed;
        }

        public static string Decode(string password)
        {
            using var hmac = new HMACSHA512(StringToByte(password));
            var hashed = hmac.ComputeHash(StringToByte(password)).ToString();
            return hashed;
        }

        public static byte[] PasswordSalt(string password)
        {
            using var hmac = new HMACSHA512();
            Console.WriteLine(hmac.Key);
            return hmac.Key;
        }

        public static string ByteToString(byte[] password)
        {
            return Encoding.UTF8.GetString(password);
        }

        public static byte[] StringToByte(string password)
        {
            return Encoding.UTF8.GetBytes(password);
        }

        public static string DateConvert(DateTime dateTime)
        {
            return dateTime.Date.ToString("dd.MM.yyyy").Substring(0, 10);
        }
    }
}