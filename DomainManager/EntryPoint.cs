using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace DomainManager
{
    public class EntryPoint
    {
        // ReSharper disable once NotAccessedField.Local
        private static ClassicFrameworkDomain cfd;

        [STAThread]
        public static int Main(string args)
        {
            try
            {
                cfd = new ClassicFrameworkDomain();
                Thread.Sleep(10);
            }
            catch (Exception e)
            {
                MessageBox.Show("Entry Point Exception: " + e);
            }
            Process.GetCurrentProcess().Kill();
            return 1;
        }
    }
}