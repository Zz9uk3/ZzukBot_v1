namespace ZzukBot.Mem
{
    internal static class Lua
    {
        private static string LuaCode = "";

        internal static void RunInMainthread(string parCode)
        {
            LuaCode = parCode;
            DirectX.RunAndSwapback(EndScene);
        }

        private static void EndScene(ref int frameCounter, bool IsIngame)
        {
            if (LuaCode != "")
            {
                Functions.DoString(LuaCode);
                LuaCode = "";
            }
        }
    }
}