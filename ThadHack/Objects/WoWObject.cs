using System;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using Ptr = ZzukBot.Constants.Offsets;

namespace ZzukBot.Objects
{
    internal class WoWObject
    {
        /// <summary>
        ///     Constructor taking guid aswell Ptr to object
        /// </summary>
        internal WoWObject(ulong parGuid, IntPtr parPointer, Enums.WoWObjectTypes parType)
        {
            Guid = parGuid;
            Pointer = parPointer;
            WoWType = parType;
        }

        /// <summary>
        ///     Pointer + Guid
        /// </summary>
        internal IntPtr Pointer { get; set; }

        internal ulong Guid { get; private set; }

        internal Enums.WoWObjectTypes WoWType { get; private set; }

        /// <summary>
        ///     Position of object
        /// </summary>
        internal virtual XYZ Position { get; set; }

        /// <summary>
        ///     Name of object
        /// </summary>
        internal virtual string Name { get; set; }

        /// <summary>
        ///     Get descriptor function to avoid some code
        /// </summary>
        internal T GetDescriptor<T>(int descriptor) where T : struct
        {
            var ptr = Pointer.Add(Offsets.ObjectManager.DescriptorOffset).ReadAs<uint>();
            return new IntPtr(ptr + descriptor).ReadAs<T>();
        }

        internal void SetDescriptor<T>(int descriptor, T parValue) where T : struct
        {
            var ptr = Pointer.Add(Offsets.ObjectManager.DescriptorOffset).ReadAs<uint>();
            Memory.Reader.Write(new IntPtr(ptr + descriptor), parValue);
        }

        /// <summary>
        ///     Read relative to base pointer
        /// </summary>
        internal T ReadRelative<T>(int offset) where T : struct
        {
            return Pointer.Add(offset).ReadAs<T>();
        }
    }
}