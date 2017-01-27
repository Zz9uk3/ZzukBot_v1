using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateLoadNextWaypoint : State
    {
        internal override int Priority => 14;

        internal override bool NeedToRun => Grinder.Access.Info.Waypoints.NeedToLoadNextWaypoint() && ObjectManager.Player.Casting == 0 &&
                                            ObjectManager.Player.Channeling == 0;

        internal override string Name => "Loading next Waypoint";

        internal override void Run()
        {
            Grinder.Access.Info.PathAfterFightToWaypoint.DisableAfterFightMovement();
            // load the next waypoint in line
            Grinder.Access.Info.Waypoints.LoadNextWaypoint();
            // face the waypoint and start moving
            ObjectManager.Player.CtmTo(Grinder.Access.Info.Waypoints.CurrentWaypoint);
        }
    }
}