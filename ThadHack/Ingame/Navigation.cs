using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Ingame
{
    internal static class Navigation
    {
        [DllImport(Libs.Navigation)]
        private static extern unsafe _XYZ* CalculatePath(uint mapId, _XYZ start, _XYZ end, bool parSmooth,
            out int length);

        [DllImport(Libs.Navigation)]
        private static extern unsafe void FreePathArr(_XYZ* pathArr);

        //[DllImport(Libs.NavigationPath)]
        //private static extern int GetPathArray([Out] _XYZ[] path, int length);

        internal static unsafe XYZ[] CalculatePath(XYZ parStart, XYZ parEnd, bool parSmooth)
        {
            XYZ[] ret;
            try
            {
                int length;
                var pathArr = CalculatePath((uint) ObjectManager.Player.GetMapID, parStart.ToStruct, parEnd.ToStruct,
                    parSmooth, out length);
                ret = new XYZ[length];
                for (var i = 0; i < length; i++)
                {
                    ret[i] = new XYZ(pathArr[i]);
                }
                FreePathArr(pathArr);
            }
            catch
            {
                ret = new[] {parStart, parEnd};
            }
            return ret;
        }

        internal static void CalculatePathAsync(XYZ parStart, XYZ parEnd, bool parSmooth,
            CalculatePathAsyncCallBack parCallback)
        {
            var list = new ArrayList {parStart, parEnd, parSmooth, parCallback};

            ParameterizedThreadStart pts = CalculatePathProxy;
            var thr = new Thread(pts) {IsBackground = true};
            thr.Start(list);
        }

        private static void CalculatePathProxy(object parDetails)
        {
            var list = (ArrayList) parDetails;
            var res = CalculatePath((XYZ) list[0], (XYZ) list[1], (bool) list[2]);
            ((CalculatePathAsyncCallBack) list[3])(res);
        }

        internal static XYZ GetRandomPoint(XYZ parStart, float parMaxDistance)
        {
            var random = new Random();
            var end = new XYZ(parStart.X - parMaxDistance + random.Next((int) parMaxDistance*2),
                parStart.Y - parMaxDistance + random.Next((int) parMaxDistance*2), parStart.Z);
            return end;
        }

        internal static XYZ GetPointBehindPlayer(XYZ parStart, float parDistanceToMove)
        {
            var newX = parStart.X + parDistanceToMove*(float) -Math.Cos(ObjectManager.Player.Facing);
            var newY = parStart.Y + parDistanceToMove*(float) -Math.Sin(ObjectManager.Player.Facing);
            var end = new XYZ(newX, newY, parStart.Z);
            return end;
        }

        internal delegate void CalculatePathAsyncCallBack(XYZ[] parPath);
    }
}