using System.Reflection;
using ZzukBot.Constants;
using ZzukBot.Mem;
using ZzukBot.Objects;
using ZzukBot.Settings;

namespace ZzukBot.Engines.CustomClass.Objects
{
    /// <summary>
    ///     Class representing the players pet
    ///     <para>Contains functions to control the pet (Cast, Follow, Revive, Call etc)</para>
    /// </summary>
    /// <seealso cref="_Unit" />
    [Obfuscation(Feature = "renaming", ApplyToMembers = true)]
    // ReSharper disable once InconsistentNaming
    public class _Pet : _Unit
    {
        internal override WoWUnit Ptr
        {
            get { return ObjectManager.Player.Pet; }
            set { throw new System.NotImplementedException(); }
        }

        internal _Pet() { }

        /// <summary>
        ///     Determine if we stil have food for the pet (specified in settings)
        /// </summary>
        /// <value>
        ///     <c>true</c> if we have food. Otherwise <c>false</c>.
        /// </value>
        public bool GotPetFood => ObjectManager.Player.Inventory.ItemCount(Options.PetFood) != 0;

        /// <summary>
        ///     Feeds the players pet
        /// </summary>
        public void Feed()
        {
            if (!GotBuff("Feed Pet Effect"))
            {
                var encryptedName = "CanFeedMyPet".GenLuaVarName();

                if (ObjectManager.Player.Inventory.ItemCount(Options.PetFood) != 0)
                {
                    Functions.DoString(Strings.CheckFeedPet.Replace("CanFeedMyPet", encryptedName));
                    if (Functions.GetText(encryptedName).Trim().Contains("0"))
                    {
                        Functions.DoString(Strings.FeedPet);
                    }
                    Functions.DoString(Strings.UsePetFood1 + Options.PetFood.Replace("'", "\\'") + Strings.UsePetFood2);
                }
            }
            Functions.DoString("ClearCursor()");
        }

        /// <summary>
        ///     Let the pet attack your target
        /// </summary>
        public void Attack()
        {
            Functions.DoString("PetAttack()");
        }

        /// <summary>
        ///     Set pet to follow
        /// </summary>
        public void FollowPlayer()
        {
            Functions.DoString("PetFollow()");
        }

        /// <summary>
        ///     Dismisses the pet
        /// </summary>
        public void Dismiss()
        {
            Functions.DoString("PetDismiss()");
        }

        /// <summary>
        ///     Check if the pet is alive
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            if (ObjectManager.Player.HasPet && ObjectManager.Player.Pet.Health > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Let the pet cast a spell
        /// </summary>
        /// <param name="parPetSpell">Spell name</param>
        public void Cast(string parPetSpell)
        {
            Functions.DoString(Strings.CastPetSpell1 + parPetSpell + Strings.CastPetSpell2);
        }

        /// <summary>
        ///     Can the characters pet use the spell?
        /// </summary>
        /// <param name="parPetSpell">Spell name</param>
        /// <returns></returns>
        public bool CanUse(string parPetSpell)
        {
            var encryptedName = "PetSpellEnabled".GenLuaVarName();
            Functions.DoString(Strings.GetPetSpellCd1.Replace("PetSpellEnabled", encryptedName) + parPetSpell + Strings.GetPetSpellCd2);
            return Functions.GetText(encryptedName).Trim().Equals("0");
        }

        /// <summary>
        ///     Determines if the pet is happy
        /// </summary>
        /// <returns></returns>
        public bool IsHappy()
        {
            var encryptedName = "MyPetHappiness".GenLuaVarName();
            Functions.DoString(Strings.GetPetHappiness.Replace("MyPetHappiness", encryptedName));
            return Functions.GetText(encryptedName).Trim().Equals("3");
        }

        /// <summary>
        ///     Calls the players pet
        /// </summary>
        public void Call()
        {
            ObjectManager.Player.Spells.Cast("Call Pet");
        }

        /// <summary>
        ///     Revives the players pet
        /// </summary>
        public void Revive()
        {
            ObjectManager.Player.Spells.Cast("Revive Pet");
        }

        /// <summary>
        ///     Is the bots current target attacking our pet
        /// </summary>
        /// <returns>
        ///     returns <c>true</c> if the pet is tanking the target. Otherwise <c>false</c>
        /// </returns>
        public bool IsTanking()
        {
            var unit = ObjectManager.Target;
            if (unit == null) return true;
            return ObjectManager.Player.Pet.TargetGuid != 0 && ObjectManager.Player.Pet.Guid == unit.TargetGuid;
        }

        /// <summary>
        ///     Determines if the pet is attacking our current target
        /// </summary>
        /// <returns>
        ///     returns <c>true</c> if the pet is attacking our current target. <c>false</c> otherwise
        /// </returns>
        public bool IsOnMyTarget()
        {
            var unit = ObjectManager.Target;
            if (unit == null) return true;
            return ObjectManager.Player.Pet.TargetGuid == unit.Guid;
        }
    }
}