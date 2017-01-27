using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using Ptr = ZzukBot.Constants.Offsets;

namespace ZzukBot.Objects
{
    internal class WoWUnit : WoWObject
    {
        /// <summary>
        ///     Constructor taking guid aswell Ptr to object
        /// </summary>
        internal WoWUnit(ulong parGuid, IntPtr parPointer, Enums.WoWObjectTypes parType)
            : base(parGuid, parPointer, parType)
        {
        }

        /// <summary>
        ///     XYZ position of object
        /// </summary>
        internal override XYZ Position
        {
            get
            {
                var X = ReadRelative<float>(Offsets.Unit.PosX);
                var Y = ReadRelative<float>(Offsets.Unit.PosY);
                var Z = ReadRelative<float>(Offsets.Unit.PosZ);
                return new XYZ(X, Y, Z);
            }
        }

        internal float DistanceToPlayer => Calc.Distance3D(ObjectManager.Player.Position, Position);

        /// <summary>
        ///     All auras on unit (only id)
        /// </summary>
        internal List<int> Auras
        {
            get
            {
                var tmpAuras = new List<int>();
                var auraBase = Enums.DynamicFlags.AuraBase;
                var curCount = 0;
                while (true)
                {
                    var auraId = GetDescriptor<int>(auraBase);
                    if (curCount == 10) break;
                    if (auraId != 0)
                        tmpAuras.Add(auraId);
                    auraBase += Enums.DynamicFlags.NextAura;
                    curCount++;
                }
                return tmpAuras;
            }
        }

        /// <summary>
        ///     All debuffs on unit
        /// </summary>
        internal List<int> Debuffs
        {
            get
            {
                var tmpAuras = new List<int>();
                var auraBase = 0x13C;
                var curCount = 0;
                while (true)
                {
                    var auraId = GetDescriptor<int>(auraBase);
                    if (curCount == 16) break;
                    if (auraId != 0)
                        tmpAuras.Add(auraId);
                    auraBase += 4;
                    curCount++;
                }
                return tmpAuras;
            }
        }

        /// <summary>
        ///     Name of object
        /// </summary>
        internal override string Name
        {
            get
            {
                switch (WoWType)
                {
                    case Enums.WoWObjectTypes.OT_UNIT:
                        return UnitName;

                    case Enums.WoWObjectTypes.OT_PLAYER:
                        return PlayerName;
                }
                return "";
            }
        }

        private string UnitName
        {
            get
            {
                var ptr1 = ReadRelative<IntPtr>(Offsets.Unit.NameBase);
                if (ptr1 == IntPtr.Zero) return "";
                var ptr2 = ptr1.ReadAs<IntPtr>();
                if (ptr2 == IntPtr.Zero) return "";
                return ptr2.ReadString(30);
            }
        }

        private string PlayerName
        {
            get
            {
                var nameBasePtr = Offsets.PlayerObject.NameBase.ReadAs<IntPtr>();
                while (true)
                {
                    var nextGuid = nameBasePtr.Add(Offsets.PlayerObject.NameBaseNextGuid).ReadAs<ulong>();
                    if (nextGuid == 0)
                    {
                        return "";
                    }
                    if (nextGuid != Guid)
                    {
                        nameBasePtr = nameBasePtr.ReadAs<IntPtr>();
                    }
                    else
                    {
                        break;
                    }
                }
                return nameBasePtr.Add(0x14).ReadString(30);
            }
        }

        /// <summary>
        ///     Is the unit summoned by another unit?
        /// </summary>
        internal ulong SummonedBy => GetDescriptor<ulong>(Offsets.Descriptors.SummonedByGuid);

        /// <summary>
        ///     The ID + faction ID of the NPC
        /// </summary>
        internal int NpcID => ReadRelative<int>(Offsets.Descriptors.NpcId);

        internal int FactionID => GetDescriptor<int>(Offsets.Descriptors.FactionId);

