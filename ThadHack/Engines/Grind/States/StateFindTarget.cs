using ZzukBot.FSM;
using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateFindTarget : State
    {
        internal override int Priority => 20;

        internal override bool NeedToRun => Wait.For("SearchTarget", 2000) ||
                                            (Grinder.Access.Info.Target.SearchDirect && !Grinder.Access.Info.Rest.NeedToDrink &&
                                             !Grinder.Access.Info.Rest.NeedToEat);

        internal override string Name => "Find target";

        internal override void Run()
        {
            Grinder.Access.Info.Target.SearchDirect = false;
            // Get the next best target
            var Next = Grinder.Access.Info.Target.NextTarget;
            if (Next == null) return;
            // target it if avaible
            ObjectManager.Player.SetTarget(Next);
        }
    }
}