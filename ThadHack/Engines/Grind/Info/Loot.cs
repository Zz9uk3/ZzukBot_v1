using System.Collections.Generic;
using System.Linq;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _Loot
    {
        internal volatile bool BlacklistCurrentLoot;

        internal _Loot()
        {
            LootBlacklist = new List<ulong>();
        }

        private List<ulong> LootBlacklist { get; }

        internal WoWUnit LootableMob
        {
            get
            {
                var mobs = ObjectManager.Npcs;
                return mobs
                    .Where(i => (i.CanBeLooted && (i.TappedByMe || !i.TappedByOther) ||
                                 (Options.SkinUnits && i.IsSkinable && (Options.NinjaSkin || i.TappedByMe || !i.TappedByOther))
                        )
                                && !LootBlacklist.Contains(i.Guid)
                                && !i.IsSwimming
                                && Calc.Distance3D(i.Position, ObjectManager.Player.Position) < 32)
                    .OrderBy(i => Calc.Distance3D(i.Position, ObjectManager.Player.Position))
                    .FirstOrDefault();
            }
        }

        internal bool NeedToLoot
        {
            get
            {
                var tmp = LootableMob;
                if (tmp != null && BlacklistCurrentLoot)
                {
                    LootBlacklist.Add(tmp.Guid);
                    BlacklistCurrentLoot = false;
                    return false;
                }
                return ObjectManager.Player.Inventory.FreeSlots >= Options.MinFreeSlotsBeforeVendor && tmp != null;
            }
        }

        internal void AddToLootBlacklist(ulong guid)
        {
            if (!LootBlacklist.Contains(guid))
                LootBlacklist.Add(guid);
        }

        internal void RemoveRespawnedMobsFromBlacklist(List<WoWUnit> parList)
        {
            parList.ForEach(i => LootBlacklist.Remove(i.Guid));
        }

        internal void RemoveRespawnedMobsFromBlacklist(ulong parGuid)
        {
            LootBlacklist.Remove(parGuid);
        }
    }
}