using System;
using System.IO;
using System.Reflection;
using ZzukBot.Constants;
using ZzukBot.Properties;
using ZzukBot.Settings;

namespace ZzukBot.Mem
{
    internal static class Libs
    {
#if DEBUG
        internal const string FastCall = "FastCallDll.dll";
        internal const string Navigation = "Navigation.dll";
        private static readonly string PathToLibs = Paths.Internal + "\\";
#else
        internal const string FastCall = "037.mmap";
        internal const string Navigation = "038.mmap";
        private static readonly string PathToLibs = Paths.Internal + "\\mmaps\\";
#endif

        private static readonly string pathFastCall = PathToLibs + FastCall;
        private static readonly string pathNavigation = PathToLibs + Navigation;

        private static IntPtr FastCallPtr = IntPtr.Zero;
        private static IntPtr NavPtr = IntPtr.Zero;

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void InjectFastcall()
        {
            if (FastCallPtr == IntPtr.Zero)
            {
                if (!File.Exists(pathFastCall))
                    File.WriteAllBytes(pathFastCall, Resources.FastCallDll);
                FastCallPtr = WinImports.LoadLibrary(pathFastCall);
            }
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void Clear()
        {
            File.Delete(pathFastCall);
            File.Delete(pathNavigation);
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void ReloadNav()
        {
            if (NavPtr == IntPtr.Zero)
            {
                if (!File.Exists(pathNavigation))
                    File.WriteAllBytes(pathNavigation, Resources.Navigation);
                NavPtr = WinImports.LoadLibrary(pathNavigation);
            }
        }
    }
}