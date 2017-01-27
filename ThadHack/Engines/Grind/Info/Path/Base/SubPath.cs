using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.Info.Path.Base
{
    internal class SubPath
    {
        internal Waypoint EndPoint;

        internal XYZ[] FromStartToEnd;
        private int FromStartToEndIndex;
        internal Waypoint StartPoint;

        internal SubPath(Waypoint parStartPoint, Waypoint parEndPoint)
        {
            StartPoint = parStartPoint;
            EndPoint = parEndPoint;

            if (EndPoint.Type == Enums.PositionType.Hotspot)
            {
                FromStartToEnd = Navigation.CalculatePath(StartPoint.Position,
                    EndPoint.Position, true);
                FromStartToEndIndex = 1;
            }
        }

        internal XYZ CurrentWaypoint
        {
            get
            {
                if (NeedToLoadNextWaypoint)
                {
                    LoadNextWaypoint();
                }
                if (EndPoint.Type == Enums.PositionType.Waypoint)
                    return EndPoint.Position;
                return FromStartToEnd[FromStartToEndIndex];
            }
        }

        internal bool ArrivedAtEndPoint
        {
            get
            {
                if (EndPoint.Type == Enums.PositionType.Hotspot &&
                    NeedToLoadNextWaypoint &&
                    FromStartToEnd.Length - 1 == FromStartToEndIndex &&
                    Calc.Distance2D(EndPoint.Position,
                        FromStartToEnd[FromStartToEnd.Length - 1]) > 1)
                {
                    return true;
                }

                var res = false;
                if (!Grinder.Access.Info.Waypoints.NeedToLoadNextWaypoint(
                    EndPoint.Position))
                {
                    return false;
                }
                if (EndPoint.Type == Enums.PositionType.Waypoint)
                {
                    res = true;
                }
                else if (EndPoint.Type == Enums.PositionType.Hotspot
                         && FromStartToEndIndex >= FromStartToEnd.Length - 1)
                {
                    res = true;
                }
                return res;
            }
        }

        internal bool NeedToLoadNextWaypoint
        {
            get
            {
                if (EndPoint.Type == Enums.PositionType.Waypoint)
                    return false;

                if (Grinder.Access.Info.Waypoints.NeedToLoadNextWaypoint(
                    FromStartToEnd[FromStartToEndIndex]))
                {
                    return true;
                }
                return false;
            }
        }

        internal void LoadNextWaypoint()
        {
            if (!NeedToLoadNextWaypoint) return;
            if (FromStartToEndIndex <= FromStartToEnd.Length - 2)
            {
                FromStartToEndIndex++;
            }
        }

        internal void RegenerateWaypoints()
        {
            if (EndPoint.Type == Enums.PositionType.Hotspot)
            {
                FromStartToEnd = Navigation.CalculatePath(ObjectManager.Player.Position,
                    EndPoint.Position, true);
                FromStartToEndIndex = 1;
            }
        }
    }
}