        /// <summary>
        ///     The movement state of the Unit
        /// </summary>
        internal virtual uint MovementState => ReadRelative<uint>(Offsets.Descriptors.movementFlags);

        /// <summary>
        ///     Dynamic Flags of the Unit (Is lootable / Is tapped?)
        /// </summary>
        internal int DynamicFlags => GetDescriptor<int>(Offsets.Descriptors.DynamicFlags);

        internal int Flags => GetDescriptor<int>(Offsets.Descriptors.Flags);

        internal bool IsFleeing
        {
            get
            {
                var flag = Enums.UnitFlags.UNIT_FLAG_FLEEING;
                return (Flags & flag) ==
                       flag;
            }
        }

        internal bool IsConfused
        {
            get
            {
                var flag = Enums.UnitFlags.UNIT_FLAG_CONFUSED;
                return (Flags & flag) ==
                       flag;
            }
        }

        internal bool IsInCombat
        {
            get
            {
                var flag = Enums.UnitFlags.UNIT_FLAG_IN_COMBAT;
                return (Flags & flag) ==
                       flag;
            }
        }

        internal bool IsSkinable
        {
            get
            {
                var flag = Enums.UnitFlags.UNIT_FLAG_SKINNABLE;
                return (Flags & flag) ==
                       flag;
            }
        }

        internal bool IsStunned
        {
            get
            {
                var flag = Enums.UnitFlags.UNIT_FLAG_STUNNED;
                return (Flags & flag) ==
                       flag;
            }
        }

        internal bool IsMovementDisabled
        {
            get
            {
                var flag = Enums.UnitFlags.UNIT_FLAG_DISABLE_MOVE;
                return (Flags & flag) ==
                       flag;
            }
        }

        internal bool IsCrowdControlled => IsStunned | IsFleeing | IsConfused;

        internal bool TappedByMe => DynamicFlags >= Enums.DynamicFlags.TappedByMe &&
                                    DynamicFlags <= Enums.DynamicFlags.TappedByMe + 0x2;

        internal bool TappedByOther => DynamicFlags >= Enums.DynamicFlags.TappedByOther &&
                                       DynamicFlags <= Enums.DynamicFlags.TappedByOther + 2;

        internal bool IsUntouched => DynamicFlags == Enums.DynamicFlags.Untouched;

        internal bool IsMarked => DynamicFlags == Enums.DynamicFlags.IsMarked;

        internal bool IsKilledAndLooted => DynamicFlags == Enums.DynamicFlags.TappedByMe && Health == 0;

        internal bool CanBeLooted
        {
            get
            {
                if (Health == 0)
                {
                    return (DynamicFlags & 1) == 1; //== Enums.DynamicFlags.CanBeLooted;
                }
                return false;
            }
        }

        /// <summary>
        ///     Health
        /// </summary>
        internal int Health => GetDescriptor<int>(Offsets.Descriptors.Health);

        internal int MaxHealth => GetDescriptor<int>(Offsets.Descriptors.MaxHealth);

        internal int HealthPercent => (int) (Health/(float) MaxHealth*100);

        /// <summary>
        ///     Mana
        /// </summary>
        internal int Mana => GetDescriptor<int>(Offsets.Descriptors.Mana);

        internal int MaxMana => GetDescriptor<int>(Offsets.Descriptors.MaxMana);

        internal int ManaPercent => (int) (Mana/(float) MaxMana*100);

        /// <summary>
        ///     Rage
        /// </summary>
        internal int Rage => GetDescriptor<int>(Offsets.Descriptors.Rage)/10;

        /// <summary>
        ///     Energy
        /// </summary>
        internal int Energy => GetDescriptor<int>(Offsets.Descriptors.Energy);

        /// <summary>
        ///     Guid of the units target
        /// </summary>
        internal ulong TargetGuid
        {
            get { return GetDescriptor<ulong>(Offsets.Descriptors.TargetGuid); }
            set { SetDescriptor(Offsets.Descriptors.TargetGuid, value); }
        }

