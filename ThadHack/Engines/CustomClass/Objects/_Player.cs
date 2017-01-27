using System.Linq;
using System.Reflection;
using ZzukBot.Constants;
using ZzukBot.Engines.Grind;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;
using ZzukBot.Settings;

namespace ZzukBot.Engines.CustomClass.Objects
{
    /// <summary>
    ///     A class representing our character
    ///     <para>Contains functions which involve the character</para>
    /// </summary>
    /// <seealso cref="_Unit" />
    [Obfuscation(Feature = "renaming", ApplyToMembers = true)]
    public class _Player : _Unit
    {
        internal bool CanChangeTarget = true;

        internal _Player() { }

        internal override WoWUnit Ptr
        {
            get { return ObjectManager.Player; }
            set { throw new System.NotImplementedException(); }
        }

        /// <summary>
        ///     Tells if we are to close to utilise ranged physical attacks (Auto Shot etc).
        /// </summary>
        /// <value>
        ///     <c>true</c> if we are to close.
        /// </value>
        public bool ToCloseForRanged => Grinder.Access.Info.PathBackup.ToCloseForRanged;

        /// <summary>
        ///     Tell if we need to eat
        /// </summary>
        /// <value>
        ///     Return <c>true</c> if we need to eat
        /// </value>
        public bool NeedToEat => Grinder.Access.Info.Rest.NeedToEat;

        /// <summary>
        ///     Tells if we need to drink
        /// </summary>
        /// <value>
        ///     Return <c>true</c> if we need to drink
        /// </value>
        public bool NeedToDrink => Grinder.Access.Info.Rest.NeedToDrink;

        /// <summary>
        ///     Gets the combo points
        /// </summary>
        /// <value>
        ///     Combo points
        /// </value>
        public int ComboPoints => ObjectManager.Player.ComboPoints;

        /// <summary>
        ///     Tells if we can overpower
        /// </summary>
        /// <value>
        ///     <c>true</c> if Overpower is avaible
        /// </value>
        public bool CanOverpower => ObjectManager.Player.CanOverpower;


        /// <summary>
        ///     Tells if the character is in a campfire
        /// </summary>
        /// <value>
        ///     <c>true</c> if the character is colliding with a campfire.
        /// </value>
        public bool IsInCampfire => ObjectManager.Player.IsInCampfire;

        /// <summary>
        ///     Gets a value indicating whether the toon is shapeshifted
        /// </summary>
        /// <value>
        ///     <c>true</c> if shapeshifted. Otherwise <c>false</c>.
        /// </value>
        public bool IsShapeShifted => ObjectManager.Player.IsShapeShifted;

        /// <summary>
        ///     Will run behind the character (only if the target doesnt target the character)
        ///     <para>Only possible if no other combat movement is happening.</para>
        /// </summary>
        /// <param name="yards">How many yards to move away from the target</param>
        /// <returns>
        ///     Returns <c>true</c> if we are moving backwards or <c>false</c> if moving back is not possible or we already got
        ///     enough distance.
        /// </returns>
        public bool Backup(int yards)
        {
            return Grinder.Access.Info.PathBackup.MoveToNextWaypoint(yards, CCManager.CurrentCC.Target.Ptr);
        }

        /// <summary>
        ///     Force a backup
        ///     <para>Only possible if no other combat movement is happening</para>
        /// </summary>
        /// <param name="yards">Yards to move away from the target</param>
        /// <returns>
        ///     Returns <c>true</c> if we are moving backwards or <c>false</c> if we already arrived or moving back is prevented by
        ///     hostile units or an object.
        /// </returns>
        public bool ForceBackup(int yards)
        {
            return Grinder.Access.Info.PathForceBackup.MoveToNextWaypoint(yards, CCManager.CurrentCC.Target.Ptr);
        }

        /// <summary>
        ///     Stop forcing backup
        ///     <para>Must be called once you want to stop moving back</para>
        /// </summary>
        public void StopForceBackup()
        {
            Grinder.Access.Info.PathForceBackup.WeArrived();
        }

        /// <summary>
        ///     Tells if we are resting right now
        /// </summary>
        /// <returns>Return <c>true</c> if we are resting</returns>
        /// .
        public bool IsResting()
        {
            return ObjectManager.Player.IsEating || ObjectManager.Player.IsDrinking;
        }

