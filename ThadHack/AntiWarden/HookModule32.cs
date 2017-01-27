using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GreyMagic.Internals;
using ZzukBot.Constants;
using ZzukBot.Mem;
using ZzukBot.Properties;

namespace ZzukBot.AntiWarden
{
    internal static class HookModule32
    {
        /// <summary>
        ///     Delegate and Detour for our win32 functions
        /// </summary>
        private static _Module32First _module32FirstDelegate;

        private static _Module32Next _module32NextDelegate;
        private static Detour _module32FirstHook;
        private static Detour _module32NextHook;

        private static List<string> modules;

        /// <summary>
        ///     Setup the detour
        /// </summary>
        internal static void Init()
        {
            modules =
                new List<string>(Resources.ModulesOfWoW.Split(new[] {Environment.NewLine}, StringSplitOptions.None));

            var handle = WinImports.GetModuleHandle("kernel32.dll");
            var firstAddr = WinImports.GetProcAddress(handle, "Module32First");
            Console.WriteLine("Module32First: " + firstAddr.ToString("X"));
            var nextAddr = WinImports.GetProcAddress(handle, "Module32Next");

            //Registering a delegate pointing to the function we want to detour
            _module32FirstDelegate =
                Memory.Reader.RegisterDelegate<_Module32First>(firstAddr);
            _module32FirstHook =
                Memory.Reader.Detours.CreateAndApply(
                    _module32FirstDelegate, // The delegate pointing to the function we want to hook 
                    new _Module32First(Module32FirstDetour),
                    // the detour which will be called from the function we hooked
                    "Module32First"); // the name of detour

            // Registering a delegate pointing to the function we want to detour
            _module32NextDelegate =
                Memory.Reader.RegisterDelegate<_Module32Next>(nextAddr);
            _module32NextHook =
                Memory.Reader.Detours.CreateAndApply(
                    _module32NextDelegate, // The delegate pointing to the function we want to hook 
                    new _Module32Next(Module32NextDetour),
                    // the detour which will be called from the function we hooked
                    "Module32Next"); // the name of detour
        }

        /// <summary>
        ///     Detour for Module32First
        /// </summary>
        internal static bool Module32FirstDetour(IntPtr snapshot, ref WinImports.MODULEENTRY32 module)
        {
            _module32FirstHook.Remove();
            var ret = WinImports.Module32First(snapshot, ref module);
            _module32FirstHook.Apply();
            return ret;
        }

        /// <summary>
        ///     Detour for Module32Next
        /// </summary>
        internal static bool Module32NextDetour(IntPtr snapshot, ref WinImports.MODULEENTRY32 module)
        {
            _module32NextHook.Remove();
            var ret = WinImports.Module32Next(snapshot, ref module);
            _module32NextHook.Apply();
            while (!modules.Contains(module.szModule.ToLower()) && ret)
            {
                _module32NextHook.Remove();
                ret = WinImports.Module32Next(snapshot, ref module);
                _module32NextHook.Apply();
            }
            if (!ret)
            {
                if (!modules.Contains(module.szModule.ToLower()))
                {
                    module = new WinImports.MODULEENTRY32 {dwSize = 548};
                }
                WinImports.SetLastError(18);
            }


            return ret;
        }

        /// <summary>
        ///     Function pointers to Module32First/Next
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate bool _Module32First(IntPtr snapshot, ref WinImports.MODULEENTRY32 module);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate bool _Module32Next(IntPtr snapshot, ref WinImports.MODULEENTRY32 module);
    }
}