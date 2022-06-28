using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoParticipateGui.Services
{
    // Source: https://stackoverflow.com/a/27484425
    public class EncryptionService
    {
        private const string EncryptionKey = "9c110416e322e53177ff7f4bec338feb8ac55434ae3c7e6bd191a3ca07759295d5acf018ffe048a217e0442194f8b72645b28e3211ff305b21a0bd1d1a286372";
        
        public static string Encrypt(string data)
        {
            var clearBytes = Encoding.Unicode.GetBytes(data);
            using (var encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }
            return data;
        }
        public static string Decrypt(string data)
        {
            data = data.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(data);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    
                    data = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return data;
        }
    }
}