/**
 * A very basic RTMPS client
 *
 * @author Gabriel Van Eyck
 */
///////////////////////////////////////////////////////////////////////////////// 
//
//Ported to C# by Ryan A. LaSarre
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

using PVPNetConnect.Assets;
using PVPNetConnect.RiotObjects;
using PVPNetConnect.RiotObjects.Summoner;
using PVPNetConnect.RiotObjects.Statistics;
using PVPNetConnect.RiotObjects.Client;
using PVPNetConnect.RiotObjects.Game;

namespace PVPNetConnect
{
    public class PVPNetConnection
    {
        #region Member Declarations

        //RTMPS Connection Info
        private bool isConnected = false;
        private TcpClient client;
        private SslStream sslStream;
        private string ipAddress;
        private string authToken;
        private int accountID;
        private string sessionToken;
        private string DSId;

        //Initial Login Information
        private string user;
        private string password;
        private string server;
        private string loginQueue;
        private string locale;
        private string clientVersion;

        /** Garena information */
        private bool useGarena = false;
        private string garenaToken;
        private string userID;


        //Invoke Variables
        private Random rand = new Random();
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        private int invokeID = 2;

        private List<int> pendingInvokes = new List<int>();
        private Dictionary<int, TypedObject> results = new Dictionary<int, TypedObject>();
        private Dictionary<int, RiotGamesObject> callbacks = new Dictionary<int, RiotGamesObject>();
        private Thread decodeThread;

        private int heartbeatCount = 1;
        private Thread heartbeatThread;

        #endregion

        #region Event Handlers

        public delegate void OnConnectHandler(object sender, EventArgs e);
        public event OnConnectHandler OnConnect;

        public delegate void OnLoginQueueUpdateHandler(object sender, int positionInLine);
        public event OnLoginQueueUpdateHandler OnLoginQueueUpdate;

        public delegate void OnLoginHandler(object sender, string username, string ipAddress);
        public event OnLoginHandler OnLogin;

        public delegate void OnDisconnectHandler(object sender, EventArgs e);
        public event OnDisconnectHandler OnDisconnect;

        public delegate void OnErrorHandler(object sender, Error error);
        public event OnErrorHandler OnError;

        #endregion

        #region Connect, Login, and Heartbeat Methods

