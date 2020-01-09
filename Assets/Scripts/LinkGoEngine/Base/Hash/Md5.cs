using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LinkGo.Base.Hash
{
    public sealed class Md5
    {
        private static readonly StringBuilder s_StrBuilder = new StringBuilder();

        /// <summary>
        /// ��ȡ�ļ���MD5��  
        /// </summary>
        /// <param name="input">Ҫ����MD5���ַ���</param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            s_StrBuilder.Length = 0;
            // Convert the byte array to hexadecimal string
            for (int i = 0; i < hashBytes.Length; i++)
            {
                s_StrBuilder.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return s_StrBuilder.ToString();
        }

        /// <summary>  
        /// ��ȡ�ļ���MD5��  
        /// </summary>  
        /// <param name="fileName">������ļ�������·������׺����</param>  
        /// <returns></returns>  
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                s_StrBuilder.Length = 0;
                for (int i = 0; i < retVal.Length; i++)
                {
                    s_StrBuilder.Append(retVal[i].ToString("x2"));
                }
                return s_StrBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
    }
}
