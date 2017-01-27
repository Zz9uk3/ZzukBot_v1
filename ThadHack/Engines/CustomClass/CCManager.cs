using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.Engines.Grind;
using ZzukBot.GUI_Forms;
using ZzukBot.Helpers;
using ZzukBot.Mem;
using ZzukBot.Objects;
using ZzukBot.Settings;
using obj = ZzukBot.Engines.CustomClass.Objects;

namespace ZzukBot.Engines.CustomClass
{
    internal static class CCManager
    {
        internal static List<CustomClass> ccs;
        internal static int toUse;
        internal static Form SelectorForm;
        internal static string selectedCC;

        internal static WoWUnit AfterFightTarget;
        internal static obj._Player _Player = new obj._Player();
        internal static List<obj._Unit> _Attackers = new List<obj._Unit>();
        internal static obj._Target _Target = new obj._Target();
        internal static obj._Pet _Pet = new obj._Pet();

        internal static Dictionary<string, SpellBlacklistItem> SpellBlacklist =
            new Dictionary<string, SpellBlacklistItem>();

        internal static CustomClass CurrentCC => ccs[toUse];

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void Initialisate()
        {
            ccs = new List<CustomClass>();
            toUse = -1;
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static void LoadCCs()
        {
            Initialisate();
            GetCustomClasses();
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void GetCustomClasses()
        {
            ccs.Clear();
            if (!Directory.Exists(Paths.CCFolder))
            {
                Directory.CreateDirectory(Paths.CCFolder);
                return;
            }
            if (Directory.Exists(Paths.CCFolder + "\\Compiled"))
                Directory.Delete(Paths.CCFolder + "\\Compiled", true);

            var files = Directory.GetFiles(Paths.CCFolder);

            try
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        using (var sr = new StreamReader(file))
                        {
                            var compiledSource = CodeCompiler.CompileSource(sr.ReadToEnd());
                            if (compiledSource == null) return;
                            foreach (var t in compiledSource.CompiledAssembly.GetTypes())
                            {
                                if (t.BaseType != null &&
                                    t.BaseType.FullName == "ZzukBot.Engines.CustomClass.CustomClass")
                                {
                                    var cc = compiledSource.TryLoadCompiledType(t.FullName) as CustomClass;
                                    if (cc != null)
                                    {
                                        ccs.Add(cc);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex as ReflectionTypeLoadException;
                if (exception != null)
                {
                    MessageBox.Show(exception.Message);
                    var typeLoadException = exception;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    foreach (var x in loaderExceptions)
                    {
                        MessageBox.Show(x.Message
                                        + "\n\n"
                                        + x.InnerException);
                    }
                }
            }
        }

        /// <summary>
        ///     Chooses the CustomClass for the specific class you entered
        /// </summary>
        /// <param name="wowClass"></param>
        /// <returns>If there is more than one CC for a Class then it returns false</returns>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal static bool ChooseCustomClassByWowClass(byte wowClass)
        {
            var temp_ccs = new List<CustomClass>();
            for (var i = 0; i < ccs.Count; i++)
            {
                if (ccs[i].DesignedForClass == wowClass)
                {
                    temp_ccs.Add(ccs[i]);
                    toUse = i;
                }
            }
            if (temp_ccs.Count != 0)
            {
                //We have more than 1 CC, Let the user decide which one to use
                if (temp_ccs.Count >= 2)
                {
                    selectedCC = "";

                    SelectorForm = new CC_Selector(temp_ccs);
                    SelectorForm.ShowDialog();
                    SelectorForm.Close();
                    SelectorForm.Dispose();

                    if (selectedCC.Equals(""))
                        return false;

                    for (var i = 0; i < ccs.Count; i++)
                    {
                        if (ccs[i].CustomClassName == selectedCC)
                        {
                            toUse = i;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        internal static void FightPulse(ref WoWUnit parTarget)
        {
            try
            {
                if (DoActions())
                {
                    if (parTarget == null) return;
                    AfterFightTarget = null;
                    if (PrepareFightPulse(parTarget))
                    {
                        CurrentCC.Fight();
                        if (AfterFightTarget == null) return;
                        parTarget = AfterFightTarget;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private static bool PrepareFightPulse(WoWUnit parTarget)
        {
            try
            {
                _Attackers.Clear();
                var units = Grinder.Access.Info.Combat.Attackers;
                foreach (WoWUnit t in units)
                {
                    var tmp = new obj._Unit {Ptr = t};
                    _Attackers.Add(tmp);
                }
                CurrentCC.Target.Update(parTarget);
                CurrentCC.Player.CanChangeTarget = true;
                return true;
            }
            catch
            {
                return false;
            }
        }


        internal static void UpdateTarget(WoWUnit parTarget)
        {
            if (parTarget == null) return;
            _Target.Update(parTarget);
            AfterFightTarget = parTarget;
        }

        private static bool DoActions()
        {
            var res = Wait.For("FightTimeout", 100);
            if (res)
            {
                if (SpellBlacklist.Count != 0)
                {
                    foreach (var entry in SpellBlacklist)
                    {
                        if (ObjectManager.Player.CastingAsName != entry.Key)
                        {
                            if (entry.Value.til == 0)
                                entry.Value.til = Environment.TickCount + entry.Value.blacklistFor;
                            else
                            {
                                if (entry.Value.til <= Environment.TickCount)
                                    entry.Value.til = -1;
                            }
                        }
                    }
                    var tmpList = SpellBlacklist.Where(kvp => kvp.Value.til == -1).ToList();
                    foreach (var item in tmpList)
                    {
                        SpellBlacklist.Remove(item.Key);
                    }
                }
                //if (SpellBlacklist.ContainsKey(parName))
                //{
                //    if (SpellBlacklist[parName] == 0)
                //    {
                //        SpellBlacklist[parName] = Environment.TickCount + parBlacklistForMs;
                //    }
                //    else if (Environment.TickCount >= SpellBlacklist[parName])
                //        SpellBlacklist.Remove(parName);
                //    return false;
                //}
            }

            return res;
        }

        internal static void PreFightPulse(ref WoWUnit parTarget)
        {
            try
            {
                if (DoActions())
                {
                    if (parTarget == null) return;
                    _Target.Update(parTarget);
                    _Attackers.Clear();
                    CurrentCC.PreFight();
                }
            }
            catch
            {
            }
        }


        internal static bool IsBuffed()
        {
            try
            {
                return ccs[toUse].Buff();
            }
            catch
            {
            }
            return false;
        }

        internal static void Rest()
        {
            try
            {
                if (Wait.For("Restup133278", 500))
                    CurrentCC.Rest();
            }
            catch
            {
            }
        }


        internal class SpellBlacklistItem
        {
            internal int blacklistFor;
            internal int til;

            internal SpellBlacklistItem(int parTil, int parBlacklistFor)
            {
                til = parTil;
                blacklistFor = parBlacklistFor;
            }
        }
    }
}