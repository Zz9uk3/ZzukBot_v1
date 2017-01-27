using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using GreyMagic.Internals;
using funcs = ZzukBot.Constants.Offsets.Functions;

namespace ZzukBot.Mem
{
    internal static class GetEndScene
    {
        private static Direct3D9IsSceneEnd _isSceneEndDelegate;
        private static Detour _isSceneEndHook;

        private static IntPtr EndScenePtr = IntPtr.Zero;

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static IntPtr ToPointer()
        {
            if (EndScenePtr != IntPtr.Zero) return EndScenePtr;

            _isSceneEndDelegate =
                Memory.Reader.RegisterDelegate<Direct3D9IsSceneEnd>(funcs.IsSceneEnd);
            _isSceneEndHook =
                Memory.Reader.Detours.CreateAndApply(
                    _isSceneEndDelegate,
                    new Direct3D9IsSceneEnd(IsSceneEndHook),
                    "IsSceneEnd");

            while (EndScenePtr == IntPtr.Zero) Thread.Sleep(5);

            return EndScenePtr;
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static IntPtr IsSceneEndHook(IntPtr device)
        {
            //[[ESI+38A8]]+A8
            var ptr1 = device.Add((int) funcs.EndScenePtr1).ReadAs<IntPtr>();
            var ptr2 = ptr1.ReadAs<IntPtr>();
            var ptr3 = ptr2.Add((int) funcs.EndScenePtr2).ReadAs<IntPtr>();
            EndScenePtr = ptr3;

            _isSceneEndHook.Remove();
            return _isSceneEndDelegate(device);
        }


        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr Direct3D9IsSceneEnd(IntPtr device);
    }
}