using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;

namespace EasySettings
{
    public class Settings
    {
        public static string file = "settings.cfg";
        private static Dictionary<string,string> Dict = new Dictionary<string,string>();
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
                    Dict.Add(name, value);
                }
            }
        }
        public static string GetValueOfKey(string name)
        {
            if (!Dict.ContainsKey(name))
            {
                return null;
            }
            return Dict[name];
        }
        public static void AddValueOfKey(string name, string value)
        {
            if (Dict.ContainsKey(name))
            {
                Dict[name] = value;
                return;
            }
            Dict.Add(name, value);
        }
        private static List<string> GetKeysOfDictAsAList(Dictionary<string,string> dict)
        {
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string,string> item in dict)
            {
                keys.Add(item.Key);
            }
            return keys;
        }
        public static void Save()
        {
            List<string> lines = new List<string>(File.ReadAllLines(file));
            List<string> settings = GetKeysOfDictAsAList(Dict);
            for (int i = 0; i < lines.Count; i++)
            {
                string item = lines[i];
                if (!item.StartsWith("#"))
                {
                    string name = item.Split('=')[0];
                    settings.Remove(name);
                    lines[i] = name+"="+GetValueOfKey(name);
                }
            }
            foreach (string item in settings)
            {
                lines.Add(item + "=" + GetValueOfKey(item));
            }
            File.WriteAllLines(file, lines);
        }
    }
}
