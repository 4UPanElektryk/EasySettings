﻿using System;

namespace EasySettings.tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Settings.file);
            Settings.Load();
            Console.WriteLine(Settings.GetValueOfKey("test"));
            Console.ReadKey();
        }
    }
}