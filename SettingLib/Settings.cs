using System;
using System.Collections.Generic;
using System.IO;

namespace EasySettings
{
    public class Settings
    {
        public static string file = "settings.cfg";
        private static Dictionary<string,string> dict = new Dictionary<string,string>();
        public static void Load()
        {
            if (!File.Exists(file))
            {
                return;
            }
            foreach (string item in File.ReadAllLines(file))
            {
                if (!item.StartsWith("#"))
                {
                    string name = item.Split('=')[0];
                    string value = item.Substring(name.Length + 1);
                    dict.Add(name, value);
                }
            }
        }
        public static string GetValueOfKey(string name)
        {
            if (!dict.ContainsKey(name))
            {
                return null;
            }
            return dict[name];
        }
    }
}