        public void Connect(string user, string password, Region region, string clientVersion)
        {
            if (!isConnected)
            {
                Thread t = new Thread(() =>
                {
                    this.user = user;
                    this.password = password;
                    this.clientVersion = clientVersion;

                    if (region == Region.NA)
                    {
                        this.server = "prod.na1.lol.riotgames.com";
                        this.loginQueue = "https://lq.na1.lol.riotgames.com/";
                        this.locale = "en_US";
                    }
                    else if (region == Region.EUW)
                    {
                        this.server = "prod.eu.lol.riotgames.com";
                        this.loginQueue = "https://lq.eu.lol.riotgames.com/";
                        this.locale = "en_GB";
                    }
                    else if (region == Region.EUN)
                    {
                        this.server = "prod.eun1.lol.riotgames.com";
                        this.loginQueue = "https://lq.eun1.lol.riotgames.com/";
                        this.locale = "en_GB";
                    }
                    else if (region == Region.KR)
                    {
                        this.server = "prod.kr.lol.riotgames.com";
                        this.loginQueue = "https://lq.kr.lol.riotgames.com/";
                        this.locale = "ko_KR";
                    }
                    else if (region == Region.BR)
                    {
                        this.server = "prod.br.lol.riotgames.com";
                        this.loginQueue = "https://lq.br.lol.riotgames.com/";
                        this.locale = "pt_BR";
                    }
                    else if (region == Region.TR)
                    {
                        this.server = "prod.tr.lol.riotgames.com";
                        this.loginQueue = "https://lq.tr.lol.riotgames.com/";
                        this.locale = "pt_BR";
                    }
                    else if (region == Region.PBE)
                    {
                        this.server = "prod.pbe1.lol.riotgames.com";
                        this.loginQueue = "https://lq.pbe1.lol.riotgames.com/";
                        this.locale = "en_US";
                    }
                    else if (region == Region.SG || region == Region.MY || region == Region.SGMY)
                    {
                        this.server = "prod.lol.garenanow.com";
                        this.loginQueue = "https://lq.lol.garenanow.com/";
                        this.locale = "en_US";
                        this.useGarena = true;
                    }
                    else if (region == Region.TW)
                    {
                        this.server = "prodtw.lol.garenanow.com";
                        this.loginQueue = "https://loginqueuetw.lol.garenanow.com/";
                        this.locale = "en_US";
                        this.useGarena = true;
                    }
                    else if (region == Region.TH)
                    {
                        this.server = "prodth.lol.garenanow.com";
                        this.loginQueue = "https://lqth.lol.garenanow.com/";
                        this.locale = "en_US";
                        this.useGarena = true;
                    }
                    else if (region == Region.PH)
                    {
                        this.server = "prodph.lol.garenanow.com";
                        this.loginQueue = "https://storeph.lol.garenanow.com/";
                        this.locale = "en_US";
                        this.useGarena = true;
                    }
                    else if (region == Region.VN)
                    {
                        this.server = "prodvn.lol.garenanow.com";
                        this.loginQueue = "https://lqvn.lol.garenanow.com/";
                        this.locale = "en_US";
                        this.useGarena = true;
                    }
                    else
                    {
                        Error("Unknown Region: " + region, ErrorType.General);
                        Disconnect();
                        return;
                    }


                    //Sets up our sslStream to riots servers
                    try
                    {
                        client = new TcpClient(server, 2099);
                    }
                    catch
                    {
                        Error("Riots servers are currently unabailable.", ErrorType.AuthKey);
                        Disconnect();
                        return;
                    }

                    //Check for riot webserver status
                    //along with gettin out Auth Key that we need for the login process.
                    if (useGarena)
                        if (!GetGarenaToken())
                            return;

                    if (!GetAuthKey())
                        return;

                    if (!GetIpAddress())
                        return;

                    sslStream = new SslStream(client.GetStream(), false, AcceptAllCertificates);
                    var ar = sslStream.BeginAuthenticateAsClient(server, null, null);
                    using (ar.AsyncWaitHandle)
                    {
                        if (ar.AsyncWaitHandle.WaitOne(-1))
                        {
                            sslStream.EndAuthenticateAsClient(ar);
                        }
                    }

                    if (!Handshake())
                        return;

                    BeginReceive();

                    if (!SendConnect())
                        return;

                    if (!Login())
                        return;

                    StartHeartbeat();
                });

                t.Start();
            }
        }

