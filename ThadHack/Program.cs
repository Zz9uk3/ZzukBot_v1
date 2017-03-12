using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using ZzukBot.Constants;
using ZzukBot.Engines.CustomClass;
using ZzukBot.GUI_Forms;
using ZzukBot.Helpers;
using ZzukBot.OOP;
using ZzukBot.Properties;
using ZzukBot.Settings;
using System.Management;

namespace ZzukBot
{
    [Obfuscation(Feature = "Apply to member * when method or constructor: virtualization", Exclude = false)]
    internal static class Program
    {
        private const int mmapVersionConst = 2;

        private static EventWaitHandle s_event;

        private static bool AreWeInjected => !Process.GetCurrentProcess().ProcessName.StartsWith("ZzukBot");

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name).Name;
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + name + ".dll";
            return Assembly.LoadFile(path);
        }

        private static bool IsAlreadyRunning()
        {
            bool created;
            s_event = new EventWaitHandle(false,
                EventResetMode.ManualReset, Assembly.GetExecutingAssembly().Location.Replace("\\", "#"), out created);
            return !created;
        }

        [STAThread]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Main()
        {
            if (!IsAlreadyRunning())
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                LaunchBot();
            }
            else
            {
                QuitWithMessage("An instance is already running from this location");
            }
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void LaunchBot()
        {
            // Setting culture for float etc (. instead of ,)
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!AreWeInjected)
            {
                //var attForm = new AttachForm();
                //attForm.ShowDialog();
                //switch (attForm.Result)
                //{
                //    case AttachFormResult.Fresh:
                        PrepareAndInject();
                //        break;

                //    case AttachFormResult.Attach:
                //        PrepareAndInject(attForm.AttachTo.PID);
                //        break;
                //}
            }
            else
            {
#if DEBUG
                WinImports.AllocConsole();
                Logger.Append("DEBUG BUILD");
                DebugAssist.Init();

#endif
                SetPaths();
                SetRealmlist();
                try
                {
                    Application.Run(new Main());
                }
                catch (Exception e)
                {
                    Logger.Append(e.Message, "Exceptions.txt");
                }
            }
            Environment.Exit(0);
        }

        private static void PrepareAndInject(int? pId = null)
        {
            if (!Directory.Exists("mmaps"))
            {
                QuitWithMessage("Download the mmaps first please");
            }
            if (Directory.GetFileSystemEntries("mmaps").Length < 1000)
            {
                QuitWithMessage("Download the mmaps first please");
            }
            else
            {
                if (!File.Exists("mmaps\\Version.ini"))
                    QuitWithMessage("Wrong mmaps version! Please redownload");

                int mmapVersion;
                var result = int.TryParse(File.ReadAllText("mmaps\\Version.ini"), out mmapVersion);
                if (!result)
                    QuitWithMessage("Bad mmaps version identifier! Please check mmaps\\Version.ini");

                if (mmapVersion != mmapVersionConst)
                    QuitWithMessage("Wrong mmaps version! Please redownload");
            }
            if (GetMD5AsBase64("Fasm.NET.dll") != Resources.FasmNetMd5)
            {
                QuitWithMessage("Fastm.NET.dll is broken. Please redownload");
            }

            // Do the settings exist?
            if (!File.Exists("..\\Settings\\Settings.xml"))
            {
                while (true)
                {
                    var loc = new OpenFileDialog
                    {
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Filter = "executable (*.exe)|*.exe",
                        FilterIndex = 1,
                        Title = "Please locate your WoW.exe"
                    };
                    if (loc.ShowDialog() == DialogResult.OK)
                    {
                        if (loc.FileName == Assembly.GetExecutingAssembly().Location)
                        {
                            MessageBox.Show(
                                "FFS DIDNT THE WINDOW TITLE STATE TO TARGET THE WOW.EXE? WHY THE FCK SHOULD YOU SELECT THE ZZUKBOT.EXE?!?! FFS");
                        }
                        else
                        {
                            OOP.Settings.Recreate(loc.FileName);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            s_event.Close();
            Launch.Run(pId);
        }

        private static void SetPaths()
        {
            Paths.PathToWoW = Directory.GetCurrentDirectory();
            // get all kind of paths the bot need to operate
            var strPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Paths.Settings = Path.GetDirectoryName(strPath) + "\\Settings\\Settings.xml";
            Paths.Root = Path.GetDirectoryName(strPath);
            Paths.ProfileFolder = Path.GetDirectoryName(strPath) + "\\Profiles";
            Paths.ThadHack = strPath + "\\ZzukBot.exe";
            Paths.CCFolder = strPath + "\\CustomClasses";
            Paths.Internal = strPath;
        }

        private static void SetRealmlist()
        {
            var rlmList = Paths.PathToWoW + "\\realmlist.wtf";
            var project = File.ReadAllLines(rlmList);
            var name = "";
            foreach (var x in project)
            {
                if (x.ToLower().StartsWith("set realmlist "))
                    name = x.ToLower();
            }
            Options.RealmList = name;
        }


        internal static string GetMD5AsBase64(string parFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(parFile))
                {
                    return Convert.ToBase64String(md5.ComputeHash(stream));
                }
            }
        }

        internal static void QuitWithMessage(string parMessage)
        {
            MessageBox.Show(parMessage);
            Environment.Exit(0);
        }
    }
}