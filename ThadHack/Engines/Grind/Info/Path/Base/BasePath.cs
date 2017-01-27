using System.Collections.Generic;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.Info.Path.Base
{
    internal class BasePath
    {
        private int SubPathIndex;
        private readonly List<SubPath> SubPaths;

        internal BasePath(List<Waypoint> parWaypoints)
        {
            SubPaths = new List<SubPath>();
            SubPathIndex = 0;

            for (var i = 0; i < parWaypoints.Count; i++)
            {
                if (i == 0)
                {
                    var Start = new Waypoint
                    {
                        Position = ObjectManager.Player.Position,
                        Type = Enums.PositionType.Hotspot
                    };
                    var End = parWaypoints[i];
                    SubPaths.Add(new SubPath(Start, End));
                }
                else
                {
                    var Start = parWaypoints[i - 1];
                    var End = parWaypoints[i];
                    SubPaths.Add(new SubPath(Start, End));
                }
            }
        }

        private SubPath CurrentSubPath => SubPaths[SubPathIndex];

        internal bool NeedToLoadNextSubPath => CurrentSubPath.ArrivedAtEndPoint;

        private bool AtLastSubPath => SubPathIndex == SubPaths.Count - 1;

        internal bool ArrivedAtDestination
        {
            get
            {
                if (!AtLastSubPath) return false;
                return CurrentSubPath.ArrivedAtEndPoint;
            }
        }

        internal XYZ NextWaypoint
        {
            get
            {
                if (NeedToLoadNextSubPath)
                {
                    LoadNextSubPath();
                }
                return CurrentSubPath.CurrentWaypoint;
            }
        }

        internal void LoadNextSubPath()
        {
            if (!NeedToLoadNextSubPath) return;
            if (SubPathIndex <= SubPaths.Count - 2)
            {
                SubPathIndex++;
            }
        }

        internal void RegenerateSubPath()
        {
            CurrentSubPath.RegenerateWaypoints();
        }
    }
}