        /// <summary>
        ///     Eats the specified food
        /// </summary>
        /// <param name="parFoodName">Name of the food</param>
        public void Eat(string parFoodName)
        {
            // basically a copy paste of needtodrink with food lel
            if (Grinder.Access.Info.Rest.NeedToEat)
            {
                if (!ObjectManager.Player.IsEating)
                {
                    if (ObjectManager.Player.Inventory.ItemCount(parFoodName) != 0)
                    {
                        if (Wait.For("EatTimeout", 100))
                            ObjectManager.Player.Inventory.UseItem(parFoodName);
                    }
                }
            }
        }


        /// <summary>
        ///     Drinks the specified drink
        /// </summary>
        /// <param name="parDrinkName">Name of the drink</param>
        public void Drink(string parDrinkName)
        {
            // do we need to drink?
            if (Grinder.Access.Info.Rest.NeedToDrink)
            {
                // arent we drinking already?
                if (!ObjectManager.Player.IsDrinking)
                {
                    // do we stil have drinks?
                    if (ObjectManager.Player.Inventory.ItemCount(parDrinkName) != 0)
                    {
                        if (Wait.For("DrinkTimeout", 100))
                            ObjectManager.Player.Inventory.UseItem(parDrinkName);
                    }
                }
            }
        }


        /// <summary>
        ///     Starts attacking the current target
        /// </summary>
        public void Attack()
        {
            ObjectManager.Player.Spells.Attack();
        }

        /// <summary>
        ///     Stops attacking the target
        /// </summary>
        public void StopAttack()
        {
            ObjectManager.Player.Spells.StopAttack();
        }

        /// <summary>
        ///     Starts ranged attack (Auto Shot)
        /// </summary>
        public void RangedAttack()
        {
            ObjectManager.Player.Spells.StartRangedAttack();
        }

        /// <summary>
        ///     Stops ranged attack (Auto Shot)
        /// </summary>
        public void StopRangedAttack()
        {
            ObjectManager.Player.Spells.StopRangedAttack();
        }


        /// <summary>
        ///     Starts using the wand
        /// </summary>
        public void StartWand()
        {
            ObjectManager.Player.Spells.StartWand();
        }


        /// <summary>
        ///     Stops using the wand
        /// </summary>
        public void StopWand()
        {
            ObjectManager.Player.Spells.StopWand();
        }


        /// <summary>
        ///     Tells if the character can use the spell
        /// </summary>
        /// <param name="parName">Name of the spell</param>
        /// <returns>
        ///     Return <c>true</c> if we can use the spell
        /// </returns>
        public bool CanUse(string parName)
        {
            return ObjectManager.Player.Spells.IsSpellReady(parName);
        }


