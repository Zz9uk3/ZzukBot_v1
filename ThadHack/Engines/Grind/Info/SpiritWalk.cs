using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _SpiritWalk
    {
        internal bool ArrivedAtCorpse;
        internal bool GeneratePath;

        internal _SpiritWalk()
        {
            GeneratePath = true;
            ArrivedAtCorpse = false;
        }

        internal float DistanceToCorpse => Calc.Distance3D(ObjectManager.Player.Position, ObjectManager.Player.CorpsePosition);
    }
}