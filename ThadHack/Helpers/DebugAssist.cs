using System;
using System.Threading;

namespace ZzukBot.Helpers
{
#if DEBUG
    internal static class DebugAssist
    {
        private static bool Applied;


        private static Thread thrConsole;

        internal static void Init()
        {
            if (Applied) return;
            thrConsole = new Thread(ConsoleReader) {IsBackground = true};
            thrConsole.Start();
            Applied = true;
        }

        private static void ConsoleReader()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "test":
                            //DirectX.RunAndSwapback(delegate (ref int frameCounter, bool IsIngame)
                            //{
                            //    string str =
                            //    "CharSelectEnterWorldButton:Click()";
                            //    Functions.DoString(str);


                            //});
                            break;

                        case "objects":
                            break;
                    }
                }
                catch
                {
                }
                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
#endif
}