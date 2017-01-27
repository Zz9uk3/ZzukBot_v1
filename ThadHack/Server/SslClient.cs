using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using ZzukBot.Constants;
using ZzukBot.Settings;

namespace ZzukBot.Server
{
    [Obfuscation(Feature = "Apply to member * when method or constructor or event: virtualization", Exclude = false)]
    internal class SslClient
    {
        //General Client Stuff
        internal const int IM_BOTVERSION = 101; // Current Bot Version
        internal const byte IM_OK = 17; // OK
        internal const byte IM_Login = 21; // Login
        internal const byte IM_WrongPass = 23; // Wrong password
        internal const byte IM_Send = 28; // Send message
        internal const byte IM_Received = 29; // Message received
        internal const byte IM_AlreadyOnline = 30; // Already Logged in
        internal const byte IM_KeyExpired = 31; //Key has expired
        internal const byte IM_TempBan = 39; //User has been locked out temporarily
        internal const byte IM_HeartBeat = 42; //keep the connection alive
        internal const byte IM_WRONGVERSION = 46; //Wrong bot version

        //Offset Stuff - client requesting offset
        internal const byte IM_SendOffset = 172; //Recieve offets
        internal const byte IM_SendWarden = 173; //Recieve warden
        internal const byte IM_RequestWarden = 179; //Request an offset
        internal const byte IM_RequestOffset = 183; //Request an offset
        private bool _conn; // Is connected/connecting?
        private BinaryReader br;
        private BinaryWriter bw;


        private TcpClient client;
        private Thread heartBeatThread; // Heartbeat
        private NetworkStream netStream;
        private SslStream ssl;
        private Thread tcpThread; // Receiver

        internal string Server => "vanillaauth.zzukbot.com";
        // "localhost"; } }//"138.130.238.131"; } }  // Address of server. In this case - local IP address.

        internal int Port => 6121;

        internal bool IsLoggedIn { get; private set; }
        internal string UserName { get; private set; }
        internal string Password { get; private set; }

        // Start connection thread and login or register.
        internal void connect(string user, string password)
        {
            if (!_conn)
            {
                _conn = true;
                UserName = user;
                Password = password;
                tcpThread = new Thread(SetupConn) {IsBackground = true};
                tcpThread.Start();
            }
        }

        internal void Login(string user, string password)
        {
            connect(user, password);
        }

        internal void Register(string user, string password)
        {
            connect(user, password);
        }

        internal void Disconnect()
        {
            if (_conn)
                CloseConn(true);
        }

        internal void RequestWarden()
        {
            if (_conn)
            {
                bw.Write(IM_RequestWarden);
                bw.Flush();
            }
        }

        internal void RequestOffset()
        {
            if (_conn)
            {
                bw.Write(IM_RequestOffset);
                bw.Flush();
            }
        }

        internal void HeartBeat()
        {
            if (_conn)
            {
                bw.Write(IM_HeartBeat);
                bw.Flush();
            }
        }


        // Events
        internal event EventHandler LoginOK;
        internal event IMErrorEventHandler LoginFailed;
        internal event EventHandler Disconnected;
        internal event IMReceivedWardenHandler OffsetReceived;