        /// <summary>
        ///     Id of the spell the unit is casting currently
        /// </summary>
        internal virtual int Casting => ReadRelative<int>(0xC8C);

        /// <summary>
        ///     Id of the spell the unit is channeling currently
        /// </summary>
        internal int Channeling => GetDescriptor<int>(Offsets.Descriptors.IsChanneling);

        /// <summary>
        ///     Units reaction to the player
        /// </summary>
        internal virtual Enums.UnitReaction Reaction => (Enums.UnitReaction) Functions.UnitReaction(Pointer, ObjectManager.Player.Pointer);

        internal bool IsPlayer => WoWType == Enums.WoWObjectTypes.OT_PLAYER;

        internal bool IsMob => WoWType == Enums.WoWObjectTypes.OT_UNIT;

        /// <summary>
        ///     characters current facing
        /// </summary>
        internal float Facing => ReadRelative<float>(0x9C4);

        // Is the unit a critter?
        internal bool IsCritter => Enums.CreatureType.Critter == CreatureType;

        internal bool IsTotem => Enums.CreatureType.Totem == CreatureType;


        private int CreatureRank => Functions.GetCreatureRank(Pointer);

        internal bool IsRareElite => CreatureRank == 2;

        internal int CreatureType => Functions.GetCreatureType(Pointer);

        internal int Level => GetDescriptor<int>(Offsets.Descriptors.Level);

        internal bool IsMounted => GetDescriptor<int>(Offsets.Descriptors.MountDisplayId) != 0;

        internal bool IsPet => SummonedBy != 0;

        internal bool IsPlayerPet
        {
            get
            {
                var tmpGuid = SummonedBy;
                if (tmpGuid == 0) return false;
                var obj = ObjectManager.Players.FirstOrDefault(i => i.Guid == tmpGuid);
                return obj != null;
            }
        }

        internal bool IsSwimming => (MovementState & (uint) Enums.MovementFlags.Swimming) == (uint) Enums.MovementFlags.Swimming;

        /// <summary>
        ///     Interact with target
        /// </summary>
        internal void Interact(bool parAutoLoot)
        {
            Functions.OnRightClickUnit(Pointer, Convert.ToInt32(parAutoLoot));
        }

        internal float FacingRelativeTo(WoWObject parObject)
        {
            return FacingRelativeTo(parObject.Position);
        }

        internal float FacingRelativeTo(XYZ parPosition)
        {
            return (float) Math.Round(Math.Abs(RequiredFacing(parPosition) - Facing), 2);
        }

        internal float RequiredFacing(WoWObject parObject)
        {
            return RequiredFacing(parObject.Position);
        }

        internal float RequiredFacing(XYZ parPosition)
        {
            var f = (float) Math.Atan2(parPosition.Y - Position.Y, parPosition.X - Position.X);
            if (f < 0.0f)
            {
                f = f + (float) Math.PI*2.0f;
            }
            else
            {
                if (f > (float) Math.PI*2)
                {
                    f = f - (float) Math.PI*2.0f;
                }
            }
            return f;
        }


        /// <summary>
        ///     Got buff?
        /// </summary>
        internal bool GotAura(string parName)
        {
            var tmpAuras = Auras;
            return tmpAuras.Select(i => string.Equals(ObjectManager.Player.Spells.GetName(i), parName, StringComparison.OrdinalIgnoreCase)).Any(tmpBool => tmpBool);
        }

        /// <summary>
        ///     Got debuff?
        /// </summary>
        internal bool GotDebuff(string parName)
        {
            var tmpAuras = Debuffs;
            return tmpAuras.Select(i => string.Equals(ObjectManager.Player.Spells.GetName(i), parName, StringComparison.OrdinalIgnoreCase)).Any(tmpBool => tmpBool);
        }
    }
}