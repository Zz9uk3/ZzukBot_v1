using System;
using System.Collections;
using System.Runtime.InteropServices;
using GreyMagic.Internals;

namespace ZzukBot.Mem
{
    internal static class DirectX
    {
        /// <summary>
        ///     Fields
        /// </summary>
        //private const int VMT_ENDSCENE = 42;
        //private const int VMT_RESET = 16;
        private static Direct3D9EndScene _endSceneDelegate;

        private static volatile Detour _endSceneHook;
        private static volatile bool Applied;

        /// <summary>
        ///     The code EndScene get detoured to
        /// </summary>
        private static volatile int frameCounter = -1;

        private static volatile Run _Run = RunDummy;
        private static volatile bool _CanRun = true;

        private static volatile bool IsIngame;
        private static readonly Queue EndSceneExecuteOnce = new Queue();
        private static readonly Queue EndSceneExecuteOnceIngame = new Queue();

        /// <summary>
        ///     The allmighty endscene hook
        /// </summary>
        private static int LastFrameTick;

        private static int TimeBetweenFrame;
        private static int WaitTilNextFrame;

        private static volatile bool RemoveHook;

        /// <summary>
        ///     Applying the EndScene detour
        /// </summary>
        internal static void Init()
        {
            if (Applied) return;
            _endSceneDelegate =
                Memory.Reader.RegisterDelegate<Direct3D9EndScene>(GetEndScene.ToPointer());
            _endSceneHook =
                Memory.Reader.Detours.CreateAndApply(
                    _endSceneDelegate,
                    new Direct3D9EndScene(EndSceneHook),
                    "D9EndScene");
            Applied = true;
        }

        /// <summary>
        ///     Run the dummy again to stop executing current method which
        ///     _Run delegates points to
        /// </summary>
        private static void RunDummy(ref int parFrameCount, bool ParIsIngame)
        {
            _CanRun = true;
        }

        /// <summary>
        ///     Run the provided method in EndScene
        /// </summary>
        internal static bool RunInEndScene(Run parMethod)
        {
            if (_CanRun)
            {
                _Run = parMethod;
                _CanRun = false;
                return true;
            }
            return false;
        }

        internal static void ForceRunInEndScene(Run parMethod)
        {
            _Run = parMethod;
        }

        internal static void RunAndSwapback(Run parMethod)
        {
            EndSceneExecuteOnce.Enqueue(parMethod);
        }

        internal static void RunAndSwapbackIngame(Run parMethod)
        {
            EndSceneExecuteOnceIngame.Enqueue(parMethod);
        }

        /// <summary>
        ///     Stop running the current method in EndScene
        /// </summary>
        internal static void StopRunning()
        {
            _Run = RunDummy;
        }


        internal static void RemoveHookAsync()
        {
            RemoveHook = true;
        }

        internal static void ApplyHook()
        {
            if (_endSceneHook == null) return;
            if (_endSceneHook.IsApplied) return;
            RemoveHook = false;
            _endSceneHook.Apply();
        }

        private static int EndSceneHook(IntPtr parDevice)
        {
            //Console.WriteLine("EndScene" + frameCounter);
            //_endSceneHook.Remove();
            //return _endSceneDelegate(parDevice);

            if (!RemoveHook)
            {
                IsIngame = ObjectManager.EnumObjects();
                if (EndSceneExecuteOnce.Count > 0)
                {
                    ((Run) EndSceneExecuteOnce.Dequeue())(ref frameCounter, IsIngame);
                }
                else if (IsIngame && EndSceneExecuteOnceIngame.Count > 0)
                {
                    ((Run) EndSceneExecuteOnceIngame.Dequeue())(ref frameCounter, IsIngame);
                }
                else
                {
                    _Run(ref frameCounter, IsIngame);
                }
            }
            else
            {
                _endSceneHook.Remove();
                return _endSceneDelegate(parDevice);
            }
            frameCounter = frameCounter%1800 + 1;
            if (LastFrameTick != 0)
            {
                TimeBetweenFrame = Environment.TickCount - LastFrameTick;
                if (TimeBetweenFrame < 15)
                {
                    WaitTilNextFrame = Environment.TickCount + (15 - TimeBetweenFrame);
                    while (Environment.TickCount < WaitTilNextFrame)
                    {
                    }
                }
            }
            LastFrameTick = Environment.TickCount;
            return (int)_endSceneHook.CallOriginal(parDevice);
        }

        internal delegate void Run(ref int FrameCount, bool IsIngame);


        /// <summary>
        ///     EndScene delegate
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int Direct3D9EndScene(IntPtr device);
    }
}