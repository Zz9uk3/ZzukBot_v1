using System;
using System.Collections.Generic;

namespace ZzukBot.FSM
{
    internal abstract class State : IComparable<State>, IComparer<State>
    {
        internal abstract int Priority { get; }

        internal abstract bool NeedToRun { get; }

        internal abstract string Name { get; }


        public int CompareTo(State other)
        {
            // We want the highest first.
            // int, by default, chooses the lowest to be sorted
            // at the bottom of the list. We want the opposite.
            return -Priority.CompareTo(other.Priority);
        }

        public int Compare(State x, State y)
        {
            return -x.Priority.CompareTo(y.Priority);
        }

        internal abstract void Run();
    }
}