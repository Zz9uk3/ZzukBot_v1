using System;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Engines.Grind;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;
using Ptr = ZzukBot.Constants.Offsets;

namespace ZzukBot.Objects
{
    internal delegate void CtmEventHandler(CtmAction e);

    internal class CtmAction
    {
        internal CtmAction(Enums.CtmType parType, XYZ parPos)
        {
            Type = parType;
            Position = parPos;
        }

        internal Enums.CtmType Type { get; }

        internal XYZ Position { get; }
    }

    internal class LocalPlayer : WoWUnit
    {
        /// <summary>
        ///     facing with coordinates instead of a passed unit
        /// </summary>
        private const float facingComparer = 0.2f;

        /// <summary>
        ///     Inventory management
        /// </summary>
        internal Inventory Inventory;

        internal volatile Spells Spells;

        /// <summary>
        ///     Constructor
        /// </summary>
        internal LocalPlayer(ulong parGuid, IntPtr parPointer, Enums.WoWObjectTypes parType)
            : base(parGuid, parPointer, parType)
        {
            Inventory = new Inventory();
            Spells = new Spells();
        }

        /// <summary>
        ///     Position of corpse
        /// </summary>
        internal XYZ CorpsePosition => new XYZ(
            Offsets.Player.CorpsePositionX.ReadAs<float>(),
            Offsets.Player.CorpsePositionY.ReadAs<float>(),
            Offsets.Player.CorpsePositionZ.ReadAs<float>());

        internal float CtmX => Offsets.Player.CtmX.ReadAs<float>();

        internal override Enums.UnitReaction Reaction => Enums.UnitReaction.Friendly;

        internal float CtmY => Offsets.Player.CtmY.ReadAs<float>();

        internal override int Casting
        {
            get
            {
                var tmpId = base.Casting;
                var tmpName = Spells.GetName(tmpId);
                if (tmpName == "Heroic Strike" || tmpName == "Maul")
                    return 0;
                return tmpId;
            }
        }

        internal string CastingAsName => Spells.GetName(Casting);


        internal bool IsCtmIdle => CtmState == (int) Enums.CtmType.None ||
                                   CtmState == 12;

        internal int Money => ReadRelative<int>(0x2FD0);

        /// <summary>
        ///     Get or set the current ctm state
        /// </summary>
        private int CtmState
        {
            get
            {
                return
                    Offsets.Player.CtmState.ReadAs<int>();
            }

            set { Memory.Reader.Write(Offsets.Player.CtmState, value); }
        }

        internal new uint MovementState
        {
            get { return ReadRelative<uint>(Offsets.Descriptors.movementFlags); }
            set { Memory.Reader.Write(Pointer.Add(Offsets.Descriptors.movementFlags), value); }
        }

        internal bool IsInCampfire
        {
            get
            {
                var playerPos = ObjectManager.Player.Position;
                var tmp = ObjectManager.GameObjects
                    .FirstOrDefault(i => i.Name == "Campfire" && Calc.Distance2D(i.Position, playerPos) <= 2.9f);
                return tmp != null;
            }
        }

        /// <summary>
        ///     Are we looting?
        /// </summary>
        internal bool IsLooting => Offsets.Player.IsLooting.ReadAs<int>() != 0;

        internal int LootSlots => Functions.GetLootSlots();

        /// <summary>
        ///     How many items can we loot?
        /// </summary>
        /// <summary>
        ///     the ID of the map we are on
        /// </summary>
        internal int GetMapID => Offsets.ObjectManager.ManagerBase.ReadAs<IntPtr>().Add(0xCC).ReadAs<int>();

        ///// <summary>
        ///// Are we in LoS with object?
        ///// </summary>
        //internal bool InLoSWith(WoWObject parObject)
        //{
        //    // return 1 if something is inbetween the two coordinates
        //    return Functions.Intersect(Position, parObject.Position) == 0;
        //}

        internal bool IsInCC => 0 == Offsets.Player.IsInCC.ReadAs<int>();

        /// <summary>
        ///     guid support field to get right combopoint count
        /// </summary>
        private ulong ComboPointGuid { get; set; }

        /// <summary>
        ///     Get combopoints for current mob
        /// </summary>
        internal byte ComboPoints
        {
            get
            {
                var ptr1 = Pointer.Add(Offsets.Player.ComboPoints1).ReadAs<IntPtr>();
                var ptr2 = ptr1.Add(Offsets.Player.ComboPoints2);
                if (ComboPointGuid == 0)
                    Memory.Reader.Write(ptr2, 0);
                var points = ptr2.ReadAs<byte>();
                if (points == 0)
                {
                    ComboPointGuid = TargetGuid;
                    return points;
                }
                if (ComboPointGuid != TargetGuid)
                {
                    Memory.Reader.Write<byte>(ptr2, 0);
                    return 0;
                }
                return ptr2.ReadAs<byte>();
            }
        }

