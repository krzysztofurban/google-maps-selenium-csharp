using System.IO;
using System.Text.RegularExpressions;

namespace SeleniumGoogleMapsExample.Test.e2e.config
{
    class SecurePathUtils
    {
        public static string secureWindowsPath(string illegalPath)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            string result = r.Replace(illegalPath, "");
            return result;
        }
    }
}
