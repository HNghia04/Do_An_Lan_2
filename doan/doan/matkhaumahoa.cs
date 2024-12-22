using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace doan
{
    internal class matkhaumahoa
    {
        // Khóa mã hóa mặc định (có thể thay đổi)
        private static string key = "haiconcho";  // Đặt khóa mặc định của bạn ở đây

        // Phương thức mã hóa
        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EnsureKeyLength(key)); // Đảm bảo khóa có độ dài 16 byte
                aesAlg.IV = new byte[16]; // IV mặc định (có thể thay đổi theo yêu cầu)

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encrypted = PerformCryptography(Encoding.UTF8.GetBytes(plainText), encryptor);
                return Convert.ToBase64String(encrypted);
            }
        }

        // Phương thức giải mã
        public static string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EnsureKeyLength(key));  // Đảm bảo khóa có độ dài 16 byte
                aesAlg.IV = new byte[16];  // IV mặc định

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] decrypted = PerformCryptography(Convert.FromBase64String(cipherText), decryptor);
                return Encoding.UTF8.GetString(decrypted);
            }
        }

        // Phương thức thực hiện mã hóa hoặc giải mã
        private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        // Phương thức thay đổi khóa
        public static void ChangeEncryptionKey(string newKey)
        {
            key = newKey;
        }

        // Phương thức lấy khóa mã hóa hiện tại
        public static string GetEncryptionKey()
        {
            return key;
        }

        // Phương thức đảm bảo khóa có độ dài 16 byte (128 bit)
        private static string EnsureKeyLength(string inputKey)
        {
            if (inputKey.Length < 16)
            {
                inputKey = inputKey.PadRight(16, '0');  // Nếu khóa ngắn hơn 16 byte, thêm số 0 vào cuối
            }
            else if (inputKey.Length > 16)
            {
                inputKey = inputKey.Substring(0, 16);  // Nếu khóa dài hơn 16 byte, cắt bớt
            }
            return inputKey;
        }
    }
}