        private bool AcceptAllCertificates(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private bool GetGarenaToken()
        {
            /*
            try
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

                //GET OUR USER ID
                List<byte> userIdRequestBytes = new List<byte>();

                byte[] junk = new byte[] { 0x49, 0x00, 0x00, 0x00, 0x10, 0x01, 0x00, 0x79, 0x2f };
                userIdRequestBytes.AddRange(junk);
                userIdRequestBytes.AddRange(encoding.GetBytes(user));
                for (int i = 0; i < 16; i++)
                    userIdRequestBytes.Add(0x00);

                System.Security.Cryptography.MD5 md5Cryp = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = encoding.GetBytes(password);
                byte[] md5 = md5Cryp.ComputeHash(inputBytes);

                foreach (byte b in md5)
                    userIdRequestBytes.AddRange(encoding.GetBytes(String.Format("%02x", b)));

                userIdRequestBytes.Add(0x00);
                userIdRequestBytes.Add(0x01);
                junk = new byte[] { 0xD4, 0xAE, 0x52, 0xC0, 0x2E, 0xBA, 0x72, 0x03 };
                userIdRequestBytes.AddRange(junk);

                int timestamp = (int)(DateTime.UtcNow.TimeOfDay.TotalMilliseconds / 1000);
                for (int i = 0; i < 4; i++)
                    userIdRequestBytes.Add((byte)((timestamp >> (8 * i)) & 0xFF));

                userIdRequestBytes.Add(0x00);
                userIdRequestBytes.AddRange(encoding.GetBytes("intl"));
                userIdRequestBytes.Add(0x00);

                byte[] userIdBytes = userIdRequestBytes.ToArray();

                TcpClient client = new TcpClient("203.117.158.170", 9100);
                client.GetStream().Write(userIdBytes, 0, userIdBytes.Length);
                client.GetStream().Flush();

                int id = 0;
                for (int i = 0; i < 4; i++)
                    id += client.GetStream().ReadByte() * (1 << (8 * i));

                userID = Convert.ToString(id);


                //GET TOKEN
                List<byte> tokenRequestBytes = new List<byte>();
                junk = new byte[] { 0x32, 0x00, 0x00, 0x00, 0x01, 0x03, 0x80, 0x00, 0x00 };
                tokenRequestBytes.AddRange(junk);
                tokenRequestBytes.AddRange(encoding.GetBytes(user));
                tokenRequestBytes.Add(0x00);
                foreach (byte b in md5)
                    tokenRequestBytes.AddRange(encoding.GetBytes(String.Format("%02x", b)));
                tokenRequestBytes.Add(0x00);
                tokenRequestBytes.Add(0x00);
                tokenRequestBytes.Add(0x00);

                byte[] tokenBytes = tokenRequestBytes.ToArray();

                client = new TcpClient("lol.auth.garenanow.com", 12000);
                client.GetStream().Write(tokenBytes, 0, tokenBytes.Length);
                client.GetStream().Flush();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 5; i++)
                    client.GetStream().ReadByte();
                int c;
                while ((c = client.GetStream().ReadByte()) != 0)
                    sb.Append((char)c);

                garenaToken = sb.ToString();

                client.Close();
                return true;
            }
            catch
            {
                Error("Unable to acquire garena token", ErrorType.Login);
                Disconnect();
                return false;
            }
             */

            Error("Garena Servers are not yet supported", ErrorType.Login);
            Disconnect();
            return false;
        }

        private bool GetAuthKey()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string payload = "user=" + user + ",password=" + password;
                string query = "payload=" + payload;

                if (useGarena)
                    payload = garenaToken;

                WebRequest con = WebRequest.Create(loginQueue + "login-queue/rest/queue/authenticate");
                con.Method = "POST";

                Stream outputStream = con.GetRequestStream();
                outputStream.Write(Encoding.ASCII.GetBytes(query), 0, Encoding.ASCII.GetByteCount(query));

                WebResponse webresponse = con.GetResponse();
                Stream inputStream = webresponse.GetResponseStream();

                int c;
                while ((c = inputStream.ReadByte()) != -1)
                    sb.Append((char)c);

                TypedObject result = serializer.Deserialize<TypedObject>(sb.ToString());
                outputStream.Close();
                inputStream.Close();
                con.Abort();

                if (!result.ContainsKey("token"))
                {
                    int node = (int)result.GetInt("node");
                    string champ = result.GetString("champ");
                    int rate = (int)result.GetInt("rate");
                    int delay = (int)result.GetInt("delay");

                    int id = 0;
                    int cur = 0;

                    object[] tickers = result.GetArray("tickers");
                    foreach (object o in tickers)
                    {
                        Dictionary<string, object> to = (Dictionary<string, object>)o;

                        int tnode = (int)to["node"];
                        if (tnode != node)
                            continue;

                        id = (int)to["id"];
                        cur = (int)to["current"];
                        break;
                    }

                    while (id - cur > rate)
                    {
                        sb.Clear();

                        OnLoginQueueUpdate(this, id - cur);

                        Thread.Sleep(delay);
                        con = WebRequest.Create(loginQueue + "login-queue/rest/queue/ticker/" + champ);
                        con.Method = "GET";
                        webresponse = con.GetResponse();
                        inputStream = webresponse.GetResponseStream();

                        int d;
                        while ((d = inputStream.ReadByte()) != -1)
                            sb.Append((char)d);

                        result = serializer.Deserialize<TypedObject>(sb.ToString());


                        inputStream.Close();
                        con.Abort();

                        if (result == null)
                            continue;

                        cur = HexToInt(result.GetString(node.ToString()));
                    }



                    while (sb.ToString() == null || !result.ContainsKey("token"))
                    {
                        try
                        {
                            sb.Clear();

                            if (id - cur < 0)
                                OnLoginQueueUpdate(this, 0);
                            else
                                OnLoginQueueUpdate(this, id - cur);

                            Thread.Sleep(delay / 10);
                            con = WebRequest.Create(loginQueue + "login-queue/rest/queue/authToken/" + user.ToLower());
                            con.Method = "GET";
                            webresponse = con.GetResponse();
                            inputStream = webresponse.GetResponseStream();

                            int f;
                            while ((f = inputStream.ReadByte()) != -1)
                                sb.Append((char)f);

                            result = serializer.Deserialize<TypedObject>(sb.ToString());

                            inputStream.Close();
                            con.Abort();
                        }
                        catch
                        {

                        }
                    }
                }

                OnLoginQueueUpdate(this, 0);
                authToken = result.GetString("token");

                return true;
            }
            catch (Exception e)
            {
                if (e.Message == "The remote name could not be resolved: '" + loginQueue + "'")
                {
                    Error("Please make sure you are connected the internet!", ErrorType.AuthKey);
                    Disconnect();
                }
                else if (e.Message == "The remote server returned an error: (403) Forbidden.")
                {
                    Error("Your username or password is incorrect!", ErrorType.Password);
                    Disconnect();
                }
                else
                {
                    Error("Unable to get Auth Key \n" + e, ErrorType.AuthKey);
                    Disconnect();
                }

                return false;
            }
        }

