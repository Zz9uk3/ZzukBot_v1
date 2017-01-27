using System;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _Target
    {
        internal volatile bool FixFacing;
        internal int HostileMobRange = 15;

        internal volatile bool InSightWithTarget;
        internal volatile int ResetToNormalAt = 0;

        internal bool SearchDirect = false;

        internal _Target()
        {
            InSightWithTarget = true;
            FixFacing = false;
        }

        // Get the next mob we should attack
        internal WoWUnit NextTarget
        {
            get
            {
                var mobs = ObjectManager.Npcs;

                mobs = mobs
                    .Where(
                        i =>
                            i.IsMob && i.Health != 0 && !Grinder.Access.Info.Combat.BlacklistContains(i.Guid) &&
                            (Grinder.Access.Profile.Factions == null ||
                             (Grinder.Access.Profile.Factions != null &&
                              Grinder.Access.Profile.Factions.Contains(i.FactionID))) &&
                            i.Reaction != Enums.UnitReaction.Friendly && !i.IsCritter && i.IsUntouched &&
                            i.HealthPercent == 100 &&
                            Calc.Distance3D(i.Position, ObjectManager.Player.Position) <= Options.MobSearchRange &&
                            Math.Abs(ObjectManager.Player.Position.Z - i.Position.Z) <= 4 && i.SummonedBy == 0 &&
                            i.TargetGuid == 0)
                    .OrderBy(i => Calc.Distance3D(i.Position, ObjectManager.Player.Position)).ToList();
                return mobs.FirstOrDefault();
            }
        }

        // should we attack our current target?
        internal bool ShouldAttackTarget
        {
            get
            {
                var target = ObjectManager.Target;
                if (target == null) return false;
                var ret =
                    target.IsMob && !Grinder.Access.Info.Combat.BlacklistContains(target.Guid) &&
                    target.Reaction != Enums.UnitReaction.Friendly /*&&
                    (target.IsUntouched || target.IsMarked)*/&& !target.TappedByOther && target.HealthPercent == 100
                    && !target.IsCritter;

                if (!ret)
                {
                    ObjectManager.Player.SetTarget(0);

                    if (ObjectManager.Player.Casting != 0 || ObjectManager.Player.Channeling != 0)
                    {
                        ObjectManager.Player.StartMovement(Enums.ControlBits.Front);
                        ObjectManager.Player.StopMovement(Enums.ControlBits.Front);
                    }
                }
                return ret;
            }
        }

        // In range ton attack target
        internal float CombatDistance
        {
            get
            {
                var tmp = Options.CombatDistance;
                if (!InSightWithTarget)
                {
                    if (!Grinder.Access.Info.Combat.IsMovingBack)
                        tmp = 3;
                    if (Environment.TickCount >= ResetToNormalAt)
                        InSightWithTarget = true;
                }
                return tmp;
            }
        }
    }
}