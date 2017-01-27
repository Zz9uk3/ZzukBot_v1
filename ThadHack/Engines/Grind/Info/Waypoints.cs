using System;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _Waypoints
    {
        private Action LoadFirstWaypointCallback;

        internal _Waypoints()
        {
            CurrentWaypointIndex = 0;
            CurrentHotspotIndex = ClosestHotspotIndex;
        }

        // index of the current waypoint
        internal int CurrentWaypointIndex { get; private set; }
        // index of the current hotspot
        internal int CurrentHotspotIndex { get; set; }
        // list of waypoints generated from player to hotspot
        private XYZ[] CurrentWaypoints { get; set; }

        /// <summary>
        ///     Object of our current hotspot
        /// </summary>
        internal XYZ CurrentHotspot => Grinder.Access.Profile.Hotspots[CurrentHotspotIndex].Position;

        /// <summary>
        ///     Object of our current waypoint
        /// </summary>
        internal XYZ CurrentWaypoint => CurrentWaypoints[CurrentWaypointIndex];

        /// <summary>
        ///     Did we arrive at the last waypoint?
        /// </summary>
        internal bool AtLastWaypoint => CurrentWaypointIndex == CurrentWaypoints.Length - 1 && NeedToLoadNextWaypoint(1.8f);

        // Distance to current waypoint

        private int ClosestHotspotIndex
        {
            get
            {
                var closestIndex = -1;
                for (var i = CurrentWaypointIndex; i < Grinder.Access.Profile.Hotspots.Length; i++)
                {
                    if (closestIndex == -1)
                    {
                        closestIndex = 0;
                        continue;
                    }
                    var disA = Calc.Distance2D(ObjectManager.Player.Position,
                        Grinder.Access.Profile.Hotspots[closestIndex].Position);
                    var disB = Calc.Distance2D(ObjectManager.Player.Position,
                        Grinder.Access.Profile.Hotspots[i].Position);
                    if (disB < disA)
                    {
                        closestIndex = i;
                    }
                }
                return closestIndex;
            }
        }

        /// <summary>
        ///     Set current waypoint to the closest
        /// </summary>
        internal void SetCurrentWaypointToClosest()
        {
            var closestIndex = CurrentWaypointIndex;
            for (var i = CurrentWaypointIndex; i < CurrentWaypoints.Length; i++)
            {
                float disA;
                float disB;

                if (Shared.IgnoreZAxis)
                {
                    disA = Calc.Distance2D(ObjectManager.Player.Position, CurrentWaypoints[closestIndex]);
                    disB = Calc.Distance2D(ObjectManager.Player.Position, CurrentWaypoints[i]);
                }
                else
                {
                    disA = Calc.Distance3D(ObjectManager.Player.Position, CurrentWaypoints[closestIndex]);
                    disB = Calc.Distance3D(ObjectManager.Player.Position, CurrentWaypoints[i]);
                }
                


                if (disB < disA)
                {
                    closestIndex = i;
                }
            }
            CurrentWaypointIndex = closestIndex;
        }

        internal int GetClosestHotspotIndex()
        {
            var closestIndex = CurrentHotspotIndex;
            for (var i = 0; i < Grinder.Access.Profile.Hotspots.Length; i++)
            {
                float disA;
                float disB;

                if (Shared.IgnoreZAxis)
                {
                    disA = Calc.Distance2D(ObjectManager.Player.Position,
                    Grinder.Access.Profile.Hotspots[closestIndex].Position);
                    disB = Calc.Distance2D(ObjectManager.Player.Position, Grinder.Access.Profile.Hotspots[i].Position);
                }
                else
                {
                    disA = Calc.Distance3D(ObjectManager.Player.Position,
                    Grinder.Access.Profile.Hotspots[closestIndex].Position);
                    disB = Calc.Distance3D(ObjectManager.Player.Position, Grinder.Access.Profile.Hotspots[i].Position);
                }
                


                if (disB < disA)
                {
                    closestIndex = i;
                }
            }
            return closestIndex;
        }

        /// <summary>
        ///     Load the next hotspot if we reached the end of our current waypoints
        /// </summary>
        internal void LoadNextHotspot()
        {
            var tmp = CurrentHotspotIndex + 1;
            if (tmp < Grinder.Access.Profile.Hotspots.Length)
                CurrentHotspotIndex = tmp;
            else
            {
                Array.Reverse(Grinder.Access.Profile.Hotspots);
                CurrentHotspotIndex = 1;
            }
            // load the next set of waypoints and set the index to 0
            Grinder.Access.Info.Waypoints.LoadWaypoints();
        }

        /// <summary>
        ///     Load the next waypoint in lsit if we reached the current one
        /// </summary>
        internal void LoadNextWaypoint()
        {
            CurrentWaypointIndex =
                (CurrentWaypointIndex + 1)%CurrentWaypoints.Length;
        }

        /// <summary>
        ///     Load waypoints from player to current hotspot
        /// </summary>
        internal void LoadWaypoints()
        {
            var pos = ObjectManager.Player.Position;
            CurrentWaypoints = Navigation.CalculatePath(pos, CurrentHotspot, true);
            CurrentWaypointIndex = 1;
        }

        internal void LoadFirstWaypointsAsync(Action Callback)
        {
            var pos = ObjectManager.Player.Position;
            LoadFirstWaypointCallback = Callback;
            Navigation.CalculatePathAsync(pos, CurrentHotspot, true, LoadFirstCallBack);
        }

        private void LoadFirstCallBack(XYZ[] parPath)
        {
            CurrentWaypoints = parPath;
            CurrentWaypointIndex = 1;
            LoadFirstWaypointCallback();
        }

        /// <summary>
        ///     Are we close enough to the current waypoint? If yes load next
        /// </summary>
        internal bool NeedToLoadNextWaypoint(XYZ parPoint, float disToPoint = 1.3f)
        {
            var dis = Calc.Distance2D(ObjectManager.Player.Position, parPoint);
            return dis <= disToPoint + (float) Options.WaypointModifier;
        }

        internal bool NeedToLoadNextWaypoint(float disToPoint = 1.3f)
        {
            var dis = Calc.Distance2D(ObjectManager.Player.Position, CurrentWaypoint);
            return dis <= disToPoint + (float) Options.WaypointModifier;
        }

        internal void ResetGrindPath()
        {
            CurrentHotspotIndex = 0;
            CurrentWaypointIndex = 0;
            RevertHotspotsToOriginal();
        }

        internal void RevertHotspotsToOriginal()
        {
            Grinder.Access.Profile.Hotspots = Grinder.Access.Profile.OriginalHotspots;
        }
    }
}