using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace ZzukBot.Engines.CustomClass
{
    internal static class CodeCompiler
    {
        public static CompilerResults CompileSource(string sourceCode)
        {
            var csc = new CSharpCodeProvider();

            var referencedAssemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.FullName.StartsWith("mscorlib", StringComparison.InvariantCultureIgnoreCase))
                    .Where(a => !a.IsDynamic)
                    //necessary because a dynamic assembly will throw and exception when calling a.Location
                    .Select(a => a.Location)
                    .ToArray();

            var parameters = new CompilerParameters(
                referencedAssemblies) {GenerateInMemory = true};

            var cr = csc.CompileAssemblyFromSource(parameters,
                sourceCode);
            if (cr.Errors.Count > 0)
            {
                foreach (CompilerError err in cr.Errors)
                {
                    MessageBox.Show("Error in " + err.FileName + " at Line " + err.Line + ": " + err.ErrorText);
                }
                return null;
            }
            return cr;
        }

        public static object TryLoadCompiledType(this CompilerResults compilerResults, string typeName,
            params object[] constructorArgs)
        {
            var type = compilerResults.CompiledAssembly.GetType(typeName);
            return Activator.CreateInstance(type, constructorArgs);
        }
    }
}