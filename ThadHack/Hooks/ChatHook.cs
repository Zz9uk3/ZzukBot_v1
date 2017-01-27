using System.Runtime.InteropServices;
using ZzukBot.Constants;
using ZzukBot.Mem;

namespace ZzukBot.Hooks
{
    internal delegate void ChatMessageEventHandler(ChatMessage e);

    /// <summary>
    ///     Message class to store new chat messages
    /// </summary>
    internal class ChatMessage
    {
        internal ChatMessage(int parType, string parOwner, string parMsg)
        {
            Type = parType;
            Owner = parOwner;
            Message = parMsg;
        }

        internal string Message { get; }

        internal int Type { get; }

        internal string Owner { get; }
    }

    internal static class ChatHook
    {
        /// <summary>
        ///     Delegate to our C# function
        /// </summary>
        private static chatMessageDelegate _chatMessageDelegate;

        private static bool Applied;
        internal static event ChatMessageEventHandler OnNewChatMessage;

        private static void OnNewMessageEvent(ChatMessage e)
        {
            OnNewChatMessage?.Invoke(e);
        }

        /// <summary>
        ///     Init the hook and set enabled to true
        /// </summary>
        internal static void Init()
        {
            if (Applied) return;
            // Pointer the delegate to our c# function
            _chatMessageDelegate = ChatMessageHook;
            // get PTR for our c# function
            var addrToDetour = Marshal.GetFunctionPointerForDelegate(_chatMessageDelegate);
            // Alloc space for the ASM part of our detour
            string[] asmCode =
            {
                "PUSH 0x008444FC",
                "pushfd",
                "pushad",
                "mov ecx, [esp+40]",
                "mov edx, [esp+48]",
                "mov eax, [esp+52]",
                "push eax",
                "push edx",
                "push ecx",
                "call " + (uint) addrToDetour,
                "popad",
                "popfd",
                "jmp " + (uint) (Offsets.Hooks.ChatMessage + 5)
            };
            // Inject the asm code which calls our c# function
            var codeCave = Memory.InjectAsm(asmCode, "ChatMessageDetour");
            // set the jmp from WoWs code to my injected code
            Memory.InjectAsm((uint) Offsets.Hooks.ChatMessage, "jmp " + codeCave, "ChatMessageDetourJmp");
            Applied = true;
        }


        /// <summary>
        ///     Will be called from the ASM stub
        ///     parErrorCode contains the red message popping up on the
        ///     interface for the error
        /// </summary>
        private static void ChatMessageHook(int parType, string parOwner, string parMessage)
        {
            var msg = new ChatMessage(parType,
                parOwner,
                parMessage);

            OnNewMessageEvent(msg);
        }

        /// <summary>
        ///     Delegate for our c# function
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void chatMessageDelegate(int parType, string parOwner, string parMessage);
    }
}