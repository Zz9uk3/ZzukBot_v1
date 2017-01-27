using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info.Path
{
    internal class _PathAfterFightToWaypoint
    {
        private bool _AdjustWaypoints;
        private bool ShouldReturn;

        internal bool AfterFightMovement { get; private set; }

        // Do we need to get back to our current path?

        internal void SetAfterFightMovement()
        {
            AfterFightMovement = true;
            _AdjustWaypoints = true;
        }

        internal void DisableAfterFightMovement()
        {
            AfterFightMovement = false;
        }

        internal void AdjustPath()
        {
            if (_AdjustWaypoints)
            {
                Shared.RandomResetJumperComplete();
                Grinder.Access.Info.Waypoints.SetCurrentWaypointToClosest();
                _AdjustWaypoints = false;
            }
        }

        internal bool NeedToReturn()
        {
            var diff = Calc.Distance2D(Grinder.Access.Info.Waypoints.CurrentWaypoint,
                ObjectManager.Player.Position);
            if (diff > Options.MaxDiffToWp)
            {
                ShouldReturn = true;
                if (AfterFightMovement)
                {
                    Shared.RandomResetJumper();
                    Grinder.Access.Info.Waypoints.SetCurrentWaypointToClosest();
                    AfterFightMovement = false;
                }
            }
            else if (diff <= 1.2f)
            {
                ShouldReturn = false;
            }
            return ShouldReturn;
        }
    }
}