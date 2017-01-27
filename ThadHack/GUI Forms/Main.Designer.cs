namespace ZzukBot.GUI_Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpGrind = new System.Windows.Forms.TabPage();
            this.cbLoadLastProfile = new System.Windows.Forms.CheckBox();
            this.bClearChatLog = new System.Windows.Forms.Button();
            this.dgChat = new System.Windows.Forms.DataGridView();
            this.dType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dSender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bLogin = new System.Windows.Forms.Button();
            this.lGrindState = new System.Windows.Forms.Label();
            this.bGrindStop = new System.Windows.Forms.Button();
            this.lGrindLoadProfile = new System.Windows.Forms.Label();
            this.bGrindRun = new System.Windows.Forms.Button();
            this.tpProfile = new System.Windows.Forms.TabPage();
            this.lAddPointAs = new System.Windows.Forms.Label();
            this.rbWaypoint = new System.Windows.Forms.RadioButton();
            this.bCreateHelp = new System.Windows.Forms.Button();
            this.rbHotspot = new System.Windows.Forms.RadioButton();
            this.tcWaypoints = new System.Windows.Forms.TabControl();
            this.tpHotspots = new System.Windows.Forms.TabPage();
            this.bClearHotspots = new System.Windows.Forms.Button();
            this.tbHotspots = new System.Windows.Forms.TextBox();
            this.lHotspotCount = new System.Windows.Forms.Label();
            this.bAddHotspots = new System.Windows.Forms.Button();
            this.tpVendor = new System.Windows.Forms.TabPage();
            this.bClearVendorHotspots = new System.Windows.Forms.Button();
            this.tbVendorHotspots = new System.Windows.Forms.TextBox();
            this.lVendorHotspotCount = new System.Windows.Forms.Label();
            this.bAddVendorHotspot = new System.Windows.Forms.Button();
            this.tpGhost = new System.Windows.Forms.TabPage();
            this.bClearGhostHotspots = new System.Windows.Forms.Button();
            this.tbGhostHotspots = new System.Windows.Forms.TextBox();
            this.lGhostHotspotCount = new System.Windows.Forms.Label();
            this.bAddGhostHotspot = new System.Windows.Forms.Button();
            this.bClearRestockItems = new System.Windows.Forms.Button();
            this.bAddRestockItem = new System.Windows.Forms.Button();
            this.tbRestockItems = new System.Windows.Forms.TextBox();
            this.lRecording = new System.Windows.Forms.Label();
            this.lRestockItems = new System.Windows.Forms.Label();
            this.gbVendor = new System.Windows.Forms.GroupBox();
            this.bClearVendor = new System.Windows.Forms.Button();
            this.bAddVendor = new System.Windows.Forms.Button();
            this.tbVendor = new System.Windows.Forms.TextBox();
            this.lVendor = new System.Windows.Forms.Label();
            this.bClearRestock = new System.Windows.Forms.Button();
            this.bAddRestock = new System.Windows.Forms.Button();
            this.bClearRepair = new System.Windows.Forms.Button();
            this.bAddRepair = new System.Windows.Forms.Button();
            this.tbRestock = new System.Windows.Forms.TextBox();
            this.tbRepair = new System.Windows.Forms.TextBox();
            this.lRestock = new System.Windows.Forms.Label();
            this.lRepair = new System.Windows.Forms.Label();
            this.gbFaction = new System.Windows.Forms.GroupBox();
            this.bClearFactions = new System.Windows.Forms.Button();
            this.bAddFaction = new System.Windows.Forms.Button();
            this.tbFactions = new System.Windows.Forms.TextBox();
            this.lFactionCount = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.bCreate = new System.Windows.Forms.Button();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.cbNinjaSkin = new System.Windows.Forms.CheckBox();
            this.cbLootUnits = new System.Windows.Forms.CheckBox();
            this.cbMine = new System.Windows.Forms.CheckBox();
            this.cbHerb = new System.Windows.Forms.CheckBox();
            this.cbSkinUnits = new System.Windows.Forms.CheckBox();
            this.cbNotifyRare = new System.Windows.Forms.CheckBox();
            this.cbStopOnRare = new System.Windows.Forms.CheckBox();
            this.gbChat = new System.Windows.Forms.GroupBox();
            this.cbBeepName = new System.Windows.Forms.CheckBox();
            this.cbBeepSay = new System.Windows.Forms.CheckBox();
            this.cbBeepWhisper = new System.Windows.Forms.CheckBox();
            this.bSettingsHelp = new System.Windows.Forms.Button();
            this.gbOther = new System.Windows.Forms.GroupBox();
            this.nudBreakFor = new System.Windows.Forms.NumericUpDown();
            this.lBreakFor = new System.Windows.Forms.Label();
            this.nudForceBreakAfter = new System.Windows.Forms.NumericUpDown();
            this.lForceBreak = new System.Windows.Forms.Label();
            this.nudWaypointModifier = new System.Windows.Forms.NumericUpDown();
            this.lWaypointModifier = new System.Windows.Forms.Label();
            this.bReload = new System.Windows.Forms.Button();
            this.tbProtectedItems = new System.Windows.Forms.TextBox();
            this.lProtectedItems = new System.Windows.Forms.Label();
            this.gbVendoring = new System.Windows.Forms.GroupBox();
            this.cbKeepQuality = new System.Windows.Forms.ComboBox();
            this.nudFreeSlots = new System.Windows.Forms.NumericUpDown();
            this.lKeepQuality = new System.Windows.Forms.Label();
            this.lFreeSlots = new System.Windows.Forms.Label();
            this.gbDistances = new System.Windows.Forms.GroupBox();
            this.nudRoamFromWp = new System.Windows.Forms.NumericUpDown();
            this.lRoamFromWp = new System.Windows.Forms.Label();
            this.nudCombatRange = new System.Windows.Forms.NumericUpDown();
            this.nudMobSearchRange = new System.Windows.Forms.NumericUpDown();
            this.lCombatRange = new System.Windows.Forms.Label();
            this.lMobSearchRange = new System.Windows.Forms.Label();
            this.gbRest = new System.Windows.Forms.GroupBox();
            this.tbPetFood = new System.Windows.Forms.TextBox();
            this.lPetFood = new System.Windows.Forms.Label();
            this.nudEatAt = new System.Windows.Forms.NumericUpDown();
            this.nudDrinkAt = new System.Windows.Forms.NumericUpDown();
            this.lEatAt = new System.Windows.Forms.Label();
            this.lDrinkAt = new System.Windows.Forms.Label();
            this.tbFood = new System.Windows.Forms.TextBox();
            this.lFood = new System.Windows.Forms.Label();
            this.tbDrink = new System.Windows.Forms.TextBox();
            this.lDrink = new System.Windows.Forms.Label();
            this.bSaveSettings = new System.Windows.Forms.Button();
            this.gbRelog = new System.Windows.Forms.GroupBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.lAccount = new System.Windows.Forms.Label();
            this.tpIRC = new System.Windows.Forms.TabPage();
            this.lIRCDescription = new System.Windows.Forms.Label();
            this.tbIRCBotChannel = new System.Windows.Forms.TextBox();
            this.tbIRCBotNickname = new System.Windows.Forms.TextBox();
            this.cbIRCConnect = new System.Windows.Forms.CheckBox();
            this.lIrcBotname = new System.Windows.Forms.Label();
            this.lIrcChannel = new System.Windows.Forms.Label();
            this.tpNotifications = new System.Windows.Forms.TabPage();
            this.dgNotifications = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpNews = new System.Windows.Forms.TabPage();
            this.rtbNews = new System.Windows.Forms.RichTextBox();
            this.cbIgnoreZ = new System.Windows.Forms.CheckBox();
            this.tcMain.SuspendLayout();
            this.tpGrind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChat)).BeginInit();
            this.tpProfile.SuspendLayout();
            this.tcWaypoints.SuspendLayout();
            this.tpHotspots.SuspendLayout();
            this.tpVendor.SuspendLayout();
            this.tpGhost.SuspendLayout();
            this.gbVendor.SuspendLayout();
            this.gbFaction.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.gbMisc.SuspendLayout();
            this.gbChat.SuspendLayout();
            this.gbOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBreakFor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudForceBreakAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaypointModifier)).BeginInit();
            this.gbVendoring.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreeSlots)).BeginInit();
            this.gbDistances.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoamFromWp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCombatRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobSearchRange)).BeginInit();
            this.gbRest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEatAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDrinkAt)).BeginInit();
            this.gbRelog.SuspendLayout();
            this.tpIRC.SuspendLayout();
            this.tpNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotifications)).BeginInit();
            this.tpNews.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpGrind);
            this.tcMain.Controls.Add(this.tpProfile);
            this.tcMain.Controls.Add(this.tpSettings);
            this.tcMain.Controls.Add(this.tpIRC);
            this.tcMain.Controls.Add(this.tpNotifications);
            this.tcMain.Controls.Add(this.tpNews);
            this.tcMain.Location = new System.Drawing.Point(12, 6);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(485, 493);
            this.tcMain.TabIndex = 0;
            // 
            // tpGrind
            // 
            this.tpGrind.Controls.Add(this.cbLoadLastProfile);
            this.tpGrind.Controls.Add(this.bClearChatLog);
            this.tpGrind.Controls.Add(this.dgChat);
            this.tpGrind.Controls.Add(this.bLogin);
            this.tpGrind.Controls.Add(this.lGrindState);
            this.tpGrind.Controls.Add(this.bGrindStop);
            this.tpGrind.Controls.Add(this.lGrindLoadProfile);
            this.tpGrind.Controls.Add(this.bGrindRun);
            this.tpGrind.Location = new System.Drawing.Point(4, 22);
            this.tpGrind.Name = "tpGrind";
            this.tpGrind.Padding = new System.Windows.Forms.Padding(10);
            this.tpGrind.Size = new System.Drawing.Size(477, 467);
            this.tpGrind.TabIndex = 0;
            this.tpGrind.Text = "Main";
            this.tpGrind.UseVisualStyleBackColor = true;
            // 
            // cbLoadLastProfile
            // 
            this.cbLoadLastProfile.AutoSize = true;
            this.cbLoadLastProfile.Location = new System.Drawing.Point(107, 62);
            this.cbLoadLastProfile.Name = "cbLoadLastProfile";
            this.cbLoadLastProfile.Size = new System.Drawing.Size(100, 17);
            this.cbLoadLastProfile.TabIndex = 7;
            this.cbLoadLastProfile.Text = "Load last profile";
            this.cbLoadLastProfile.UseVisualStyleBackColor = true;
            // 
            // bClearChatLog
            // 
            this.bClearChatLog.Location = new System.Drawing.Point(15, 387);
            this.bClearChatLog.Name = "bClearChatLog";
            this.bClearChatLog.Size = new System.Drawing.Size(83, 20);
            this.bClearChatLog.TabIndex = 3;
            this.bClearChatLog.Text = "Clear Log";
            this.bClearChatLog.UseVisualStyleBackColor = true;
            this.bClearChatLog.Click += new System.EventHandler(this.bClearChatLog_Click);
            // 
            // dgChat
            // 
            this.dgChat.AllowUserToAddRows = false;
            this.dgChat.AllowUserToDeleteRows = false;
            this.dgChat.AllowUserToResizeColumns = false;
            this.dgChat.AllowUserToResizeRows = false;
            this.dgChat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgChat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dType,
            this.dTime,
            this.dSender,
            this.dMessage});
            this.dgChat.Location = new System.Drawing.Point(15, 101);
            this.dgChat.MultiSelect = false;
            this.dgChat.Name = "dgChat";
            this.dgChat.ReadOnly = true;
            this.dgChat.Size = new System.Drawing.Size(439, 282);
            this.dgChat.TabIndex = 4;
            // 
            // dType
            // 
            this.dType.HeaderText = "Type";
            this.dType.Name = "dType";
            this.dType.ReadOnly = true;
            this.dType.Width = 50;
            // 
            // dTime
            // 
            this.dTime.HeaderText = "Time";
            this.dTime.Name = "dTime";
            this.dTime.ReadOnly = true;
            this.dTime.Width = 35;
            // 
            // dSender
            // 
            this.dSender.HeaderText = "Sender";
            this.dSender.Name = "dSender";
            this.dSender.ReadOnly = true;
            this.dSender.Width = 75;
            // 
            // dMessage
            // 
            this.dMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dMessage.HeaderText = "Message";
            this.dMessage.Name = "dMessage";
            this.dMessage.ReadOnly = true;
            // 
            // bLogin
            // 
            this.bLogin.Location = new System.Drawing.Point(15, 62);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(83, 20);
            this.bLogin.TabIndex = 2;
            this.bLogin.Text = "Login";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.bLogin_Click);
            // 
            // lGrindState
            // 
            this.lGrindState.AutoSize = true;
            this.lGrindState.Location = new System.Drawing.Point(104, 40);
            this.lGrindState.Name = "lGrindState";
            this.lGrindState.Size = new System.Drawing.Size(38, 13);
            this.lGrindState.TabIndex = 6;
            this.lGrindState.Text = "State: ";
            // 
            // bGrindStop
            // 
            this.bGrindStop.Location = new System.Drawing.Point(15, 36);
            this.bGrindStop.Name = "bGrindStop";
            this.bGrindStop.Size = new System.Drawing.Size(83, 20);
            this.bGrindStop.TabIndex = 1;
            this.bGrindStop.Text = "Stop";
            this.bGrindStop.UseVisualStyleBackColor = true;
            this.bGrindStop.Click += new System.EventHandler(this.bGrindStop_Click);
            // 
            // lGrindLoadProfile
            // 
            this.lGrindLoadProfile.AutoSize = true;
            this.lGrindLoadProfile.Location = new System.Drawing.Point(104, 14);
            this.lGrindLoadProfile.Name = "lGrindLoadProfile";
            this.lGrindLoadProfile.Size = new System.Drawing.Size(39, 13);
            this.lGrindLoadProfile.TabIndex = 5;
            this.lGrindLoadProfile.Text = "Profile:";
            // 
            // bGrindRun
            // 
            this.bGrindRun.Location = new System.Drawing.Point(15, 10);
            this.bGrindRun.Name = "bGrindRun";
            this.bGrindRun.Size = new System.Drawing.Size(83, 20);
            this.bGrindRun.TabIndex = 0;
            this.bGrindRun.Text = "Run";
            this.bGrindRun.UseVisualStyleBackColor = true;
            this.bGrindRun.Click += new System.EventHandler(this.bGrindLoadProfile_Click);
            // 
            // tpProfile
            // 
            this.tpProfile.BackColor = System.Drawing.Color.Transparent;
            this.tpProfile.Controls.Add(this.cbIgnoreZ);
            this.tpProfile.Controls.Add(this.lAddPointAs);
            this.tpProfile.Controls.Add(this.rbWaypoint);
            this.tpProfile.Controls.Add(this.bCreateHelp);
            this.tpProfile.Controls.Add(this.rbHotspot);
            this.tpProfile.Controls.Add(this.tcWaypoints);
            this.tpProfile.Controls.Add(this.bClearRestockItems);
            this.tpProfile.Controls.Add(this.bAddRestockItem);
            this.tpProfile.Controls.Add(this.tbRestockItems);
            this.tpProfile.Controls.Add(this.lRecording);
            this.tpProfile.Controls.Add(this.lRestockItems);
            this.tpProfile.Controls.Add(this.gbVendor);
            this.tpProfile.Controls.Add(this.gbFaction);
            this.tpProfile.Controls.Add(this.bSave);
            this.tpProfile.Controls.Add(this.bCreate);
            this.tpProfile.Location = new System.Drawing.Point(4, 22);
            this.tpProfile.Name = "tpProfile";
            this.tpProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tpProfile.Size = new System.Drawing.Size(477, 467);
            this.tpProfile.TabIndex = 1;
            this.tpProfile.Text = "Profile";
            // 
            // lAddPointAs
            // 
            this.lAddPointAs.AutoSize = true;
            this.lAddPointAs.Location = new System.Drawing.Point(21, 377);
            this.lAddPointAs.Name = "lAddPointAs";
            this.lAddPointAs.Size = new System.Drawing.Size(43, 13);
            this.lAddPointAs.TabIndex = 13;
            this.lAddPointAs.Text = "Add as:";
            // 
            // rbWaypoint
            // 
            this.rbWaypoint.AutoSize = true;
            this.rbWaypoint.Location = new System.Drawing.Point(138, 373);
            this.rbWaypoint.Name = "rbWaypoint";
            this.rbWaypoint.Size = new System.Drawing.Size(70, 17);
            this.rbWaypoint.TabIndex = 9;
            this.rbWaypoint.Text = "Waypoint";
            this.rbWaypoint.UseVisualStyleBackColor = true;
            // 
            // bCreateHelp
            // 
            this.bCreateHelp.Location = new System.Drawing.Point(305, 419);
            this.bCreateHelp.Name = "bCreateHelp";
            this.bCreateHelp.Size = new System.Drawing.Size(115, 32);
            this.bCreateHelp.TabIndex = 2;
            this.bCreateHelp.Text = "Help";
            this.bCreateHelp.UseVisualStyleBackColor = true;
            this.bCreateHelp.Click += new System.EventHandler(this.bCreateHelp_Click);
            // 
            // rbHotspot
            // 
            this.rbHotspot.AutoSize = true;
            this.rbHotspot.Checked = true;
            this.rbHotspot.Location = new System.Drawing.Point(70, 373);
            this.rbHotspot.Name = "rbHotspot";
            this.rbHotspot.Size = new System.Drawing.Size(62, 17);
            this.rbHotspot.TabIndex = 8;
            this.rbHotspot.TabStop = true;
            this.rbHotspot.Text = "Hotspot";
            this.rbHotspot.UseVisualStyleBackColor = true;
            // 
            // tcWaypoints
            // 
            this.tcWaypoints.Controls.Add(this.tpHotspots);
            this.tcWaypoints.Controls.Add(this.tpVendor);
            this.tcWaypoints.Controls.Add(this.tpGhost);
            this.tcWaypoints.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcWaypoints.Location = new System.Drawing.Point(17, 214);
            this.tcWaypoints.Name = "tcWaypoints";
            this.tcWaypoints.SelectedIndex = 0;
            this.tcWaypoints.Size = new System.Drawing.Size(281, 151);
            this.tcWaypoints.TabIndex = 11;
            this.tcWaypoints.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tcWaypoints_DrawItem_1);
            // 
            // tpHotspots
            // 
            this.tpHotspots.Controls.Add(this.bClearHotspots);
            this.tpHotspots.Controls.Add(this.tbHotspots);
            this.tpHotspots.Controls.Add(this.lHotspotCount);
            this.tpHotspots.Controls.Add(this.bAddHotspots);
            this.tpHotspots.Location = new System.Drawing.Point(4, 22);
            this.tpHotspots.Name = "tpHotspots";
            this.tpHotspots.Padding = new System.Windows.Forms.Padding(3);
            this.tpHotspots.Size = new System.Drawing.Size(273, 125);
            this.tpHotspots.TabIndex = 0;
            this.tpHotspots.Text = "Hotspots";
            this.tpHotspots.UseVisualStyleBackColor = true;
            // 
            // bClearHotspots
            // 
            this.bClearHotspots.Location = new System.Drawing.Point(110, 94);
            this.bClearHotspots.Name = "bClearHotspots";
            this.bClearHotspots.Size = new System.Drawing.Size(83, 20);
            this.bClearHotspots.TabIndex = 1;
            this.bClearHotspots.Text = "Clear";
            this.bClearHotspots.UseVisualStyleBackColor = true;
            this.bClearHotspots.Click += new System.EventHandler(this.bClearHotspots_Click);
            // 
            // tbHotspots
            // 
            this.tbHotspots.Enabled = false;
            this.tbHotspots.Location = new System.Drawing.Point(15, 16);
            this.tbHotspots.Multiline = true;
            this.tbHotspots.Name = "tbHotspots";
            this.tbHotspots.Size = new System.Drawing.Size(243, 72);
            this.tbHotspots.TabIndex = 2;
            // 
            // lHotspotCount
            // 
            this.lHotspotCount.AutoSize = true;
            this.lHotspotCount.Location = new System.Drawing.Point(199, 98);
            this.lHotspotCount.Name = "lHotspotCount";
            this.lHotspotCount.Size = new System.Drawing.Size(38, 13);
            this.lHotspotCount.TabIndex = 2;
            this.lHotspotCount.Text = "Count:";
            // 
            // bAddHotspots
            // 
            this.bAddHotspots.Location = new System.Drawing.Point(15, 94);
            this.bAddHotspots.Name = "bAddHotspots";
            this.bAddHotspots.Size = new System.Drawing.Size(83, 20);
            this.bAddHotspots.TabIndex = 0;
            this.bAddHotspots.Text = "Add";
            this.bAddHotspots.UseVisualStyleBackColor = true;
            this.bAddHotspots.Click += new System.EventHandler(this.bAddWaypoint_Click);
            // 
            // tpVendor
            // 
            this.tpVendor.Controls.Add(this.bClearVendorHotspots);
            this.tpVendor.Controls.Add(this.tbVendorHotspots);
            this.tpVendor.Controls.Add(this.lVendorHotspotCount);
            this.tpVendor.Controls.Add(this.bAddVendorHotspot);
            this.tpVendor.Location = new System.Drawing.Point(4, 22);
            this.tpVendor.Name = "tpVendor";
            this.tpVendor.Padding = new System.Windows.Forms.Padding(3);
            this.tpVendor.Size = new System.Drawing.Size(273, 125);
            this.tpVendor.TabIndex = 1;
            this.tpVendor.Text = "Vendor Hotspots";
            this.tpVendor.UseVisualStyleBackColor = true;
            // 
            // bClearVendorHotspots
            // 
            this.bClearVendorHotspots.Location = new System.Drawing.Point(110, 94);
            this.bClearVendorHotspots.Name = "bClearVendorHotspots";
            this.bClearVendorHotspots.Size = new System.Drawing.Size(83, 20);
            this.bClearVendorHotspots.TabIndex = 11;
            this.bClearVendorHotspots.Text = "Clear";
            this.bClearVendorHotspots.UseVisualStyleBackColor = true;
            this.bClearVendorHotspots.Click += new System.EventHandler(this.bClearVendorHotspots_Click);
            // 
            // tbVendorHotspots
            // 
            this.tbVendorHotspots.Enabled = false;
            this.tbVendorHotspots.Location = new System.Drawing.Point(15, 16);
            this.tbVendorHotspots.Multiline = true;
            this.tbVendorHotspots.Name = "tbVendorHotspots";
            this.tbVendorHotspots.Size = new System.Drawing.Size(243, 72);
            this.tbVendorHotspots.TabIndex = 9;
            // 
            // lVendorHotspotCount
            // 
            this.lVendorHotspotCount.AutoSize = true;
            this.lVendorHotspotCount.Location = new System.Drawing.Point(199, 98);
            this.lVendorHotspotCount.Name = "lVendorHotspotCount";
            this.lVendorHotspotCount.Size = new System.Drawing.Size(38, 13);
            this.lVendorHotspotCount.TabIndex = 8;
            this.lVendorHotspotCount.Text = "Count:";
            // 
            // bAddVendorHotspot
            // 
            this.bAddVendorHotspot.Location = new System.Drawing.Point(15, 94);
            this.bAddVendorHotspot.Name = "bAddVendorHotspot";
            this.bAddVendorHotspot.Size = new System.Drawing.Size(83, 20);
            this.bAddVendorHotspot.TabIndex = 10;
            this.bAddVendorHotspot.Text = "Add";
            this.bAddVendorHotspot.UseVisualStyleBackColor = true;
            this.bAddVendorHotspot.Click += new System.EventHandler(this.bAddVendorHotspot_Click);
            // 
            // tpGhost
            // 
            this.tpGhost.Controls.Add(this.bClearGhostHotspots);
            this.tpGhost.Controls.Add(this.tbGhostHotspots);
            this.tpGhost.Controls.Add(this.lGhostHotspotCount);
            this.tpGhost.Controls.Add(this.bAddGhostHotspot);
            this.tpGhost.Location = new System.Drawing.Point(4, 22);
            this.tpGhost.Name = "tpGhost";
            this.tpGhost.Padding = new System.Windows.Forms.Padding(3);
            this.tpGhost.Size = new System.Drawing.Size(273, 125);
            this.tpGhost.TabIndex = 2;
            this.tpGhost.Text = "Ghost Hotspots";
            this.tpGhost.UseVisualStyleBackColor = true;
            // 
            // bClearGhostHotspots
            // 
            this.bClearGhostHotspots.Location = new System.Drawing.Point(110, 94);
            this.bClearGhostHotspots.Name = "bClearGhostHotspots";
            this.bClearGhostHotspots.Size = new System.Drawing.Size(83, 20);
            this.bClearGhostHotspots.TabIndex = 4;
            this.bClearGhostHotspots.Text = "Clear";
            this.bClearGhostHotspots.UseVisualStyleBackColor = true;
            this.bClearGhostHotspots.Click += new System.EventHandler(this.bClearGhostHotspots_Click);
            // 
            // tbGhostHotspots
            // 
            this.tbGhostHotspots.Enabled = false;
            this.tbGhostHotspots.Location = new System.Drawing.Point(15, 16);
            this.tbGhostHotspots.Multiline = true;
            this.tbGhostHotspots.Name = "tbGhostHotspots";
            this.tbGhostHotspots.Size = new System.Drawing.Size(243, 72);
            this.tbGhostHotspots.TabIndex = 5;
            // 
            // lGhostHotspotCount
            // 
            this.lGhostHotspotCount.AutoSize = true;
            this.lGhostHotspotCount.Location = new System.Drawing.Point(199, 98);
            this.lGhostHotspotCount.Name = "lGhostHotspotCount";
            this.lGhostHotspotCount.Size = new System.Drawing.Size(38, 13);
            this.lGhostHotspotCount.TabIndex = 6;
            this.lGhostHotspotCount.Text = "Count:";
            // 
            // bAddGhostHotspot
            // 
            this.bAddGhostHotspot.Location = new System.Drawing.Point(15, 94);
            this.bAddGhostHotspot.Name = "bAddGhostHotspot";
            this.bAddGhostHotspot.Size = new System.Drawing.Size(83, 20);
            this.bAddGhostHotspot.TabIndex = 3;
            this.bAddGhostHotspot.Text = "Add";
            this.bAddGhostHotspot.UseVisualStyleBackColor = true;
            this.bAddGhostHotspot.Click += new System.EventHandler(this.bAddGhostHotspot_Click);
            // 
            // bClearRestockItems
            // 
            this.bClearRestockItems.Enabled = false;
            this.bClearRestockItems.Location = new System.Drawing.Point(326, 300);
            this.bClearRestockItems.Name = "bClearRestockItems";
            this.bClearRestockItems.Size = new System.Drawing.Size(119, 20);
            this.bClearRestockItems.TabIndex = 10;
            this.bClearRestockItems.Text = "Clear";
            this.bClearRestockItems.UseVisualStyleBackColor = true;
            this.bClearRestockItems.Click += new System.EventHandler(this.bClearRestockItems_Click);
            // 
            // bAddRestockItem
            // 
            this.bAddRestockItem.Enabled = false;
            this.bAddRestockItem.Location = new System.Drawing.Point(326, 274);
            this.bAddRestockItem.Name = "bAddRestockItem";
            this.bAddRestockItem.Size = new System.Drawing.Size(119, 20);
            this.bAddRestockItem.TabIndex = 9;
            this.bAddRestockItem.Text = "Add";
            this.bAddRestockItem.UseVisualStyleBackColor = true;
            this.bAddRestockItem.Click += new System.EventHandler(this.bAddRestockItem_Click);
            // 
            // tbRestockItems
            // 
            this.tbRestockItems.Enabled = false;
            this.tbRestockItems.Location = new System.Drawing.Point(326, 163);
            this.tbRestockItems.Multiline = true;
            this.tbRestockItems.Name = "tbRestockItems";
            this.tbRestockItems.Size = new System.Drawing.Size(119, 105);
            this.tbRestockItems.TabIndex = 8;
            // 
            // lRecording
            // 
            this.lRecording.AutoSize = true;
            this.lRecording.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRecording.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lRecording.Location = new System.Drawing.Point(126, 137);
            this.lRecording.Name = "lRecording";
            this.lRecording.Size = new System.Drawing.Size(125, 29);
            this.lRecording.TabIndex = 7;
            this.lRecording.Text = "Recording";
            this.lRecording.Visible = false;
            // 
            // lRestockItems
            // 
            this.lRestockItems.AutoSize = true;
            this.lRestockItems.Enabled = false;
            this.lRestockItems.Location = new System.Drawing.Point(356, 147);
            this.lRestockItems.Name = "lRestockItems";
            this.lRestockItems.Size = new System.Drawing.Size(50, 13);
            this.lRestockItems.TabIndex = 5;
            this.lRestockItems.Text = "Restock:";
            // 
            // gbVendor
            // 
            this.gbVendor.Controls.Add(this.bClearVendor);
            this.gbVendor.Controls.Add(this.bAddVendor);
            this.gbVendor.Controls.Add(this.tbVendor);
            this.gbVendor.Controls.Add(this.lVendor);
            this.gbVendor.Controls.Add(this.bClearRestock);
            this.gbVendor.Controls.Add(this.bAddRestock);
            this.gbVendor.Controls.Add(this.bClearRepair);
            this.gbVendor.Controls.Add(this.bAddRepair);
            this.gbVendor.Controls.Add(this.tbRestock);
            this.gbVendor.Controls.Add(this.tbRepair);
            this.gbVendor.Controls.Add(this.lRestock);
            this.gbVendor.Controls.Add(this.lRepair);
            this.gbVendor.Location = new System.Drawing.Point(119, 10);
            this.gbVendor.Name = "gbVendor";
            this.gbVendor.Size = new System.Drawing.Size(288, 125);
            this.gbVendor.TabIndex = 4;
            this.gbVendor.TabStop = false;
            this.gbVendor.Text = "Vendoring";
            // 
            // bClearVendor
            // 
            this.bClearVendor.Enabled = false;
            this.bClearVendor.Location = new System.Drawing.Point(195, 84);
            this.bClearVendor.Name = "bClearVendor";
            this.bClearVendor.Size = new System.Drawing.Size(83, 20);
            this.bClearVendor.TabIndex = 15;
            this.bClearVendor.Text = "Clear";
            this.bClearVendor.UseVisualStyleBackColor = true;
            this.bClearVendor.Click += new System.EventHandler(this.bClearVendor_Click);
            // 
            // bAddVendor
            // 
            this.bAddVendor.Enabled = false;
            this.bAddVendor.Location = new System.Drawing.Point(195, 58);
            this.bAddVendor.Name = "bAddVendor";
            this.bAddVendor.Size = new System.Drawing.Size(83, 20);
            this.bAddVendor.TabIndex = 14;
            this.bAddVendor.Text = "Add";
            this.bAddVendor.UseVisualStyleBackColor = true;
            this.bAddVendor.Click += new System.EventHandler(this.bAddVendor_Click);
            // 
            // tbVendor
            // 
            this.tbVendor.Enabled = false;
            this.tbVendor.Location = new System.Drawing.Point(195, 32);
            this.tbVendor.Name = "tbVendor";
            this.tbVendor.Size = new System.Drawing.Size(82, 20);
            this.tbVendor.TabIndex = 13;
            // 
            // lVendor
            // 
            this.lVendor.AutoSize = true;
            this.lVendor.Enabled = false;
            this.lVendor.Location = new System.Drawing.Point(192, 16);
            this.lVendor.Name = "lVendor";
            this.lVendor.Size = new System.Drawing.Size(41, 13);
            this.lVendor.TabIndex = 12;
            this.lVendor.Text = "Vendor";
            // 
            // bClearRestock
            // 
            this.bClearRestock.Enabled = false;
            this.bClearRestock.Location = new System.Drawing.Point(104, 85);
            this.bClearRestock.Name = "bClearRestock";
            this.bClearRestock.Size = new System.Drawing.Size(83, 20);
            this.bClearRestock.TabIndex = 11;
            this.bClearRestock.Text = "Clear";
            this.bClearRestock.UseVisualStyleBackColor = true;
            this.bClearRestock.Click += new System.EventHandler(this.bClearRestock_Click);
            // 
            // bAddRestock
            // 
            this.bAddRestock.Enabled = false;
            this.bAddRestock.Location = new System.Drawing.Point(104, 59);
            this.bAddRestock.Name = "bAddRestock";
            this.bAddRestock.Size = new System.Drawing.Size(83, 20);
            this.bAddRestock.TabIndex = 10;
            this.bAddRestock.Text = "Add";
            this.bAddRestock.UseVisualStyleBackColor = true;
            this.bAddRestock.Click += new System.EventHandler(this.bAddRestock_Click);
            // 
            // bClearRepair
            // 
            this.bClearRepair.Location = new System.Drawing.Point(12, 85);
            this.bClearRepair.Name = "bClearRepair";
            this.bClearRepair.Size = new System.Drawing.Size(83, 20);
            this.bClearRepair.TabIndex = 9;
            this.bClearRepair.Text = "Clear";
            this.bClearRepair.UseVisualStyleBackColor = true;
            this.bClearRepair.Click += new System.EventHandler(this.bClearRepair_Click);
            // 
            // bAddRepair
            // 
            this.bAddRepair.Location = new System.Drawing.Point(12, 59);
            this.bAddRepair.Name = "bAddRepair";
            this.bAddRepair.Size = new System.Drawing.Size(83, 20);
            this.bAddRepair.TabIndex = 8;
            this.bAddRepair.Text = "Add";
            this.bAddRepair.UseVisualStyleBackColor = true;
            this.bAddRepair.Click += new System.EventHandler(this.bAddRepair_Click);
            // 
            // tbRestock
            // 
            this.tbRestock.Enabled = false;
            this.tbRestock.Location = new System.Drawing.Point(104, 33);
            this.tbRestock.Name = "tbRestock";
            this.tbRestock.Size = new System.Drawing.Size(82, 20);
            this.tbRestock.TabIndex = 3;
            // 
            // tbRepair
            // 
            this.tbRepair.Enabled = false;
            this.tbRepair.Location = new System.Drawing.Point(12, 33);
            this.tbRepair.Name = "tbRepair";
            this.tbRepair.Size = new System.Drawing.Size(83, 20);
            this.tbRepair.TabIndex = 2;
            // 
            // lRestock
            // 
            this.lRestock.AutoSize = true;
            this.lRestock.Enabled = false;
            this.lRestock.Location = new System.Drawing.Point(101, 17);
            this.lRestock.Name = "lRestock";
            this.lRestock.Size = new System.Drawing.Size(47, 13);
            this.lRestock.TabIndex = 1;
            this.lRestock.Text = "Restock";
            // 
            // lRepair
            // 
            this.lRepair.AutoSize = true;
            this.lRepair.Location = new System.Drawing.Point(9, 17);
            this.lRepair.Name = "lRepair";
            this.lRepair.Size = new System.Drawing.Size(38, 13);
            this.lRepair.TabIndex = 0;
            this.lRepair.Text = "Repair";
            // 
            // gbFaction
            // 
            this.gbFaction.Controls.Add(this.bClearFactions);
            this.gbFaction.Controls.Add(this.bAddFaction);
            this.gbFaction.Controls.Add(this.tbFactions);
            this.gbFaction.Controls.Add(this.lFactionCount);
            this.gbFaction.Location = new System.Drawing.Point(15, 10);
            this.gbFaction.Name = "gbFaction";
            this.gbFaction.Size = new System.Drawing.Size(98, 176);
            this.gbFaction.TabIndex = 2;
            this.gbFaction.TabStop = false;
            this.gbFaction.Text = "Factions";
            // 
            // bClearFactions
            // 
            this.bClearFactions.Location = new System.Drawing.Point(9, 146);
            this.bClearFactions.Name = "bClearFactions";
            this.bClearFactions.Size = new System.Drawing.Size(83, 20);
            this.bClearFactions.TabIndex = 8;
            this.bClearFactions.Text = "Clear";
            this.bClearFactions.UseVisualStyleBackColor = true;
            this.bClearFactions.Click += new System.EventHandler(this.bClearFactions_Click);
            // 
            // bAddFaction
            // 
            this.bAddFaction.Location = new System.Drawing.Point(9, 120);
            this.bAddFaction.Name = "bAddFaction";
            this.bAddFaction.Size = new System.Drawing.Size(83, 20);
            this.bAddFaction.TabIndex = 7;
            this.bAddFaction.Text = "Add";
            this.bAddFaction.UseVisualStyleBackColor = true;
            this.bAddFaction.Click += new System.EventHandler(this.bAddFaction_Click);
            // 
            // tbFactions
            // 
            this.tbFactions.Enabled = false;
            this.tbFactions.Location = new System.Drawing.Point(9, 42);
            this.tbFactions.Multiline = true;
            this.tbFactions.Name = "tbFactions";
            this.tbFactions.Size = new System.Drawing.Size(80, 72);
            this.tbFactions.TabIndex = 1;
            // 
            // lFactionCount
            // 
            this.lFactionCount.AutoSize = true;
            this.lFactionCount.Location = new System.Drawing.Point(6, 26);
            this.lFactionCount.Name = "lFactionCount";
            this.lFactionCount.Size = new System.Drawing.Size(38, 13);
            this.lFactionCount.TabIndex = 0;
            this.lFactionCount.Text = "Count:";
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(168, 419);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(115, 32);
            this.bSave.TabIndex = 1;
            this.bSave.Text = "Save / Cancel";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(31, 419);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(115, 32);
            this.bCreate.TabIndex = 0;
            this.bCreate.Text = "Create";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.gbMisc);
            this.tpSettings.Controls.Add(this.gbChat);
            this.tpSettings.Controls.Add(this.bSettingsHelp);
            this.tpSettings.Controls.Add(this.gbOther);
            this.tpSettings.Controls.Add(this.bReload);
            this.tpSettings.Controls.Add(this.tbProtectedItems);
            this.tpSettings.Controls.Add(this.lProtectedItems);
            this.tpSettings.Controls.Add(this.gbVendoring);
            this.tpSettings.Controls.Add(this.gbDistances);
            this.tpSettings.Controls.Add(this.gbRest);
            this.tpSettings.Controls.Add(this.bSaveSettings);
            this.tpSettings.Controls.Add(this.gbRelog);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(477, 467);
            this.tpSettings.TabIndex = 2;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // gbMisc
            // 
            this.gbMisc.Controls.Add(this.cbNinjaSkin);
            this.gbMisc.Controls.Add(this.cbLootUnits);
            this.gbMisc.Controls.Add(this.cbMine);
            this.gbMisc.Controls.Add(this.cbHerb);
            this.gbMisc.Controls.Add(this.cbSkinUnits);
            this.gbMisc.Controls.Add(this.cbNotifyRare);
            this.gbMisc.Controls.Add(this.cbStopOnRare);
            this.gbMisc.Location = new System.Drawing.Point(355, 130);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(110, 309);
            this.gbMisc.TabIndex = 13;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Misc";
            // 
            // cbNinjaSkin
            // 
            this.cbNinjaSkin.AutoSize = true;
            this.cbNinjaSkin.Location = new System.Drawing.Point(4, 89);
            this.cbNinjaSkin.Name = "cbNinjaSkin";
            this.cbNinjaSkin.Size = new System.Drawing.Size(74, 17);
            this.cbNinjaSkin.TabIndex = 6;
            this.cbNinjaSkin.Text = "Ninja Skin";
            this.cbNinjaSkin.UseVisualStyleBackColor = true;
            // 
            // cbLootUnits
            // 
            this.cbLootUnits.AutoSize = true;
            this.cbLootUnits.Checked = true;
            this.cbLootUnits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLootUnits.Location = new System.Drawing.Point(4, 158);
            this.cbLootUnits.Name = "cbLootUnits";
            this.cbLootUnits.Size = new System.Drawing.Size(74, 17);
            this.cbLootUnits.TabIndex = 5;
            this.cbLootUnits.Text = "Loot Units";
            this.cbLootUnits.UseVisualStyleBackColor = true;
            // 
            // cbMine
            // 
            this.cbMine.AutoSize = true;
            this.cbMine.Location = new System.Drawing.Point(4, 135);
            this.cbMine.Name = "cbMine";
            this.cbMine.Size = new System.Drawing.Size(49, 17);
            this.cbMine.TabIndex = 4;
            this.cbMine.Text = "Mine";
            this.cbMine.UseVisualStyleBackColor = true;
            // 
            // cbHerb
            // 
            this.cbHerb.AutoSize = true;
            this.cbHerb.Location = new System.Drawing.Point(4, 112);
            this.cbHerb.Name = "cbHerb";
            this.cbHerb.Size = new System.Drawing.Size(49, 17);
            this.cbHerb.TabIndex = 3;
            this.cbHerb.Text = "Herb";
            this.cbHerb.UseVisualStyleBackColor = true;
            // 
            // cbSkinUnits
            // 
            this.cbSkinUnits.AutoSize = true;
            this.cbSkinUnits.Location = new System.Drawing.Point(4, 66);
            this.cbSkinUnits.Name = "cbSkinUnits";
            this.cbSkinUnits.Size = new System.Drawing.Size(74, 17);
            this.cbSkinUnits.TabIndex = 2;
            this.cbSkinUnits.Text = "Skin Units";
            this.cbSkinUnits.UseVisualStyleBackColor = true;
            // 
            // cbNotifyRare
            // 
            this.cbNotifyRare.AutoSize = true;
            this.cbNotifyRare.Location = new System.Drawing.Point(4, 43);
            this.cbNotifyRare.Name = "cbNotifyRare";
            this.cbNotifyRare.Size = new System.Drawing.Size(89, 17);
            this.cbNotifyRare.TabIndex = 1;
            this.cbNotifyRare.Text = "Notify on rare";
            this.cbNotifyRare.UseVisualStyleBackColor = true;
            // 
            // cbStopOnRare
            // 
            this.cbStopOnRare.AutoSize = true;
            this.cbStopOnRare.Location = new System.Drawing.Point(4, 20);
            this.cbStopOnRare.Name = "cbStopOnRare";
            this.cbStopOnRare.Size = new System.Drawing.Size(84, 17);
            this.cbStopOnRare.TabIndex = 0;
            this.cbStopOnRare.Text = "Stop on rare";
            this.cbStopOnRare.UseVisualStyleBackColor = true;
            // 
            // gbChat
            // 
            this.gbChat.Controls.Add(this.cbBeepName);
            this.gbChat.Controls.Add(this.cbBeepSay);
            this.gbChat.Controls.Add(this.cbBeepWhisper);
            this.gbChat.Location = new System.Drawing.Point(355, 11);
            this.gbChat.Name = "gbChat";
            this.gbChat.Size = new System.Drawing.Size(110, 111);
            this.gbChat.TabIndex = 12;
            this.gbChat.TabStop = false;
            this.gbChat.Text = "Chat";
            // 
            // cbBeepName
            // 
            this.cbBeepName.AutoSize = true;
            this.cbBeepName.Location = new System.Drawing.Point(4, 64);
            this.cbBeepName.Name = "cbBeepName";
            this.cbBeepName.Size = new System.Drawing.Size(95, 17);
            this.cbBeepName.TabIndex = 2;
            this.cbBeepName.Text = "Beep on name";
            this.cbBeepName.UseVisualStyleBackColor = true;
            // 
            // cbBeepSay
            // 
            this.cbBeepSay.AutoSize = true;
            this.cbBeepSay.Location = new System.Drawing.Point(4, 42);
            this.cbBeepSay.Name = "cbBeepSay";
            this.cbBeepSay.Size = new System.Drawing.Size(85, 17);
            this.cbBeepSay.TabIndex = 1;
            this.cbBeepSay.Text = "Beep on say";
            this.cbBeepSay.UseVisualStyleBackColor = true;
            // 
            // cbBeepWhisper
            // 
            this.cbBeepWhisper.AutoSize = true;
            this.cbBeepWhisper.Location = new System.Drawing.Point(4, 20);
            this.cbBeepWhisper.Name = "cbBeepWhisper";
            this.cbBeepWhisper.Size = new System.Drawing.Size(105, 17);
            this.cbBeepWhisper.TabIndex = 0;
            this.cbBeepWhisper.Text = "Beep on whisper";
            this.cbBeepWhisper.UseVisualStyleBackColor = true;
            // 
            // bSettingsHelp
            // 
            this.bSettingsHelp.Location = new System.Drawing.Point(202, 418);
            this.bSettingsHelp.Name = "bSettingsHelp";
            this.bSettingsHelp.Size = new System.Drawing.Size(89, 21);
            this.bSettingsHelp.TabIndex = 11;
            this.bSettingsHelp.Text = "Help";
            this.bSettingsHelp.UseVisualStyleBackColor = true;
            this.bSettingsHelp.Click += new System.EventHandler(this.bSettingsHelp_Click);
            // 
            // gbOther
            // 
            this.gbOther.Controls.Add(this.nudBreakFor);
            this.gbOther.Controls.Add(this.lBreakFor);
            this.gbOther.Controls.Add(this.nudForceBreakAfter);
            this.gbOther.Controls.Add(this.lForceBreak);
            this.gbOther.Controls.Add(this.nudWaypointModifier);
            this.gbOther.Controls.Add(this.lWaypointModifier);
            this.gbOther.Location = new System.Drawing.Point(160, 272);
            this.gbOther.Name = "gbOther";
            this.gbOther.Size = new System.Drawing.Size(187, 119);
            this.gbOther.TabIndex = 10;
            this.gbOther.TabStop = false;
            this.gbOther.Text = "Other";
            // 
            // nudBreakFor
            // 
            this.nudBreakFor.Location = new System.Drawing.Point(102, 70);
            this.nudBreakFor.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudBreakFor.Name = "nudBreakFor";
            this.nudBreakFor.Size = new System.Drawing.Size(71, 20);
            this.nudBreakFor.TabIndex = 15;
            // 
            // lBreakFor
            // 
            this.lBreakFor.AutoSize = true;
            this.lBreakFor.Location = new System.Drawing.Point(4, 72);
            this.lBreakFor.Name = "lBreakFor";
            this.lBreakFor.Size = new System.Drawing.Size(50, 13);
            this.lBreakFor.TabIndex = 14;
            this.lBreakFor.Text = "Break for";
            // 
            // nudForceBreakAfter
            // 
            this.nudForceBreakAfter.Location = new System.Drawing.Point(102, 44);
            this.nudForceBreakAfter.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudForceBreakAfter.Name = "nudForceBreakAfter";
            this.nudForceBreakAfter.Size = new System.Drawing.Size(71, 20);
            this.nudForceBreakAfter.TabIndex = 13;
            // 
            // lForceBreak
            // 
            this.lForceBreak.AutoSize = true;
            this.lForceBreak.Location = new System.Drawing.Point(4, 46);
            this.lForceBreak.Name = "lForceBreak";
            this.lForceBreak.Size = new System.Drawing.Size(89, 13);
            this.lForceBreak.TabIndex = 12;
            this.lForceBreak.Text = "Force Break after";
            // 
            // nudWaypointModifier
            // 
            this.nudWaypointModifier.DecimalPlaces = 1;
            this.nudWaypointModifier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudWaypointModifier.Location = new System.Drawing.Point(102, 18);
            this.nudWaypointModifier.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudWaypointModifier.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147418112});
            this.nudWaypointModifier.Name = "nudWaypointModifier";
            this.nudWaypointModifier.Size = new System.Drawing.Size(71, 20);
            this.nudWaypointModifier.TabIndex = 11;
            // 
            // lWaypointModifier
            // 
            this.lWaypointModifier.AutoSize = true;
            this.lWaypointModifier.Location = new System.Drawing.Point(4, 20);
            this.lWaypointModifier.Name = "lWaypointModifier";
            this.lWaypointModifier.Size = new System.Drawing.Size(92, 13);
            this.lWaypointModifier.TabIndex = 1;
            this.lWaypointModifier.Text = "Waypoint Modifier";
            // 
            // bReload
            // 
            this.bReload.Location = new System.Drawing.Point(107, 418);
            this.bReload.Name = "bReload";
            this.bReload.Size = new System.Drawing.Size(89, 21);
            this.bReload.TabIndex = 9;
            this.bReload.Text = "Reload";
            this.bReload.UseVisualStyleBackColor = true;
            this.bReload.Click += new System.EventHandler(this.bReload_Click);
            // 
            // tbProtectedItems
            // 
            this.tbProtectedItems.Location = new System.Drawing.Point(14, 279);
            this.tbProtectedItems.Multiline = true;
            this.tbProtectedItems.Name = "tbProtectedItems";
            this.tbProtectedItems.Size = new System.Drawing.Size(136, 70);
            this.tbProtectedItems.TabIndex = 8;
            // 
            // lProtectedItems
            // 
            this.lProtectedItems.AutoSize = true;
            this.lProtectedItems.Location = new System.Drawing.Point(26, 256);
            this.lProtectedItems.Name = "lProtectedItems";
            this.lProtectedItems.Size = new System.Drawing.Size(81, 13);
            this.lProtectedItems.TabIndex = 7;
            this.lProtectedItems.Text = "Protected Items";
            // 
            // gbVendoring
            // 
            this.gbVendoring.Controls.Add(this.cbKeepQuality);
            this.gbVendoring.Controls.Add(this.nudFreeSlots);
            this.gbVendoring.Controls.Add(this.lKeepQuality);
            this.gbVendoring.Controls.Add(this.lFreeSlots);
            this.gbVendoring.Location = new System.Drawing.Point(178, 174);
            this.gbVendoring.Name = "gbVendoring";
            this.gbVendoring.Size = new System.Drawing.Size(167, 88);
            this.gbVendoring.TabIndex = 6;
            this.gbVendoring.TabStop = false;
            this.gbVendoring.Text = "Vendoring";
            // 
            // cbKeepQuality
            // 
            this.cbKeepQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKeepQuality.FormattingEnabled = true;
            this.cbKeepQuality.Items.AddRange(new object[] {
            "White",
            "Green",
            "Blue",
            "Epic"});
            this.cbKeepQuality.Location = new System.Drawing.Point(65, 51);
            this.cbKeepQuality.Name = "cbKeepQuality";
            this.cbKeepQuality.Size = new System.Drawing.Size(73, 21);
            this.cbKeepQuality.TabIndex = 7;
            // 
            // nudFreeSlots
            // 
            this.nudFreeSlots.Location = new System.Drawing.Point(65, 26);
            this.nudFreeSlots.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudFreeSlots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFreeSlots.Name = "nudFreeSlots";
            this.nudFreeSlots.Size = new System.Drawing.Size(71, 20);
            this.nudFreeSlots.TabIndex = 6;
            this.nudFreeSlots.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lKeepQuality
            // 
            this.lKeepQuality.AutoSize = true;
            this.lKeepQuality.Location = new System.Drawing.Point(4, 54);
            this.lKeepQuality.Name = "lKeepQuality";
            this.lKeepQuality.Size = new System.Drawing.Size(55, 13);
            this.lKeepQuality.TabIndex = 5;
            this.lKeepQuality.Text = "Keep from";
            // 
            // lFreeSlots
            // 
            this.lFreeSlots.AutoSize = true;
            this.lFreeSlots.Location = new System.Drawing.Point(4, 28);
            this.lFreeSlots.Name = "lFreeSlots";
            this.lFreeSlots.Size = new System.Drawing.Size(54, 13);
            this.lFreeSlots.TabIndex = 4;
            this.lFreeSlots.Text = "Free Slots";
            // 
            // gbDistances
            // 
            this.gbDistances.Controls.Add(this.nudRoamFromWp);
            this.gbDistances.Controls.Add(this.lRoamFromWp);
            this.gbDistances.Controls.Add(this.nudCombatRange);
            this.gbDistances.Controls.Add(this.nudMobSearchRange);
            this.gbDistances.Controls.Add(this.lCombatRange);
            this.gbDistances.Controls.Add(this.lMobSearchRange);
            this.gbDistances.Location = new System.Drawing.Point(34, 95);
            this.gbDistances.Name = "gbDistances";
            this.gbDistances.Size = new System.Drawing.Size(136, 152);
            this.gbDistances.TabIndex = 5;
            this.gbDistances.TabStop = false;
            this.gbDistances.Text = "Distances";
            // 
            // nudRoamFromWp
            // 
            this.nudRoamFromWp.Location = new System.Drawing.Point(7, 119);
            this.nudRoamFromWp.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudRoamFromWp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRoamFromWp.Name = "nudRoamFromWp";
            this.nudRoamFromWp.Size = new System.Drawing.Size(71, 20);
            this.nudRoamFromWp.TabIndex = 10;
            this.nudRoamFromWp.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // lRoamFromWp
            // 
            this.lRoamFromWp.AutoSize = true;
            this.lRoamFromWp.Location = new System.Drawing.Point(4, 103);
            this.lRoamFromWp.Name = "lRoamFromWp";
            this.lRoamFromWp.Size = new System.Drawing.Size(109, 13);
            this.lRoamFromWp.TabIndex = 9;
            this.lRoamFromWp.Text = "Roam From Waypoint";
            // 
            // nudCombatRange
            // 
            this.nudCombatRange.Location = new System.Drawing.Point(7, 78);
            this.nudCombatRange.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudCombatRange.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudCombatRange.Name = "nudCombatRange";
            this.nudCombatRange.Size = new System.Drawing.Size(71, 20);
            this.nudCombatRange.TabIndex = 8;
            this.nudCombatRange.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // nudMobSearchRange
            // 
            this.nudMobSearchRange.Location = new System.Drawing.Point(7, 36);
            this.nudMobSearchRange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMobSearchRange.Name = "nudMobSearchRange";
            this.nudMobSearchRange.Size = new System.Drawing.Size(71, 20);
            this.nudMobSearchRange.TabIndex = 7;
            this.nudMobSearchRange.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lCombatRange
            // 
            this.lCombatRange.AutoSize = true;
            this.lCombatRange.Location = new System.Drawing.Point(4, 62);
            this.lCombatRange.Name = "lCombatRange";
            this.lCombatRange.Size = new System.Drawing.Size(78, 13);
            this.lCombatRange.TabIndex = 3;
            this.lCombatRange.Text = "Combat Range";
            // 
            // lMobSearchRange
            // 
            this.lMobSearchRange.AutoSize = true;
            this.lMobSearchRange.Location = new System.Drawing.Point(4, 20);
            this.lMobSearchRange.Name = "lMobSearchRange";
            this.lMobSearchRange.Size = new System.Drawing.Size(99, 13);
            this.lMobSearchRange.TabIndex = 1;
            this.lMobSearchRange.Text = "Search mob Range";
            // 
            // gbRest
            // 
            this.gbRest.Controls.Add(this.tbPetFood);
            this.gbRest.Controls.Add(this.lPetFood);
            this.gbRest.Controls.Add(this.nudEatAt);
            this.gbRest.Controls.Add(this.nudDrinkAt);
            this.gbRest.Controls.Add(this.lEatAt);
            this.gbRest.Controls.Add(this.lDrinkAt);
            this.gbRest.Controls.Add(this.tbFood);
            this.gbRest.Controls.Add(this.lFood);
            this.gbRest.Controls.Add(this.tbDrink);
            this.gbRest.Controls.Add(this.lDrink);
            this.gbRest.Location = new System.Drawing.Point(178, 11);
            this.gbRest.Name = "gbRest";
            this.gbRest.Size = new System.Drawing.Size(167, 157);
            this.gbRest.TabIndex = 4;
            this.gbRest.TabStop = false;
            this.gbRest.Text = "Rest";
            // 
            // tbPetFood
            // 
            this.tbPetFood.Location = new System.Drawing.Point(65, 122);
            this.tbPetFood.Name = "tbPetFood";
            this.tbPetFood.Size = new System.Drawing.Size(71, 20);
            this.tbPetFood.TabIndex = 8;
            // 
            // lPetFood
            // 
            this.lPetFood.AutoSize = true;
            this.lPetFood.Location = new System.Drawing.Point(4, 125);
            this.lPetFood.Name = "lPetFood";
            this.lPetFood.Size = new System.Drawing.Size(50, 13);
            this.lPetFood.TabIndex = 9;
            this.lPetFood.Text = "Pet Food";
            // 
            // nudEatAt
            // 
            this.nudEatAt.Location = new System.Drawing.Point(65, 43);
            this.nudEatAt.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudEatAt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEatAt.Name = "nudEatAt";
            this.nudEatAt.Size = new System.Drawing.Size(71, 20);
            this.nudEatAt.TabIndex = 7;
            this.nudEatAt.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // nudDrinkAt
            // 
            this.nudDrinkAt.Location = new System.Drawing.Point(65, 17);
            this.nudDrinkAt.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudDrinkAt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDrinkAt.Name = "nudDrinkAt";
            this.nudDrinkAt.Size = new System.Drawing.Size(71, 20);
            this.nudDrinkAt.TabIndex = 6;
            this.nudDrinkAt.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // lEatAt
            // 
            this.lEatAt.AutoSize = true;
            this.lEatAt.Location = new System.Drawing.Point(4, 42);
            this.lEatAt.Name = "lEatAt";
            this.lEatAt.Size = new System.Drawing.Size(46, 13);
            this.lEatAt.TabIndex = 5;
            this.lEatAt.Text = "Eat at %";
            // 
            // lDrinkAt
            // 
            this.lDrinkAt.AutoSize = true;
            this.lDrinkAt.Location = new System.Drawing.Point(4, 20);
            this.lDrinkAt.Name = "lDrinkAt";
            this.lDrinkAt.Size = new System.Drawing.Size(55, 13);
            this.lDrinkAt.TabIndex = 4;
            this.lDrinkAt.Text = "Drink at %";
            // 
            // tbFood
            // 
            this.tbFood.Location = new System.Drawing.Point(65, 95);
            this.tbFood.Name = "tbFood";
            this.tbFood.Size = new System.Drawing.Size(71, 20);
            this.tbFood.TabIndex = 2;
            // 
            // lFood
            // 
            this.lFood.AutoSize = true;
            this.lFood.Location = new System.Drawing.Point(4, 98);
            this.lFood.Name = "lFood";
            this.lFood.Size = new System.Drawing.Size(31, 13);
            this.lFood.TabIndex = 3;
            this.lFood.Text = "Food";
            // 
            // tbDrink
            // 
            this.tbDrink.Location = new System.Drawing.Point(65, 69);
            this.tbDrink.Name = "tbDrink";
            this.tbDrink.Size = new System.Drawing.Size(71, 20);
            this.tbDrink.TabIndex = 0;
            // 
            // lDrink
            // 
            this.lDrink.AutoSize = true;
            this.lDrink.Location = new System.Drawing.Point(4, 69);
            this.lDrink.Name = "lDrink";
            this.lDrink.Size = new System.Drawing.Size(32, 13);
            this.lDrink.TabIndex = 1;
            this.lDrink.Text = "Drink";
            // 
            // bSaveSettings
            // 
            this.bSaveSettings.Location = new System.Drawing.Point(12, 418);
            this.bSaveSettings.Name = "bSaveSettings";
            this.bSaveSettings.Size = new System.Drawing.Size(89, 21);
            this.bSaveSettings.TabIndex = 3;
            this.bSaveSettings.Text = "Save";
            this.bSaveSettings.UseVisualStyleBackColor = true;
            this.bSaveSettings.Click += new System.EventHandler(this.bSaveSettings_Click);
            // 
            // gbRelog
            // 
            this.gbRelog.Controls.Add(this.tbPassword);
            this.gbRelog.Controls.Add(this.lPassword);
            this.gbRelog.Controls.Add(this.tbAccount);
            this.gbRelog.Controls.Add(this.lAccount);
            this.gbRelog.Location = new System.Drawing.Point(35, 11);
            this.gbRelog.Name = "gbRelog";
            this.gbRelog.Size = new System.Drawing.Size(136, 73);
            this.gbRelog.TabIndex = 2;
            this.gbRelog.TabStop = false;
            this.gbRelog.Text = "Relog";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(57, 43);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(71, 20);
            this.tbPassword.TabIndex = 2;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(4, 46);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(53, 13);
            this.lPassword.TabIndex = 3;
            this.lPassword.Text = "Password";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(57, 17);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(71, 20);
            this.tbAccount.TabIndex = 0;
            // 
            // lAccount
            // 
            this.lAccount.AutoSize = true;
            this.lAccount.Location = new System.Drawing.Point(4, 20);
            this.lAccount.Name = "lAccount";
            this.lAccount.Size = new System.Drawing.Size(47, 13);
            this.lAccount.TabIndex = 1;
            this.lAccount.Text = "Account";
            // 
            // tpIRC
            // 
            this.tpIRC.Controls.Add(this.lIRCDescription);
            this.tpIRC.Controls.Add(this.tbIRCBotChannel);
            this.tpIRC.Controls.Add(this.tbIRCBotNickname);
            this.tpIRC.Controls.Add(this.cbIRCConnect);
            this.tpIRC.Controls.Add(this.lIrcBotname);
            this.tpIRC.Controls.Add(this.lIrcChannel);
            this.tpIRC.Location = new System.Drawing.Point(4, 22);
            this.tpIRC.Name = "tpIRC";
            this.tpIRC.Padding = new System.Windows.Forms.Padding(3);
            this.tpIRC.Size = new System.Drawing.Size(477, 467);
            this.tpIRC.TabIndex = 5;
            this.tpIRC.Text = "IRC";
            this.tpIRC.UseVisualStyleBackColor = true;
            // 
            // lIRCDescription
            // 
            this.lIRCDescription.AutoSize = true;
            this.lIRCDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lIRCDescription.Location = new System.Drawing.Point(15, 131);
            this.lIRCDescription.Name = "lIRCDescription";
            this.lIRCDescription.Size = new System.Drawing.Size(409, 208);
            this.lIRCDescription.TabIndex = 5;
            this.lIRCDescription.Text = resources.GetString("lIRCDescription.Text");
            // 
            // tbIRCBotChannel
            // 
            this.tbIRCBotChannel.Location = new System.Drawing.Point(110, 52);
            this.tbIRCBotChannel.Name = "tbIRCBotChannel";
            this.tbIRCBotChannel.Size = new System.Drawing.Size(114, 20);
            this.tbIRCBotChannel.TabIndex = 4;
            // 
            // tbIRCBotNickname
            // 
            this.tbIRCBotNickname.Location = new System.Drawing.Point(110, 26);
            this.tbIRCBotNickname.Name = "tbIRCBotNickname";
            this.tbIRCBotNickname.Size = new System.Drawing.Size(114, 20);
            this.tbIRCBotNickname.TabIndex = 3;
            // 
            // cbIRCConnect
            // 
            this.cbIRCConnect.AutoCheck = false;
            this.cbIRCConnect.AutoSize = true;
            this.cbIRCConnect.Location = new System.Drawing.Point(15, 81);
            this.cbIRCConnect.Name = "cbIRCConnect";
            this.cbIRCConnect.Size = new System.Drawing.Size(99, 17);
            this.cbIRCConnect.TabIndex = 2;
            this.cbIRCConnect.Text = "Connect to IRC";
            this.cbIRCConnect.UseVisualStyleBackColor = true;
            this.cbIRCConnect.Click += new System.EventHandler(this.cbIRCConnect_Click);
            // 
            // lIrcBotname
            // 
            this.lIrcBotname.AutoSize = true;
            this.lIrcBotname.Location = new System.Drawing.Point(15, 29);
            this.lIrcBotname.Name = "lIrcBotname";
            this.lIrcBotname.Size = new System.Drawing.Size(89, 13);
            this.lIrcBotname.TabIndex = 1;
            this.lIrcBotname.Text = "Nickname of Bot:";
            // 
            // lIrcChannel
            // 
            this.lIrcChannel.AutoSize = true;
            this.lIrcChannel.Location = new System.Drawing.Point(15, 55);
            this.lIrcChannel.Name = "lIrcChannel";
            this.lIrcChannel.Size = new System.Drawing.Size(49, 13);
            this.lIrcChannel.TabIndex = 0;
            this.lIrcChannel.Text = "Channel:";
            // 
            // tpNotifications
            // 
            this.tpNotifications.Controls.Add(this.dgNotifications);
            this.tpNotifications.Location = new System.Drawing.Point(4, 22);
            this.tpNotifications.Name = "tpNotifications";
            this.tpNotifications.Size = new System.Drawing.Size(477, 467);
            this.tpNotifications.TabIndex = 3;
            this.tpNotifications.Text = "Notifications";
            this.tpNotifications.UseVisualStyleBackColor = true;
            // 
            // dgNotifications
            // 
            this.dgNotifications.AllowUserToAddRows = false;
            this.dgNotifications.AllowUserToDeleteRows = false;
            this.dgNotifications.AllowUserToResizeColumns = false;
            this.dgNotifications.AllowUserToResizeRows = false;
            this.dgNotifications.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgNotifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotifications.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4});
            this.dgNotifications.Location = new System.Drawing.Point(15, 101);
            this.dgNotifications.MultiSelect = false;
            this.dgNotifications.Name = "dgNotifications";
            this.dgNotifications.ReadOnly = true;
            this.dgNotifications.Size = new System.Drawing.Size(439, 282);
            this.dgNotifications.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Time";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 35;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Message";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // tpNews
            // 
            this.tpNews.Controls.Add(this.rtbNews);
            this.tpNews.Location = new System.Drawing.Point(4, 22);
            this.tpNews.Name = "tpNews";
            this.tpNews.Padding = new System.Windows.Forms.Padding(3);
            this.tpNews.Size = new System.Drawing.Size(477, 467);
            this.tpNews.TabIndex = 4;
            this.tpNews.Text = "News";
            this.tpNews.UseVisualStyleBackColor = true;
            // 
            // rtbNews
            // 
            this.rtbNews.Location = new System.Drawing.Point(15, 10);
            this.rtbNews.Name = "rtbNews";
            this.rtbNews.ReadOnly = true;
            this.rtbNews.Size = new System.Drawing.Size(432, 436);
            this.rtbNews.TabIndex = 0;
            this.rtbNews.Text = "";
            this.rtbNews.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNews_LinkClicked);
            // 
            // cbIgnoreZ
            // 
            this.cbIgnoreZ.AutoSize = true;
            this.cbIgnoreZ.Location = new System.Drawing.Point(223, 373);
            this.cbIgnoreZ.Name = "cbIgnoreZ";
            this.cbIgnoreZ.Size = new System.Drawing.Size(148, 17);
            this.cbIgnoreZ.TabIndex = 14;
            this.cbIgnoreZ.Text = "Ignore Z axis in this profile";
            this.cbIgnoreZ.UseVisualStyleBackColor = true;
            this.cbIgnoreZ.CheckedChanged += new System.EventHandler(this.cbIgnoreZ_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 511);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "ZzukBot";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.tcMain.ResumeLayout(false);
            this.tpGrind.ResumeLayout(false);
            this.tpGrind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChat)).EndInit();
            this.tpProfile.ResumeLayout(false);
            this.tpProfile.PerformLayout();
            this.tcWaypoints.ResumeLayout(false);
            this.tpHotspots.ResumeLayout(false);
            this.tpHotspots.PerformLayout();
            this.tpVendor.ResumeLayout(false);
            this.tpVendor.PerformLayout();
            this.tpGhost.ResumeLayout(false);
            this.tpGhost.PerformLayout();
            this.gbVendor.ResumeLayout(false);
            this.gbVendor.PerformLayout();
            this.gbFaction.ResumeLayout(false);
            this.gbFaction.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            this.gbChat.ResumeLayout(false);
            this.gbChat.PerformLayout();
            this.gbOther.ResumeLayout(false);
            this.gbOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBreakFor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudForceBreakAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaypointModifier)).EndInit();
            this.gbVendoring.ResumeLayout(false);
            this.gbVendoring.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreeSlots)).EndInit();
            this.gbDistances.ResumeLayout(false);
            this.gbDistances.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRoamFromWp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCombatRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMobSearchRange)).EndInit();
            this.gbRest.ResumeLayout(false);
            this.gbRest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEatAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDrinkAt)).EndInit();
            this.gbRelog.ResumeLayout(false);
            this.gbRelog.PerformLayout();
            this.tpIRC.ResumeLayout(false);
            this.tpIRC.PerformLayout();
            this.tpNotifications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNotifications)).EndInit();
            this.tpNews.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpGrind;
        private System.Windows.Forms.TabPage tpProfile;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.GroupBox gbRelog;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lAccount;

        /// <summary>
        /// Controls used to display options
        /// </summary>
        internal System.Windows.Forms.TextBox tbAccount;
        internal System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button bSaveSettings;
        private System.Windows.Forms.GroupBox gbRest;
        internal System.Windows.Forms.NumericUpDown nudEatAt;
        internal System.Windows.Forms.NumericUpDown nudDrinkAt;
        private System.Windows.Forms.Label lEatAt;
        private System.Windows.Forms.Label lDrinkAt;
        internal System.Windows.Forms.TextBox tbFood;
        private System.Windows.Forms.Label lFood;
        internal System.Windows.Forms.TextBox tbDrink;
        private System.Windows.Forms.Label lDrink;
        private System.Windows.Forms.GroupBox gbDistances;
        private System.Windows.Forms.Label lCombatRange;
        private System.Windows.Forms.Label lMobSearchRange;
        internal System.Windows.Forms.NumericUpDown nudCombatRange;
        internal System.Windows.Forms.NumericUpDown nudMobSearchRange;
        internal System.Windows.Forms.NumericUpDown nudRoamFromWp;
        private System.Windows.Forms.Label lRoamFromWp;
        private System.Windows.Forms.GroupBox gbVendoring;
        internal System.Windows.Forms.NumericUpDown nudFreeSlots;
        private System.Windows.Forms.Label lKeepQuality;
        private System.Windows.Forms.Label lFreeSlots;
        internal System.Windows.Forms.ComboBox cbKeepQuality;
        internal System.Windows.Forms.TextBox tbProtectedItems;
        private System.Windows.Forms.Label lProtectedItems;
        private System.Windows.Forms.Button bReload;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bGrindRun;
        private System.Windows.Forms.Button bGrindStop;
        internal System.Windows.Forms.Label lGrindState;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.TabPage tpHotspots;
        private System.Windows.Forms.TabPage tpVendor;
        private System.Windows.Forms.DataGridView dgChat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dSender;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMessage;
        private System.Windows.Forms.Button bClearChatLog;
        private System.Windows.Forms.GroupBox gbOther;
        internal System.Windows.Forms.NumericUpDown nudWaypointModifier;
        private System.Windows.Forms.Label lWaypointModifier;
        private System.Windows.Forms.Button bSettingsHelp;
        private System.Windows.Forms.Button bCreateHelp;
        internal System.Windows.Forms.TextBox tbPetFood;
        private System.Windows.Forms.Label lPetFood;
        private System.Windows.Forms.GroupBox gbChat;
        internal System.Windows.Forms.CheckBox cbBeepWhisper;
        internal System.Windows.Forms.CheckBox cbBeepSay;
        internal System.Windows.Forms.CheckBox cbBeepName;
        private System.Windows.Forms.TabPage tpGhost;
        private System.Windows.Forms.GroupBox gbMisc;
        internal System.Windows.Forms.CheckBox cbStopOnRare;
        internal System.Windows.Forms.CheckBox cbNotifyRare;
        private System.Windows.Forms.TabPage tpNotifications;
        private System.Windows.Forms.DataGridView dgNotifications;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        internal System.Windows.Forms.NumericUpDown nudBreakFor;
        private System.Windows.Forms.Label lBreakFor;
        internal System.Windows.Forms.NumericUpDown nudForceBreakAfter;
        private System.Windows.Forms.Label lForceBreak;
        private System.Windows.Forms.TabPage tpNews;
        internal System.Windows.Forms.RichTextBox rtbNews;
        internal System.Windows.Forms.GroupBox gbVendor;
        internal System.Windows.Forms.GroupBox gbFaction;
        internal System.Windows.Forms.Label lRestockItems;
        internal System.Windows.Forms.Label lRecording;
        internal System.Windows.Forms.Button bClearRestockItems;
        internal System.Windows.Forms.Button bAddRestockItem;
        internal System.Windows.Forms.TextBox tbRestockItems;
        internal System.Windows.Forms.TabControl tcWaypoints;
        internal System.Windows.Forms.Label lAddPointAs;
        internal System.Windows.Forms.RadioButton rbWaypoint;
        internal System.Windows.Forms.RadioButton rbHotspot;
        internal System.Windows.Forms.Button bClearVendor;
        internal System.Windows.Forms.Button bAddVendor;
        internal System.Windows.Forms.TextBox tbVendor;
        internal System.Windows.Forms.Label lVendor;
        internal System.Windows.Forms.Button bClearRestock;
        internal System.Windows.Forms.Button bAddRestock;
        internal System.Windows.Forms.Button bClearRepair;
        internal System.Windows.Forms.Button bAddRepair;
        internal System.Windows.Forms.TextBox tbRestock;
        internal System.Windows.Forms.TextBox tbRepair;
        internal System.Windows.Forms.Label lRestock;
        internal System.Windows.Forms.Label lRepair;
        internal System.Windows.Forms.TextBox tbHotspots;
        internal System.Windows.Forms.Label lHotspotCount;
        internal System.Windows.Forms.Button bAddHotspots;
        internal System.Windows.Forms.Button bClearHotspots;
        internal System.Windows.Forms.Button bClearVendorHotspots;
        internal System.Windows.Forms.TextBox tbVendorHotspots;
        internal System.Windows.Forms.Label lVendorHotspotCount;
        internal System.Windows.Forms.Button bAddVendorHotspot;
        internal System.Windows.Forms.Button bClearGhostHotspots;
        internal System.Windows.Forms.TextBox tbGhostHotspots;
        internal System.Windows.Forms.Label lGhostHotspotCount;
        internal System.Windows.Forms.Button bAddGhostHotspot;
        internal System.Windows.Forms.TextBox tbFactions;
        internal System.Windows.Forms.Label lFactionCount;
        internal System.Windows.Forms.Button bAddFaction;
        internal System.Windows.Forms.Button bClearFactions;
        internal System.Windows.Forms.Label lGrindLoadProfile;
        private System.Windows.Forms.TabPage tpIRC;
        private System.Windows.Forms.Label lIrcBotname;
        private System.Windows.Forms.Label lIrcChannel;
        internal System.Windows.Forms.CheckBox cbIRCConnect;
        internal System.Windows.Forms.TextBox tbIRCBotChannel;
        internal System.Windows.Forms.TextBox tbIRCBotNickname;
        private System.Windows.Forms.Label lIRCDescription;
        internal System.Windows.Forms.CheckBox cbMine;
        internal System.Windows.Forms.CheckBox cbHerb;
        internal System.Windows.Forms.CheckBox cbSkinUnits;
        internal System.Windows.Forms.CheckBox cbLootUnits;
        internal System.Windows.Forms.CheckBox cbNinjaSkin;
        private System.Windows.Forms.CheckBox cbLoadLastProfile;
        internal System.Windows.Forms.CheckBox cbIgnoreZ;
    }
}

