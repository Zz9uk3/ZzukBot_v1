using System;
using System.Threading;
using ZzukBot.Helpers.IrcDotNet;

namespace ZzukBot.Helpers
{
    internal class IrcMonitor
    {
        /// <summary>
        /// The _instance
        /// </summary>
        private static readonly Lazy<IrcMonitor> _instance = new Lazy<IrcMonitor>(() => new IrcMonitor());
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        internal static IrcMonitor Instance => _instance.Value;
        /// <summary>
        /// Prevents a default instance of the <see cref="IrcMonitor" /> class from being created.
        /// </summary>
        private IrcMonitor()
        {
        }

        internal void Start(string parChannel, string parNickname)
        {
            if (Client != null) return;
            Channel = parChannel.ToLower();
            Nickname = parNickname;
            var reg = new IrcUserRegistrationInfo
            {
                RealName = Nickname,
                NickName = Nickname,
                UserName = Nickname
            };
            Client = new StandardIrcClient { FloodPreventer = new IrcStandardFloodPreventer(4, 2000) };

            Client.ErrorMessageReceived += (sender, args) =>
            {
                Console.WriteLine(args.Message);
            };

            Client.Connected += (sender, args) =>
            {
            };

            Client.Disconnected += (sender, args) =>
            {
                Client.Dispose();
                Client = null;
                Thread.Sleep(120000);
                Start(Channel, Nickname);
            };

            Client.Registered += (sender, args) =>
            {
                var tmp = (StandardIrcClient)sender;
                tmp.LocalUser.JoinedChannel += (o, JoinedChannelArgs) =>
                {
                    if (JoinedChannelArgs.Channel.Name == Channel)
                    {
                        tmp.RawMessageReceived += (sender1, eventArgs) =>
                        {
                            if (eventArgs.Message.Command != "PRIVMSG") return;
                            var msgArgs = new MessageArgs(eventArgs.Message.Parameters[1], eventArgs.Message.Source.Name);
                            MessageReceivedHandler(msgArgs);
                        };
                    }

                };
                tmp.Channels.Join(Channel);
            };
            Client.Connect(Server, false, reg);
        }

        internal void SendMessage(string parMessage)
        {
            if (Client == null) return;
            foreach (IrcChannel chan in Client.Channels)
            {
                if (chan.Name == Channel)
                    Client.LocalUser.SendMessage(chan, parMessage);
            }
        }

        private string Server = "irc.quakenet.org";
        private string Channel;
        private string Nickname;
        private StandardIrcClient Client;

        internal EventHandler<MessageArgs> MessageReceived;
        internal class MessageArgs : EventArgs
        {
            internal readonly string Message;
            internal readonly string Sender;
            internal MessageArgs(string parMessage, string parSender)
            {
                Message = parMessage;
                Sender = parSender;
            }
        }
        private void MessageReceivedHandler(MessageArgs parArgs)
        {
            MessageReceived?.Invoke(this, parArgs);
        }
    }
}