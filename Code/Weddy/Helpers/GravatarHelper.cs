using System.Security.Cryptography;
using System.Web.Mvc;

namespace Weddy.Helpers
{
    public static class GravatarHelper
    {
        public static string Gravatar(this HtmlHelper helper, string email, int size)
        {
            const string result = "<img src=\"{0}\" alt=\"Gravatar\" class=\"gravatar\" />";
            var url = GetGravatarUrl(email, size);
            return string.Format(result, url);
        }

        public static string GetGravatarUrl(string email, int size)
        {
            return (string.Format("http://www.gravatar.com/avatar/{0}?s={1}&r=PG",
                        EncryptMD5(email), size.ToString()));
        }

        public static string GetGravatarUrl(string email, int size, string defaultImagePath)
        {
            return GetGravatarUrl(email, size) + string.Format("&default={0}",
                       defaultImagePath);
        }

        private static string EncryptMD5(string Value)
        {
            var md5 = new MD5CryptoServiceProvider();
            var valueArray = System.Text.Encoding.ASCII.GetBytes(Value);
            valueArray = md5.ComputeHash(valueArray);
            var encrypted = "";
            for (var i = 0; i < valueArray.Length; i++)
                encrypted += valueArray[i].ToString("x2").ToLower();
            return encrypted;
        }
    }
}