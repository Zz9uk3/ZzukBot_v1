using ZzukBot.FSM;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateLoadNextHotspot : State
    {
        internal override int Priority => 15;

        internal override bool NeedToRun => Grinder.Access.Info.Waypoints.AtLastWaypoint;

        internal override string Name => "Loading next Hotspot";

        internal override void Run()
        {
            // Load the next hotspot in line
            Grinder.Access.Info.Waypoints.LoadNextHotspot();
        }
    }
}