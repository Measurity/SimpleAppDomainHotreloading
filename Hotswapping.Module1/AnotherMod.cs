using System;
using Hotswapping.ModApi;

namespace Hotswapping.Module1
{
    internal class AnotherMod : IMod
    {
        public void Init()
        {
            Console.WriteLine("AnotherMod also has been initialized.");
        }
    }
}