        internal bool CanOverpower => ComboPoints > 0;


        /// <summary>
        ///     Get the players pet
        /// </summary>
        internal WoWUnit Pet
        {
            get
            {
                return
                    ObjectManager.Npcs.FirstOrDefault(i => i.SummonedBy == Guid);
            }
        }

        internal bool HasPet
        {
            get
            {
                return
                    ObjectManager.Npcs.FirstOrDefault(i => i.SummonedBy == Guid) != null;
            }
        }

        /// <summary>
        ///     Is Eating
        /// </summary>
        internal bool IsEating => GotAura("Food");

        /// <summary>
        ///     Is Drinking
        /// </summary>
        internal bool IsDrinking => GotAura("Drink");

        /// <summary>
        ///     the toons class
        /// </summary>
        internal Enums.ClassIds Class => (Enums.ClassIds) Offsets.Player.Class.ReadAs<Byte>();

        internal bool IsStealth
        {
            get
            {
                switch (Class)
                {
                    case Enums.ClassIds.Rogue:
                    case Enums.ClassIds.Druid:
                        return (PlayerBytes & 0x02000000) == 0x02000000;
                }
                return false;
            }
        }

        internal bool IsShapeShifted
        {
            get
            {
                switch (Class)
                {
                    case Enums.ClassIds.Druid:
                        return (PlayerBytes & 0x000FEE00) != 0x0000EE00;
                }
                return false;
            }
        }

        internal uint PlayerBytes => GetDescriptor<uint>(0x228);

        /// <summary>
        ///     Are we dead?
        /// </summary>
        internal bool IsDead => Health == 0;

        internal string Race
        {
            get
            {
                string encrypted = Strings.GT_GetUnitRace.GenLuaVarName();
                Functions.DoString(Strings.GetUnitRace.Replace(Strings.GT_GetUnitRace, encrypted));
                return Functions.GetText(encrypted);
            }
        }

        /// <summary>
        ///     Are we in ghost form
        /// </summary>
        internal bool InGhostForm => Offsets.Player.IsGhost.ReadAs<byte>() == 1;

        internal float ZAxis
        {
            set { Memory.Reader.Write(Pointer.Add(Offsets.Unit.PosZ), value); }
            get { return ReadRelative<float>((int) Pointer.Add(Offsets.Unit.PosZ)); }
        }

        internal int TimeUntilResurrect
        {
            get
            {
                var encrypted = "zzRezz12".GenLuaVarName();
                Functions.DoString(encrypted + " = GetCorpseRecoveryDelay()");
                return Convert.ToInt32(Functions.GetText(encrypted));
            }
        }

        internal int CurrentXp => GetDescriptor<int>(Offsets.Descriptors.CurrentXp);

        internal int NextLevelXp => GetDescriptor<int>(Offsets.Descriptors.NextLevelXp);

        internal string RealZoneText
        {
            get
            {
                var encrypted = "zzRealZone".GenLuaVarName();
                Functions.DoString(encrypted + " = GetRealZoneText();");
                return Functions.GetText(encrypted);
            }
        }

        internal string MinimapZoneText
        {
            get
            {
                var encrypted = "zzMinimapZone".GenLuaVarName();
                Functions.DoString(encrypted + " = GetMinimapZoneText();");
                return Functions.GetText(encrypted);
            }
        }

        internal void TurnOnSelfCast()
        {
            Functions.DoString(Strings.TurnOnSelfCast);
        }

        /// <summary>
        ///     Movement
        /// </summary>
        internal void StartMovement(Enums.ControlBits parBits)
        {
            Functions.SetControlBit((int) parBits, 1, Environment.TickCount);
            Shared.StartedMovement = true;
        }

        /// <summary>
        ///     Stop movement
        /// </summary>
        internal void StopMovement(Enums.ControlBits parBits)
        {
            Functions.SetControlBit((int) parBits, 0, Environment.TickCount);
            Shared.StartedMovement = false;
        }


        internal event CtmEventHandler OnCtmAction;

        private void OnCtmActionEvent(CtmAction e)
        {
            OnCtmAction?.Invoke(e);
        }

        /// <summary>
        ///     Start a ctm movement towards
        /// </summary>
        internal void CtmTo(XYZ parPosition)
        {
            //float disX = Math.Abs(this.CtmX - parPosition.X);
            //float disY = Math.Abs(this.CtmY - parPosition.Y);
            //if (disX < 0.2f && disY < 0.2f) return;

            OnCtmActionEvent(new CtmAction(Enums.CtmType.Move, parPosition));
            Functions.Ctm(Pointer, Enums.CtmType.Move, parPosition, 0);
            //SendMovementUpdate((int)Enums.MovementOpCodes.setFacing);
        }

