using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global
{
    public Global()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public static model.db.user loggedInUserAsAdmin;

    public static int getUserId()
    {
        model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
        return u._uid;
    }
    public static string trimHTML(string str)
    {
        try
        {
            //string pattern = @"<(.|\n)*?>";
            //string pattern = @"<[^/bp][^>]*>|<p[a-z][^>]*>|<b[^r][^>]*>|<br[a-z][^>]*>|</[^bp]+>|</p[a-z]+>|</b[^r]+>|</br[a-z]+>";
            //string pattern = @"(\<)(?!img(\s|\/|\>))(.*?\>)";

            //string pattern = @"{<(?!i|b|h[1-6]|/i|/b|/h[1-6][\s|>|/])[^>]*>}";
            //return Regex.Replace(str, pattern, string.Empty);
            //return Regex.Replace(str, pattern, string.Empty);
            //return Regex.Replace(str, pattern, string.Empty);

            //return Global.RemoveHTMLTag(str, "font", "");
            //str = str.Replace("<p ", "<p style='text-align: justify;text-justify: inter-word;'");
            return str.Replace("font-family", "");


            //return Regex.Replace(str, "<style>(.|\n)*?</style>", string.Empty);
        }
        catch (Exception ex)
        {
            Log.logErr("Global.trimHTML", ex);
        }
        return null;
    }


    public static string trimAllHTMLTags(string str)
    {
        try
        {
            string pattern = @"<(.|\n)*?>";
            //string pattern = @"<[^/bp][^>]*>|<p[a-z][^>]*>|<b[^r][^>]*>|<br[a-z][^>]*>|</[^bp]+>|</p[a-z]+>|</b[^r]+>|</br[a-z]+>";
            //string pattern = @"(\<)(?!img(\s|\/|\>))(.*?\>)";

            //string pattern = @"{<(?!i|b|h[1-6]|/i|/b|/h[1-6][\s|>|/])[^>]*>}";
            //return Regex.Replace(str, pattern, string.Empty);
            //return Regex.Replace(str, pattern, string.Empty);
            return Regex.Replace(str, pattern, string.Empty);

            //return Global.RemoveHTMLTag(str, "font", "");



            //return Regex.Replace(str, "<style>(.|\n)*?</style>", string.Empty);
        }
        catch (Exception ex)
        {
            Log.logErr("Global.trimHTML", ex);
        }
        return null;
    }

    public static String RemoveHTMLTag(String html, String startTag, String endTag)
    {
        Boolean bAgain;
        do
        {
            bAgain = false;
            Int32 startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
            if (startTagPos < 0)
                continue;
            Int32 endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
            if (endTagPos <= startTagPos)
                continue;
            html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
            bAgain = true;
        } while (bAgain);
        return html;
    }

    public static bool deleteFile(string filePath)
    {
        try
        {

            FileInfo fi = new FileInfo(filePath);
            fi.Delete();
            return true;
        }
        catch (Exception ex)
        {
            Log.logErr("Global.deleteFile", ex);
        }
        return false;
    }

    public static bool clearFolder(string FolderName, bool deleteFolder)
    {
        try
        {

            DirectoryInfo dir = new DirectoryInfo(FolderName);
            if (dir.Exists == false)
                return true;
            if (dir != null)
            {
                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.Delete();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    clearFolder(di.FullName, true);
                    di.Delete();
                }
                if (deleteFolder)
                    dir.Delete();
            }
            return true;
        }
        catch (Exception ex)
        {
            Log.logErr("Global.clearFolder", ex);
        }
        return false;
    }

    public static string getPartOfString(string str, int numOfCharacters)
    {
        try
        {
            //if (str.Length > 0)
            //{
            //    if (str.Length > numOfCharacters)
            str = str.Substring(0, numOfCharacters) + "..";
            //    else
            //        res = str;
            //}
        }
        catch (Exception ex)
        {
            //Log.logErr("Global.getPartOfString", ex);
        }
        return str;
    }

    public static String Encode(String text)
    {
        System.Security.Cryptography.SHA512 sha = System.Security.Cryptography.SHA512.Create();
        return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.Unicode.GetBytes(text)));
    }

    public static bool isValidEmail(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static String getLangFromSession() {
        return "en";
    }

    public static String getSessionPassword()
    {
        /**
         * Added By A.Sous 19-5-2014
         * Reason: To generate session password for transfer user name and password
         * Password is 4 chars Randomally: Arabic,En:Cabotal, En:Small, and number
         * Number code:         [48,57]
         * Arabic:              [1569,1610] start: 1578
         * English Capital:     [64,90]
         * English Small  :     [97,122]
         * 
         * */

        Random rnd = new Random();
        int firstNumber = -1;
        int secondNumber = -1;
        int thirdNumber = -1;
        int fourthNumber = -1;
        firstNumber = rnd.Next(4);
        while (secondNumber == firstNumber || secondNumber == -1)
            secondNumber = rnd.Next(4);
        while (thirdNumber == firstNumber || thirdNumber == secondNumber || thirdNumber == -1)
            thirdNumber = rnd.Next(4);
        fourthNumber = 6 - (firstNumber + secondNumber + thirdNumber);
        int[] a = { firstNumber, secondNumber, thirdNumber, fourthNumber };
        String str = "";
        for (int i = 0; i < a.Length; i++)
        {
            int curr = a[i];
            if (curr == 0)
            {
                str += (char)(rnd.Next(9) + 48);//Number
            }
            else if (curr == 1)
            {
                str += (char)(rnd.Next(27) + 63);//Capital
            }
            else if (curr == 2)
            {
                str += (char)(rnd.Next(27) + 97);//Small
            }
            else if (curr == 3)
            {//Arabic
                int num = -1;
                while (num == -1 || (num >= 1595 && num <= 1600))
                    num = rnd.Next(51) + 1578;

                str += (char)num;
            }
            else
                str += "" + i;
        }
        return str;
    }
}



public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}

public class PasswordAdvisor
{
    public static PasswordScore CheckStrength(string password)
    {
        int score = 1;

        if (password.Length < 1)
            return PasswordScore.Blank;
        if (password.Length < 4)
            return PasswordScore.VeryWeak;

        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (Regex.IsMatch(password, @"/\d+/", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"/[a-z]/", RegexOptions.ECMAScript) &&
            Regex.IsMatch(password, @"/[A-Z]/", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript))
            score++;

        return (PasswordScore)score;
    }
}


public static class StringCipher
{
    // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    private const string initVector = "tu89geji340t89u2";

    // This constant is used to determine the keysize of the encryption algorithm.
    private const int keysize = 256;

    private const string password = "sous";
    public static string Encrypt(string plainText, string passPhrase = password)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }

    public static string Decrypt(string cipherText, string passPhrase = password)
    {
        try
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        catch (Exception ex)
        {
            Log.logErr("StringCypher.Decrypt", ex);
            return cipherText;
        }
    }
}
