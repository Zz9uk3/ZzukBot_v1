using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.AntiWarden;
using ZzukBot.Engines.CustomClass;
using ZzukBot.Engines.Grind.States;
using ZzukBot.FSM;
using ZzukBot.GUI_Forms;
using ZzukBot.Helpers;
using ZzukBot.Hooks;
using ZzukBot.Mem;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind
{
    internal class Grinder
    {
        internal static Grinder Access;

        internal Grinder()
        {
            Access = this;
            ErrorEnumHook.OnNewError += ErrorEnum_OnNewError;
        }

        internal _StuckHelper StuckHelper { get; set; }
        // holds details about the currently loaded profile
        internal GrindProfile Profile { get; private set; }
        // the fsm for our grindbot
        internal _Engine Engine { get; private set; }
        // ingame informations
        internal _Info Info { get; private set; }
        // the last state that got run
        internal string LastState { get; private set; }

        private void ErrorEnum_OnNewError(ErrorEnumArgs e)
        {
            if (e.Message.StartsWith("Target not"))
            {
                if (Access.Info.Combat.IsMovingBack) return;
                if (!Access.Info.Target.InSightWithTarget) return;
                // LosTimer is used within info.target.combatdistance
                Access.Info.Target.InSightWithTarget = false;
                Access.Info.Target.ResetToNormalAt = Environment.TickCount + 3000;
            }
            else if (e.Message.StartsWith("You must be standing") ||
                     e.Message.StartsWith("You need to be standing up to loot"))
            {
                Functions.DoString("SitOrStand()");
            }
            else if (e.Message.StartsWith("You cannot attack that target") || e.Message.StartsWith("Invalid target"))
            {
                var target = ObjectManager.Target;
                if (target == null) return;
                if (target.Health != 0)
                {
                    Access.Info.Combat.AddToBlacklist(target.Guid);
                }
            }
            else if (e.Message.StartsWith("You are facing the") || e.Message.StartsWith("Target needs to be"))
            {
                var tar = ObjectManager.Target;
                if (tar == null) return;
                if (Access.Info.Combat.IsMoving) return;
                Access.Info.Target.FixFacing = true;
                Wait.Remove("FixFacingTimer");
            }
            else if (e.Message.StartsWith("Target too close"))
            {
                if (!Access.Info.Combat.IsMoving)
                    Access.Info.PathBackup.SetToCloseForRanged();
            }
            else if (e.Message.StartsWith("You can't carry any")
                     || e.Message.StartsWith("Requires Skinning"))
            {
                Access.Info.Loot.BlacklistCurrentLoot = true;
            }
        }

        /// <summary>
        ///     Code of the grindbot to run in Endscene
        /// </summary>
        private void RareCheck()
        {
            if (Options.StopOnRare || Options.NotifyOnRare)
            {
                if (Wait.For("RareScan12", 10000))
                {
                    if (Options.NotifyOnRare)
                    {
                        var tmp = ObjectManager.Npcs.FirstOrDefault(i => i.IsRareElite && i.Health != 0);
                        if (tmp != null)
                        {
                            if (Calc.Distance3D(ObjectManager.Player.Position, tmp.Position) < 25)
                            {
                                if (!Info.RareSpotter.Notified(tmp.Guid))
                                {
                                    Main.MainForm.updateNotification("Found a rare: " + tmp.Name);
                                }
                            }
                        }
                    }
                    if (Options.StopOnRare)
                    {
                        Stop();
                    }
                }
            }
        }

        private void Refreshments()
        {
            Info.Latency = ObjectManager.Player.GetLatency()*2;
        }



        private void RelogRoutine()
        {
            if (Relog.CurrentWindowName == "RealmList")
            {
                if (Wait.For("CancelRealmSelection", 2000))
                {
                    Wait.Remove("PressLogin");
                    Relog.ResetLogin();
                }
            }
            switch (Relog.LoginState)
            {
                case "login":
                    {
                        var glueText = Relog.GetGlueDialogText().ToLower();
                        if (!glueText.Contains("is full"))
                        {
                            if (Wait.For("WrongInfo", 5000, false) && (glueText.Contains("the information you have") ||
                                                                       glueText.Contains("disconnected")))
                            {
                                if (!Wait.For("RelogReset", 2000, false))
                                {
                                    if (!Wait.For("RelogReset2", 1, false))
                                    {
                                        Wait.Remove("PressLogin");
                                        Relog.ResetLogin();
                                    }
                                }
                            }
                        }
                        if (glueText == "" && Wait.For("SendAccountDetailsWait", 5000))
                        {
                            Relog.Login();
                            Wait.Remove("RelogReset");
                            Wait.Remove("RelogReset2");
                            Wait.Remove("StartGhostWalk");
                            Access.Info.SpiritWalk.GeneratePath = true;
                            Wait.Remove("WrongInfo");
                        }
                    }
                    break;

                case "charselect":
                    if (Wait.For("EnterWorldClicker", 2000))
                        Functions.EnterWorld();
                    break;
            }
        }

        private void RunGrinder(ref int FrameCounter, bool IsIngame)
        {
            try
            {
                if (FrameCounter%3 == 0)
                {
                    if (FrameCounter%15 == 0 && IsIngame)
                    {
                        var dottedUnitsToRemove =
                           Info.Combat.UnitsDottedByPlayer.Where(kvp => Environment.TickCount - kvp.Value >= 40000).ToList();
                        foreach (var item in dottedUnitsToRemove)
                        {
                            Info.Combat.UnitsDottedByPlayer.Remove(item.Key);
                        }
                        var target = ObjectManager.Target;
                        if (target != null)
                        {
                            var debuffCount = target.Debuffs.Count;
                            if (!target.TappedByOther && !target.TappedByMe && debuffCount > 0)
                            {
                                if (!Info.Combat.UnitsDottedByPlayer.ContainsKey(target.Guid))
                                    Info.Combat.UnitsDottedByPlayer.Add(target.Guid, Environment.TickCount);
                            }
                        }
                    }
                    ObjectManager.Player.AntiAfk();

                    if (IsIngame)
                    {
                        if (FrameCounter%300 == 0)
                        {
                            RareCheck();
                            if (FrameCounter%1800 == 0)
                            {
                                Refreshments();
                            }
                        }

                        LastState = Engine.Pulse();
                        Main.MainForm.UpdateControl("State: " + LastState, Main.MainForm.lGrindState);
                    }
                    else
                    {
                        if (Info.BreakHelper.NeedToBreak)
                            return;
                        RelogRoutine();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Append(e.Message, "Exceptions.txt");
            }
        }

        private void StopGrinder(ref int FrameCounter, bool IsIngame)
        {
            Memory.GetHack("Ctm").Remove();
            if (IsIngame)
            {
                // disable all current ingame movements if we are ingame
                ObjectManager.Player.CtmStopMovement();
            }
            HookWardenMemScan.GetHack("Collision3").Remove();
            HookWardenMemScan.GetHack("Collision").Remove();
            // we arent running anymore
            Access = null;
            ErrorEnumHook.OnNewError -= ErrorEnum_OnNewError;
            DirectX.StopRunning();
        }

        /// <summary>
        ///     Prepare everything (setup fsm, parse profile etc.)
        ///     return true if ingame and profile is valid
        /// </summary>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal bool Prepare(string parProfilePath, Action parCallback)
        {
            if (!ObjectManager.EnumObjects()) return false;
            Profile = new GrindProfile(parProfilePath);
            if (!Profile.ProfileValid) return false;

            if (!CCManager.ChooseCustomClassByWowClass((byte) ObjectManager.Player.Class))
            {
                MessageBox.Show("Couldnt find a Custom Class we can use");
                return false;
            }

            StuckHelper = new _StuckHelper();
            Info = new _Info();
            Info.Waypoints.LoadFirstWaypointsAsync(parCallback);

            var tmpStates = new List<State>
            {
                new StateIdle(),
                new StateLoadNextHotspot(),
                new StateLoadNextWaypoint(),
                new StateWalk(),
                new StateFindTarget(),
                new StateApproachTarget(),
                new StateFight(),
                new StateRest(),
                new StateBuff()
            };
            if (Options.LootUnits)
            {
                tmpStates.Add(new StateLoot());
            }
            tmpStates.Add(new StateReleaseSpirit());
            tmpStates.Add(new StateGhostWalk());
            tmpStates.Add(new StateWalkToRepair());
            tmpStates.Add(new StateWalkBackToGrind());
            tmpStates.Add(new StateAfterFightToPath());
            tmpStates.Add(new StateWaitAfterFight());
            tmpStates.Add(new StateDoRandomShit());

            if (Options.BreakFor != 0 && Options.ForceBreakAfter != 0)
            {
                Info.BreakHelper.SetBreakAt(60000);
                tmpStates.Add(new StateStartBreak());
            }

            if (Profile.RepairNPC != null)
                tmpStates.Add(new StateRepair());
            tmpStates.Sort();

            Engine = new _Engine(tmpStates);
            return true;
        }

        /// <summary>
        ///     Start running the fsm
        /// </summary>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal bool Run()
        {
            if (!ObjectManager.EnumObjects()) return false;
            // start running the grindbot in endscene
            if (DirectX.RunInEndScene(RunGrinder))
            {
                // Enable the ctm patch to not stutter while walking
                Memory.GetHack("Ctm").Apply();
                ObjectManager.Player.TurnOnSelfCast();
                if (ObjectManager.Player.InGhostForm) Access.Info.SpiritWalk.GeneratePath = true;
                // we are running now
                Shared.ResetJumper();
                Wait.RemoveAll();
                return true;
            }
            return false;
        }

        internal void Stop()
        {
            DirectX.ForceRunInEndScene(StopGrinder);
        }

        internal void SetWaypointModifier(float parModifier)
        {
            Options.WaypointModifier = (decimal) parModifier;
        }
    }
}