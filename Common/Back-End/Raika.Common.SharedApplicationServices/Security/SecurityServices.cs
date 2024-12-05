using System.Security.Cryptography;
using System.Text;

namespace Raika.Common.SharedApplicationServices.Security
{
    public class SecurityServices : ISecurityServices
    {
        public string CreatePasswordHash(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            var sha512 = SHA512.Create();
            var hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));
            return Convert.ToBase64String(hash);
        }

        public string CreateRandomSalt()
        {
            int size = 24;          
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            byte[] buff = new byte[size];
            rnd.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string EncryptText(string plainText)
        {
            string encryptionKey = "?D(G+KbPdSgVkYp3s6v9y$B&E)H@McQfThWmZq4t7w!z%C*FJaNdRgUkXn2r5u8";
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.ISO10126;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }

        public string DecryptText(string cipherText)
        {
            string encryptionKey = "?D(G+KbPdSgVkYp3s6v9y$B&E)H@McQfThWmZq4t7w!z%C*FJaNdRgUkXn2r5u8";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.ISO10126;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public string CreateRandomString(int size)
        {
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            byte[] buff = new byte[size];
            rnd.GetBytes(buff);
            var result = Convert.ToBase64String(buff);
            return result
                .Replace("/", "")
                .Replace("\\", "")
                .Replace(".", "");
        }
    }
}
