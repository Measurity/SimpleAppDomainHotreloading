using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Hotswapping.ModApi;
using Hotswapping.Reloader;

namespace Hotswapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = ModManager.Create();

            while (true)
            {
                Console.WriteLine("Waiting for input.. (press 'q' to quit)");
                char key = Console.ReadKey(true).KeyChar;
                if (key == 'q')
                {
                    return;
                }

                manager.Reload();
                PrintModules(manager);
            }
        }

        static void PrintModules(ModManager manager)
        {
            Console.WriteLine("\tAll modules:");
            foreach (var module in manager.GetModModules())
            {
                foreach (var mod in module.ModEntries)
                {
                    mod.Init();
                }
            }
        }
    }
}
