using ZzukBot.Engines.CustomClass;
using ZzukBot.FSM;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateBuff : State
    {
        internal override int Priority => 44;

        internal override bool NeedToRun => !CCManager.IsBuffed();

        internal override string Name => "Buffing";

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
            ClearTarget();
            ObjectManager.Player.CtmStopMovement();
        }
    }
}