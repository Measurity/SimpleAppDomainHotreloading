using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using Hotswapping.ModApi;
using Hotswapping.ModApi.Marshalling;

namespace Hotswapping.Reloader
{
    public class ModContext : IDisposable
    {
        private AppDomain domain;
        private DomainMediator proxy;
        private List<IMod> modEntries;

        public ICollection<IMod> ModEntries => modEntries == null || !modEntries.Any() ? (modEntries = proxy.GetMods()) : modEntries;

        public string FileName { get; }

        public ModContext(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Cannot find .dll file for mod.", fileName);
            }

            FileName = fileName;
        }

        public static ModContext Create(string modDllPath)
        {
            ModContext sandbox = new ModContext(modDllPath);

            return sandbox;
        }

        public void Reload()
        {
            modEntries = null;
            if (domain != null)
            {
                AppDomain.Unload(domain);
                domain = null;
            }

            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = Environment.CurrentDirectory;
            setup.DisallowCodeDownload = true;
            setup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            setup.ShadowCopyFiles = true.ToString().ToLowerInvariant();
            
            domain = AppDomain.CreateDomain(Directory.GetParent(FileName).Name, AppDomain.CurrentDomain.Evidence, setup);

            proxy =
                (DomainMediator)domain.CreateInstanceAndUnwrap(typeof(DomainMediator).Assembly.FullName,
                    typeof(DomainMediator).FullName);

            proxy.LoadAssembly(FileName);
        }

        public void Dispose()
        {
            AppDomain.Unload(domain);
        }
    }
}
