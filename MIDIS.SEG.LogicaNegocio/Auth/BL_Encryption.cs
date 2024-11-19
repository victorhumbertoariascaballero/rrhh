using System;
using System.Text;
using MIDIS.SEG.AccesoDatosSQL;
using MIDIS.ORI.Entidades;
using System.IO;
using System.Security.Cryptography;
using MIDIS.ORI.Entidades.Auth;

namespace MIDIS.ORI.LogicaNegocio
{
    public class BL_Encryption
    {
        public static string EncryptBase64(string text)
        {
            return Convert.ToBase64String(
                    Encoding.Unicode.GetBytes(text));
        }

        public static string DecryptBase64(string text)
        {
            return Encoding.Unicode.GetString(
                     Convert.FromBase64String(text));
        }

        public static String encryptText(string textToEncrypt)
        {

            String result = null;

            byte[] cipherText = null;
            try
            {
                DA_Authentication da = new DA_Authentication();
                BE_EncryptionAttribs encriptionDatos = null;
                encriptionDatos = da.obtenerDatosTipoEncriptacion(new BE_EncryptionAttribs() { vTipoEncriptacion = "AES" });

                byte[] rawPlaintext = System.Text.Encoding.Unicode.GetBytes(textToEncrypt);

                byte[] plainText = null;
                using (Aes aes = new AesManaged())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = encriptionDatos.iKeySizeInBits;
                    int KeyStrengthInBytes = aes.KeySize / 8;
                    System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                        new System.Security.Cryptography.Rfc2898DeriveBytes(encriptionDatos.vClaveEncriptacion, encriptionDatos.vbSalt, encriptionDatos.iKeygenIterations);
                    aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                    aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(rawPlaintext, 0, rawPlaintext.Length);
                        }
                        cipherText = ms.ToArray();
                    }

                }
                //string s = System.Text.Encoding.Unicode.GetString(plainText);
                //return System.Text.Encoding.UTF8.GetString(cipherText);
                result = Convert.ToBase64String(cipherText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        
        public static String desencryptText(string textToDesencrypt)
        {

            string result;
            try
            {
                DA_Authentication da = new DA_Authentication();
                BE_EncryptionAttribs encriptionDatos = null;
                encriptionDatos = da.obtenerDatosTipoEncriptacion(new BE_EncryptionAttribs() { vTipoEncriptacion = "AES" });
                
                byte[] cipherText = Convert.FromBase64String(textToDesencrypt);

                byte[] plainText = null;
                using (Aes aes = new AesManaged())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = encriptionDatos.iKeySizeInBits;
                    int KeyStrengthInBytes = aes.KeySize / 8;
                    System.Security.Cryptography.Rfc2898DeriveBytes rfc2898 =
                        new System.Security.Cryptography.Rfc2898DeriveBytes(encriptionDatos.vClaveEncriptacion, encriptionDatos.vbSalt, encriptionDatos.iKeygenIterations);
                    aes.Key = rfc2898.GetBytes(KeyStrengthInBytes);
                    aes.IV = rfc2898.GetBytes(KeyStrengthInBytes);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherText, 0, cipherText.Length);
                        }
                        plainText = ms.ToArray();
                    }

                }
                result= System.Text.Encoding.Unicode.GetString(plainText);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
