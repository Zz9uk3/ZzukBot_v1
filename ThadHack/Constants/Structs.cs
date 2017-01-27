using System.Runtime.InteropServices;
using ZzukBot.Helpers;

namespace ZzukBot.Constants
{
    /// <summary>
    ///     Intersection struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Intersection
    {
        internal float X;
        internal float Y;
        internal float Z;
        internal float R;

        public override string ToString()
        {
            return $"Intersection -> X: {X} Y: {Y} Z: {Z} R: {R}";
        }
    }

    /// <summary>
    ///     two coordinates (XYZ 1 and XYZ 2)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct _XYZXYZ
    {
        internal float X1;
        internal float Y1;
        internal float Z1;
        internal float X2;
        internal float Y2;
        internal float Z2;

        internal _XYZXYZ(float x1, float y1, float z1,
            float x2, float y2, float z2)
            : this()
        {
            X1 = x1;
            Y1 = y1;
            Z1 = z1;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
        }

        public override string ToString()
        {
            return $"Start -> X: {X1} Y: {Y1} Z: {Z1}\n" + $"End -> X: {X2} Y: {Y2} Z: {Z2}";
        }
    }

    /// <summary>
    ///     Coordinate struct
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct _XYZ
    {
        internal float X;
        internal float Y;
        internal float Z;

        internal _XYZ(float x, float y, float z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    /// <summary>
    ///     Struct with an item to restock at the restock npc
    /// </summary>
    internal struct RestockItem
    {
        internal string Item;
        internal int RestockUpTo;

        internal RestockItem(string parItem, int parRestockUpTo)
        {
            Item = parItem;
            RestockUpTo = parRestockUpTo;
        }
    }

    internal class NPC
    {
        internal NPC(string parName, XYZ parPos, string parMapPos)
        {
            Name = parName;
            Coordinates = parPos;
            MapPosition = parMapPos;
        }

        internal string Name { get; private set; }
        internal XYZ Coordinates { get; private set; }
        internal string MapPosition { get; private set; }
    }
}