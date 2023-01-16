using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySettings
{
	public class Settings
	{
		public static string path = "settings.cfg";
		private static Dictionary<string,string> Dict = new Dictionary<string,string>();
		/// <summary>
		/// Loads all settings from <c>Settings.path</c>
		/// </summary>
		public static void Load()
		{
			if (!File.Exists(path))
			{
				return;
			}
			string nestation = "";
			foreach (string item in File.ReadAllLines(path))
			{
				if (item.StartsWith("["))
				{
					nestation = item.Substring(1).TrimEnd(']');
				}
				else if (!item.StartsWith("#") && item.Contains("="))
				{
					string name = nestation + "." + item.Split('=')[0];
					if (nestation == "")
					{
						name = item.Split('=')[0];
					}
					string value = item.Substring(item.Split('=')[0].Length + 1);
					Dict.Add(name, value);
				}
			}
		}
		/// <summary>
		/// Gets the value by the name
		/// </summary>
		/// <param name="name">name of the value</param>
		/// <returns>value as string or null if value does not exist</returns>
		public static string GetValueOfKey(string name)
		{
			if (!Dict.ContainsKey(name))
			{
				return null;
			}
			return Dict[name];
		}
		/// <summary>
		/// replaces the value of <c>name</c> by <c>value</c> but if <c>name</c> thoes not exist its added
		/// </summary>
		/// <param name="name">name of the value</param>
		/// <param name="value">value you want to set</param>
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
		/// <summary>
		/// Saves all values to <c>Settings.path</c>
		/// </summary>
		public static void Save()
		{
			List<string> lines = null;
			if (File.Exists(path))
			{
				lines = new List<string>(File.ReadAllLines(path));
			}
			List<string> settings = GetKeysOfDictAsAList(Dict);

			if (lines != null)
			{
				string nestation = "";
				for (int i = 0; i < lines.Count; i++)
				{
					string item = lines[i];

					if (!item.StartsWith("#") && item.Contains("="))
					{
						string name = nestation + item.Split('=')[0];
						settings.Remove(name);
						lines[i] = item.Split('=')[0] + "=" + GetValueOfKey(name);
					}
					else if (item.StartsWith("["))
					{
						nestation += item.Substring(1).TrimEnd(']');
					}
				}
			}
			else
			{
				lines = new List<string>();
			}
			foreach (string item in settings)
			{
				string value = GetValueOfKey(item);
				if (value == null)
				{
					value = "";
				}
				lines.Add(item + "=" + value);
			}
			File.WriteAllLines(path, lines);
		}
	}
}
