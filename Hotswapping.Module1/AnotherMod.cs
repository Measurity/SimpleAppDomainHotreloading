using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotswapping.ModApi;

namespace Hotswapping.Module1
{
    class AnotherMod : IMod
    {
        public void Init()
        {
            Console.WriteLine("AnotherMod also has been initialized.");
        }
    }
}
