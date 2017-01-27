using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateWalkToRepair : State
    {
        internal override int Priority => 41;

        internal override bool NeedToRun => Grinder.Access.Info.Vendor.TravelingToVendor;

        internal override string Name => "Travel to Vendor";

        internal override void Run()
        {
            if (Grinder.Access.Info.Vendor.RegenerateSubPath)
            {
                Grinder.Access.Info.PathManager.GrindToVendor.RegenerateSubPath();
                Grinder.Access.Info.Vendor.RegenerateSubPath = false;
            }

            var to = Grinder.Access.Info.PathManager.GrindToVendor.NextWaypoint;
            ObjectManager.Player.CtmTo(to);

            if (Grinder.Access.Info.PathManager.GrindToVendor.ArrivedAtDestination)
            {
                Grinder.Access.Info.Vendor.TravelingToVendor = false;
            }
        }
    }
}