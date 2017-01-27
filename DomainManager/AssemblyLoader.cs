using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DomainManager
{
    [ComVisible(true)]
    public class ClassicFrameworkAssemblyLoader : MarshalByRefObject
    {
        public ClassicFrameworkAssemblyLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        #region IAssemblyLoader Members

        public void LoadAndRun(string file)
        {
            var asm = Assembly.LoadFrom(file);
            var entry = asm.EntryPoint;
            //object o = asm.CreateInstance(entry.Name);
            entry.Invoke(null, null);
        }

        #endregion

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name == Assembly.GetExecutingAssembly().FullName)
                return Assembly.GetExecutingAssembly();

            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var shortAsmName = Path.GetFileName(args.Name);
            Debug.Assert(shortAsmName != null, "shortAsmName != null");
            Debug.Assert(appDir != null, "appDir != null");
            var fileName = Path.Combine(appDir, shortAsmName);

            if (File.Exists(fileName))
            {
                return Assembly.LoadFrom(fileName);
            }
            return Assembly.GetExecutingAssembly().FullName == args.Name ? Assembly.GetExecutingAssembly() : null;
        }
    }
}