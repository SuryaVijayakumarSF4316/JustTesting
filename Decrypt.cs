using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rijndael256;
using System.Security.Cryptography;
using System.Text;


namespace Assignment
{
    public class Decrypt
    {
        public string DecryptFile(string ToDecrypt){
            //ToDecrypt="HfDj69O0CjXLjxT84uqedXXsdDarAJKVVkogKLoymJWjllkbjVrvz5+ZYfxnmaHbnotbQLCVEC41MS2efM/4RNQGgrRNoNPXUgXXiAJa3l2PXFjCi3RJIcaHH/1MzpBXf2RMsWCNkL5tnrxpN7XRy2mvBkG0KtjltqWnInkG4rwIPr993CAP0BFQkXyeq/fCGIE4sOdUyrprnJ7mBXdBTmR+v4hFq6PO5K7wqPYeup4n2YGSgKP0I6HQ3Xda/Wyau3BFmlD+z5qOFAwrpE/lTVIat4T/N2Y+BTh5UXvb5xC+YnecZ73yOqM3MOIigllVpsLYVOMUYq/L3IF1TGioTtn5V7TDe2M6AiK9groFlthEJAumrjPl+Uz1d3w6kRVE98luzhyyeSoi/EugErZKQdBeHkkAO4QTuMmYRTNAqRqwuxNQHoDrsOzYYBwJ+5lL7JS/+rTZNjIQFM5M/MAGSyFTjswxeQ7/UiBkXGza8a7B92vydWzuotJ7n9CBxHduJ0cs9YfvPfw6GqPu5egWDOdKgc8otHoWRk3xlLcqfKuSlDeE9uiR3AfgC5m6+eHCZW+6R5RpRSw2kVES8iOXSBbQ+PE9vHqFRuE5cxB30JZCd1G/M6b2NyTs9wR6204lHwwb0aarqSVG5cuel+N34W2jUO+AtpdrNA8dC/HHMLDVSXhpiOAcUmvFH0jiMMIJmQyDEbnDRuq/2OmZstehXOxbfeu4oR82c7B1SfMl3YQ/VmGFIRdCoCC4oALA3EAk5Iku+IPs8BzoCun2Cv61xNhOx3Qz+GaQ3aM3Ab4sHMBICtw2kguFylhAQHJt+aPr1TAIlCOyE5J0+16A59WZbYW59sHUiENU2ykJHy8iUV4woVUmEQmNX/5D1hQm3O58jmYcfvZTVwzSdaVhPWnvVhLN4xvLAnXvoupa55333ROW4EeC6Bt7r3RoGgnJ2OC23Yfn+uYlVE30tDJqHAM414ePgB7NVCOvzi+lGteDaprTpZ70v9gaqgjDFmzg4VAkQZ/TDVEmRwuqC7ZWo8zHRQ==";
             string PlainText=String.Empty;
            try{
                if(string.IsNullOrEmpty(ToDecrypt)){
                    System.Console.WriteLine("Should not be null");
                }
                using RijndaelManaged  AES = new();
                byte[] encryptedData = Convert.FromBase64String(ToDecrypt);
                byte[] plainTextBytes = new byte[encryptedData.Length];
                PasswordDeriveBytes secretKey = GetSecretKey();
                ICryptoTransform decryptor = AES.CreateDecryptor(secretKey.GetBytes(16), secretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(encryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                int decryptedCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                PlainText = Encoding.ASCII.GetString(plainTextBytes, 0, decryptedCount);
                return PlainText;
            }
            catch (Exception ex)
            {
                return "null";
            }
        } 
        private static PasswordDeriveBytes GetSecretKey()
        {
            string password = "a1c2e3g4i5k6m7o8q9s0u9w8y7";
            byte[] salt  = System.Text.Encoding.ASCII.GetBytes(password.Length.ToString());

            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(password, salt);

            return secretKey;
        }
    }
}