using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ZzukBot.Constants;
using ZzukBot.FSM;
using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateDoRandomShit : State
    {
        private readonly Random ran = new Random();
        internal override int Priority => 19;
        internal override bool NeedToRun => Wait.For("DoRandomShit", 2000);
        internal override string Name => "Do random shit";
        private static readonly Enums.ControlBits[][] _movementFlags =
        {
            new[] {Enums.ControlBits.StrafeLeft, Enums.ControlBits.StrafeRight},
            new[] {Enums.ControlBits.Left, Enums.ControlBits.Right,},
            new[] {Enums.ControlBits.Front, Enums.ControlBits.Back,}
        };
        private Enums.ControlBits _randomFlag
        {
            get
            {
                var times = ran.Next(1, 4);
                var hashSet = new HashSet<int>();
                for (int i = 0; i < times; i++)
                {
                    var nextIndex = ran.Next(0, 3);
                    if (hashSet.Contains(nextIndex))
                        i--;
                    else
                        hashSet.Add(nextIndex);
                }
                var flagResult = Enums.ControlBits.Nothing;
                int indexTurnTaken = -1;
                foreach (var item in hashSet)
                {
                    var randomCount = ran.Next(0, 2);
                    if (item == 0 || item == 1)
                    {
                        if (indexTurnTaken == -1)
                        {
                            flagResult |= _movementFlags[item][randomCount];
                            indexTurnTaken = randomCount;
                        }
                        else
                        {
                            flagResult |= _movementFlags[item][1 - indexTurnTaken];
                        }
                    }
                    else
                    {
                        flagResult |= _movementFlags[item][randomCount];
                    }
                }
                return flagResult;
            }
        }

        internal override void Run()
        {
            if (Wait.For("DRS_TarPlayer", ran.Next(4000, 8001)))
            {
                var players =
                    ObjectManager.Players.Where(i => Calc.Distance2D(i.Position, ObjectManager.Player.Position) <= 30)
                        .ToList();
                if (players.Count == 1) return;
                var ranValue = ran.Next(0, players.Count);
                var randomPlayer = players[ranValue];
                ObjectManager.Player.SetTarget(randomPlayer.Guid);
            }
            if (Wait.For("DRS_RandomMovement", ran.Next(8000, 16001)))
            {
                var lastFlags = _randomFlag;
                ObjectManager.Player.StartMovement(lastFlags);
                var stopThread = new Thread(() =>
                {
                    Thread.Sleep(ran.Next(0, 601));
                    DirectX.RunAndSwapbackIngame((ref int count, bool ingame) =>
                    {
                        ObjectManager.Player.StopMovement(lastFlags);
                    });
                });
                stopThread.Start();
            }
        }
    }
}