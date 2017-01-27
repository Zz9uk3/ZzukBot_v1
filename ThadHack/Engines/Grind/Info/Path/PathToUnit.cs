using System;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;
using ZzukBot.Objects;

namespace ZzukBot.Engines.Grind.Info.Path
{
    internal class _PathToUnit
    {
        private ulong unitLastGuid;
        private IntPtr unitLastPtr;

        internal _PathToUnit()
        {
            unitLastPtr = IntPtr.Zero;
            unitLastGuid = 0;
        }

        private XYZ unitLastPos { get; set; }
        private int unitLastWaypointIndex { get; set; }
        private XYZ[] unitPath { get; set; }

        internal Tuple<bool, XYZ> ToUnit(WoWUnit parUnit)
        {
            if (Calc.Distance2D(ObjectManager.Player.Position, parUnit.Position) < 3)
                return Tuple.Create(true, parUnit.Position);


            if (parUnit.Guid != unitLastGuid ||
                parUnit.Pointer != unitLastPtr ||
                Calc.Distance2D(parUnit.Position, unitLastPos) > 1.2f)
            {
                unitPath = Navigation.CalculatePath(
                    ObjectManager.Player.Position,
                    parUnit.Position, true);
                unitLastWaypointIndex = 0;
                unitLastPos = parUnit.Position;
                unitLastGuid = parUnit.Guid;
                unitLastPtr = parUnit.Pointer;
            }
            if (unitPath.Length > 0)
            {
                if (Grinder.Access.Info.Waypoints.
                    NeedToLoadNextWaypoint(unitPath[unitLastWaypointIndex]))
                    //if (ObjectManager.Player.DistanceTo(
                    //    unitPath[unitLastWaypointIndex]) < 1.2f)
                {
                    var tmp = unitLastWaypointIndex + 1;
                    if (tmp != unitPath.Length)
                        unitLastWaypointIndex = tmp;
                }
            }
            var nextPoint = unitPath[unitLastWaypointIndex];
            return Tuple.Create(true, nextPoint);
        }
    }
}