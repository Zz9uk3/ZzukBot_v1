using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Objects;

namespace ZzukBot.Mem
{
    internal static class ObjectManager
    {
        /// <summary>
        ///     Delegate Instances for: Enum, Callback, GetPtrByGuid, GetActivePlayer
        /// </summary>
        private static bool Prepared;

        private static IntPtr ourCallback;
        private static /*readonly*/ EnumVisibleObjectsCallback _callback;


        /// <summary>
        ///     Objectmanager internal dictionary
        /// </summary>
        private static readonly Dictionary<ulong, WoWObject> _Objects = new Dictionary<ulong, WoWObject>();

        private static volatile List<WoWObject> Objects = new List<WoWObject>();
        private static volatile bool ShouldUpdateSpells;

        /// <summary>
        ///     Objectmanager internal lists
        /// </summary>
        internal static LocalPlayer Player { get; private set; }

        // the toons current target
        internal static WoWUnit Target
        {
            get
            {
                if (Player.TargetGuid == 0) return null;
                var mobs = Units;
                return mobs
                    .FirstOrDefault(i => i.Guid == Player.TargetGuid);
            }
        }

        internal static List<WoWUnit> Units => Objects.OfType<WoWUnit>().ToList();

        internal static List<WoWUnit> Players
        {
            get
            {
                return
                    Objects.OfType<WoWUnit>().ToList().Where(i => i.WoWType == Enums.WoWObjectTypes.OT_PLAYER).ToList();
            }
        }

        internal static List<WoWUnit> Npcs
        {
            get { return Objects.OfType<WoWUnit>().Where(i => i.WoWType == Enums.WoWObjectTypes.OT_UNIT).ToList(); }
        }


        internal static List<WoWGameObject> GameObjects => Objects.OfType<WoWGameObject>()
            .ToList();

        internal static List<WoWItem> Items => Objects.OfType<WoWItem>()
            .ToList();

        internal static bool IsIngame { get; private set; }

        /// <summary>
        ///     Initialise Object Manager
        /// </summary>
        internal static void Init()
        {
            if (Prepared) return;
            IsIngame = false;
            _callback = Callback;
            ourCallback = Marshal.GetFunctionPointerForDelegate(_callback);
            Prepared = false;
        }

        internal static void UpdateSpells()
        {
            ShouldUpdateSpells = true;
        }


        private static object enumLock = new object();

        /// <summary>
        ///     Enumerate through the object manager
        ///     true if ingame
        ///     false if not ingame
        /// </summary>
        internal static bool EnumObjects()
        {
            if (Offsets.Player.IsIngame.ReadAs<byte>() == 0) return false;
            return _EnumObjects();
        }

        private static bool _EnumObjects()
        {
            lock (enumLock)
            {
                // renew playerptr if invalid
                // return if no pointer can be retrieved
                var playerGuid = Functions.GetPlayerGuid();
                if (playerGuid == 0) return false;
                var playerObject = Functions.GetPtrForGuid(playerGuid);
                if (playerObject == IntPtr.Zero) return false;
                if (Player == null || playerObject != Player.Pointer)
                    Player = new LocalPlayer(playerGuid, playerObject, Enums.WoWObjectTypes.OT_PLAYER);

                if (ShouldUpdateSpells)
                {
                    Player.RefreshSpells();
                    ShouldUpdateSpells = false;
                }

                // set the pointer of all objects to 0
                foreach (var obj in _Objects.Values)
                    obj.Pointer = IntPtr.Zero;

                Functions.EnumVisibleObjects(ourCallback, -1);

                // remove the pointer that are stil zero 
                // (pointer not updated from 0 = object not in manager anymore)
                foreach (var pair in _Objects.Where(p => p.Value.Pointer == IntPtr.Zero).ToList())
                    _Objects.Remove(pair.Key);

                // assign dictionary to list which is viewable from internal
                Objects = _Objects.Values.ToList();
                return true;
            }
        }

        /// <summary>
        ///     The callback for EnumVisibleObjects
        /// </summary>
        private static int Callback(int filter, ulong guid)
        {
            if (guid == 0) return 0;
            var ptr = Functions.GetPtrForGuid(guid);
            if (ptr == IntPtr.Zero) return 0;
            if (_Objects.ContainsKey(guid))
            {
                _Objects[guid].Pointer = ptr;
            }
            else
            {
                var objType =
                    (Enums.WoWObjectTypes)
                        ptr.Add((int) Offsets.ObjectManager.ObjType).ReadAs<byte>();
                switch (objType)
                {
                    case Enums.WoWObjectTypes.OT_CONTAINER:
                    case Enums.WoWObjectTypes.OT_ITEM:
                        var tmpItem = new WoWItem(guid, ptr, objType);

                        _Objects.Add(guid, tmpItem);
                        break;

                    case Enums.WoWObjectTypes.OT_UNIT:
                        var tmpUnit = new WoWUnit(guid, ptr, objType);

                        _Objects.Add(guid, tmpUnit);
                        break;

                    case Enums.WoWObjectTypes.OT_PLAYER:
                        var tmpPlayer = new WoWUnit(guid, ptr, objType);
                        _Objects.Add(guid, tmpPlayer);
                        break;

                    case Enums.WoWObjectTypes.OT_GAMEOBJ:
                        var tmpGameObject = new WoWGameObject(guid, ptr, objType);
                        _Objects.Add(guid, tmpGameObject);
                        break;
                }
            }
            return 1;
        }

        /// <summary>
        ///     Delegates for: Enum, Callback, GetPtrByGuid, GetActivePlayer
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int EnumVisibleObjectsCallback(int filter, ulong guid);
    }
}