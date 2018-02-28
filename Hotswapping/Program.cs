using System;
using Hotswapping.Reloader;

namespace Hotswapping
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var manager = ModManager.Create();

            while (true)
            {
                Console.WriteLine("Waiting for input.. (press 'q' to quit)");
                var key = Console.ReadKey(true).KeyChar;
                if (key == 'q') return;

                manager.Reload();
                PrintModules(manager);
            }
        }

        private static void PrintModules(ModManager manager)
        {
            Console.WriteLine("\tAll modules:");
            foreach (var module in manager.GetModModules())
            foreach (var mod in module.ModEntries)
                mod.Init();
        }
    }
}