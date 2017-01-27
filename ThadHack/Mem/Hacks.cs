using System;
using System.Linq;

namespace ZzukBot.Mem
{
    /// <summary>
    ///     Class for a simple hack (read: changing bytes in memory)
    /// </summary>
    internal class Hack
    {
        // address where the bytes will be changed
        private IntPtr _address = IntPtr.Zero;
        internal bool DynamicHide = false;
        internal byte[] OriginalOutOfGame;
        internal bool OutofGameValueDiffers = false;
        // is the hack applied
        //internal bool IsApplied { get; private set; }

        internal bool RelativeToPlayerBase = false;

        /// <summary>
        ///     Constructor: addr and the new bytes
        /// </summary>
        internal Hack(IntPtr parAddress, byte[] parCustomBytes, string parName)
        {
            Address = parAddress;
            CustomBytes = parCustomBytes;
            OriginalBytes = Memory.Reader.ReadBytes(Address, CustomBytes.Length);
            Name = parName;
        }

        /// <summary>
        ///     Constructor: addr, new bytes aswell old bytes
        /// </summary>
        internal Hack(IntPtr parAddress, byte[] parCustomBytes, byte[] parOriginalBytes, string parName)
        {
            Address = parAddress;
            CustomBytes = parCustomBytes;
            OriginalBytes = parOriginalBytes;
            Name = parName;
        }

        internal Hack(uint offset, byte[] parCustomBytes, string parName)
        {
            _address = (IntPtr) offset;
            CustomBytes = parCustomBytes;
            Name = parName;
        }

        internal IntPtr Address
        {
            get
            {
                if (!RelativeToPlayerBase)
                {
                    return _address;
                }
                return IntPtr.Add(ObjectManager.Player.Pointer, (int) _address);
            }
            private set { _address = value; }
        }

        internal bool IsWithinScan(IntPtr scanStartAddress, int size)
        {
            var scanStart = (int)scanStartAddress;
            var scanEnd = (int)IntPtr.Add(scanStartAddress, size);

            var hackStart = (int)Address;
            var hackEnd = (int)Address + CustomBytes.Length;

            if (hackStart >= scanStart && hackStart < scanEnd)
                return true;

            if (hackEnd > scanStart && hackEnd <= scanEnd)
                return true;

            return false;
        }

        // old bytes
        internal byte[] OriginalBytes { get; set; }
        // new bytes
        internal byte[] CustomBytes { get; set; }
        // name of hack
        internal string Name { get; private set; }

        internal bool IsActivated
        {
            get
            {
                if (RelativeToPlayerBase)
                {
                    if (!ObjectManager.EnumObjects()) return false;
                    if (ObjectManager.Player == null) return false;
                }
                var curBytes = Memory.Reader.ReadBytes(Address, OriginalBytes.Length);
                return !curBytes.SequenceEqual(OriginalBytes);
            }
        }

        /// <summary>
        ///     Apply the new bytes to address
        /// </summary>
        internal void Apply()
        {
            if (RelativeToPlayerBase)
            {
                if (!ObjectManager.EnumObjects()) return;
                if (ObjectManager.Player == null) return;
                if (OriginalBytes == null)
                {
                    OriginalBytes = Memory.Reader.ReadBytes(Address, CustomBytes.Length);
                }
            }
            Memory.Reader.WriteBytes(Address, CustomBytes);
        }

        /// <summary>
        ///     Restore the old bytes to the address
        /// </summary>
        internal void Remove()
        {
            if (RelativeToPlayerBase)
            {
                if (!ObjectManager.EnumObjects()) return;
                if (ObjectManager.Player == null) return;
            }
            if (DynamicHide && IsActivated)
            {
                CustomBytes = Memory.Reader.ReadBytes(Address, OriginalBytes.Length);
            }
            Memory.Reader.WriteBytes(Address, OriginalBytes);
        }
    }
}