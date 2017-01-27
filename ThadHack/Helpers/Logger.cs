using System;
using System.IO;
using System.Reflection;

namespace ZzukBot.Helpers
{
    internal static class Logger
    {
        internal static void Append(string parMessage, string toFile = "")
        {
#if DEBUG
            var msg = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " +
                      parMessage;
            Console.WriteLine(msg);

            if (toFile != "")
            {
                File.AppendAllText(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\"
                    + toFile, msg);
            }
#endif
        }
    }
}