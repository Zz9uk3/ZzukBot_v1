using System;
using System.Runtime.InteropServices;
using System.Text;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using static ZzukBot.Constants.Offsets;
using funcs = ZzukBot.Constants.Offsets.Functions;
// ReSharper disable UnusedMember.Local

namespace ZzukBot.Mem
{
    internal static class Functions
    {
        private static GetLootSlotsDelegate GetLootSlotsFunction;
        private static SetControlBitDelegate SetControlBitFunction;
        private static SetFacingDelegate SetFacingFunction;
        private static ClntObjMgrObjectPtr GetPtrForGuidFunction;
        private static GetPlayerGuidDelegate GetPlayerGuidFunction;
        private static SendMovementUpdateDelegate SendMovementUpdateFunction;
        private static OnRightClickUnitDelegate OnRightClickUnitFunction;
        private static OnRightClickObjectDelegate OnRightClickObjectFunction;
        private static LootAllDelegate LootAllFunction;
        private static UnitReactionDelegate UnitReactionFunction;
        private static SetTargetDelegate SetTargetFunction;
        private static ItemCacheGetRowDelegate ItemCacheGetRowFunction;
        private static GetSpellCooldownDelegate GetSpellCooldownFunction;
        private static UseItemDelegate UseItemFunction;
        private static CtmDelegate CtmFunction;
        private static NetClientSendDelegate NetClientSendFunction;
        private static ClientConnectionDelegate ClientConnectionFunction;
        private static GetCreatureRankDelegate GetCreatureRankFunction;
        private static GetCreatureTypeDelegate GetCreatureTypeFunction;
        private static LuaGetArgCountDelegate LuaGetArgCountFunction;
        private static HandleSpellTerrainDelegate HandleSpellTerrainFunction;

        /// <summary>
        ///     DoString: Delegate + Function
        /// </summary>
        [DllImport(Libs.FastCall, EntryPoint = "_DoString", CallingConvention = CallingConvention.StdCall)]
        private static extern void _DoString(string parLuaCode, IntPtr ptr);

        internal static void DoString(string parLuaCode)
        {
            _DoString(parLuaCode, funcs.DoString);
            //DoString_Stub(new IntPtr((uint)Offsets.functions.DoString), parLuaCode, parScriptName);
        }

        internal static void CastSpellByName(string parSpellName)
        {
            DoString("CastSpellByName('" + parSpellName + "')");
        }

        //[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        //private delegate void CastSpellByNameDelegate(IntPtr parLuaState);
        //private static CastSpellByNameDelegate CastSpellByNameFunction;
        //internal static unsafe void CastSpellByName(string parSpellName)
        //{
        //    if (CastSpellByNameFunction == null)
        //        CastSpellByNameFunction = Memory.Reader.RegisterDelegate<CastSpellByNameDelegate>((IntPtr)Constants.Offsets.Functions.CastSpellByName);
        //    IntPtr Ptr = Memory.Reader.Read<IntPtr>(Offsets.Misc.CGInputControlActive);

        //    _LuaPushString(*(IntPtr*)Misc.LuaState, parSpellName, (IntPtr)0x006F3890);
        //    CastSpellByNameFunction(*(IntPtr*)Misc.LuaState);
        //}

        [DllImport(Libs.FastCall, EntryPoint = "_RegFunc", CallingConvention = CallingConvention.StdCall)]
        private static extern void _RegFunc(string parFuncName, uint parFuncPtr, IntPtr ptr);


        [DllImport(Libs.FastCall, EntryPoint = "_LuaPushString", CallingConvention = CallingConvention.StdCall)]
        private static extern void _LuaPushString(IntPtr parLuaState, string parString, IntPtr ptr);

        internal static void RegisterFunction(string parFuncName, uint parFuncPtr)
        {
            _RegFunc(parFuncName, parFuncPtr, funcs.LuaRegisterFunc);
            //DoString_Stub(new IntPtr((uint)Offsets.functions.DoString), parLuaCode, parScriptName);
        }

        [DllImport(Libs.FastCall, EntryPoint = "_UnregFunc", CallingConvention = CallingConvention.StdCall)]
        private static extern void _UnregFunc(string parFuncName, uint parFuncPtr, IntPtr ptr);

