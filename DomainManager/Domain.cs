using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DomainManager
{
    /// <summary>
    ///     The actual domain object we'll be using to load and run the Onyx binaries.
    /// </summary>
    public class ClassicFrameworkDomain
    {
        private readonly Random _rand = new Random();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ClassicFrameworkDomain()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            ext();
        }

        private void ext()
        {
            try
            {
                var appBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var ads = new AppDomainSetup {ApplicationBase = appBase, PrivateBinPath = appBase};
                DomainManager.CurrentDomain = AppDomain.CreateDomain("ZzukBotDomain_Internal_" + _rand.Next(0, 100000),
                    null, ads);

                var str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ZzukBot.exe";

                var type = typeof (ClassicFrameworkAssemblyLoader);

                DomainManager.CurrentAssemblyLoader =
                    DomainManager.CurrentDomain.CreateInstanceAndUnwrap(type.Assembly.FullName,
                        type.FullName);


                DomainManager.CurrentAssemblyLoader.LoadAndRun(
                    str);
                DomainManager.CurrentDomain.UnhandledException += ExceptionHandler.Unhandled;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ZzukBot.exe");
            }
            finally
            {
                DomainManager.CurrentAssemblyLoader = null;
                AppDomain.Unload(DomainManager.CurrentDomain);
            }
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var assembly = Assembly.Load(args.Name);
                if (assembly != null)
                    return assembly;
            }
            catch
            {
                // ignore load error 
            }

            // *** Try to load by filename - split out the filename of the full assembly name
            // *** and append the base path of the original assembly (ie. look in the same dir)
            // *** NOTE: this doesn't account for special search paths but then that never
            //           worked before either.
            var parts = args.Name.Split(',');
            MessageBox.Show("Trying to load: " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +
                            parts[0].Trim() + ".dll");
            var file = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + parts[0].Trim() + ".exe";

            return Assembly.LoadFrom(file);
        }
    }
}