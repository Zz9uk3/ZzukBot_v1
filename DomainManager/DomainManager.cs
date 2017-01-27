using System;

namespace DomainManager
{
    public static class DomainManager
    {
        public static AppDomain CurrentDomain { get; set; }
        //public static ClassicFrameworkAssemblyLoader
        public static dynamic CurrentAssemblyLoader { get; set; }
    }
}