using System;
using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateWaitAfterFight : State
    {
        internal override int Priority => 43;

        internal override bool NeedToRun => Environment.TickCount - Grinder.Access.Info.Combat.LastFightTick <=
                                            Grinder.Access.Info.Latency*2 + 450
                                            && !Grinder.Access.Info.Target.ShouldAttackTarget;

        internal override string Name => "Waiting";

        internal override void Run()
        {
            Grinder.Access.Info.PathForceBackup.WeArrived();
            ObjectManager.Player.CtmStopMovement();
            // Nothing to do here
        }
    }
}