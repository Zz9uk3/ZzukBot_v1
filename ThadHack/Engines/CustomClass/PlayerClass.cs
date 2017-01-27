using System.Reflection;

namespace ZzukBot.Engines.CustomClass
{
    /// <summary>
    ///     Classes of WoW
    /// </summary>
    [Obfuscation(Feature = "renaming", ApplyToMembers = true)]
    public static class PlayerClass
    {
        public const byte Warrior = 1;
        public const byte Paladin = 2;
        public const byte Hunter = 3;
        public const byte Rogue = 4;
        public const byte Priest = 5;
        public const byte Shaman = 7;
        public const byte Mage = 8;
        public const byte Warlock = 9;
        public const byte Druid = 11;
    }
}