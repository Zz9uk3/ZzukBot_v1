using System.Collections.Generic;

namespace ZzukBot.FSM
{
    internal class _Engine
    {
        internal _Engine(List<State> parStates)
        {
            States = parStates;
            // Remember: We implemented the IComparer, and IComparable
            // interfaces on the State class!
        }

        internal List<State> States { get; }
        internal bool Running { get; }

        internal virtual string Pulse()
        {
            // This starts at the highest priority state,
            // and iterates its way to the lowest priority.
            foreach (var state in States)
            {
                if (state.NeedToRun)
                {
                    state.Run();
                    return state.Name;
                    // Break out of the iteration,
                    // as we found a state that has run.
                    // We don't want to run any more states
                    // this time around.
                }
            }
            return "";
        }
    }
}