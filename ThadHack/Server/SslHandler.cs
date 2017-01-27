using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ZzukBot.Engines;
using ZzukBot.GUI_Forms;

namespace ZzukBot.Server
{
    internal class SslHandler
    {
        private SslClient _Connection;

        internal SslHandler()
        {
            SetupEvents();
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private void SetupEvents()
        {
            _Connection = new SslClient();

            _Connection.LoginOK += im_LoginOK;
            _Connection.LoginFailed += im_LoginFailed;
#if !DEBUG
            _Connection.Disconnected += new EventHandler(im_Disconnected);
#endif
            _Connection.OffsetReceived += im_OffsetRecieved;
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal void StartAuth(string parUsername, string parPassword)
        {
            _Connection.Login(parUsername, parPassword);
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private void im_LoginOK(object sender, EventArgs e)
        {
            Main.MainForm.Invoke(new MethodInvoker(delegate { }));
        }

        //Called when login fails
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal void im_LoginFailed(object sender, IMErrorEventArgs e)
        {
            Main.MainForm.Invoke(new MethodInvoker(delegate
            {
                if ((byte) e.Error == SslClient.IM_WRONGVERSION)
                {
                    Program.QuitWithMessage("Zzukbot has been updated. Please re-download.");
                }
                else
                {
                    MessageBox.Show(
                        "Login failed because: \n  - Invalid Username/Password \n  - Account out of credit \n  - Auth server is updating (Wait 5 mins)");
                    var login = new LoginForm();
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        _Connection.Login(login.Email, login.Password);
                    }
                    else
                    {
                        Environment.Exit(-1);
                        return;
                    }
                    login.Dispose();
                }
            }));
        }

        //Called when the ssl times out
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal void im_Disconnected(object sender, EventArgs e)
        {
            Main.MainForm.Invoke(new MethodInvoker(delegate
            {
                if (EngineManager.CurrentEngineType != Engines.Engines.None)
                    EngineManager.StopCurrentEngine();
                Program.QuitWithMessage("Connection to server was lost.");
            }));
        }

        //Event for warden downloaded
        //Places it into an array
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        internal void im_OffsetRecieved(object sender, IMReceivedWardenArgs e)
        {
            Main.MainForm.Invoke(new MethodInvoker(delegate
            {
                string[] stringSeparators = {"[|]"};
                var warden = e.GetList[0].Split(stringSeparators, StringSplitOptions.None);

                for (var i = 0; i < warden.Length - 1; i++)
                {
                    DownloadedOffsets.WardenDetour[i] = warden[i];
                }
                Main.MainForm.EndLaunchPrepare();
            }));
        }
    }
}