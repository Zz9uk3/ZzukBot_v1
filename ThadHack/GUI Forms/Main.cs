using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.Constants;
using ZzukBot.Engines;
using ZzukBot.Engines.CustomClass;
using ZzukBot.Engines.Grind;
using ZzukBot.Engines.Grind.Info;
using ZzukBot.Engines.ProfileCreation;
using ZzukBot.Helpers;
using ZzukBot.Hooks;
using ZzukBot.Ingame;
using ZzukBot.Mem;
using ZzukBot.Properties;
using ZzukBot.Server;
using ZzukBot.Settings;

namespace ZzukBot.GUI_Forms
{
    //[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    internal partial class Main : Form
    {
        internal static Main MainForm;

        private SslHandler AuthHandler;

        #region Constructor

        /// <summary>
        ///     Main constructor
        /// </summary>
        internal Main()
        {
            InitializeComponent();
            Text += " - " + Assembly.GetExecutingAssembly().GetName().Version;
            PrepareForLaunch();
            var login = new LoginForm();
            if (login.ShowDialog() == DialogResult.OK)
            {
                AuthHandler.StartAuth(login.Email, login.Password);
            }
            else
            {
                Environment.Exit(0);
            }
            login.Dispose();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private void PrepareForLaunch()
        {
            MainForm = this;
            Enabled = false;
            OptionManager.LoadXmlSettings();
            LoginBlock.Enable();
            AuthHandler = new SslHandler();
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal void EndLaunchPrepare()
        {
            CCManager.LoadCCs();
            Memory.Init();
            OptionManager.LoadSettings();
            try
            {
                using (var wc = new WebClient())
                {
                    MainForm.rtbNews.Text = wc.DownloadString("http://zzukbot.com/Hk3VEXpjfj8K/Important.txt");
                }
            }
            catch
            {
                MainForm.rtbNews.Text = "Error while fetching news";
            }
            IrcMonitor.Instance.MessageReceived += ChannelMessageRecieved;
            SetupIrc();
            Enums.DynamicFlags.AdjustToRealm();
            MainForm.Enabled = true;
            ChatHook.OnNewChatMessage += updateChat;
            LoginBlock.Disable();
        }

        private void SetupIrc()
        {
            if (Options.UseIRC)
            {
                IrcMonitor.Instance.Start(Options.IRCBotChannel, Options.IRCBotNickname);
            }
        }

        private void PlayBeep()
        {
            WinImports.PlaySound(Resources.beep, IntPtr.Zero,
                WinImports.SoundFlags.SND_ASYNC | WinImports.SoundFlags.SND_MEMORY);
        }


        internal void updateNotification(string parMessage)
        {
            Invoke(new MethodInvoker(delegate
            {
                var count = dgNotifications.Rows.Count;
                dgNotifications.Rows.Add(DateTime.Now.ToString("HH:mm"), parMessage);
                dgNotifications.Rows[count].Cells[1].ToolTipText = parMessage;
                IrcMonitor.Instance.SendMessage(parMessage);
                PlayBeep();
            }));
        }

        internal void updateChat(ChatMessage e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (e.Type == (int) Enums.ChatType.Channel)
                {
                    if (!e.Message.ToLower().Contains(ObjectManager.Player.Name.ToLower()))
                        return;
                }

                if (
                    e.Type == (int) Enums.ChatType.Say && Options.BeepOnSay ||
                    e.Type == (int) Enums.ChatType.Whisper && Options.BeepOnWhisper ||
                    e.Type == (int) Enums.ChatType.Channel && Options.BeepOnName
                    )
                {
                    PlayBeep();
                }

                var count = dgChat.Rows.Count;
                dgChat.Rows.Add(((Enums.ChatType) e.Type).ToString(), DateTime.Now.ToString("HH:mm"), e.Owner, e.Message);
                dgChat.Rows[count].Cells[3].ToolTipText = e.Message;
                dgChat.CurrentCell = dgChat[0, dgChat.Rows.Count - 1];

                IrcMonitor.Instance.SendMessage("[" + e.Owner + "] " + (Enums.ChatType)e.Type + ": " + e.Message);
                //Monitor.SendChannelMessage("[" + e.Owner + "] " + (Enums.ChatType) e.Type + ": " + e.Message);
            }));
        }

