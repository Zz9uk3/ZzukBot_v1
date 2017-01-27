using System.Collections.Generic;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _RareSpotter
    {
        internal _RareSpotter()
        {
            Rares = new List<ulong>();
        }

        private List<ulong> Rares { get; }

        internal bool Notified(ulong parGuid)
        {
            if (!Rares.Contains(parGuid))
            {
                Rares.Add(parGuid);
                return false;
            }
            return true;
        }
    }
}