using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.Info.Path
{
    internal class _PathToPosition
    {
        private XYZ posEndPos;
        private int posLastWaypointIndex;
        private XYZ[] posPath;

        internal XYZ ToPos(XYZ parPosition)
        {
            if (Shared.IgnoreZAxis)
            {
                if (Calc.Distance2D(ObjectManager.Player.Position, parPosition) < 3)
                    return parPosition;
            }
            else
            {
                if (Calc.Distance3D(ObjectManager.Player.Position, parPosition) < 3)
                    return parPosition;
            }
            

            var recalc = false;
            if (Wait.For("BetweenPoints", 5000))
            {
                recalc = !ToonBetweenPoints();
            }

            if (posEndPos == null || Calc.Distance2D(parPosition, posEndPos) > 2f ||
                recalc)
            {
                posPath = Navigation.CalculatePath(ObjectManager.Player.Position,
                    parPosition, true);
                posLastWaypointIndex = 0;
                posEndPos = parPosition;
            }
            if (posPath.Length > 0)
            {
                if (Grinder.Access.Info.Waypoints.
                    NeedToLoadNextWaypoint(posPath[posLastWaypointIndex]))
                    //if (ObjectManager.Player.DistanceTo(
                    //posPath[posLastWaypointIndex]) < 1.2f)
                {
                    var tmp = posLastWaypointIndex + 1;
                    if (tmp != posPath.Length)
                        posLastWaypointIndex = tmp;
                }
                return posPath[posLastWaypointIndex];
            }
            return posEndPos;
        }

        private bool ToonBetweenPoints()
        {
            if (posLastWaypointIndex == 0) return true;
            var PreWaypoint = posPath[posLastWaypointIndex - 1];
            var Waypoint = posPath[posLastWaypointIndex];
            var PlayerPos = ObjectManager.Player.Position;

            var DisAC = Calc.Distance2D(PreWaypoint, Waypoint);
            var DisAB = Calc.Distance2D(PreWaypoint, PlayerPos);
            var DisBC = Calc.Distance2D(PlayerPos, Waypoint);
            var f = DisAB + DisBC;
            return f > DisAC - 1.5
                   && f < DisAC + 1.5;
        }
    }
}