        //#region Modify, Save and load settings
        /// <summary>
        ///     Delegate used to update controls on the form
        /// </summary>
        internal void UpdateControl<T>(object Value, T control) where T : Control
        {
            Invoke(new MethodInvoker(delegate
            {
                if (control.GetType() == typeof (TextBox))
                {
                    if (Value.GetType() == typeof (string[]))
                    {
                        (control as TextBox).Text = "";
                        foreach (var x in (string[]) Value)
                        {
                            (control as TextBox).Text
                                += x + Environment.NewLine;
                        }
                    }
                    else if (Value is string)
                    {
                        (control as TextBox).Text = (string) Value;
                    }
                }
                else if (control.GetType() == typeof (NumericUpDown))
                {
                    var val = Convert.ToDecimal(Value);
                    if (val < (control as NumericUpDown).Minimum)
                        // ReSharper disable once RedundantAssignment
                        val = (control as NumericUpDown).Minimum;
                    (control as NumericUpDown).Value = Convert.ToDecimal(Value);
                }
                else if (control.GetType() == typeof (ComboBox))
                {
                    var i = 0;
                    for (; i < (control as ComboBox).Items.Count; i++)
                    {
                        if (
                            (string) (control as ComboBox).Items[i] ==
                            ((Enums.ItemQuality) Value).ToString())
                        {
                            break;
                        }
                    }
                    (control as ComboBox).SelectedIndex = i;
                }
                else if (control.GetType() == typeof (Label))
                {
                    (control as Label).Text = (string) Value;
                }
                else if (control.GetType() == typeof (CheckBox))
                {
                    (control as CheckBox).Checked = (bool) Value;
                }
            }));
        }

        /// <summary>
        ///     Save the settings
        /// </summary>
        private void bSaveSettings_Click(object sender, EventArgs e)
        {
            Options.BeepOnName = cbBeepName.Checked;
            Options.BeepOnSay = cbBeepSay.Checked;
            Options.BeepOnWhisper = cbBeepWhisper.Checked;
            Options.NotifyOnRare = cbNotifyRare.Checked;
            Options.StopOnRare = cbStopOnRare.Checked;

            Options.PetFood = tbPetFood.Text;
            Options.AccountName = tbAccount.Text;
            Options.AccountPassword = tbPassword.Text;
            Options.RestManaAt = (int) nudDrinkAt.Value;
            Options.Drink = tbDrink.Text;
            Options.RestHealthAt = (int) nudEatAt.Value;
            Options.Food = tbFood.Text;
            Options.MobSearchRange = (float) nudMobSearchRange.Value;
            Options.MaxDiffToWp = (float) nudRoamFromWp.Value;
            Options.CombatDistance = (float) nudCombatRange.Value;
            Options.MinFreeSlotsBeforeVendor = (int) nudFreeSlots.Value;
            Options.WaypointModifier = nudWaypointModifier.Value;
            Options.KeepItemsFromQuality =
                (int) Enum.Parse(typeof (Enums.ItemQuality),
                    (string) cbKeepQuality.SelectedItem);

            Options.ForceBreakAfter = (int) nudForceBreakAfter.Value;
            Options.BreakFor = (int) nudBreakFor.Value;

            Options.ProtectedItems = tbProtectedItems.Text.Split(
                new[] {Environment.NewLine},
                StringSplitOptions.None);

            Options.IRCBotChannel = tbIRCBotChannel.Text;
            Options.IRCBotNickname = tbIRCBotNickname.Text;
            Options.UseIRC = cbIRCConnect.Checked;

            Options.SkinUnits = cbSkinUnits.Checked;
            Options.NinjaSkin = cbNinjaSkin.Checked;
            Options.Herb = cbHerb.Checked;
            Options.Mine = cbMine.Checked;
            Options.LootUnits = cbLootUnits.Checked;

            OptionManager.SaveSettings();

            SetupIrc();
        }

        /// <summary>
        ///     Reload all settings
        /// </summary>
        private void bReload_Click(object sender, EventArgs e)
        {
            OptionManager.LoadSettings();
            if (EngineManager.CurrentEngineType == Engines.Engines.None)
                CCManager.LoadCCs();
        }

        #region Grindbot gui part

