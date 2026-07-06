using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TripMitraHolidays.Core.Helpers
{
    public static class IdProtector
    {
        // 16-byte key / IV — change these to any 16-char string in production
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("TripMitra@Admin!");
        private static readonly byte[] IV  = Encoding.UTF8.GetBytes("TM_Holidays_2025");

        public static string Protect(int id)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV  = IV;
                using (var enc = aes.CreateEncryptor())
                using (var ms  = new MemoryStream())
                using (var cs  = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                {
                    byte[] data = BitConverter.GetBytes(id);
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return ToUrlSafeBase64(ms.ToArray());
                }
            }
        }

        public static int? Unprotect(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;
            try
            {
                byte[] cipher = FromUrlSafeBase64(token);
                using (var aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV  = IV;
                    using (var dec = aes.CreateDecryptor())
                    using (var ms  = new MemoryStream(cipher))
                    using (var cs  = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                    {
                        byte[] data = new byte[4];
                        cs.Read(data, 0, 4);
                        return BitConverter.ToInt32(data, 0);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private static string ToUrlSafeBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        private static byte[] FromUrlSafeBase64(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            switch (s.Length % 4)
            {
                case 2: s += "=="; break;
                case 3: s += "=";  break;
            }
            return Convert.FromBase64String(s);
        }
    }
}
