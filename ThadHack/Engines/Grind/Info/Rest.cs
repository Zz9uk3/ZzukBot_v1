using System;
using ZzukBot.Constants;
using ZzukBot.Mem;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _Rest
    {
        private bool ForceEatRest;
        private bool ForceManaRest;

        private readonly Random ran = new Random();

        private bool ContinueEat { get; set; }
        // decides if we should eat another food
        internal bool NeedToEat
        {
            get
            {
                // health percent dropped below the resting threshold
                if (ObjectManager.Player.HealthPercent < Options.RestHealthAt + ran.Next(-1, 3))
                    // We want to continue eating even after being above
                    // the threshold again
                    ContinueEat = true;
                else if (ContinueEat || ForceEatRest)
                {
                    if (ForceEatRest)
                        ContinueEat = true;

                    // We got over 95 percent health?
                    if (ObjectManager.Player.HealthPercent
                        >= 95)
                    {
                        // if yes set continue eat to false
                        ContinueEat = false;
                        ForceEatRest = false;
                    }
                }
                // return continue eat
                return ContinueEat;
            }
        }

        private bool ContinueDrink { get; set; }
        // decides if we should drink another drink
        internal bool NeedToDrink
        {
            get
            {
                // We are warrior or rogue? never need to rest manag!
                // just return true
                var tmp = ObjectManager.Player.Class;
                if (tmp == Enums.ClassIds.Rogue ||
                    tmp == Enums.ClassIds.Warrior) return false;

                // mana percent dropped below the resting threshold
                if (ObjectManager.Player.ManaPercent < Options.RestManaAt + ran.Next(-1, 3))
                    // We want to continue drinking even after being above
                    // the threshold again
                    ContinueDrink = true;
                else if (ContinueDrink || ForceManaRest)
                {
                    if (ForceManaRest)
                        ContinueDrink = true;

                    // We got over 95 percent health?
                    if (ObjectManager.Player.ManaPercent
                        >= 95)
                    {
                        // if yes set continue eat to false
                        ContinueDrink = false;
                        ForceManaRest = false;
                    }
                }
                // return continue eat
                return ContinueDrink;
            }
        }

        internal void ForceRest()
        {
            ForceManaRest = true;
            ForceEatRest = true;
        }
    }
}