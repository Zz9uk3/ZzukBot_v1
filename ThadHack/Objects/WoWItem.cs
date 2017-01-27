using System;
using System.Text;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using Ptr = ZzukBot.Constants.Offsets;

namespace ZzukBot.Objects
{
    internal class WoWItem : WoWObject
    {
        /// <summary>
        ///     Constructor taking guid aswell Ptr to object
        /// </summary>
        internal WoWItem(ulong parGuid, IntPtr parPointer, Enums.WoWObjectTypes parType)
            : base(parGuid, parPointer, parType)
        {
        }

        /// <summary>
        ///     Pointer to WDB cache
        /// </summary>
        private IntPtr ItemCachePointer => Functions.ItemCacheGetRow(ItemId);

        /// <summary>
        ///     Pointer to Cache element for UseItem
        /// </summary>
        internal IntPtr UseItemPointer => IntPtr.Add(ItemCachePointer, Offsets.Item.UseItemPtr1);

        /// <summary>
        ///     Get Item ID
        /// </summary>
        private int ItemId => GetDescriptor<int>(Offsets.Descriptors.ItemId);

        /// <summary>
        ///     Durability
        /// </summary>
        internal int Durability => GetDescriptor<int>(Offsets.Descriptors.ItemDurability);

        internal int MaxDurability => GetDescriptor<int>(Offsets.Descriptors.ItemMaxDurability);

        internal int DurabilityPercent => (int) (Durability/(float) MaxDurability*100);

        /// <summary>
        ///     Stack count
        /// </summary>
        internal int StackCount => GetDescriptor<int>(Offsets.Descriptors.ItemStackCount);

        /// <summary>
        ///     Item quality | 0 = grey
        /// </summary>
        internal int Quality => ItemCachePointer.Add(Offsets.Item.ItemCachePtrQuality).ReadAs<int>();

        /// <summary>
        ///     Item name
        /// </summary>
        internal override string Name
        {
            get
            {
                var ptr = ItemCachePointer.Add(Offsets.Item.ItemCachePtrName).ReadAs<IntPtr>();
                return ptr.ReadString();
            }
        }

        /// <summary>
        ///     0, 0, 0 cause items have no coordinates
        /// </summary>
        internal override XYZ Position => new XYZ(0, 0, 0);

        /// <summary>
        ///     total slots of item
        /// </summary>
        internal int Slots => ReadRelative<int>(Offsets.Item.ItemSlots);

        public override string ToString()
        {
            return Name + "-> Stackcount: " + StackCount + " Durability: " + Durability + " Quality: " + Quality;
        }
    }
}