using System;
using System.Text;
using ZzukBot.Constants;
using ZzukBot.Settings;

namespace ZzukBot.Mem
{
    /// <summary>
    ///     Important for relogging
    /// </summary>
    internal static class Relog
    {
        internal static int NumCharacterCount => Offsets.CharacterScreen.NumCharacters.ReadAs<int>();

        internal static string LoginState => Offsets.CharacterScreen.LoginState.ReadString();

        internal static string CurrentWindowName
        {
            get
            {
                try
                {
                    var first = Memory.Reader.Read<IntPtr>((IntPtr)0xCF0BD8);
                    var curWindow = Memory.Reader.Read<IntPtr>(IntPtr.Add(first, 0x7c));
                    if (curWindow == IntPtr.Zero) return "";
                    return Memory.Reader.ReadString(Memory.Reader.Read<IntPtr>(IntPtr.Add(curWindow, 0x98)), Encoding.ASCII);
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        internal static void ResetLogin()
        {
            Functions.DoString("arg1 = 'ESCAPE' GlueDialog_OnKeyDown()");
            Functions.DoString(
                "if RealmListCancelButton ~= nil then if RealmListCancelButton:IsVisible() then RealmListCancelButton:Click(); end end");
            ClearGlueDialogText();
        }

        internal static void Login()
        {
            Functions.DoString("DefaultServerLogin('" + Options.AccountName + "', '" + Options.AccountPassword + "');");
        }

        internal static void Enter()
        {
            Functions.EnterWorld();
        }

        internal static string GetGlueDialogText()
        {
            var enc = "myShit".GenLuaVarName();
            Functions.DoString(enc + " = GlueDialogText:GetText()");
            return Functions.GetText(enc);
        }

        internal static void ClearGlueDialogText()
        {
            Functions.DoString("GlueDialogText:SetText('')");
        }
    }
}