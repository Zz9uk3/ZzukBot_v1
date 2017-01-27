using System.Reflection;

namespace ZzukBot.Engines.CustomClass
{
    /// <summary>
    ///     Types of creatures
    /// </summary>
    [Obfuscation(Feature = "renaming", ApplyToMembers = true)]
    public static class CreatureType
    {
        public const int Beast = 1;
        public const int Dragonkin = 2;
        public const int Demon = 3;
        public const int Elemental = 4;
        public const int Giant = 5;
        public const int Undead = 6;
        public const int Humanoid = 7;
        public const int Critter = 8;
        public const int Mechanical = 9;
        public const int NotSpecified = 10;
        public const int Totem = 11;
    }
}