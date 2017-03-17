using System;
using ZzukBot.Settings;

namespace ZzukBot.Constants
{
    internal static class Enums
    {
        internal enum WoWObjectTypes : byte
        {
            OT_NONE = 0,
            OT_ITEM = 1,
            OT_CONTAINER = 2,
            OT_UNIT = 3,
            OT_PLAYER = 4,
            OT_GAMEOBJ = 5,
            OT_DYNOBJ = 6,
            OT_CORPSE = 7
        }

        internal static class CreatureType
        {
            internal static int Beast = 1;
            internal static int Dragonkin = 2;
            internal static int Demon = 3;
            internal static int Elemental = 4;
            internal static int Giant = 5;
            internal static int Undead = 6;
            internal static int Humanoid = 7;
            internal static int Critter = 8;
            internal static int Mechanical = 9;
            internal static int NotSpecified = 10;
            internal static int Totem = 11;
        }


        internal static class DynamicFlags
        {
            internal static uint IsMarked = 0x2;
            internal static uint CanBeLooted = 0xD;
            internal static uint TappedByMe = 0xC;
            internal static uint TappedByOther = 0x4;
            internal static uint Untouched = 0x0;
            internal static int AuraBase = 0xBC;
            internal static int NextAura = 4;
            //internal static uint CanBeLooted = 0xD;
            //internal static uint TappedByMe = 0xC;
            //internal static uint TappedByOther = 0x4;
            //internal static uint Untouched = 0x0;

            internal static void AdjustToRealm()
            {
                var isElysium = Options.RealmList.Contains("elysium");
                if (!Options.RealmList.Contains("nostalrius") && !isElysium)
                {
                    CanBeLooted = 0x1;
                    TappedByMe = 0x0;
                }
                if (Options.RealmList.Contains("vanillagaming"))
                {
                    AuraBase = 0x138;
                    NextAura = -4;
                }
            }
        }

        internal static class UnitFlags
        {
            internal static int UNIT_FLAG_FLEEING = 0x00800000;
            internal static int UNIT_FLAG_CONFUSED = 0x00400000;
            internal static int UNIT_FLAG_IN_COMBAT = 0x00080000;
            internal static int UNIT_FLAG_SKINNABLE = 0x04000000;
            internal static int UNIT_FLAG_STUNNED = 0x00040000;
            internal static int UNIT_FLAG_DISABLE_MOVE = 0x00000004;
        }

        internal enum MovementFlags : uint
        {
            None = 0x0,
            Front = 0x00000001,
            Back = 0x00000002,
            Left = 0x00000010,
            Right = 0x00000020,
            StrafeLeft = 0x00000004,
            StrafeRight = 0x00000008,

            Swimming = 0x00200000,
            jumping = 0x00002000,
            Falling = 0x0000A000,
            Levitate = 0x70000000
        }

        [Flags]
        internal enum ControlBits : uint
        {
            All = Front | Right | Left | StrafeLeft | StrafeRight | Back,
            Nothing = 0x0,
            CtmWalk = 0x00001000,
            Front = 0x00000010,
            Back = 0x00000020,
            Left = 0x00000100,
            Right = 0x00000200,
            StrafeLeft = 0x00000040,
            StrafeRight = 0x00000080
        }

        internal enum ControlBitsMouse : uint
        {
            Rightclick = 0x00000001,
            Leftclick = 0x00000002
        }

        internal enum ChatType
        {
            Say = 0,
            Yell = 5,
            Channel = 14,
            Group = 1,
            Guild = 3,
            Whisper = 6
        }

        internal enum LoginState
        {
            login,
            charselect
        }

        internal enum UnitReaction : uint
        {
            Neutral = 3,
            Friendly = 4,

            // Guards of the other faction are for example hostile 2.
            // All other hostile mobs I met are just hostile.
            Hostile = 1,
            Hostile2 = 0
        }

        internal enum ClassIds : byte
        {
            Warrior = 1,
            Paladin = 2,
            Hunter = 3,
            Rogue = 4,
            Priest = 5,
            Shaman = 7,
            Mage = 8,
            Warlock = 9,
            Druid = 11
        }

        internal enum MovementOpCodes : uint
        {
            stopTurn = 0xBE,
            turnLeft = 0xBC,
            turnRight = 0xBD,

            moveStop = 0xB7,
            moveFront = 0xB5,
            moveBack = 0xB6,

            setFacing = 0xDA,

            heartbeat = 0xEE,

            strafeLeft = 0xB8,
            strafeRightStart = 0xB9,
            strafeStop = 0xBA
        }

        internal enum ItemQuality
        {
            Grey = 0,
            White = 1,
            Green = 2,
            Blue = 3,
            Epic = 4
        }

        internal enum WaypointType
        {
            Waypoint = 0,
            VendorWaypoint = 1,
            GhostWaypoint = 2
        }

        internal enum CtmType : uint
        {
            FaceTarget = 0x1,
            Face = 0x2,

            /// <summary>
            ///     Will throw a UI error. Have not figured out how to avoid it!
            /// </summary>
            // ReSharper disable InconsistentNaming
            Stop_ThrowsException = 0x3,
            // ReSharper restore InconsistentNaming
            Move = 0x4,
            NpcInteract = 0x5,
            Loot = 0x6,
            ObjInteract = 0x7,
            FaceOther = 0x8,
            Skin = 0x9,
            AttackPosition = 0xA,
            AttackGuid = 0xB,
            ConstantFace = 0xC,
            None = 0xD,
            Attack = 0x10,
            Idle = 0xC
        }

        internal enum PositionType
        {
            Hotspot,
            Waypoint
        }
    }
}