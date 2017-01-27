using ZzukBot.FSM;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateIdle : State
    {
        internal override int Priority => int.MinValue;

        internal override bool NeedToRun => true;

        internal override string Name => "Idle";

        internal override void Run()
        {
        }
    }
}