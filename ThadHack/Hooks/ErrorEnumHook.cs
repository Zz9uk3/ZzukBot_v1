using System;
using System.Runtime.InteropServices;
using ZzukBot.Constants;
using ZzukBot.Mem;

namespace ZzukBot.Hooks
{
    internal delegate void ErrorEnumEventHandler(ErrorEnumArgs e);

    internal class ErrorEnumArgs : EventArgs
    {
        internal ErrorEnumArgs(string Text)
        {
            Message = Text;
        }

        internal string Message { get; }
    }

    internal static class ErrorEnumHook
    {
        /// <summary>
        ///     Delegate to our C# function
        /// </summary>
        private static errorEnumDelegate _errorEnumDelegate;

        private static bool Applied;
        internal static event ErrorEnumEventHandler OnNewError;

        private static void OnNewErrorEvent(ErrorEnumArgs e)
        {
            OnNewError?.Invoke(e);
        }

        /// <summary>
        ///     Init the hook and set enabled to true
        /// </summary>
        internal static void Init()
        {
            if (Applied) return;
            // Pointer the delegate to our c# function
            _errorEnumDelegate = _ErrorEnumHook;
            // get PTR for our c# function
            var addrToDetour = Marshal.GetFunctionPointerForDelegate(_errorEnumDelegate);
            // Alloc space for the ASM part of our detour
            string[] asmCode =
            {
                "MOV EAX, [EBP+8]",
                "LEA EAX, [EAX+EAX*4]",
                "pushfd",
                "pushad",
                "push 0x0B4DA40",
                "call " + (uint) addrToDetour,
                "popad",
                "popfd",
                "jmp " + (uint) (Offsets.Hooks.ErrorEnum + 6)
            };
            // Inject the asm code which calls our c# function
            var codeCave = Memory.InjectAsm(asmCode, "ErrorEnumDetour");
            // set the jmp from WoWs code to my injected code
            Memory.InjectAsm((uint) Offsets.Hooks.ErrorEnum, "jmp " + codeCave, "ErrorEnumDetourJmp");
            Applied = true;
        }


        /// <summary>
        ///     Will be called from the ASM stub
        ///     parErrorCode contains the red message popping up on the
        ///     interface for the error
        /// </summary>
        private static void _ErrorEnumHook(string parErrorCode)
        {
            OnNewErrorEvent(new ErrorEnumArgs(parErrorCode));
        }

        /// <summary>
        ///     Delegate for our c# function
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void errorEnumDelegate(string parErrorCode);
    }
}