        /// <summary>
        ///     Will check if the Spell is avaible (rank != 0) aswell if we can use it. If both checks return true the method will
        ///     call Cast and return true.
        /// </summary>
        /// <param name="parName">Name of the spell</param>
        /// <returns>
        ///     Returns <c>true</c> if the Cast method was called
        /// </returns>
        public bool TryCast(string parName)
        {
            if (GetSpellRank(parName) != 0 && CanUse(parName))
            {
                Cast(parName);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Casts the specified spell
        /// </summary>
        /// <param name="parName">Name of the spell</param>
        /// <param name="parRank">(Optional) The spells rank</param>
        /// <returns>
        ///     Temporarily always returning <c>false</c> (will be changed)
        /// </returns>
        public bool Cast(string parName, int parRank = -1)
        {
            return ObjectManager.Player.Spells.Cast(parName, parRank);
        }

        /// <summary>
        ///     Cancels the shape shift.
        /// </summary>
        public void CancelShapeShift()
        {
            ObjectManager.Player.CancelShapeshift();
        }

        /// <summary>
        ///     Casts the specified spell at the units position (fe: Blizzard)
        /// </summary>
        /// <param name="parSpell">The spell</param>
        /// <param name="parUnit">The unit</param>
        /// <returns>
        ///     Currently not working and always returning false (will be changed)
        /// </returns>
        public bool CastAoe(string parSpell, _Unit parUnit)
        {
            return ObjectManager.Player.Spells.CastAoe(parSpell, parUnit.Ptr);
        }

        /// <summary>
        ///     Will abort the current cast/channeling
        /// </summary>
        public void StopCasting()
        {
            ObjectManager.Player.Spells.StopCasting();
        }


        /// <summary>
        ///     Casts the specified spell and blacklist it for the usage by CastWait for a specified timeframe
        /// </summary>
        /// <param name="parName">Name of the spell</param>
        /// <param name="parBlacklistForMs">The time in ms from now where CastWait wont be able to cast this spell again</param>
        /// <returns>
        ///     Return <c>true</c> if the character started to cast the spell
        /// </returns>
        public bool CastWait(string parName, int parBlacklistForMs)
        {
            var tmpCastingName = IsCasting;
            if (tmpCastingName != "")
            {
                if (tmpCastingName == parName)
                {
                    if (!CCManager.SpellBlacklist.ContainsKey(tmpCastingName))
                        CCManager.SpellBlacklist.Add(tmpCastingName,
                            new CCManager.SpellBlacklistItem(0, parBlacklistForMs));
                    return false;
                }
            }
            if (CCManager.SpellBlacklist.ContainsKey(parName))
                return false;
            Cast(parName);
            return true;
        }

        /// <summary>
        ///     Executes Lua
        /// </summary>
        /// <param name="parLua">Lua code</param>
        public void DoString(string parLua)
        {
            ObjectManager.Player.DoString(parLua);
        }


        /// <summary>
        ///     Obtains a value of a global lua variable
        /// </summary>
        /// <param name="parVarName">Name of the variable</param>
        /// <returns>
        ///     Returns the value as a string
        /// </returns>
        public string GetText(string parVarName)
        {
            return ObjectManager.Player.GetText(parVarName);
        }


        /// <summary>
        ///     Obtains the spells rank
        /// </summary>
        /// <param name="parSpell">The spells name</param>
        /// <returns>
        ///     0 if spell is not known to the character or the rank.
        /// </returns>
        public int GetSpellRank(string parSpell)
        {
            return ObjectManager.Player.GetSpellRank(parSpell);
        }


        /// <summary>
        ///     Determines whether the mainhand weapon is temp. chanted (poisons etc)
        /// </summary>
        /// <returns>
        ///     Returns <c>true</c> if the mainhand is enchanted
        /// </returns>
        public bool IsMainhandEnchanted()
        {
            return ObjectManager.Player.Inventory.IsMainhandEnchanted;
        }

        /// <summary>
        ///     Determines whether the offhand weapon is temp. chanted (poisons etc)
        /// </summary>
        /// <returns>
        ///     Returns <c>true</c> if the offhand is enchanted
        /// </returns>
        public bool IsOffhandEnchanted()
        {
            return ObjectManager.Player.Inventory.IsOffhandEnchanted;
        }

        /// <summary>
        ///     Enchants the mainhand
        /// </summary>
        /// <param name="parItemName">Name of the item to use on the mainhand weapon</param>
        public void EnchantMainhandItem(string parItemName)
        {
            ObjectManager.Player.Inventory.EnchantMainhandItem(parItemName);
        }

        /// <summary>
        ///     Enchants the offhand
        /// </summary>
        /// <param name="parItemName">Name of the item to apply</param>
        public void EnchantOffhandItem(string parItemName)
        {
            ObjectManager.Player.Inventory.EnchantOffhandItem(parItemName);
        }

        /// <summary>
        ///     Determines whether a wand is equipped
        /// </summary>
        /// <returns>
        ///     Return <c>true</c> if a wand is equipped
        /// </returns>
        public bool IsWandEquipped()
        {
            var encryptedName = "thisHasWandEquipped".GenLuaVarName();
            Functions.DoString(Strings.CheckWand.Replace("thisHasWandEquipped", encryptedName));
            return Functions.GetText(encryptedName).Contains("1");
        }

        /// <summary>
        ///     Tells if using aoe will engage the character with other units that arent fighting right now
        /// </summary>
        /// <param name="parRange">The radius around the character</param>
        /// <returns>
        ///     Returns <c>true</c> if we can use AoE without engaging other unpulled units
        /// </returns>
        public bool IsAoeSafe(int parRange)
        {
            var mobs = ObjectManager.Npcs
                .Where(i => (i.Reaction == Enums.UnitReaction.Hostile || i.Reaction == Enums.UnitReaction.Neutral) &&
                            Calc.Distance3D(ObjectManager.Player.Position, i.Position) < parRange).ToList();

            return mobs.All(mob => CCManager._Attackers.FirstOrDefault(i => i.Guid == mob.Guid) != null);
        }

        /// <summary>
        ///     Tells if the user has an active pet
        ///     <para>Query this before doing anything pet related.</para>
        /// </summary>
        /// <returns>
        ///     Returns <c>true</c> if the character got a pet summoned
        /// </returns>
        public bool GotPet()
        {
            return ObjectManager.Player.HasPet;
        }

        /// <summary>
        ///     Tells if a totem is spawned
        /// </summary>
        /// <param name="parName">Name of the totem</param>
        /// <returns>
        ///     Returns the distance from the player to the totem or -1 if the totem isnt summoned
        /// </returns>
        public float IsTotemSpawned(string parName)
        {
            var totem =
                ObjectManager.Npcs
                    .FirstOrDefault(i => i.Name.Contains(parName) && i.SummonedBy == ObjectManager.Player.Guid);
            if (totem != null)
            {
                return Calc.Distance2D(totem.Position, ObjectManager.Player.Position);
            }
            return -1;
        }


        /// <summary>
        ///     Tells how many items we got
        /// </summary>
        /// <param name="parName">Name of the item</param>
        /// <returns>
        ///     Returns the count of specified items
        /// </returns>
        public int ItemCount(string parName)
        {
            return ObjectManager.Player.Inventory.ItemCount(parName);
        }


        /// <summary>
        ///     Uses an item
        /// </summary>
        /// <param name="parName">Name of the item</param>
        public void UseItem(string parName)
        {
            ObjectManager.Player.Inventory.UseItem(parName);
        }


        /// <summary>
        ///     Tells if a specific item exists in the characters inventory
        ///     <para>Example: We have a weaker potion X and a stronger one named Y</para>
        ///     We pass a list with X (first item) and Y (second item)
        ///     <para>If Y isnt found it will look for X</para>
        ///     Like this we can find the best item to use in the current situation
        ///     <para>If the stronger potion isnt avaible we will use a weaker one</para>
        /// </summary>
        /// <param name="parListOfNames">The list of items</param>
        /// <returns>
        ///     Returns the name of the most important item found
        ///     <para>If no item of the list is found the string will be empty</para>
        /// </returns>
        public string GetLastItem(string[] parListOfNames)
        {
            return ObjectManager.Player.Inventory.GetLastItem(parListOfNames);
        }


        /// <summary>
        ///     Eat food specified in settings if we arent already eating
        /// </summary>
        public void Eat()
        {
            // basically a copy paste of needtodrink with food lel
            if (Grinder.Access.Info.Rest.NeedToEat)
            {
                if (!ObjectManager.Player.IsEating)
                {
                    if (ObjectManager.Player.Inventory.ItemCount(Options.Food) != 0)
                    {
                        if (Wait.For("EatTimeout", 100))
                            ObjectManager.Player.Inventory.UseItem(Options.Food);
                    }
                }
            }
        }


        /// <summary>
        ///     Drinks drink specified in settings if we arent already drinking
        /// </summary>
        public void Drink()
        {
            // do we need to drink?
            if (Grinder.Access.Info.Rest.NeedToDrink)
            {
                // arent we drinking already?
                if (!ObjectManager.Player.IsDrinking)
                {
                    // do we stil have drinks?
                    if (ObjectManager.Player.Inventory.ItemCount(Options.Drink) != 0)
                    {
                        if (Wait.For("DrinkTimeout", 100))
                            ObjectManager.Player.Inventory.UseItem(Options.Drink);
                    }
                }
            }
        }


        /// <summary>
        ///     Sets the characters target to the specified unit
        ///     <para>SetTargetTo will only work if we arent moving (Backup, ForceBackup, FixFacing)</para>
        ///     Only working once each Fight() pulse
        /// </summary>
        /// <param name="parUnit">The unit.</param>
        /// <param name="parStopCastingOnChange">(Optional) if true the current cast will be aborted on succcessful target change</param>
        /// <returns>
        ///     Returns <c>true</c> if the target got changed successfully
        /// </returns>
        /// <seealso cref="CustomClass.Fight" />
        public bool SetTargetTo(_Unit parUnit, bool parStopCastingOnChange = false)
        {
            try
            {
                if (parUnit.Ptr == null) return false;

                if (!Grinder.Access.Info.Combat.IsMoving &&
                    CanChangeTarget && (CCManager._Target.IsNull || CCManager._Target.Guid != parUnit.Guid))
                {
                    ObjectManager.Player.SetTarget(parUnit.Ptr);
                    ObjectManager.Player.Face(parUnit.Ptr);
                    CCManager.UpdateTarget(parUnit.Ptr);
                    CanChangeTarget = false;
                    if (parStopCastingOnChange)
                        StopCasting();
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        ///     Checks if there are any non-blacklisted lootable corpses nearby which would trigger the loot state.
        /// </summary>
        /// <returns>
        ///     Retrusn <c>true</c> if there are lootable corpses. Otherwise <c>false</c>
        /// </returns>
        public bool NeedToLoot()
        {
            return Grinder.Access.Info.Loot.NeedToLoot;
        }
    }
}