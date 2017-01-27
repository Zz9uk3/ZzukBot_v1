using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateAfterFightToPath : State
    {
        internal override int Priority => 33;

        internal override bool NeedToRun => Grinder.Access.Info.PathAfterFightToWaypoint.NeedToReturn();

        internal override string Name => "Back to path";

        internal override void Run()
        {
            Shared.RandomJump();

            ObjectManager.Player.CtmTo(Grinder.Access.Info.PathToPosition.ToPos(
                Grinder.Access.Info.Waypoints.CurrentWaypoint));
            // Nothing to do here
        }
    }
}