using ZzukBot.Engines.CustomClass;

namespace something
{
    public class EmuMage : CustomClass
    {
        public override byte DesignedForClass
        {
            get
            {
                return PlayerClass.Rogue;
            }
        }

        public override string CustomClassName
        {
            get
            {
                return "TestRogue";
            }
        }

        public override void PreFight()
        {
            Player.Attack();
        }


        public override void Fight()
        {
            Player.Attack();
        }

        //public override void Rest()
        //{

        //}

        public override bool Buff()
        {

            return true;
        }

    }
}