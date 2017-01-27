using System;
using System.Reflection;

namespace ZzukBot.Server
{
    internal enum IMError : byte
    {
        WrongPassword = SslClient.IM_WrongPass
    }

    [Obfuscation(Feature = "Apply to member * when method or constructor: virtualization", Exclude = false)]
    internal class IMErrorEventArgs : EventArgs
    {
        internal IMErrorEventArgs(IMError error)
        {
            Error = error;
        }

        internal IMError Error { get; }
    }

    [Obfuscation(Feature = "Apply to member * when method or constructor: virtualization", Exclude = false)]
    internal class IMReceivedEventArgs : EventArgs
    {
        internal IMReceivedEventArgs(string wardenModule)
        {
            this.wardenModule = wardenModule;
        }

        internal string wardenModule { get; }
    }

    [Obfuscation(Feature = "Apply to member * when method or constructor: virtualization", Exclude = false)]
    internal class IMReceivedWardenArgs : EventArgs
    {
        internal IMReceivedWardenArgs(string[] list)
        {
            GetList = list;
        }

        internal string[] GetList { get; }
    }

    internal delegate void IMErrorEventHandler(object sender, IMErrorEventArgs e);

    internal delegate void IMReceivedEventHandler(object sender, IMReceivedEventArgs e);

    internal delegate void IMReceivedWardenHandler(object sender, IMReceivedWardenArgs e);
}