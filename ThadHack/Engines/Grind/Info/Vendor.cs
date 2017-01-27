using System.Collections.Generic;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _Vendor
    {
        internal bool GoBackToGrindAfterVendor = false;
        internal List<XYZ> HotspotsToVendor;
        internal bool RegenerateSubPath = false;

        internal bool TravelingToVendor;

        internal _Vendor()
        {
            _NeedToVendor = false;
            TravelingToVendor = false;
        }

        private bool _NeedToVendor { get; set; }

        internal bool NeedToVendor
        {
            get
            {
                var res = ObjectManager.Player.Inventory.FreeSlots < Options.MinFreeSlotsBeforeVendor;
                if (res) _NeedToVendor = true;
                return _NeedToVendor;
            }
        }

        internal bool GossipOpen
        {
            get
            {
                var encryptedName = Strings.GT_IsVendorOpen.GenLuaVarName();
                Functions.DoString(Strings.IsVendorOpen.Replace(Strings.GT_IsVendorOpen, encryptedName));
                var res = Functions.GetText(encryptedName) == "true";
                return res;
            }
        }

        internal void DoneVendoring()
        {
            _NeedToVendor = false;
        }
    }
}