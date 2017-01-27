using ZzukBot.FSM;
using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateStartBreak : State
    {
        internal override int Priority => 49;

        internal override bool NeedToRun => Grinder.Access.Info.BreakHelper.NeedToBreak;

        internal override string Name => "Starting break";

        internal override void Run()
        {
            if (Wait.For("ForceBreakLogoutTimer", 5000))
            {
                Functions.DoString("Logout()");
            }
        }
    }
}