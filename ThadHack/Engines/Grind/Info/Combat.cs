using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _Combat
    {
        private int lastCheck;

        internal int LastFightTick = 0;

        internal Dictionary<ulong, int> UnitsDottedByPlayer { get; set; } = new Dictionary<ulong, int>();

        internal _Combat()
        {
            BlacklistedUnits = new List<ulong>();
            OldGuid = 0;
            OldHpPercent = 100;
        }

        // Mobs that attck us
        internal List<WoWUnit> Attackers
        {
            get
            {
                var mobs = ObjectManager.Npcs;
                mobs = mobs
                    .Where(i =>
                        i.IsMob && i.Health != 0 && i.Reaction != Enums.UnitReaction.Friendly &&
                        (
                            i.TargetGuid == ObjectManager.Player.Guid ||
                            (ObjectManager.Player.HasPet && i.TargetGuid == ObjectManager.Player.Pet.Guid)
                            ||
                            (i.IsInCombat && i.TappedByMe &&
                             (((i.Debuffs.Count > 0 || i.IsCrowdControlled) && UnitsDottedByPlayer.ContainsKey(i.Guid) && !ObjectManager.Player.IsEating && !ObjectManager.Player.IsDrinking)
                             || ObjectManager.Player.TargetGuid == i.Guid)
                                )) && !i.IsPlayerPet
                    )
                    .ToList();
                return mobs;
            }
        }

        private List<ulong> BlacklistedUnits { get; }
        private ulong OldGuid { get; set; }
        private float OldHpPercent { get; set; }

        internal bool IsMoving => Grinder.Access.Info.PathBackup.MovingBack || Grinder.Access.Info.PathForceBackup.MovingBack ||
                                  Grinder.Access.Info.Target.FixFacing || !Grinder.Access.Info.Target.InSightWithTarget;

        internal bool IsMovingBack => Grinder.Access.Info.PathBackup.MovingBack || Grinder.Access.Info.PathForceBackup.MovingBack;

        internal bool IsAttacker(ulong parGuid)
        {
            var tmpAtt = Attackers;
            return tmpAtt.Any(x => x.Guid == parGuid);
        }

        internal bool IsBlacklisted(WoWUnit parUnit)
        {
            if (parUnit == null) return false;
            if (BlacklistedUnits.Contains(parUnit.Guid))
                return true;

            if (OldGuid != parUnit.Guid)
            {
                OldGuid = parUnit.Guid;
                OldHpPercent = parUnit.HealthPercent;
                Wait.Remove("UnitBlacklist");
                lastCheck = Environment.TickCount;
            }
            else
            {
                if (Environment.TickCount - lastCheck >= 5000)
                {
                    Wait.Remove("UnitBlacklist");
                }
                lastCheck = Environment.TickCount;
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (OldHpPercent == parUnit.HealthPercent)
                {
                    if (Wait.For("UnitBlacklist", 25000))
                    {
                        if (!BlacklistedUnits.Contains(parUnit.Guid))
                            BlacklistedUnits.Add(parUnit.Guid);
                    }
                }
                else
                    OldGuid = 0;
            }
            return false;
        }

        internal void AddToBlacklist(ulong parGuid)
        {
            if (!BlacklistedUnits.Contains(parGuid))
                BlacklistedUnits.Add(parGuid);
        }

        internal bool BlacklistContains(ulong parGuid)
        {
            return BlacklistedUnits.Contains(parGuid);
        }
    }
}