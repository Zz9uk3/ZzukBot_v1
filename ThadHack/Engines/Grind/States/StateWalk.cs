using System;
using ZzukBot.Constants;
using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateWalk : State
    {
        internal Random ran = new Random();

        internal override int Priority => 10;

        internal override bool NeedToRun => (((ObjectManager.Player.MovementState &
                                               (int) Enums.MovementFlags.Front)
                                              != (int) Enums.MovementFlags.Front)
                                             || !Grinder.Access.Info.Waypoints.NeedToLoadNextWaypoint())
                                            && ObjectManager.Player.Casting == 0
                                            && ObjectManager.Player.Channeling == 0;

        internal override string Name => "Walking";

        internal override void Run()
        {
            // start movement to the current waypoint
            if (ObjectManager.Player.Casting != 0)
                return;

            Shared.RandomJump();
            Grinder.Access.Info.PathAfterFightToWaypoint.AdjustPath();

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (Grinder.Access.Info.PathAfterFightToWaypoint.AfterFightMovement)
            {
                ObjectManager.Player.CtmTo(
                    Grinder.Access.Info.PathToPosition.ToPos(Grinder.Access.Info.Waypoints.CurrentWaypoint));
            }
            else
            {
                ObjectManager.Player.CtmTo(Grinder.Access.Info.Waypoints.CurrentWaypoint);
            }
        }
    }
}