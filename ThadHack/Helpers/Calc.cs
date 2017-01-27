using System;
using ZzukBot.Constants;
using ZzukBot.Mem;

namespace ZzukBot.Helpers
{
    internal class XYZ
    {
        private float _X;
        private float _Y;
        private float _Z;

        internal _XYZ ToStruct;

        internal XYZ(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            ToStruct = new _XYZ(X, Y, Z);
        }

        internal XYZ()
        {
            X = 0;
            Y = 0;
            Z = 0;
            ToStruct = new _XYZ(X, Y, Z);
        }

        internal XYZ(_XYZ parStruct)
        {
            X = parStruct.X;
            Y = parStruct.Y;
            Z = parStruct.Z;
            ToStruct = parStruct;
        }

        internal XYZ(ref _XYZ parStruct)
        {
            X = parStruct.X;
            Y = parStruct.Y;
            Z = parStruct.Z;
            ToStruct = parStruct;
        }

        internal XYZ(float x, float y)
        {
            X = x;
            Y = y;
            Z = 0;
            ToStruct = new _XYZ(X, Y, Z);
        }

        internal float X
        {
            get { return _X; }
            set
            {
                _X = value;
                ToStruct.X = value;
            }
        }

        internal float Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                ToStruct.Y = value;
            }
        }

        internal float Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
                ToStruct.Z = value;
            }
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Z: {Z}";
        }
    }

    internal static class Calc
    {
        internal static float Distance3D(XYZ parA, XYZ parB)
        {
            double a = parA.X - parB.X;
            double b = parA.Y - parB.Y;
            double c = parA.Z - parB.Z;
            return (float) Math.Sqrt(a*a + b*b + c*c);
        }

        internal static float Distance2D(XYZ parA, XYZ parB)
        {
            double a = parA.X - parB.X;
            double b = parA.Y - parB.Y;
            return (float) Math.Sqrt(a*a + b*b);
        }

        internal static float Distance2D(XYZ parA)
        {
            var pos = ObjectManager.Player.Position;
            double a = parA.X - pos.X;
            double b = parA.Y - pos.Y;
            return (float) Math.Sqrt(a*a + b*b);
        }
    }
}