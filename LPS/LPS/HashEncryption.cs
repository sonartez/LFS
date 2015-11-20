using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

namespace LPS.Core
{
    /// <summary>
    /// Summary description for HashEncryption
    /// </summary>
    public class HashEncryption
    {
        public HashEncryption()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string MD5Hash(string Data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>   
        /// Encrypts a string using the SHA256 (Secure Hash Algorithm) algorithm.   
        /// This works in the same manner as MD5, providing however 256bit encryption.   
        /// </summary>   
        /// <param name="Data">A string containing the data to encrypt.</param>   
        /// <returns>A string containing the string, encrypted with the SHA256 algorithm.</returns>   
        public string SHA256Hash(string Data)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>   
        /// Encrypts a string using the SHA384(Secure Hash Algorithm) algorithm.   
        /// This works in the same manner as MD5, providing 384bit encryption.   
        /// </summary>   
        /// <param name="Data">A string containing the data to encrypt.</param>   
        /// <returns>A string containing the string, encrypted with the SHA384 algorithm.</returns>   
        public string SHA384Hash(string Data)
        {
            SHA384 sha = new SHA384Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }


        /// <summary>   
        /// Encrypts a string using the SHA512 (Secure Hash Algorithm) algorithm.   
        /// This works in the same manner as MD5, providing 512bit encryption.   
        /// </summary>   
        /// <param name="Data">A string containing the data to encrypt.</param>   
        /// <returns>A string containing the string, encrypted with the SHA512 algorithm.</returns>   
        public string SHA512Hash(string Data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(Data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();

        }




        //Ma hoa dung  HashMD5 và TripleDES
        public string Encrypt(string i_strEncrypt, bool i_blUseHashing)
        {
            byte[] v_arrKey;
            byte[] v_arrEncrypt = new byte[(int)i_strEncrypt.Length];
            try
            {
                v_arrEncrypt = UTF8Encoding.UTF8.GetBytes(i_strEncrypt);
            }
            catch (Exception v_ex)
            {
                throw v_ex;
            }
            string v_strKey = "fatherofbill";
            if (i_blUseHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                v_arrKey = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(v_strKey));
                hashmd5.Clear();
            }
            else
            {
                v_arrKey = UTF8Encoding.UTF8.GetBytes(v_strKey);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = v_arrKey;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(v_arrEncrypt, 0, v_arrEncrypt.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //Giai ma  HashMD5 và TripleDES
        public string Decrypt(string cipherString, bool i_blUseHashing)
        {
            byte[] v_arrKey;
            byte[] v_arrEncrypt = new byte[(int)cipherString.Length];
            try
            {
                v_arrEncrypt = Convert.FromBase64String(cipherString);
            }
            catch (Exception v_ex)
            {
                throw v_ex;
            }
            string v_strKey = "fatherofbill";
            if (i_blUseHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                v_arrKey = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(v_strKey));
                hashmd5.Clear();
            }
            else
            {
                v_arrKey = UTF8Encoding.UTF8.GetBytes(v_strKey);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = v_arrKey;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(v_arrEncrypt, 0, v_arrEncrypt.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}