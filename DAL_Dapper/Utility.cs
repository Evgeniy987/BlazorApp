using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class Utility
{
    public static string Encrypt(string password)
    {
        var provider = MD5.Create();
        string salt = "E7m3R@nssd0mGtkPepper";
        byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}
