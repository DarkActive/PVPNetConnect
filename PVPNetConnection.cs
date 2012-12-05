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
using PVPNetConnect.Callbacks;

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
                     Error(GetErrorMessage(result), ErrorType.Receive);

                     if (callbacks.ContainsKey((int)id))
                        callbacks.Remove((int)id);
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

      public void GetSumonerActiveBoosts(RiotGamesObject callback)
      {
         InvokeWithCallback("inventoryService", "getSumonerActiveBoosts", new object[] { }, callback);
      }

      public void GetAvailableChampions(RiotGamesObject callback)
      {
         InvokeWithCallback("inventoryService", "getAvailableChampions", new object[] { }, callback);
      }

      public void GetAvailableQueues(RiotGamesObject callback)
      {
         InvokeWithCallback("matchmakerService", "getAvailableQueues", new object[] { }, callback);
      }

      public void RetreivePlayerStatsByAccountId(int summonerID, string season, RiotGamesObject callback)
      {
         InvokeWithCallback("playerStatsService", "retreivePlayerStatsByAccountId", new object[] { summonerID, season }, callback);
      }

      public void RetrieveTopPlayedChampions(int summonerID, string queueType, RiotGamesObject callback)
      {
         InvokeWithCallback("playerStatsService", "retrieveTopPlayedChampions", new object[] { summonerID, queueType }, callback);
      }

      public void GetRecentGames(int summonerID, RiotGamesObject callback)
      {
         InvokeWithCallback("playerStatsService", "getRecentGames", new object[] { summonerID }, callback);
      }

      public void GetSummonerRuneInventory(int summonerID, RiotGamesObject callback)
      {
         InvokeWithCallback("summonerRuneService", "getSummonerRuneInventory", new object[] { summonerID }, callback);
      }

      public void GetAllPublicSummonerDataByAccount(int summonerID, RiotGamesObject callback)
      {
         InvokeWithCallback("summonerService", "getAllPublicSummonerDataByAccount", new object[] { summonerID }, callback);
      }

      public void GetAllSummonerDataByAccount(int summonerID, RiotGamesObject callback)
      {
         InvokeWithCallback("summonerService", "getAllSummonerDataByAccount", new object[] { summonerID }, callback);
      }

      public void GetSummonerByName(string summonerName, RiotGamesObject callback)
      {
         InvokeWithCallback("summonerService", "getSummonerByName", new object[] { summonerName }, callback);
      }

      public void GetSummonerNames(object[] summonerIDs, RiotGamesObject callback)
      {
         InvokeWithCallback("summonerService", "getSummonerNames", new object[] { summonerIDs }, callback);
      }

      //Chat Information Methods
      public void GetSummonerChatIdByName(string summonerName, RiotGamesObject callback)
      {
         InvokeWithCallback("summonerService", "getSummonerInternalNameByName", new object[] { summonerName }, callback);
      }

      public void GetSummonerSummaryByChatId(string summonerChatId, RiotGamesObject callback)
      {
         InvokeWithCallback("statisticsService", "getSummonerSummaryByInternalName", new object[] { summonerChatId }, callback);
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
