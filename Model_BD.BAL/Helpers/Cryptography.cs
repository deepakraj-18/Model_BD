using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Helpers
{
    public class Cryptography
    {

        public string HashData(string any)
        {
            using SHA256 sHA256 = SHA256.Create();
            byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(any));
            string hashedData = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashedData;
        }


        public string EncryptPassword(string cleanText)
        {
            return Encrypt(ConstantValue.Password_EncryptionKey, cleanText);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            return Decrypt(ConstantValue.Password_EncryptionKey, encryptedPassword);
        }


        /// <summary>
        /// Encrypt the passing string using Advanced Encryption Standrad
        /// </summary>
        /// <param name="clearText">Normal String</param>
        /// <returns>Encrypted String</returns>
        public string Encrypt(string encryptionKey, string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Decrypt the passing encrypted string
        /// </summary>
        /// <param name="cipherText">Encrypted String</param>
        /// <returns>Decrypted String</returns>
        public string Decrypt(string encryptedKey, string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptedKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
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
    }
}