        internal static void UnregisterFunction(string parFuncName, uint parFuncPtr)
        {
            _UnregFunc(parFuncName, parFuncPtr, funcs.LuaUnregFunc);
            //DoString_Stub(new IntPtr((uint)Offsets.functions.DoString), parLuaCode, parScriptName);
        }

        //[DllImport(Libs.FastCallPath, EntryPoint = "_LuaToNumber", CallingConvention = CallingConvention.StdCall)]
        //private static extern double _LuaToNumber(IntPtr parLuaState, int number, IntPtr Ptr);

        [DllImport(Libs.FastCall, EntryPoint = "_LuaToString", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr _LuaToString(IntPtr parLuaState, int number, IntPtr ptr);

        //[DllImport(Libs.FastCallPath, EntryPoint = "_LuaIsString", CallingConvention = CallingConvention.StdCall)]
        //private static extern int _LuaIsString(IntPtr parLuaState, int number, IntPtr Ptr);

        //[DllImport(Libs.FastCallPath, EntryPoint = "_LuaIsNumber", CallingConvention = CallingConvention.StdCall)]
        //private static extern int _LuaIsNumber(IntPtr parLuaState, int number, IntPtr Ptr);

        //internal static bool LuaIsString(IntPtr parLuaState, int number)
        //{
        //    return (_LuaIsString(parLuaState, number, funcs.LuaIsString) == 1 ? true : false);
        //}

        //internal static bool LuaIsNumber(IntPtr parLuaState, int number)
        //{
        //    return (_LuaIsNumber(parLuaState, number, funcs.LuaIsNumber) == 1 ? true : false);
        //}

        /// <summary>
        ///     GetText: Delegate + Function
        /// </summary>
        [DllImport(Libs.FastCall, EntryPoint = "_GetText", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr _GetText(string varName, IntPtr ptr);

        internal static string GetText(string parVarName)
        {
            var addr = _GetText(parVarName, funcs.GetText);
                //GetText_Stub(new IntPtr((uint)Offsets.functions.GetText), "hallo12", 0xFFFFFFFF, 0);
            return addr.ReadString();
        }

        internal static int GetLootSlots()
        {
            if (GetLootSlotsFunction == null)
                GetLootSlotsFunction = Memory.Reader.RegisterDelegate<GetLootSlotsDelegate>(funcs.GetLootSlots);
            return GetLootSlotsFunction();
        }


        internal static void EnterWorld()
        {
            //if (EnterWorldFunction == null)
            //    EnterWorldFunction = Memory.Reader.RegisterDelegate<EnterWorldDelegate>(Offsets.Functions.EnterWorld);
            //EnterWorldFunction();
            var str = "if CharSelectEnterWorldButton ~= nil then CharSelectEnterWorldButton:Click()  end";
            DoString(str);
        }

        internal static void SetControlBit(int parBit, int parState, int parTickCount)
        {
            if (SetControlBitFunction == null)
                SetControlBitFunction =
                    Memory.Reader.RegisterDelegate<SetControlBitDelegate>(funcs.CGInputControl__SetControlBit);
            var ptr = Misc.CGInputControlActive.ReadAs<IntPtr>();
            SetControlBitFunction(ptr, parBit, parState, parTickCount);
        }

        internal static void SetFacing(IntPtr parPlayerPtr, float parFacing)
        {
            if (SetFacingFunction == null)
                SetFacingFunction = Memory.Reader.RegisterDelegate<SetFacingDelegate>(funcs.SetFacing);
            SetFacingFunction(parPlayerPtr, parFacing);
        }


        /// <summary>
        ///     Enum Visible objects
        /// </summary>
        [DllImport(Libs.FastCall, EntryPoint = "_EnumVisibleObjects", CallingConvention = CallingConvention.StdCall)]
        private static extern void _EnumVisibleObjects(IntPtr callback, int filter, IntPtr ptr);

        internal static void EnumVisibleObjects(IntPtr callback, int filter)
        {
            _EnumVisibleObjects(callback, filter, funcs.EnumVisibleObjects);
        }

        internal static IntPtr GetPtrForGuid(ulong parGuid)
        {
            if (GetPtrForGuidFunction == null)
                GetPtrForGuidFunction = Memory.Reader.RegisterDelegate<ClntObjMgrObjectPtr>(funcs.GetPtrForGuid);
            return GetPtrForGuidFunction(parGuid);
        }

        internal static ulong GetPlayerGuid()
        {
            if (GetPlayerGuidFunction == null)
                GetPlayerGuidFunction =
                    Memory.Reader.RegisterDelegate<GetPlayerGuidDelegate>(funcs.ClntObjMgrGetActivePlayer);
            return GetPlayerGuidFunction();
        }

        internal static void SendMovementUpdate(IntPtr parPlayerPtr, int parTimeStamp, int parOpcode)
        {
            if (SendMovementUpdateFunction == null)
                SendMovementUpdateFunction =
                    Memory.Reader.RegisterDelegate<SendMovementUpdateDelegate>(funcs.SendMovementPacket);
            SendMovementUpdateFunction(parPlayerPtr, parTimeStamp, parOpcode, 0, 0);
        }

        internal static void OnRightClickUnit(IntPtr parPlayerPtr, int parAutoLoot)
        {
            if (OnRightClickUnitFunction == null)
                OnRightClickUnitFunction =
                    Memory.Reader.RegisterDelegate<OnRightClickUnitDelegate>(funcs.OnRightClickUnit);
            OnRightClickUnitFunction(parPlayerPtr, parAutoLoot);
        }

        internal static void OnRightClickObject(IntPtr parPlayerPtr, int parAutoLoot)
        {
            if (OnRightClickObjectFunction == null)
                OnRightClickObjectFunction =
                    Memory.Reader.RegisterDelegate<OnRightClickObjectDelegate>(funcs.OnRightClickObject);
            OnRightClickObjectFunction(parPlayerPtr, parAutoLoot);
        }

        internal static void LootAll()
        {
            if (LootAllFunction == null)
                LootAllFunction = Memory.Reader.RegisterDelegate<LootAllDelegate>(funcs.AutoLoot);
            LootAllFunction();
        }

        internal static int UnitReaction(IntPtr unitPtr1, IntPtr unitPtr2)
        {
            if (UnitReactionFunction == null)
                UnitReactionFunction = Memory.Reader.RegisterDelegate<UnitReactionDelegate>(funcs.UnitReaction);
            return UnitReactionFunction(unitPtr1, unitPtr2);
        }

        internal static void SetTarget(ulong parGuid)
        {
            if (SetTargetFunction == null)
                SetTargetFunction = Memory.Reader.RegisterDelegate<SetTargetDelegate>(funcs.SetTarget);
            SetTargetFunction(parGuid);
        }

        internal static IntPtr ItemCacheGetRow(int parItemId)
        {
            if (ItemCacheGetRowFunction == null)
                ItemCacheGetRowFunction = Memory.Reader.RegisterDelegate<ItemCacheGetRowDelegate>(funcs.ItemCacheGetRow);
            return ItemCacheGetRowFunction(funcs.ItemCacheGetRowFixedPtr1, parItemId, funcs.ItemCacheGetRowFixedPtr2, 0,
                0, 0x0);
        }

        internal static bool IsSpellReady(int spellId)
        {
            if (GetSpellCooldownFunction == null)
                GetSpellCooldownFunction =
                    Memory.Reader.RegisterDelegate<GetSpellCooldownDelegate>(funcs.GetSpellCooldown);

            var CdDuration = 0;
            var CdStartedAt = 0;
            var third = false;
            GetSpellCooldownFunction(funcs.GetSpellCooldownPtr1, spellId, 0, ref CdDuration, ref CdStartedAt, ref third);
            return CdDuration == 0 || CdStartedAt == 0;
        }

        [DllImport(Libs.FastCall, EntryPoint = "_CastSpell", CallingConvention = CallingConvention.StdCall)]
        private static extern uint _CastSpell(int SpellId, IntPtr ptr);

        internal static uint CastSpell(int spellId, ulong parGuid)
        {
            try
            {
                return _CastSpell(spellId, funcs.CastSpell);
            }
            catch (Exception) //Access violation
            {
                return 0;
            }
        }

        internal static void UseItem(IntPtr ptr, ulong guid)
        {
            if (UseItemFunction == null)
                UseItemFunction = Memory.Reader.RegisterDelegate<UseItemDelegate>(funcs.UseItem);
            ulong zeroPtr = guid;
            UseItemFunction(ptr, ref zeroPtr, 0);
        }

        internal static void Ctm(IntPtr parPlayerPtr, Enums.CtmType parType, XYZ parPosition, ulong parGuid)
        {
            if (CtmFunction == null)
                CtmFunction = Memory.Reader.RegisterDelegate<CtmDelegate>(funcs.ClickToMove);
            var guid = parGuid;
            CtmFunction(parPlayerPtr, (uint) parType, ref guid,
                ref parPosition.ToStruct, 2);
        }

        internal static void NetClientSend(IntPtr pDataStore)
        {
            if (NetClientSendFunction == null)
                NetClientSendFunction = Memory.Reader.RegisterDelegate<NetClientSendDelegate>(funcs.NetClientSend);
            NetClientSendFunction(ClientConnection(), pDataStore.ToInt32());
        }

        internal static IntPtr ClientConnection()
        {
            if (ClientConnectionFunction == null)
                ClientConnectionFunction =
                    Memory.Reader.RegisterDelegate<ClientConnectionDelegate>(funcs.ClientConnection);
            return ClientConnectionFunction();
        }

        internal static int GetCreatureRank(IntPtr parUnitPtr)
        {
            if (GetCreatureRankFunction == null)
                GetCreatureRankFunction = Memory.Reader.RegisterDelegate<GetCreatureRankDelegate>(funcs.GetCreatureRank);
            return GetCreatureRankFunction(parUnitPtr);
        }

        internal static int GetCreatureType(IntPtr parUnitPtr)
        {
            if (GetCreatureTypeFunction == null)
                GetCreatureTypeFunction = Memory.Reader.RegisterDelegate<GetCreatureTypeDelegate>(funcs.GetCreatureType);
            return GetCreatureTypeFunction(parUnitPtr);
        }

        internal static int LuaGetArgCount(IntPtr parLuaState)
        {
            if (LuaGetArgCountFunction == null)
                LuaGetArgCountFunction = Memory.Reader.RegisterDelegate<LuaGetArgCountDelegate>(funcs.LuaGetArgCount);
            return LuaGetArgCountFunction(parLuaState);
        }

        internal static int HandleSpellTerrain(XYZ parPos)
        {
            if (HandleSpellTerrainFunction == null)
                HandleSpellTerrainFunction =
                    Memory.Reader.RegisterDelegate<HandleSpellTerrainDelegate>(funcs.HandleSpellTerrainClick);
            Memory.Reader.Write<uint>((IntPtr) 0xCECAC0, 0x40);
            return HandleSpellTerrainFunction(ref parPos.ToStruct);
        }

        //internal static double LuaToNumber(IntPtr parLuaState, int number)
        //{
        //    return _LuaToNumber(parLuaState, number, funcs.LuaToNumber);
        //}

        internal static string LuaToString(IntPtr parLuaState, int number)
        {
            var ptr = _LuaToString(parLuaState, number, funcs.LuaToString);
            return ptr.ReadString();
        }


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetLootSlotsDelegate();

        /// <summary>
        ///     Basic movement: GetActive CGInputControl
        /// </summary>
        /// <summary>
        ///     Basic movement: Set CGInputControlBit
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetControlBitDelegate(IntPtr device, int bit, int state, int tickCount);

        /// <summary>
        ///     Basic movement: Set Facing
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SetFacingDelegate(IntPtr playerPtr, float facing);

        /// <summary>
        ///     Get Ptr for guid
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate IntPtr ClntObjMgrObjectPtr(ulong guid);

        /// <summary>
        ///     Get Ptr for player
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong GetPlayerGuidDelegate();


        /// <summary>
        ///     Send a movement update
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void SendMovementUpdateDelegate(
            IntPtr playerPtr, int timestamp, int opcode, float zero, int zero2);

        /// <summary>
        ///     Interact with Unit
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void OnRightClickUnitDelegate(IntPtr unitPtr, int autoLoot);

        /// <summary>
        ///     Interact with Object
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void OnRightClickObjectDelegate(IntPtr unitPtr, int autoLoot);

        ///// <summary>
        ///// Are we looting?
        ///// </summary>
        //[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        //private delegate byte IsLootingDelegate(IntPtr playerPtr);
        //private static IsLootingDelegate IsLootingFunction;
        //internal static bool IsLooting(IntPtr parPlayerPtr)
        //{
        //    if (IsLootingFunction == null)
        //        IsLootingFunction = Memory.Reader.RegisterDelegate<IsLootingDelegate>((IntPtr)Constants.Offsets.Functions.IsLooting);

        //    return IsLootingFunction(parPlayerPtr) == 1;
        //}

        /// <summary>
        ///     Lets loot everything
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void LootAllDelegate();


        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int UnitReactionDelegate(IntPtr unitPtr1, IntPtr unitPtr2);

        /// <summary>
        ///     How many items can we loot?
        /// </summary>
        /// <summary>
        ///     Get the current map id
        /// </summary>
        /// <summary>
        ///     Set the target by guid
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetTargetDelegate(ulong guid);

        ///// <summary>
        ///// Intersect between two points
        ///// </summary>
        //[DllImport(Libs.FastCallPath, EntryPoint = "_Intersect", CallingConvention = CallingConvention.StdCall)]
        //private static extern byte _Intersect(ref _XYZXYZ points, ref float distance, ref Intersection intersection, int flags, IntPtr Ptr);

        //internal static byte Intersect(XYZ parStart, XYZ parEnd)
        //{
        //    _XYZXYZ points = new _XYZXYZ(parStart.X, parStart.Y, parStart.Z,
        //        parEnd.X, parEnd.Y, parEnd.Z);
        //    points.Z1 += 2;
        //    points.Z2 += 2;
        //    Intersection intersection = new Intersection();
        //    float distance = Calc.Distance2D(parStart, parEnd);
        //    int flags = 0x100111;
        //    return _Intersect(ref points, ref distance, ref intersection, flags, funcs.Intersect);
        //}

        /// <summary>
        ///     Get Item from Cache
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate IntPtr ItemCacheGetRowDelegate(
            IntPtr fixedPtr, int itemId, IntPtr fixedPtr2, int zero, int _zero, int __zero);


        /// <summary>
        ///     GETSPELLCOOLDOWN
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void GetSpellCooldownDelegate(
            IntPtr spellCooldownPtr, int spellId, int zero, ref int first, ref int second, ref bool third);

        /// <summary>
        ///     Use an item
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void UseItemDelegate(IntPtr ptr, ref ulong zeroPtr, int zero);

        /// <summary>
        ///     ClickToMove
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void CtmDelegate
            (IntPtr playerPtr, uint clickType, ref ulong interactGuidPtr, ref _XYZ posPtr, float precision);

        ///// <summary>
        ///// QueryDbCreatureCache
        ///// </summary>
        //[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        //private delegate IntPtr DbQueryCreatureCacheDelegate
        //(IntPtr ptrToCache, int IdOfCreature, int randomShit, int randomShit2, int randomShit3, int randomShit4);
        //private static DbQueryCreatureCacheDelegate DbQueryCreatureCacheFunction;
        //internal static IntPtr DbQueryCreatureCache(int parCreatureId)
        //{
        //    if (DbQueryCreatureCacheFunction == null)
        //        DbQueryCreatureCacheFunction = Memory.Reader.RegisterDelegate<DbQueryCreatureCacheDelegate>(funcs.DbQueryCreatureCache);
        //    return DbQueryCreatureCacheFunction(funcs.DbQueryCreatureCachePtr1, parCreatureId, 0, 0, 0, 0);
        //}

        /// <summary>
        ///     NetClientSend
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate void NetClientSendDelegate
            (IntPtr clientConn, int pDataStore);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate IntPtr ClientConnectionDelegate
            ();

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetCreatureRankDelegate
            (IntPtr parUnitPtr);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int GetCreatureTypeDelegate
            (IntPtr parUnitPtr);


        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int LuaGetArgCountDelegate
            (IntPtr parLuaState);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate int HandleSpellTerrainDelegate
            (ref _XYZ parPos);
    }
}