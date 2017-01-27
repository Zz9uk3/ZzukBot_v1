using System;
using System.Collections.Generic;
using System.Text;
using ZzukBot.AntiWarden;
using ZzukBot.Constants;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;

namespace ZzukBot.Ingame
{
    internal partial class Spells
    {
        private const int AutoShotId = 75;
        private const int WandId = 5019;
        private const int AttackId = 6603;

        private const string AutoShot = "Auto Shot";
        private const string Wand = "Shoot";
        private readonly IReadOnlyDictionary<string, uint[]> PlayerSpells;


        internal unsafe Spells()
        {
            var tmpPlayerSpells = new Dictionary<string, uint[]>();

            uint currentPlayerSpellPtr = 0x00B700F0;
            uint index = 0;
            while (index < 1024)
            {
                var currentSpellId = *(uint*) (currentPlayerSpellPtr + 4*index);
                if (currentSpellId == 0) break;
                var entryPtr = *(uint*) (*(uint*) (0x00C0D780 + 8) + currentSpellId*4);

                var entrySpellId = *(uint*) entryPtr;
                var namePtr = *(uint*) (entryPtr + 0x1E0);
                //var name = Memory.Reader.ReadString((IntPtr) namePtr, Encoding.ASCII);
                var name = namePtr.ReadString(); // Will default to ascii

                if (tmpPlayerSpells.ContainsKey(name))
                {
                    var tmpIds = new List<uint>();
                    tmpIds.AddRange(tmpPlayerSpells[name]);
                    tmpIds.Add(entrySpellId);
                    tmpPlayerSpells[name] = tmpIds.ToArray();
                }
                else
                {
                    uint[] ranks = {entrySpellId};
                    tmpPlayerSpells.Add(name, ranks);
                }
                index += 1;
            }
            PlayerSpells = tmpPlayerSpells;

            Hack tmpHack;
            if (PlayerSpells.ContainsKey(AutoShot))
            {
                if ((tmpHack = HookWardenMemScan.GetHack("AutoShotPlace")) == null)
                {
                    tmpHack = new Hack((IntPtr) 0xBC69D4, BitConverter.GetBytes(AutoShotId), "AutoShotPlace");
                    tmpHack.Apply();
                    HookWardenMemScan.AddHack(tmpHack);
                }
                else
                    tmpHack.Apply();
            }

            if (PlayerSpells.ContainsKey(Wand))
            {
                if ((tmpHack = HookWardenMemScan.GetHack("WandPlace")) == null)
                {
                    tmpHack = new Hack((IntPtr) 0x00BC69D8, BitConverter.GetBytes(WandId), "WandPlace");
                    tmpHack.Apply();
                    HookWardenMemScan.AddHack(tmpHack);
                }
                else
                    tmpHack.Apply();
            }

            if ((tmpHack = HookWardenMemScan.GetHack("AttackPlace")) == null)
            {
                tmpHack = new Hack((IntPtr) 0xBC69DC, BitConverter.GetBytes(AttackId), "AttackPlace");
                tmpHack.Apply();
                HookWardenMemScan.AddHack(tmpHack);
            }
            else
                tmpHack.Apply();
        }
    }

    internal partial class Spells
    {
        internal unsafe string GetName(int parId)
        {
            if (parId >= *(uint*) (0x00C0D780 + 0xC) ||
                parId <= 0)
                return "";
            var entryPtr = *(uint*) (*(uint*) (0x00C0D780 + 8) + parId*4);
            var namePtr = *(uint*) (entryPtr + 0x1E0);
            return namePtr.ReadString();
        }

        private int GetId(string parName, int parRank = -1)
        {
            if (!PlayerSpells.ContainsKey(parName)) return 0;
            var maxRank = PlayerSpells[parName].Length;
            if (parRank < 1 || parRank > maxRank)
                return (int) PlayerSpells[parName][maxRank - 1];
            return (int) PlayerSpells[parName][parRank - 1];
        }

        internal bool Cast(string parName, int parRank = -1)
        {
            Functions.DoString("CastSpellByName('" + parName.Replace("'", "\\'") + "')");
            return false;
            //int id = GetId(parName, parRank);
            //if (id == 0) return false;
            //Console.WriteLine(id);
            //bool res = Cast(id);
            //if (Wait.For("AntiSpellBugout", 10000))
            //    Functions.DoString("CastSpellByName('" + parName.Replace("'", "\\'") + "')");
            //return res;
        }

        internal bool IsSpellReady(string parName)
        {
            var id = GetId(parName);
            if (id == 0) return false;
            return Functions.IsSpellReady(id);
        }

        internal int GetSpellRank(string parSpell)
        {
            if (!PlayerSpells.ContainsKey(parSpell)) return 0;
            return PlayerSpells[parSpell].Length;
        }

        internal void Attack()
        {
            Functions.DoString(Strings.Attack);
            if (Wait.For("AutoAttackTimer12", 1250))
            {
                var target = ObjectManager.Target;
                if (target == null) return;
                ObjectManager.Player.DisableCtm();
                ObjectManager.Player.RightClick(target);
                ObjectManager.Player.EnableCtm();
            }
        }

        internal void StopAttack()
        {
            Functions.DoString(Strings.StopAttack);
        }

        /// <summary>
        ///     Start attacking with Wand without turning it off on second call
        /// </summary>
        internal void StartWand()
        {
            if (PlayerSpells.ContainsKey(Wand))
                Functions.DoString(Strings.WandStart);
        }

        internal void StartRangedAttack()
        {
            if (PlayerSpells.ContainsKey(AutoShot))
                Functions.DoString(Strings.RangedAttackStart);
        }

        internal void StopRangedAttack()
        {
            if (PlayerSpells.ContainsKey(AutoShot))
                Functions.DoString(Strings.RangedAttackStop);
        }

        /// <summary>
        ///     Stop wand attack without turning it back on on second call
        /// </summary>
        internal void StopWand()
        {
            if (PlayerSpells.ContainsKey(Wand))
                Functions.DoString(Strings.WandStop);
        }

        internal bool CastAoe(string parSpell, WoWUnit parUnit)
        {
            if (parUnit == null) return false;
            var res = Cast(parSpell);
            if (!res) return false;
            return Functions.HandleSpellTerrain(parUnit.Position) > 1;
        }

        /// <summary>
        ///     enchant mainhand
        /// </summary>
        internal void EnchantMainhandSpell(string spellname)
        {
            Cast(spellname);
            Functions.DoString(Strings.EnchantMainhand);
        }

        /// <summary>
        ///     enchant offhand
        /// </summary>
        internal void EnchantOffhandSpell(string spellname)
        {
            Cast(spellname);
            Functions.DoString(Strings.EnchantOffhand);
        }


        internal void StopCasting()
        {
            Functions.DoString("SpellStopCasting()");
        }
    }
}