using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hotswapping.Reloader
{
    public class ModManager
    {
        private Dictionary<string, ModContext> Mods { get; }

        private ModManager()
        {
            Mods = new Dictionary<string, ModContext>();
        }

        public static ModManager Create(bool loaded = false)
        {
            ModManager manager = new ModManager();

            foreach (var dir in Directory.GetDirectories("Mods"))
            {
                var modDll = Directory.GetFiles(dir, "*.dll").First();
                var modStub = ModContext.Create(modDll);
                manager.Mods.Add(Path.GetFileName(dir), modStub);
            }

            if (loaded)
            {
                manager.Reload();
            }

            return manager;
        }

        public void Reload()
        {
            foreach (var mod in Mods)
            {
                mod.Value.Reload();
            }
        }

        public IEnumerable<ModContext> GetModModules()
        {
            return Mods.Values;
        }
    }
}
