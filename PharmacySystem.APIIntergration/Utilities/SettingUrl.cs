using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration.Utilities
{
    public class SettingUrl
    {
        public static Dictionary<String, String> GetPort()
        {
            string fileName = "\\Ports/Port.txt";
            string currentPath = Directory.GetCurrentDirectory() + fileName;
            Dictionary<String, String> MySettings = File
                                                .ReadLines(currentPath)
                                                .ToDictionary(line => line.Substring(0, line.IndexOf('=')).Trim(),
                                                line => line.Substring(line.IndexOf('=') + 1).Trim());
            return MySettings;
        }
        public static string GetAddress()
        {
            Dictionary<String, String> settings = GetPort();
            return settings["http"] + "://" + settings["domain"] + ":" + settings["port"] + "/";
        }
    }
}
