using System;
using System.Collections.Generic;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Ingame;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.Info.Path
{
    internal class _PathSafeGhostwalk
    {
        private readonly Random _random = new Random();
        private double angleRandomiser = 0.2;
        private bool ArrivedAtLastPoint;
        private int currentSafeIndex;
        private bool FoundSafePath;

        private List<Safespot> listSpots = new List<Safespot>();
        private XYZ[] ResurrectSafePath;

        internal Tuple<XYZ, bool> NextSafeWaypoint
        {
            get
            {
                var diff = 1.2f;
                if (currentSafeIndex == ResurrectSafePath.Length)
                    diff = 1.8f;

                if (!ArrivedAtLastPoint &&
                    Grinder.Access.Info.Waypoints.NeedToLoadNextWaypoint(ResurrectSafePath[currentSafeIndex], diff))
                {
                    if (currentSafeIndex < ResurrectSafePath.Length - 1)
                        currentSafeIndex++;
                    else
                    {
                        ArrivedAtLastPoint = true;
                    }
                }

                return Tuple.Create(ResurrectSafePath[currentSafeIndex], ArrivedAtLastPoint);
            }
        }

        internal void Reset()
        {
            ArrivedAtLastPoint = false;
            ResurrectSafePath = null;
        }

        internal bool FindSafePath()
        {
            if (ResurrectSafePath != null)
            {
                return FoundSafePath;
            }

            var pos = ObjectManager.Player.CorpsePosition;
            var mobs = ObjectManager.Npcs
                .Where(i => i.Reaction == Enums.UnitReaction.Hostile &&
                            Calc.Distance2D(pos, i.Position) < 40).ToList();

            var attempts = 0;
            angleRandomiser = 0.2;
            listSpots.Clear();
            while (true)
            {
                //XYZ[] superRandom = Navigation.GetRandomPath(pos, GameConstants.MaxResurrectDistance);
                //XYZ safeRezzSpot = superRandom[superRandom.Length - 1];
                //XYZ randomPoint = Navigation.GetRandomPoint(pos, GameConstants.MaxResurrectDistance);

                var angle = angleRandomiser*Math.PI*2;
                var radius = Math.Sqrt(_random.NextDouble())*(_random.Next(70, 100)/(float) 100*40);
                var x = pos.X + radius*Math.Cos(angle);
                var y = pos.Y + radius*Math.Sin(angle);
                var randomPoint = new XYZ((float) x, (float) y, pos.Z);
                angleRandomiser += 0.2;
                var hostileCount = mobs
                    .Where(i => Calc.Distance2D(i.Position, randomPoint) < GameConstants.RezzDistanceToHostile)
                    .ToArray()
                    .Length;

                if (hostileCount == 0)
                {
                    var superRandom = Navigation.CalculatePath(pos, randomPoint, true);
                    if (Calc.Distance2D(superRandom[superRandom.Length - 1], randomPoint) >= 2 ||
                        Math.Abs(superRandom[superRandom.Length - 1].Z - ObjectManager.Player.CorpsePosition.Z) > 15 ||
                        Calc.Distance3D(superRandom[superRandom.Length - 1], ObjectManager.Player.CorpsePosition) >=
                        GameConstants.MaxResurrectDistance
                        )
                    {
                        attempts++;
                        continue;
                    }
                    ResurrectSafePath = superRandom;
                    FoundSafePath = true;
                    currentSafeIndex = 1;
                    return true;
                }
                listSpots.Add(new Safespot(hostileCount, randomPoint));
                attempts++;

                if (attempts == 500)
                {
                    //FoundSafePath = false;
                    //ResurrectSafePath = new XYZ[0];
                    break;
                }
            }

            listSpots = listSpots.OrderBy(i => i.HostileUnits).ToList();
            foreach (var x in listSpots)
            {
                var superRandom = Navigation.CalculatePath(pos, x.Position, true);
                if (Calc.Distance2D(superRandom[superRandom.Length - 1], x.Position) >= 2 ||
                    Math.Abs(superRandom[superRandom.Length - 1].Z - ObjectManager.Player.CorpsePosition.Z) > 15 ||
                    Calc.Distance3D(superRandom[superRandom.Length - 1], ObjectManager.Player.CorpsePosition) >=
                    GameConstants.MaxResurrectDistance
                    )
                {
                    continue;
                }
                ResurrectSafePath = superRandom;
                FoundSafePath = true;
                currentSafeIndex = 1;
                return true;
            }

            FoundSafePath = false;
            ResurrectSafePath = new XYZ[0];
            return false;
        }

        private class Safespot
        {
            internal readonly int HostileUnits;
            internal readonly XYZ Position;

            internal Safespot(int parAggroUnits, XYZ parSpot)
            {
                Position = parSpot;
                HostileUnits = parAggroUnits;
            }
        }
    }
}