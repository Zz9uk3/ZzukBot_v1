using ZzukBot.Hooks;

namespace ZzukBot.Mem
{
    internal static class GlobalHooks
    {
        private static bool Applied;

        internal static void Init()
        {
            if (Applied) return;
            ErrorEnumHook.OnNewError += OnNewErrorEvent;
            Applied = true;
        }

        private static void OnNewErrorEvent(ErrorEnumArgs e)
        {
            if (e.Message.StartsWith("You have learned "))
            {
                ObjectManager.UpdateSpells();
            }
        }
    }
}