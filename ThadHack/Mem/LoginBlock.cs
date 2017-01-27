using System;
using System.Reflection;
using System.Runtime.InteropServices;
using funcs = ZzukBot.Constants.Offsets.Functions;

namespace ZzukBot.Mem
{
    internal static class LoginBlock
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void Enable()
        {
            Memory.Reader.WriteBytes(funcs.DefaultServerLogin, new byte[] {0xc3});
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void Disable()
        {
            Memory.Reader.WriteBytes(funcs.DefaultServerLogin, new byte[] {0x56});
        }
    }
}