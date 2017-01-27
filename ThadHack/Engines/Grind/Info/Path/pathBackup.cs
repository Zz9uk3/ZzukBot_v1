using System;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;
using ZzukBot.Objects;

namespace ZzukBot.Engines.Grind.Info.Path
{
    internal class _PathBackup
    {
        private int CurWaypointIndex;
        internal bool MovingBack;

        internal bool PathAvaible;

        internal _PathBackup()
        {
            ToCloseForRanged = false;
        }

        internal XYZ[] PathBehindPlayer { get; private set; }

        internal bool ToCloseForRanged { get; private set; }

        internal void SetToCloseForRanged()
        {
            PathAvaible = false;
            ToCloseForRanged = true;
        }

        internal void WeArrived()
        {
            ToCloseForRanged = false;
            MovingBack = false;
        }


        internal bool GenerateBackupPath(float range)
        {
            if (!ToCloseForRanged)
            {
                return false;
            }
            if (PathAvaible) return true;

            CurWaypointIndex = 0;
            MovingBack = false;
            var tar = ObjectManager.Target;
            if (tar == null) return false;

            var targetPos = tar.Position;

            // Get all hostile mobs in a radius of 50 around the target we didnt aggro yet
            var mobs = ObjectManager.Npcs
                .Where(i => i.TargetGuid == 0 && i.Reaction == Enums.UnitReaction.Hostile &&
                            Calc.Distance2D(targetPos, i.Position) < 50).ToList();

            // Getting player position
            // Generating a random point X yards away from the target
            var playerPos = ObjectManager.Player.Position;
            var guid = ObjectManager.Player.TargetGuid;
            var getBackupPos = Navigation.GetPointBehindPlayer(playerPos, range);
            if (mobs.Count != 0)
            {
                var dummy = mobs
                    .FirstOrDefault(i => i.Guid != guid && !i.IsInCombat && i.Health != 0 &&
                            Calc.Distance2D(i.Position, getBackupPos) < GameConstants.RezzDistanceToHostile);
                if (dummy != null)
                {
                    PathAvaible = false;
                    return false;
                }
            }
            var superRandom = Navigation.CalculatePath(playerPos, getBackupPos, false);
            if (Calc.Distance2D(superRandom[superRandom.Length - 1], getBackupPos) > 2
                || superRandom.Length > 2
                || Math.Abs(playerPos.Z - superRandom[superRandom.Length - 1].Z) > 10)
            {
                PathAvaible = false;
                return false;
            }
            PathBehindPlayer = superRandom;
            PathAvaible = true;
            return true;
        }


        internal bool MoveToNextWaypoint(float parRange, WoWUnit parUnit)
        {
            if (parUnit == null) return false;

            if (parUnit.TargetGuid == ObjectManager.Player.Guid)
            {
                WeArrived();
                return true;
            }
            if (Calc.Distance2D(ObjectManager.Player.Position, parUnit.Position) >= parRange)
            {
                WeArrived();
                return true;
            }
            if (!GenerateBackupPath(parRange))
            {
                WeArrived();
                return false;
            }
            if (Grinder.Access.Info.Waypoints.NeedToLoadNextWaypoint(PathBehindPlayer[CurWaypointIndex], 1.4f))
            {
                if (CurWaypointIndex != PathBehindPlayer.Length - 1)
                {
                    MovingBack = true;
                    CurWaypointIndex++;
                }
                else
                {
                    WeArrived();
                }
            }
            ObjectManager.Player.CtmTo(PathBehindPlayer[CurWaypointIndex]);
            return true;
        }
    }
}