using ZzukBot.Engines.CustomClass;
using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateRest : State
    {
        internal override int Priority => 45;

        internal override bool NeedToRun => (Grinder.Access.Info.Rest.NeedToDrink || Grinder.Access.Info.Rest.NeedToEat) &&
                                            !ObjectManager.Player.IsInCampfire && !ObjectManager.Player.IsSwimming;

        internal override string Name => "Resting";

        private void ClearTarget()
        {
            var guid = ObjectManager.Player.Guid;
            var tarGuid = ObjectManager.Player.TargetGuid;
            if (tarGuid != 0
                &&
                tarGuid != guid && ObjectManager.Player.HasPet && tarGuid != ObjectManager.Player.Pet.Guid)
            {
                ObjectManager.Player.SetTarget(guid);
            }
        }

        internal override void Run()
        {
            ObjectManager.Player.CtmStopMovement();
            ClearTarget();
            CCManager.Rest();
            Shared.ResetJumper();
        }
    }
}