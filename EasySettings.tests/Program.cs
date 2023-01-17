using System;

namespace EasySettings.tests
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(Settings.path);
			Settings.Load();
			Console.WriteLine(Settings.GetValueOfKey("funny.more"));
            Console.WriteLine(Settings.GetValueOfKey("funny.test"));
            Console.WriteLine(Settings.GetValueOfKey("test"));
            Settings.AddValueOfKey("funny.test","Cool");
			Settings.AddValueOfKey("test", "Cool");
			Settings.Save();
			Console.WriteLine(Settings.GetValueOfKey("test"));
			Console.ReadKey();
		}
	}
}