        protected virtual void OnLoginOK()
        {
            LoginOK?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnLoginFailed(IMErrorEventArgs e)
        {
            LoginFailed?.Invoke(this, e);
        }

        protected virtual void OnDisconnected()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOffsetReceived(IMReceivedWardenArgs e)
        {
            OffsetReceived?.Invoke(this, e);
        }

        internal void SetupConn() // Setup connection and login
        {
            var progress = 0;
            try
            {
                client = new TcpClient(Server, Port); // Connect to the server.

                netStream = client.GetStream();
                ssl = new SslStream(netStream, false, ValidateCert);
                ssl.AuthenticateAsClient("ZzukbotVanilla");
                // Now we have encrypted connection.

                ssl.WriteTimeout = 60000;
                ssl.ReadTimeout = 60000;

                br = new BinaryReader(ssl, Encoding.UTF8);
                bw = new BinaryWriter(ssl, Encoding.UTF8);

                // Receive "hello"
                var hello = br.ReadInt32();
                if (hello == IM_BOTVERSION)
                {
                    // Hello OK, so answer.
                    bw.Write(IM_BOTVERSION);
                    progress = 1;

#if DEBUG
                    bw.Write("Test");
#else
                    bw.Write(Program.GetMD5AsBase64(Paths.ThadHack));
#endif

                    bw.Write(IM_Login); // Login or register
                    bw.Write(UserName);
                    bw.Write(Password);
                    bw.Flush();

                    var ans = br.ReadByte(); // Read answer.
                    if (ans == IM_OK) // Login/register OK
                    {
                        OnLoginOK(); // Login is OK (when registered, automatically logged in)
                        heartBeatThread = new Thread(heartBeat) {IsBackground = true};
                        heartBeatThread.Start();
                        Receiver(); // Time for listening for incoming messages.
                    }
                    else
                    {
                        if (_conn)
                            CloseConn(false);
                        var err = new IMErrorEventArgs((IMError) ans);
                        OnLoginFailed(err);
                        return;
                    }
                }
                if (_conn)
                    CloseConn(true);
            }
            catch (Exception)
            {
                if (progress == 1)
                {
                    var err = new IMErrorEventArgs((IMError) IM_WRONGVERSION);
                    OnLoginFailed(err);
                }

                Environment.Exit(0);
                //IMErrorEventArgs err = new IMErrorEventArgs((IMError)IM_Offline);
                //OnLoginFailed(err);
            }
        }

        internal void CloseConn(bool dc) // Close connection.
        {
            try
            {
                br.Close();
                bw.Close();
                ssl.Close();
                netStream.Close();
                client.Close();
                if (dc)
                    OnDisconnected();
            }
            catch (Exception)
            {
                //Only happens if cannot conenct to server then attempts to close the bot
            }

            _conn = false;
        }

        internal void heartBeat()
        {
            var ping = 0;
            var pingMax = 20;
            try
            {
                while (true)
                {
                    if (client.Connected)
                    {
                        Thread.Sleep(1000);
                        ping++;
                        if (ping > pingMax)
                        {
                            HeartBeat();
                            ping = 0;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        internal void Receiver() // Receive all incoming packets.
        {
            IsLoggedIn = true;
            ssl.WriteTimeout = 60000;
            ssl.ReadTimeout = 60000;
            try
            {
                //Request offsets first
                RequestOffset();

                while (client.Connected) // While we are connected.
                {
                    var type = br.ReadByte(); // Get incoming packet type.

                    if (type == IM_SendWarden)
                    {
                        string[] Warden = {br.ReadString()};
                        OnOffsetReceived(new IMReceivedWardenArgs(Warden));
                    }
                    else if (type == IM_SendOffset)
                    {
                        /*Don't look at me*/
                        Offsets.Unit.AuraBase = br.ReadInt32();
                        Offsets.Descriptors.SummonedByGuid = br.ReadInt32();
                        Offsets.Descriptors.NpcId = br.ReadInt32();
                        Offsets.Descriptors.movementFlags = br.ReadInt32();
                        Enums.DynamicFlags.CanBeLooted = (uint) br.ReadInt32();
                        Enums.DynamicFlags.TappedByMe = (uint) br.ReadInt32();
                        Enums.DynamicFlags.TappedByOther = (uint) br.ReadInt32();
                        Enums.DynamicFlags.Untouched = (uint) br.ReadInt32();

                        //Once offsets are downloaded, request warden
                        RequestWarden();
                    }
                    else if (type == IM_HeartBeat)
                    {
                        //Do nothing, keeps the connection alive
                    }

                    Thread.Sleep(100);
                }
            }
            catch (IOException)
            {
            }
            try
            {
            }
            catch
            {
                IsLoggedIn = false;
            }
        }


        internal static bool ValidateCert(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            // Uncomment this lines to disallow untrusted certificates.
            //if (sslPolicyErrors == SslPolicyErrors.None)
            //    return true;
            //else
            //    return false;

            return true; // Allow untrusted certificates.
        }
    }
}