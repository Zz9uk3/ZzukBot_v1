using System;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;
using ZzukBot.Objects;

namespace ZzukBot.Engines.Grind.Info.Path
{
    internal class _PathForceBackup
    {
        private int CurWaypointIndex;
        internal bool MovingBack;
        internal ulong oldGuid;

        internal bool PathAvaible;

        internal XYZ[] PathBehindPlayer { get; private set; }

        internal void WeArrived()
        {
            MovingBack = false;
            PathAvaible = false;
            CurWaypointIndex = 0;
        }


        internal bool GenerateBackupPath(float range)
        {
            // We got a path. lets return
            if (PathAvaible) return true;

            // no target? return
            var tar = ObjectManager.Target;
            if (tar == null) return false;
            WoWUnit player = ObjectManager.Player;
            if (player == null) return false;

            var targetPos = tar.Position;
            var playerPos = player.Position;

            if (Calc.Distance3D(targetPos, playerPos) >= range - 2)
            {
                // no need to generate we already arrived
                return false;
            }

            // We search a new path? lets already reset index and the moving back flag
            CurWaypointIndex = 0;
            MovingBack = false;
            // Get all hostile mobs in a radius of 50 around the target we didnt aggro yet
            var mobs = ObjectManager.Npcs
                .Where(i => !i.IsInCombat && i.Reaction == Enums.UnitReaction.Hostile &&
                            Calc.Distance2D(targetPos, i.Position) < 50).ToList();

            // Getting player position
            // Generating a random point X yards away from the target
            var guid = ObjectManager.Player.TargetGuid;
            var getBackupPos = Navigation.GetPointBehindPlayer(playerPos, range);
            if (mobs.Count != 0)
            {
                var dummy = mobs
                    .FirstOrDefault(i => i.Guid != guid && i.TargetGuid == 0 && i.Health != 0 &&
                            Calc.Distance2D(i.Position, getBackupPos) < GameConstants.RezzDistanceToHostile);
                if (dummy != null)
                {
                    PathAvaible = false;
                    return false;
                }
            }
            var superRandom = Navigation.CalculatePath(playerPos, getBackupPos, false);
            if (Calc.Distance2D(superRandom[superRandom.Length - 1], getBackupPos) > 2
                || superRandom.Length > 3
                || Math.Abs(playerPos.Z - superRandom[superRandom.Length - 1].Z) > 15)
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
            if (oldGuid == 0)
            {
                oldGuid = parUnit.Guid;
            }
            else if (oldGuid != parUnit.Guid)
            {
                WeArrived();
                oldGuid = parUnit.Guid;
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
                    CurWaypointIndex++;
                    MovingBack = true;
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