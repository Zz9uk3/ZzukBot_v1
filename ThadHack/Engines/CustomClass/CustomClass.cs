using System.Collections.Generic;
using System.Reflection;
using ZzukBot.Engines.Grind;
using ZzukBot.Settings;
using obj = ZzukBot.Engines.CustomClass.Objects;

namespace ZzukBot.Engines.CustomClass
{
    /// <summary>
    ///     Overrideable class to create CustomClasses
    /// </summary>
    [Obfuscation(Feature = "renaming", ApplyToMembers = true)]
    public class CustomClass
    {
        #region overrides

        /// <summary>
        ///     Tells what class the CustomClass is designed for
        /// </summary>
        /// <seealso cref="PlayerClass" />
        /// <value>
        ///     The Class
        /// </value>
        public virtual byte DesignedForClass { get; set; }


        /// <summary>
        ///     A readonly list of units we are in combat with
        ///     <para>Will only be populated while calling Fight</para>
        ///     Will have no elements while calling PreFight
        /// </summary>
        /// <seealso cref="Fight" />
        /// <seealso cref="PreFight" />
        /// <value>
        ///     The list of attackers
        /// </value>
        public IReadOnlyList<obj._Unit> Attackers => CCManager._Attackers.AsReadOnly();


        /// <summary>
        ///     Access to the characters object
        /// </summary>
        /// <value>
        ///     The characters object
        /// </value>
        public obj._Player Player => CCManager._Player;


        /// <summary>
        ///     Access to the target object
        /// </summary>
        /// <value>
        ///     The targets object
        /// </value>
        public obj._Target Target => CCManager._Target;


        /// <summary>
        ///     Access to the pet object
        /// </summary>
        /// <value>
        ///     The pets object
        /// </value>
        public obj._Pet Pet => CCManager._Pet;


        /// <summary>
        ///     The name of the Custom Class
        ///     <para>Must be overriden by the user</para>
        /// </summary>
        /// <value>
        ///     The name of the Custom Class.
        /// </value>
        public virtual string CustomClassName { get; }


        /// <summary>
        ///     Fight method
        ///     <para>Will be called each 100ms while we are in fight with an unit</para>
        ///     Must be overriden by the user
        /// </summary>
        public virtual void Fight()
        {
        }


        /// <summary>
        ///     PreFight method
        ///     <para>Will be called each 100ms while the bot is running to the unit and waiting for it to engage or get pulled</para>
        ///     Must be overriden by the user
        /// </summary>
        public virtual void PreFight()
        {
        }


        /// <summary>
        ///     Resting method
        ///     <para>Will be called when the character needs to rest</para>
        ///     Will consume food and drinks by default
        ///     <para>Can be overriden by the user</para>
        /// </summary>
        public virtual void Rest()
        {
            Player.Drink();
            Player.Eat();
        }

        // Buff function override


        /// <summary>
        ///     The buff routine.
        ///     <para>Will be called while the character is alive and not approaching or fighting an unit</para>
        ///     Can be overriden by the user
        /// </summary>
        /// <returns>
        ///     Retrusn <c>true</c> if we are buffed. Otherwise <c>false</c>
        /// </returns>
        public virtual bool Buff()
        {
            return true;
        }

        #endregion

        #region settingOverrides

        /// <summary>
        ///     Sets the combat distance at which the bot should stop moving towards the target
        /// </summary>
        /// <param name="parDistance">The distance.</param>
        public void SetCombatDistance(int parDistance)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Options.CombatDistance != parDistance
                && Grinder.Access.Info.Target.InSightWithTarget)
                Options.CombatDistance = parDistance;
        }


        /// <summary>
        ///     Modifies the distance before the next waypoint/hotspot should be loaded from -0.5 to 0.5
        /// </summary>
        /// <param name="parModifier">The distance</param>
        public void SetWaypointModifier(float parModifier)
        {
            if (parModifier <= 0.5 && parModifier >= -0.5)
                Grinder.Access.SetWaypointModifier(parModifier);
        }

        #endregion
    }
}