        private int HexToInt(string hex)
        {
            int total = 0;
            for (int i = 0; i < hex.Length; i++)
            {
                char c = hex.ToCharArray()[i];
                if (c >= '0' && c <= '9')
                    total = total * 16 + c - '0';
                else
                    total = total * 16 + c - 'a' + 10;
            }

            return total;
        }

        private bool GetIpAddress()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                WebRequest con = WebRequest.Create("http://ll.leagueoflegends.com/services/connection_info");
                WebResponse response = con.GetResponse();

                int c;
                while ((c = response.GetResponseStream().ReadByte()) != -1)
                    sb.Append((char)c);

                con.Abort();

                TypedObject result = serializer.Deserialize<TypedObject>(sb.ToString());

                ipAddress = result.GetString("ip_address");

                return true;
            }
            catch (Exception e)
            {
                Error("Unable to connect to Riot Games web server \n" + e.Message, ErrorType.General);
                Disconnect();
                return false;
            }
        }

        private bool Handshake()
        {
            byte[] handshakePacket = new byte[1537];
            rand.NextBytes(handshakePacket);
            handshakePacket[0] = (byte)0x03;
            sslStream.Write(handshakePacket);

            byte S0 = (byte)sslStream.ReadByte();
            if (S0 != 0x03)
            {
                Error("Server returned incorrect version in handshake: " + S0, ErrorType.Handshake);
                Disconnect();
                return false;
            }


            byte[] responsePacket = new byte[1536];
            sslStream.Read(responsePacket, 0, 1536);
            sslStream.Write(responsePacket);

            // Wait for response and discard result
            byte[] S2 = new byte[1536];
            sslStream.Read(S2, 0, 1536);

            // Validate handshake
            bool valid = true;
            for (int i = 8; i < 1536; i++)
            {
                if (handshakePacket[i + 1] != S2[i])
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
            {
                Error("Server returned invalid handshake", ErrorType.Handshake);
                Disconnect();
                return false;
            }
            return true;
        }

        private bool SendConnect()
        {
            Dictionary<string, object> paramaters = new Dictionary<string, object>();
            paramaters.Add("app", "");
            paramaters.Add("flashVer", "WIN 10,1,85,3");
            paramaters.Add("swfUrl", "app:/mod_ser.dat");
            paramaters.Add("tcUrl", "rtmps://" + server + ":" + 2099);
            paramaters.Add("fpad", false);
            paramaters.Add("capabilities", 239);
            paramaters.Add("audioCodecs", 3191);
            paramaters.Add("videoCodecs", 252);
            paramaters.Add("videoFunction", 1);
            paramaters.Add("pageUrl", null);
            paramaters.Add("objectEncoding", 3);


            RTMPSEncoder.startTime = (long)DateTime.Now.TimeOfDay.TotalMilliseconds;

            byte[] connect = RTMPSEncoder.EncodeConnect(paramaters);

            sslStream.Write(connect, 0, connect.Length);

            while (!results.ContainsKey(1))
                Thread.Sleep(10);
            TypedObject result = results[1];
            results.Remove(1);
            if (result["result"].Equals("_error"))
            {
                Error(GetErrorMessage(result), ErrorType.Connect);
                Disconnect();
                return false;
            }

            DSId = result.GetTO("data").GetString("id");

            isConnected = true;
            if (OnConnect != null)
                OnConnect(this, EventArgs.Empty);

            return true;
        }

        private bool Login()
        {
            TypedObject result, body;

            // Login 1
            body = new TypedObject("com.riotgames.platform.login.AuthenticationCredentials");
            body.Add("password", password);
            body.Add("clientVersion", clientVersion);
            body.Add("ipAddress", ipAddress);
            body.Add("securityAnswer", null);
            body.Add("locale", locale);
            body.Add("domain", "lolclient.lol.riotgames.com");
            body.Add("oldPassword", null);
            body.Add("authToken", authToken);
            if (useGarena)
            {
                body.Add("partnerCredentials", "8393 " + garenaToken);
                body.Add("username", userID);
            }
            else
            {
                body.Add("partnerCredentials", null);
                body.Add("username", user);
            }


            int id = Invoke("loginService", "login", new object[] { body });

            result = GetResult(id);
            if (result["result"].Equals("_error"))
            {
                Error(GetErrorMessage(result), ErrorType.Login);
                Disconnect();
                return false;
            }

            body = result.GetTO("data").GetTO("body");
            sessionToken = body.GetString("token");
            accountID = (int)body.GetTO("accountSummary").GetInt("accountId");

            // Login 2

            if (useGarena)
                body = WrapBody(Convert.ToBase64String(Encoding.UTF8.GetBytes(userID + ":" + sessionToken)), "auth", 8);
            else
                body = WrapBody(Convert.ToBase64String(Encoding.UTF8.GetBytes(user + ":" + sessionToken)), "auth", 8);

            body.type = "flex.messaging.messages.CommandMessage";

            id = Invoke(body);
            result = GetResult(id); // Read result (and discard)


            // Subscribe to the necessary items

            // bc
            body = WrapBody(new object[] { new TypedObject() }, "messagingDestination", 0);
            body.type = "flex.messaging.messages.CommandMessage";
            TypedObject headers = body.GetTO("headers");
            headers.Add("DSSubtopic", "bc");
            headers.Remove("DSRequestTimeout");
            body["clientID"] = "bc-" + accountID;
            id = Invoke(body);
            result = GetResult(id); // Read result and discard

            // cn
            body = WrapBody(new object[] { new TypedObject() }, "messagingDestination", 0);
            body.type = "flex.messaging.messages.CommandMessage";
            headers = body.GetTO("headers");
            headers.Add("DSSubtopic", "cn-" + accountID);
            headers.Remove("DSRequestTimeout");
            body["clientID"] = "cn-" + accountID;
            id = Invoke(body);
            result = GetResult(id); // Read result and discard

            // gn
            body = WrapBody(new object[] { new TypedObject() }, "messagingDestination", 0);
            body.type = "flex.messaging.messages.CommandMessage";
            headers = body.GetTO("headers");
            headers.Add("DSSubtopic", "gn-" + accountID);
            headers.Remove("DSRequestTimeout");
            body["clientID"] = "gn-" + accountID;
            id = Invoke(body);
            result = GetResult(id); // Read result and discard

            if (OnLogin != null)
                OnLogin(this, user, ipAddress);
            return true;
        }

        private string GetErrorMessage(TypedObject message)
        {
            // Works for clientVersion
            return message.GetTO("data").GetTO("rootCause").GetString("message");
        }


        private void StartHeartbeat()
        {
            heartbeatThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        long hbTime = (long)DateTime.Now.TimeOfDay.TotalMilliseconds;

                        int id = Invoke("loginService", "performLCDSHeartBeat", new object[] { accountID, sessionToken, heartbeatCount, DateTime.Now.ToString("ddd MMM d yyyy HH:mm:ss 'GMT-0700'") });
                        Cancel(id); // Ignore result for now

                        heartbeatCount++;

                        // Quick sleeps to shutdown the heartbeat quickly on a reconnect
                        while ((long)DateTime.Now.TimeOfDay.TotalMilliseconds - hbTime < 120000)
                            Thread.Sleep(100);
                    }
                    catch
                    {

                    }
                }
            });
            heartbeatThread.Start();
        }
        #endregion

        #region Disconnect Methods

        public void Disconnect()
        {
            Thread t = new Thread(() =>
            {
                if (isConnected)
                {
                    int id = Invoke("loginService", "logout", new object[] { authToken });
                    Join(id);
                }

                isConnected = false;

                if (heartbeatThread != null)
                    heartbeatThread.Abort();

                if (decodeThread != null)
                    decodeThread.Abort();

                invokeID = 2;
                heartbeatCount = 1;
                pendingInvokes.Clear();
                callbacks.Clear();
                results.Clear();

                client = null;
                sslStream = null;

                if (OnDisconnect != null)
                    OnDisconnect(this, EventArgs.Empty);
            });

            t.Start();
        }
        #endregion

        #region Error Methods

        private void Error(string message, ErrorType type)
        {
            Console.WriteLine(message);
            Error error = new Error()
            {
                Type = type,
                Message = message,
            };
            if (OnError != null)
                OnError(this, error);
        }
        #endregion

        #region Send Methods

        private int Invoke(TypedObject packet)
        {
            int id = NextInvokeID();
            pendingInvokes.Add(id);

            try
            {
                byte[] data = RTMPSEncoder.EncodeInvoke(id, packet);

                sslStream.Write(data, 0, data.Length);

                return id;
            }
            catch (IOException e)
            {
                // Clear the pending invoke
                pendingInvokes.Remove(id);

                // Rethrow
                throw e;
            }
        }

        private int Invoke(string destination, object operation, object body)
        {
            return Invoke(WrapBody(body, destination, operation));
        }

        private int InvokeWithCallback(string destination, object operation, object body, RiotGamesObject cb)
        {
            if (isConnected)
            {
                callbacks.Add(invokeID, cb); // Register the callback
                return Invoke(destination, operation, body);
            }
            else
            {
                Error("The client is not connected. Please make sure to connect before tring to execute an Invoke command.", ErrorType.Invoke);
                Disconnect();
                return -1;
            }
        }

        protected TypedObject WrapBody(object body, string destination, object operation)
        {
            TypedObject headers = new TypedObject();
            headers.Add("DSRequestTimeout", 60);
            headers.Add("DSId", DSId);
            headers.Add("DSEndpoint", "my-rtmps");

            TypedObject ret = new TypedObject("flex.messaging.messages.RemotingMessage");
            ret.Add("operation", operation);
            ret.Add("source", null);
            ret.Add("timestamp", 0);
            ret.Add("messageId", RTMPSEncoder.RandomUID());
            ret.Add("timeToLive", 0);
            ret.Add("clientId", null);
            ret.Add("destination", destination);
            ret.Add("body", body);
            ret.Add("headers", headers);

            return ret;
        }

        protected int NextInvokeID()
        {
            return invokeID++;
        }

        #endregion

        #region Receive Methods
        private void BeginReceive()
        {
            decodeThread = new Thread(() =>
            {
                try
                {
                    Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

                    while (true)
                    {
                        byte basicHeader = (byte)sslStream.ReadByte();
                        if ((int)basicHeader == -1)
                        {
                            Disconnect();
                        }

                        int channel = basicHeader & 0x2F;
                        int headerType = basicHeader & 0xC0;

                        int headerSize = 0;
                        if (headerType == 0x00)
                            headerSize = 12;
                        else if (headerType == 0x40)
                            headerSize = 8;
                        else if (headerType == 0x80)
                            headerSize = 4;
                        else if (headerType == 0xC0)
                            headerSize = 1;

                        // Retrieve the packet or make a new one
                        if (!packets.ContainsKey(channel))
                            packets.Add(channel, new Packet());
                        Packet p = packets[channel];

                        // Parse the full header
                        if (headerSize > 1)
                        {
                            byte[] header = new byte[headerSize - 1];
                            for (int i = 0; i < header.Length; i++)
                                header[i] = (byte)sslStream.ReadByte();

                            if (headerSize >= 8)
                            {
                                int size = 0;
                                for (int i = 3; i < 6; i++)
                                    size = size * 256 + (header[i] & 0xFF);
                                p.SetSize(size);

                                p.SetType(header[6]);
                            }
                        }

                        // Read rest of packet
                        for (int i = 0; i < 128; i++)
                        {
                            byte b = (byte)sslStream.ReadByte();
                            p.Add(b);

                            if (p.IsComplete())
                                break;
                        }

                        if (!p.IsComplete())
                            continue;

                        // Remove the read packet
                        packets.Remove(channel);


                        // Decode result
                        TypedObject result;
                        if (p.GetMessageType() == 0x14) // Connect
                            result = RTMPSDecoder.DecodeConnect(p.GetData());
                        else if (p.GetMessageType() == 0x11) // Invoke
                            result = RTMPSDecoder.DecodeInvoke(p.GetData());
                        else if (p.GetMessageType() == 0x06) // Set peer bandwidth
                        {
                            byte[] data = p.GetData();
                            int windowSize = 0;
                            for (int i = 0; i < 4; i++)
                                windowSize = windowSize * 256 + (data[i] & 0xFF);
                            int type = data[4];
                            continue;
                        }
                        else if (p.GetMessageType() == 0x03) // Ack
                        {
                            byte[] data = p.GetData();
                            int ackSize = 0;
                            for (int i = 0; i < 4; i++)
                                ackSize = ackSize * 256 + (data[i] & 0xFF);
                            continue;
                        }
                        else
                        // Skip most messages
                        {
                            continue;
                        }

                        // Store result

                        int? id = result.GetInt("invokeId");

                        //Check to see if the result is valid.
                        //If it isn't, give an error and remove the callback if there is one.
                        if (result["result"].Equals("_error"))
                        {
                            Error("Warning, invalid result (" + callbacks[(int)id].GetType() + ") : " + GetErrorMessage(result), ErrorType.Receive);

                            if (callbacks.ContainsKey((int)id))
                            {
                                RiotGamesObject cb = callbacks[(int)id];
                                //TODO: better way then current hotfix to send PlatformGame as a null value
                                if (cb.GetType().ToString() == "PVPNetConnect.RiotObjects.Game.PlatformGameLifecycle")
                                {
                                    if (cb != null)
                                    {
                                        cb.DoCallback(null);
                                    }
                                }
                                callbacks.Remove((int)id);
                            }
                        }


                        if (id == null || id == 0)
                        {
                        }
                        else if (callbacks.ContainsKey((int)id))
                        {
                            RiotGamesObject cb = callbacks[(int)id];
                            callbacks.Remove((int)id);
                            if (cb != null)
                            {
                                TypedObject messageBody = result.GetTO("data").GetTO("body");
                                Thread t = new Thread(() =>
                                {
                                    cb.DoCallback(messageBody);
                                });
                                t.Start();
                            }
                        }

                        else
                        {
                            results.Add((int)id, result);
                        }
                        pendingInvokes.Remove((int)id);

                    }

                }
                catch (Exception e)
                {
                    if (IsConnected())
                        Error(e.Message, ErrorType.Receive);

                    Disconnect();
                }
            });
            decodeThread.Start();
        }


        private TypedObject GetResult(int id)
        {
            while (IsConnected() && !results.ContainsKey(id))
            {
                Thread.Sleep(10);
            }

            if (!IsConnected())
                return null;

            TypedObject ret = results[id];
            results.Remove(id);
            return ret;
        }
        private TypedObject PeekResult(int id)
        {
            if (results.ContainsKey(id))
            {
                TypedObject ret = results[id];
                results.Remove(id);
                return ret;
            }
            return null;
        }

        private void Join()
        {
            while (pendingInvokes.Count > 0)
            {
                Thread.Sleep(10);
            }
        }

        private void Join(int id)
        {
            while (IsConnected() && pendingInvokes.Contains(id))
            {
                Thread.Sleep(10);
            }
        }
        private void Cancel(int id)
        {
            // Remove from pending invokes (only affects join())
            pendingInvokes.Remove(id);

            // Check if we've already received the result
            if (PeekResult(id) != null)
                return;
            // Signify a cancelled invoke by giving it a null callback
            else
            {
                callbacks.Add(id, null);

                // Check for race condition
                if (PeekResult(id) != null)
                    callbacks.Remove(id);
            }
        }

        #endregion

        #region Public Client Methods

        //PVPNet/User Information Methods
        public void GetLoginDataPacketForUser(LoginDataPacket.Callback callback)
        {
            LoginDataPacket cb = new LoginDataPacket(callback);
            InvokeWithCallback("clientFacadeService", "getLoginDataPacketForUser", new object[] { }, cb);
        }

        public void GetAllPublicSummonerDataByAccount(int accountID, AllPublicSummonerData.Callback callback)
        {
            AllPublicSummonerData cb = new AllPublicSummonerData(callback);
            InvokeWithCallback("summonerService", "getAllPublicSummonerDataByAccount", new object[] { accountID }, cb);
        }

        public void GetAllSummonerDataByAccount(int accountID, AllSummonerData.Callback callback)
        {
            AllSummonerData cb = new AllSummonerData(callback);
            InvokeWithCallback("summonerService", "getAllSummonerDataByAccount", new object[] { accountID }, cb);
        }

        public void GetSummonerByName(string summonerName, PublicSummoner.Callback callback)
        {
            PublicSummoner cb = new PublicSummoner(callback);
            InvokeWithCallback("summonerService", "getSummonerByName", new object[] { summonerName }, cb);
        }

        public void GetSummonerNames(object[] summonerIDs, UnclassedObject.Callback callback)
        {
            UnclassedObject cb = new UnclassedObject(callback);
            InvokeWithCallback("summonerService", "getSummonerNames", new object[] { summonerIDs }, cb);
        }

        public void GetRecentGames(int accountID, RecentGames.Callback callback)
        {
            RecentGames cb = new RecentGames(callback);
            InvokeWithCallback("playerStatsService", "getRecentGames", new object[] { accountID }, cb);
        }

        public void RetrievePlayerStatsByAccountId(int accountID, StringEnum.Seasons season, PlayerLifetimeStats.Callback callback)
        {
            PlayerLifetimeStats cb = new PlayerLifetimeStats(callback);
            InvokeWithCallback("playerStatsService", "retrievePlayerStatsByAccountId", new object[] { accountID, StringEnum.GetStringValue(season) }, cb);
        }

        public void GetAggregatedStats(int accountID, StringEnum.GameModes gameMode, StringEnum.Seasons season, AggregatedStats.Callback callback)
        {
            AggregatedStats cb = new AggregatedStats(callback);
            InvokeWithCallback("playerStatsService", "getAggregatedStats", new object[] { accountID, StringEnum.GetStringValue(gameMode), StringEnum.GetStringValue(season) }, cb);
        }

        public void RetrieveInProgressSpectatorGameInfo(string summonerName, PlatformGameLifecycle.Callback callback)
        {
            PlatformGameLifecycle cb = new PlatformGameLifecycle(callback);
            InvokeWithCallback("gameService", "retrieveInProgressSpectatorGameInfo", new object[] { summonerName }, cb);
        }

        public void GetMasteryBook(int summonerID, MasteryBook.Callback callback)
        {
            MasteryBook cb = new MasteryBook(callback);
            InvokeWithCallback("masteryBookService", "getMasteryBook", new object[] { summonerID }, cb);
        }

        public void GetSpellBook(int summonerID, SpellBook.Callback callback)
        {
            SpellBook cb = new SpellBook(callback);
            InvokeWithCallback("spellBookService", "getSpellBook", new object[] { summonerID }, cb);
        }

        // TODO: Not working because return type is only an object array
        public void RetrieveTopPlayedChampions(int accountID, StringEnum.GameModes gameMode, TopPlayedChampions.Callback callback)
        {
            TopPlayedChampions cb = new TopPlayedChampions(callback);
            InvokeWithCallback("playerStatsService", "retrieveTopPlayedChampions", new object[] { accountID, StringEnum.GetStringValue(gameMode) }, cb);
        }

        //Chat Information Methods
        public void GetSummonerChatIdByName(string summonerName, UnclassedObject.Callback callback)
        {
            UnclassedObject cb = new UnclassedObject(callback);
            InvokeWithCallback("summonerService", "getSummonerInternalNameByName", new object[] { summonerName }, cb);
        }

        public void GetSummonerSummaryByChatId(string summonerChatId, UnclassedObject.Callback callback)
        {
            UnclassedObject cb = new UnclassedObject(callback);
            InvokeWithCallback("statisticsService", "getSummonerSummaryByInternalName", new object[] { summonerChatId }, cb);
        }

        #endregion

        #region General Returns
        public bool IsConnected()
        {
            return isConnected;
        }
        #endregion
    }
}
