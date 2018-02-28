using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hotswapping.ModApi.Marshalling
{
    public class DomainMediator : MarshalByRefObject
    {
        public DomainMediator()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Debug.WriteLine(args.Name);
            return Assembly.GetExecutingAssembly();
        }

        public void LoadAssembly(string dllPath)
        {
            Assembly.LoadFrom(Path.GetFullPath(dllPath));
        }

        public List<IMod> GetMods()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => typeof(IMod).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && t != typeof(ModMarshallingWrapper))
                .Select(t => (IMod)Activator.CreateInstance(typeof(ModMarshallingWrapper), Activator.CreateInstance(t))).ToList();
        }
    }
}