        /// <summary>
        ///     Load a profile
        /// </summary>
        private void bGrindLoadProfile_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.None) return;
            EngineManager.StartGrinder(cbLoadLastProfile.Checked);
        }

        #endregion

        //private void bSendPvP_Click(object sender, EventArgs e)
        //{
        //    DataStore ds = new DataStore();
        //    ds.PutInt32(0x253);
        //    ds.PutBool(false);
        //    ds.Send();
        //}

        //private void bPacketLogger_Click(object sender, EventArgs e)
        //{
        //    NetclientSendHook.Init();
        //}

        private void bAddVendorHotspot_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddVendorWaypoint();
        }

        private static void ChannelMessageRecieved(object S, IrcMonitor.MessageArgs e)
        {
            var msg = e.Message.Split(' ');
            if (!msg[0].StartsWith("!")) return;

            var command = msg[0];
            var text = "";
            switch (command)
            {
                case "!help":
                    IrcMonitor.Instance.SendMessage("Commands: !help, !say, !whisper, !guild, !group, !quit, !lua");
                    IrcMonitor.Instance.SendMessage("Usage of chat commands: !command recipent message or !command message");
                    IrcMonitor.Instance.SendMessage("Usage of lua command: !lua scriptcode");
                    break;

                case "!say":
                    for (var i = 1; i < msg.Length; i++)
                        text += msg[i] + " ";
                    Lua.RunInMainthread("SendChatMessage('" + text + "' ,'say' , nil, nil);");
                    break;

                case "!guild":
                    for (var i = 1; i < msg.Length; i++)
                        text += msg[i] + " ";
                    Lua.RunInMainthread("SendChatMessage('" + text + "' ,'guild' , nil, nil);");
                    break;

                case "!party":
                    for (var i = 1; i < msg.Length; i++)
                        text += msg[i] + " ";
                    Lua.RunInMainthread("SendChatMessage('" + text + "' ,'party' , nil, nil);");
                    break;

                case "!whisper":
                    var reciever = msg[1];
                    for (var i = 2; i < msg.Length; i++)
                        text += msg[i] + " ";
                    Lua.RunInMainthread("SendChatMessage('" + text + "' ,'whisper' , nil, '" + reciever + "');");
                    break;

                case "!lua":
                    for (var i = 1; i < msg.Length; i++)
                        text += msg[i] + " ";
                    Lua.RunInMainthread(text);
                    break;

                case "!quit":
                    Environment.Exit(-1);
                    break;
            }
        }

        private void bClearVendorHotspots_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearVendorWaypoints();
        }

        private void bClearChatLog_Click(object sender, EventArgs e)
        {
            dgChat.Rows.Clear();
        }


        private void bSettingsHelp_Click(object sender, EventArgs e)
        {
            var str = "Account and Password -> Used for Relog\n";
            str += "Search Mob Range -> Radius around the toon which will searched for the next target\n";
            str += "Combat Range -> Distance at which the bot stops moving towards the target\n";
            str += "Roam from Waypoint -> Distance how far the bot will move away from the current path\n";
            str += "Free Slots -> Start vendoring if free slot count fall under this value\n";
            str +=
                "Waypoint Modifier -> Usually the next waypoint will be loaded if the toon is 1.3 yards or closer to the current one. Very bad computers could cause stuttering. You can modify the value at which a next waypoint will be loaded by a value from -0.5 (0.8) to 0.5 (1.8) to counter this";

            MessageBox.Show(str, "Help");
        }

        private void bCreateHelp_Click(object sender, EventArgs e)
        {
            var str =
                "Hotspots -> Add hotspots around the area you like to grind. The bot will automatically build paths around those\n";
            str += "Vendor Hotspots -> Visit the forums for a guide on how to use those\n";
            str += "Factions -> Target a mob which the bot should kill and add its faction\n";

            str +=
                "Every position related profile item will be saved with its ingame coordinates inside the profile to ease up the task of editing existing profiles\n";
            str += "If no factions are added the bot will attack any mob which isnt friendly";

            MessageBox.Show(str, "Help");
        }

        //private void bShowHacks_Click(object sender, EventArgs e)
        //{
        //    Console.Write(AntiWarden.HookWardenMemScan.HacksDebugOutput());
        //}

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Monitor.Quit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void bClearGhostHotspots_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearGhostWaypoints();
        }


        private void tcWaypoints_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            var f = e.Font;
            Brush br = new SolidBrush(Color.Transparent);
            if (e.Index == tcWaypoints.SelectedIndex)
            {
                f = new Font(e.Font, FontStyle.Underline);
            }

            e.Graphics.FillRectangle(br, e.Bounds);
            var sz = e.Graphics.MeasureString(tcWaypoints.TabPages[e.Index].Text, e.Font);

            e.Graphics.DrawString(tcWaypoints.TabPages[e.Index].Text, f, Brushes.Black,
                e.Bounds.Left + (e.Bounds.Width - sz.Width)/2, e.Bounds.Top + (e.Bounds.Height - sz.Height)/2 + 1);

            var rect = e.Bounds;
            rect.Offset(0, 1);
            rect.Inflate(0, -1);
            e.Graphics.DrawRectangle(Pens.Transparent, rect);
            e.DrawFocusRectangle();

            br.Dispose();
        }

        internal void RunThis(ref int fr)
        {
            ObjectManager.EnumObjects();
            ObjectManager.Player.Spells.StopCasting();
            DirectX.StopRunning();
        }

        private void rtbNews_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void cbIRCConnect_Click(object sender, EventArgs e)
        {
            if (!cbIRCConnect.Checked)
            {
                var botName = tbIRCBotNickname.Text;
                var botChannel = tbIRCBotChannel.Text;
                if (string.IsNullOrWhiteSpace(botName))
                {
                    MessageBox.Show("Bot Nickname cant be empty");
                    return;
                }
                if (!botChannel.StartsWith("#") || string.IsNullOrWhiteSpace(botChannel))
                {
                    MessageBox.Show("Channel must start with a #");
                    return;
                }
                cbIRCConnect.Checked = true;
            }
            else
                cbIRCConnect.Checked = false;
        }

        #region Profilecreation buttons & fields

        /// <summary>
        ///     Add hotspot button
        /// </summary>
        private void bAddWaypoint_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddWaypoint();
        }

        /// <summary>
        ///     Clear the list of hotspots settings
        /// </summary>
        private void bClearHotspots_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearHotspots();
        }

        /// <summary>
        ///     Add faction button
        /// </summary>
        private void bAddFaction_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddFaction();
        }

        /// <summary>
        ///     Clear faction buttons
        /// </summary>
        private void bClearFactions_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearFactions();
        }

        /// <summary>
        ///     Add repair npc
        /// </summary>
        private void bAddRepair_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddRepair();
        }

        /// <summary>
        ///     Clear repair NPC
        /// </summary>
        private void bClearRepair_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearRepair();
        }

        /// <summary>
        ///     Add restock NPC
        /// </summary>
        private void bAddRestock_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddRestock();
        }

        /// <summary>
        ///     Clear the restock NPC info
        /// </summary>
        private void bClearRestock_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearRestock();
        }

        /// <summary>
        ///     Add a vendor NPC
        /// </summary>
        private void bAddVendor_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddVendor();
        }

        private void bAddGhostHotspot_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddGWP();
        }

        /// <summary>
        ///     Clear the vendor NPC
        /// </summary>
        private void bClearVendor_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearVendor();
        }

        /// <summary>
        ///     Add a item to restock
        /// </summary>
        private void bAddRestockItem_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().AddRestockItem();
        }

        /// <summary>
        ///     Clear the list of items we should restock
        /// </summary>
        private void bClearRestockItems_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.EngineAs<ProfileCreator>().ClearRestockItems();
        }

        /// <summary>
        ///     Save the profile or cancel
        /// </summary>
        private void bSave_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.ProfileCreation) return;
            EngineManager.StopCurrentEngine();
        }

        /// <summary>
        ///     Start creating a new profile
        /// </summary>
        private void bCreate_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.None) return;
            EngineManager.StartProfileCreation();
        }

        #endregion

        #region Buttons

        private void bGrindStop_Click(object sender, EventArgs e)
        {
            if (EngineManager.CurrentEngineType != Engines.Engines.Grind) return;
            EngineManager.StopCurrentEngine();
            lGrindLoadProfile.Text = "Profile: ";
            lGrindState.Text = "State: ";
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            if (Relog.LoginState == "login")
            {
                DirectX.RunAndSwapback(LoginEndScene);
            }
        }

        private void LoginEndScene(ref int FrameCounter, bool IsIngame)
        {
            if (IsIngame) return;
            Relog.Login();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ObjectManager.EnumObjects()) return;
            _Combat combat = new _Combat();
            var attackers = combat.Attackers;
            Console.WriteLine(attackers.Count);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DirectX.RunAndSwapbackIngame((ref int FrameCount, bool IsIngame) =>
            {
                var i = ObjectManager.Target;
                var canLoot = ((i.CanBeLooted ||
                                (Options.SkinUnits && i.IsSkinable && (Options.NinjaSkin || i.TappedByMe))
                               )
                               && !i.IsSwimming
                               && Calc.Distance3D(i.Position, ObjectManager.Player.Position) < 32);
            });
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show(Relog.GetGlueDialogText());
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            DirectX.RunAndSwapbackIngame((ref int framecount , bool ingame) =>
            {
                var b = ObjectManager.Player.Inventory.IsMainhandEnchanted;
                Console.WriteLine(b);
            });
            
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            Console.WriteLine(Relog.CurrentWindowName);
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            Functions.DoString("GlueDialogText:Hide()");
            //var curWindow = Relog.CurrentWindowName;
            //if (curWindow == "") return;
            //Functions.DoString($"text13 = {curWindow}:GetNumRegions();");
            //var regions = Functions.GetText("text13");
            //Console.WriteLine(regions);
        }

        private void cbIgnoreZ_CheckedChanged(object sender, EventArgs e)
        {
        }

        #endregion

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //Clipboard.SetText(ObjectManager.Player.Pointer.ToString("X"));
        //    DirectX.RunAndSwapback((ref int count, bool ingame) =>
        //    {
        //        ObjectManager.EnumObjects();
        //        ObjectManager.Player.Inventory.UseItem("Tough Jerky");
        //    });

        //}
    }
}