using System;
using System.Text;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using Ptr = ZzukBot.Constants.Offsets;

namespace ZzukBot.Objects
{
    internal class WoWGameObject : WoWObject
    {
        /// <summary>
        ///     Constructor taking guid aswell Ptr to object
        /// </summary>
        internal WoWGameObject(ulong parGuid, IntPtr parPointer, Enums.WoWObjectTypes parType)
            : base(parGuid, parPointer, parType)
        {
        }

        /// <summary>
        ///     Position of object
        /// </summary>
        internal override XYZ Position
        {
            get
            {
                var X = GetDescriptor<float>(Offsets.GameObject.PosX);
                var Y = GetDescriptor<float>(Offsets.GameObject.PosY);
                var Z = GetDescriptor<float>(Offsets.GameObject.PosZ);
                return new XYZ(X, Y, Z);
            }
        }

        /// <summary>
        ///     Name of object
        /// </summary>
        internal override string Name
        {
            get
            {
                var ptr1 = ReadRelative<IntPtr>(Offsets.GameObject.NameBase);
                var ptr2 = Memory.Reader.Read<IntPtr>(IntPtr.Add(ptr1, Offsets.GameObject.NameBasePtr1));
                return Memory.Reader.ReadString(ptr2, Encoding.ASCII, 30);
            }
        }

        /// <summary>
        ///     Distance to object
        /// </summary>
        internal float DistanceTo(WoWObject parOtherObject)
        {
            return Calc.Distance2D(Position, parOtherObject.Position);
        }

        internal void Interact(bool parAutoLoot)
        {
            Functions.OnRightClickObject(Pointer, Convert.ToInt32(parAutoLoot));
        }
    }
}