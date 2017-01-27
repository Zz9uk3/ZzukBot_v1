using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ZzukBot.Constants
{
    internal static class WinImports
    {
        [DllImport("kernel32.dll")]
        internal static extern UIntPtr VirtualQuery(
            UIntPtr lpAddress,
            out MEMORY_BASIC_INFORMATION lpBuffer,
            UIntPtr dwLength
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateToolhelp32Snapshot(int flags, uint th32ProcessID);


        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORY_BASIC_INFORMATION
        {
            public UIntPtr BaseAddress;
            public UIntPtr AllocationBase;
            public uint AllocationProtect;
            public UIntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [DllImport("user32.dll")]
        internal static extern int SendMessage(
            int hWnd, // handle to destination window
            uint Msg, // message
            int wParam, // first message parameter
            int lParam // second message parameter
            );

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        ///     Module32First
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        /// <summary>
        ///     Module32Next
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

        /// <summary>
        ///     Get address of a function from a dll loaded inside the process (handle)
        /// </summary>
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        /// <summary>
        ///     Get Module handle for a loaded dll
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("Kernel32")]
        internal static extern void AllocConsole();

        [DllImport("Kernel32")]
        internal static extern void FreeConsole();

        [DllImport("kernel32.dll")]
        internal static extern bool CreateProcess(string lpApplicationName,
            string lpCommandLine, IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles, ProcessCreationFlags dwCreationFlags,
            IntPtr lpEnvironment, string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        internal static extern uint ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        internal static extern uint SuspendThread(IntPtr hThread);

        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void SetLastError(uint dwErrCode);


        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        internal static extern int CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int Msg, int wParam, int lParam);


        [DllImport("winmm.dll", SetLastError = true)]
        internal static extern bool PlaySound(byte[] pszSound, IntPtr hmod, SoundFlags fdwSound);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        ///     MODULEENTRY32 struct for Module32First/Next
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MODULEENTRY32
        {
            internal uint dwSize;
            internal uint th32ModuleID;
            internal uint th32ProcessID;
            internal uint GlblcntUsage;
            internal uint ProccntUsage;
            internal readonly IntPtr modBaseAddr;
            internal uint modBaseSize;
            private readonly IntPtr hModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] internal string szModule;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] internal string szExePath;
        }

        internal struct SYSTEM_INFO
        {
            internal ushort processorArchitecture;
#pragma warning disable 169
            private ushort reserved;
#pragma warning restore 169
            internal uint pageSize;
            internal IntPtr minimumApplicationAddress; // minimum address
            internal IntPtr maximumApplicationAddress; // maximum address
            internal IntPtr activeProcessorMask;
            internal uint numberOfProcessors;
            internal uint processorType;
            internal uint allocationGranularity;
            internal ushort processorLevel;
            internal ushort processorRevision;
        }

        internal struct STARTUPINFO
        {
            internal uint cb;
            internal string lpReserved;
            internal string lpDesktop;
            internal string lpTitle;
            internal uint dwX;
            internal uint dwY;
            internal uint dwXSize;
            internal uint dwYSize;
            internal uint dwXCountChars;
            internal uint dwYCountChars;
            internal uint dwFillAttribute;
            internal uint dwFlags;
            internal short wShowWindow;
            internal short cbReserved2;
            internal IntPtr lpReserved2;
            internal IntPtr hStdInput;
            internal IntPtr hStdOutput;
            internal IntPtr hStdError;
        }

        [DllImport("user32.dll")]
        internal static extern bool FlashWindow(IntPtr hwnd, bool bInvert = true);

        internal struct PROCESS_INFORMATION
        {
            internal IntPtr hProcess;
            internal IntPtr hThread;
            internal uint dwProcessId;
            internal uint dwThreadId;
        }

        [Flags]
        internal enum ProcessCreationFlags : uint
        {
            ZERO_FLAG = 0x00000000,
            CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
            CREATE_DEFAULT_ERROR_MODE = 0x04000000,
            CREATE_NEW_CONSOLE = 0x00000010,
            CREATE_NEW_PROCESS_GROUP = 0x00000200,
            CREATE_NO_WINDOW = 0x08000000,
            CREATE_PROTECTED_PROCESS = 0x00040000,
            CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
            CREATE_SEPARATE_WOW_VDM = 0x00001000,
            CREATE_SHARED_WOW_VDM = 0x00001000,
            CREATE_SUSPENDED = 0x00000004,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            DEBUG_ONLY_THIS_PROCESS = 0x00000002,
            DEBUG_PROCESS = 0x00000001,
            DETACHED_PROCESS = 0x00000008,
            EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
            INHERIT_PARENT_AFFINITY = 0x00010000
        }


        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate int WindowProc(IntPtr hWnd, int Msg, int wParam, int lParam);


        [Flags]
        internal enum SoundFlags
        {
            /// <summary>play synchronously (default)</summary>
            SND_SYNC = 0x0000,

            /// <summary>play asynchronously</summary>
            SND_ASYNC = 0x0001,

            /// <summary>silence (!default) if sound not found</summary>
            SND_NODEFAULT = 0x0002,

            /// <summary>pszSound points to a memory file</summary>
            SND_MEMORY = 0x0004,

            /// <summary>loop the sound until next sndPlaySound</summary>
            SND_LOOP = 0x0008,

            /// <summary>don't stop any currently playing sound</summary>
            SND_NOSTOP = 0x0010,

            /// <summary>Stop Playing Wave</summary>
            SND_PURGE = 0x40,

            /// <summary>don't wait if the driver is busy</summary>
            SND_NOWAIT = 0x00002000,

            /// <summary>name is a registry alias</summary>
            SND_ALIAS = 0x00010000,

            /// <summary>alias is a predefined id</summary>
            SND_ALIAS_ID = 0x00110000,

            /// <summary>name is file name</summary>
            SND_FILENAME = 0x00020000,

            /// <summary>name is resource name or atom</summary>
            SND_RESOURCE = 0x00040004
        }

        internal delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    }
}