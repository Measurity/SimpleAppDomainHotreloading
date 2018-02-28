using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotswapping.ModApi;

namespace Hotswapping.Module1
{
    class AMod : IMod
    {
        public void Init()
        {
            Console.WriteLine("A mod has initialized..");
        }
    }
}