        /// <summary>
        ///     Stop the current ctm movement
        /// </summary>
        internal void CtmStopMovement()
        {
            if (CtmState != (int) Enums.CtmType.None &&
                CtmState != 12) //&& CtmState != (int)Enums.CtmType.Face)
            {
                OnCtmActionEvent(new CtmAction(Enums.CtmType.None, new XYZ(0, 0, 0)));
                Functions.Ctm(Pointer, Enums.CtmType.None, ObjectManager.Player.Position, 0);
            }
            else if (Shared.StartedMovement && ObjectManager.Player.MovementState != 0)
            {
                ObjectManager.Player.StopMovement(Enums.ControlBits.All);
                Shared.StartedMovement = false;
            }

            //if (CtmState == 12) return;
            //CtmState = 12;
            //StartMovement(Enums.ControlBits.Front);
            //StopMovement(Enums.ControlBits.Front);
        }

        internal void CtmSetToIdle()
        {
            if (CtmState != 12)
                CtmState = 12;
        }

        internal void EnableCtm()
        {
            Functions.DoString(Strings.CtmOn);
        }

        internal void DisableCtm()
        {
            Functions.DoString(Strings.CtmOff);
        }

        internal int GetLatency()
        {
            var encrypted = Strings.GT_GetLatency.GenLuaVarName();
            Functions.DoString(Strings.GetLatency.Replace(Strings.GT_GetLatency, encrypted));
            return Convert.ToInt32(Functions.GetText(encrypted));
        }

        /// <summary>
        ///     Rightclick on a unit
        /// </summary>
        internal void RightClick(WoWUnit parUnit)
        {
            Functions.OnRightClickUnit(parUnit.Pointer, 1);
        }

        internal void RightClick(WoWUnit parUnit, bool parAuto)
        {
            var type = 0;
            if (parAuto) type = 1;
            Functions.OnRightClickUnit(parUnit.Pointer, type);
        }


        /// <summary>
        ///     ctm face / not used right now
        /// </summary>
        internal void CtmFace(WoWObject parObject)
        {
            var tmp = parObject.Position;
            Functions.Ctm(Pointer, Enums.CtmType.FaceTarget,
                new XYZ(tmp.X, tmp.Y, tmp.Z), parObject.Guid);
        }

        /// <summary>
        ///     Set Facing
        /// </summary>
        internal void Face(WoWObject parObject)
        {
            //XYZ xyz = new XYZ(parObject.Position.X, parObject.Position.Y, parObject.Position.Z);
            //Functions.Ctm(this.Pointer, Enums.CtmType.FaceTarget, xyz, parObject.Guid);
            Face(parObject.Position);
        }

        internal void Face(XYZ parPosition)
        {
            if (IsFacing(parPosition)) return;
            Functions.SetFacing(IntPtr.Add(Pointer, Offsets.Player.MovementStruct), RequiredFacing(parPosition));
            SendMovementUpdate((int) Enums.MovementOpCodes.setFacing);
        }

        internal bool IsFacing(XYZ parCoordinates)
        {
            return FacingRelativeTo(parCoordinates) < facingComparer;
        }

        /// <summary>
        ///     Send a movement update
        /// </summary>
        internal void SendMovementUpdate(int parOpCode)
        {
            Functions.SendMovementUpdate(Pointer, Environment.TickCount, parOpCode);
        }

        /// <summary>
        ///     Update last hardware action to avoid being flagged as AFK
        /// </summary>
        internal void AntiAfk()
        {
            Memory.Reader.Write(Offsets.Functions.LastHardwareAction, Environment.TickCount);
        }

        /// <summary>
        ///     Lets loot everything
        /// </summary>
        internal void LootAll()
        {
            Functions.LootAll();
        }

        /// <summary>
        ///     Set the target by guid
        /// </summary>
        internal void SetTarget(WoWObject parObject)
        {
            Functions.SetTarget(parObject.Guid);
            TargetGuid = parObject.Guid;
        }

        internal void SetTarget(ulong parGuid)
        {
            Functions.SetTarget(parGuid);
            TargetGuid = Guid;
        }

        /// <summary>
        ///     Execute LUA code
        /// </summary>
        internal void DoString(string parLua)
        {
            Functions.DoString(parLua);
        }

        /// <summary>
        ///     GetText of Lua code
        /// </summary>
        internal string GetText(string parVarName)
        {
            return Functions.GetText(parVarName);
        }

        /// <summary>
        ///     Get the rank of a spell - returns 0 if not learnt
        /// </summary>
        internal int GetSpellRank(string parSpell)
        {
            return Spells.GetSpellRank(parSpell);
        }

        internal void CancelShapeshift()
        {
            if (!IsShapeShifted) return;
            foreach (var x in Auras)
            {
                var tmp = Spells.GetName(x);
                if (tmp.Contains("Form"))
                {
                    Functions.DoString("CastSpellByName('" + tmp + "')");
                }
            }
        }

        /// <summary>
        ///     Refresh Spellbook
        /// </summary>
        internal void RefreshSpells()
        {
            Spells = new Spells();
        }
    }
}