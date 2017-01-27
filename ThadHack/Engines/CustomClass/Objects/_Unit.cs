using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;

namespace ZzukBot.Engines.CustomClass.Objects
{
    /// <summary>
    ///     Represents a unit
    /// </summary>
    [Obfuscation(Feature = "renaming", ApplyToMembers = true)]
    public class _Unit
    {
        internal _Unit()
        {
        }

        internal virtual WoWUnit Ptr { get; set; }

        internal bool IsNull => Ptr == null;

        /// <summary>
        ///     Tells the units guid
        /// </summary>
        /// <value>
        ///     The guid
        /// </value>
        public ulong Guid => Ptr.Guid;

        /// <summary>
        ///     The guid of the target the unit is targeting
        /// </summary>
        /// <value>
        ///     The targets target guid
        /// </value>
        public ulong TargetGuid => Ptr.TargetGuid;

        /// <summary>
        ///     The units current hp
        /// </summary>
        /// <value>
        ///     The units current hp
        /// </value>
        public int Health => Ptr.Health;

        /// <summary>
        ///     The units maximum hp
        /// </summary>
        /// <value>
        ///     The units maximum hp
        /// </value>
        public int MaxHealth => Ptr.MaxHealth;

        /// <summary>
        ///     The units current hp in percent
        /// </summary>
        /// <value>
        ///     The units current hp in percent
        /// </value>
        public int HealthPercent => Ptr.HealthPercent;

        /// <summary>
        ///     The units current mana
        /// </summary>
        /// <value>
        ///     The units current mana
        /// </value>
        public int Mana => Ptr.Mana;

        /// <summary>
        ///     The units maximum mana
        /// </summary>
        /// <value>
        ///     The units maximum mana
        /// </value>
        public int MaxMana => Ptr.MaxMana;

        /// <summary>
        ///     The units current mana in percent
        /// </summary>
        /// <value>
        ///     The units current mana in percent
        /// </value>
        public int ManaPercent => Ptr.ManaPercent;

        /// <summary>
        ///     The distance to the character (3D)
        /// </summary>
        /// <value>
        ///     The distance to the character (3D)
        /// </value>
        public float DistanceToPlayer => Calc.Distance3D(ObjectManager.Player.Position, Ptr.Position);

        /// <summary>
        ///     Tells if the unit is casting
        /// </summary>
        /// <value>
        ///     Returns the casted spell or null
        /// </value>
        public string IsCasting
        {
            get
            {
                if (Ptr.Casting == 0)
                    return "";

                return ObjectManager.Player.Spells.GetName(Ptr.Casting);
            }
        }

        /// <summary>
        ///     Tells if the unit is channeling
        /// </summary>
        /// <value>
        ///     Returns the channeled spell name of an empty string
        /// </value>
        public string IsChanneling
        {
            get
            {
                if (Ptr.Channeling == 0)
                    return "";

                return ObjectManager.Player.Spells.GetName(Ptr.Channeling);
            }
        }

        /// <summary>
        ///     Tells the units rage count
        /// </summary>
        /// <value>
        ///     Tells the units rage count
        /// </value>
        public int Rage => Ptr.Rage;

        /// <summary>
        ///     Tells the units energy count
        /// </summary>
        /// <value>
        ///     Tells the units energy count
        /// </value>
        public int Energy => Ptr.Energy;

        /// <summary>
        ///     Tells if the current unit is fleeing
        /// </summary>
        /// <value>
        ///     Returns <c>true</c> if the unit is fleeing
        /// </value>
        public bool IsFleeing => Ptr.IsFleeing;

        /// <summary>
        ///     Tells if the unit is stunned
        /// </summary>
        /// <value>
        ///     <c>true</c> if the unit is stunned
        /// </value>
        public bool IsStunned => Ptr.IsStunned;

        /// <summary>
        ///     Tells the units name
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name => Ptr.Name;

        /// <summary>
        ///     Tells the units creature type.
        ///     Values: <seealso cref="CreatureType" />
        /// </summary>
        /// <value>
        ///     The type of the creature
        /// </value>
        public int CreatureType => Ptr.CreatureType;

        /// <summary>
        ///     A readonly list containing all buffs the unit has by name
        /// </summary>
        /// <value>
        ///     Buffs
        /// </value>
        public IReadOnlyList<string> Buffs
        {
            get
            {
                var tmpAuras = Ptr.Auras;
                return tmpAuras.Select(x => ObjectManager.Player.Spells.GetName(x)).Where(res => res != "").ToList();
            }
        }

        /// <summary>
        ///     A readonly list containing all debuffs the unit has by name
        /// </summary>
        /// <value>
        ///     Debuffs
        /// </value>
        public IReadOnlyList<string> Debuffs
        {
            get
            {
                var tmpAuras = Ptr.Debuffs;
                var tmpNames = tmpAuras.Select(x => ObjectManager.Player.Spells.GetName(x)).Where(res => res != "").ToList();
                return tmpNames.AsReadOnly();
            }
        }

        internal void Update(WoWUnit parUnit)
        {
            Ptr = parUnit;
        }

        /// <summary>
        ///     Tells if the unit has a buff
        /// </summary>
        /// <param name="parName">Name of the buff</param>
        /// <returns>
        ///     Returns <c>true</c> if the unit has the buff
        /// </returns>
        public bool GotBuff(string parName)
        {
            return Ptr.GotAura(parName);
        }


        /// <summary>
        ///     Tells if the unit has a debuff
        /// </summary>
        /// <param name="parName">Name of the debuff</param>
        /// <returns>
        ///     Returns <c>true</c> if the unit has the debuff
        /// </returns>
        public bool GotDebuff(string parName)
        {
            return Ptr.GotDebuff(parName);
        }
    }
}