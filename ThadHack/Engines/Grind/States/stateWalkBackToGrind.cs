using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateWalkBackToGrind : State
    {
        internal override int Priority => 42;

        internal override bool NeedToRun => Grinder.Access.Info.Vendor.GoBackToGrindAfterVendor;

        internal override string Name => "Travel back to Grind";

        internal override void Run()
        {
            if (Grinder.Access.Info.Vendor.RegenerateSubPath)
            {
                Grinder.Access.Info.PathManager.GrindToVendor.RegenerateSubPath();
                Grinder.Access.Info.Vendor.RegenerateSubPath = false;
            }

            var to = Grinder.Access.Info.PathManager.VendorToGrind.NextWaypoint;
            ObjectManager.Player.CtmTo(to);

            if (Grinder.Access.Info.PathManager.VendorToGrind.ArrivedAtDestination)
            {
                Grinder.Access.Info.Vendor.GoBackToGrindAfterVendor = false;
                Grinder.Access.Info.Waypoints.ResetGrindPath();
            }
        }
    }
}