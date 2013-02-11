/*
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
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
namespace PVPNetConnect
{
    /// <summary>
    /// RTMPS decoder class that decodes RTMPS messages
    /// </summary>
   public class RTMPSDecoder
   {
      // Stores the data to be consumed while decoding
       /// <summary>
       /// The data buffer
       /// </summary>
      [ThreadStatic]
      private static byte[] dataBuffer;
      /// <summary>
      /// The data pos
      /// </summary>
      [ThreadStatic]
      private static int dataPos;

      // Lists of references and class definitions seen so far
      /// <summary>
      /// The string references
      /// </summary>
      [ThreadStatic]
      private static List<string> stringReferences;
      /// <summary>
      /// The object references
      /// </summary>
      [ThreadStatic]
      private static List<object> objectReferences;
      /// <summary>
      /// The class definitions
      /// </summary>
      [ThreadStatic]
      private static List<ClassDefinition> classDefinitions;


      /// <summary>
      /// Resets this instance.
      /// </summary>
      private static void Reset()
      {
         stringReferences = new List<string>();
         objectReferences = new List<object>();
         classDefinitions = new List<ClassDefinition>();
      }

      /// <summary>
      /// Decodes the connect.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      /// <exception cref="System.Exception">
      /// There is other data in the buffer!
      /// or
      /// Did not consume entire buffer:  + dataPos +  of  + dataBuffer.Length
      /// </exception>
      public static TypedObject DecodeConnect(byte[] data)
      {
         Reset();

         dataBuffer = data;
         dataPos = 0;

         TypedObject result = new TypedObject("Invoke");
         result.Add("result", DecodeAMF0());
         result.Add("invokeId", DecodeAMF0());
         result.Add("serviceCall", DecodeAMF0());
         result.Add("data", DecodeAMF0());
         if (dataPos != dataBuffer.Length)
         {
            for (int i = dataPos; i < data.Length; i++)
            {
               if (ReadByte() != '\0')
                  throw new Exception("There is other data in the buffer!");
            }
         }
         if (dataPos != dataBuffer.Length)
            throw new Exception("Did not consume entire buffer: " + dataPos + " of " + dataBuffer.Length);

         return result;
      }

      /// <summary>
      /// Decodes the invoke.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      /// <exception cref="System.Exception">Did not consume entire buffer:  + dataPos +  of  + dataBuffer.Length</exception>
      public static TypedObject DecodeInvoke(byte[] data)
      {
         Reset();

         dataBuffer = data;
         dataPos = 0;

         TypedObject result = new TypedObject("Invoke");
         if (dataBuffer[0] == 0x00)
         {
            dataPos++;
            result.Add("version", 0x00);
         }
         result.Add("result", DecodeAMF0());
         result.Add("invokeId", DecodeAMF0());
         result.Add("serviceCall", DecodeAMF0());
         result.Add("data", DecodeAMF0());

         
         if (dataPos != dataBuffer.Length)
            throw new Exception("Did not consume entire buffer: " + dataPos + " of " + dataBuffer.Length);

         return result;
      }

      /// <summary>
      /// Decodes the specified data.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      /// <exception cref="System.Exception">Did not consume entire buffer:  + dataPos +  of  + dataBuffer.Length</exception>
      public static object Decode(byte[] data)
      {
         dataBuffer = data;
         dataPos = 0;

         object result = Decode();

         if (dataPos != dataBuffer.Length)
            throw new Exception("Did not consume entire buffer: " + dataPos + " of " + dataBuffer.Length);

         return result;
      }

      /// <summary>
      /// Decodes this instance.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.Exception">
      /// Undefined data type
      /// or
      /// Unexpected AMF3 data type:  + type
      /// </exception>
      private static object Decode()
      {
         byte type = ReadByte();
         switch (type)
         {
            case 0x00:
               throw new Exception("Undefined data type");

            case 0x01:
               return null;

            case 0x02:
               return false;

            case 0x03:
               return true;

            case 0x04:
               return ReadInt();

            case 0x05:
               return ReadDouble();

            case 0x06:
               return ReadString();

            case 0x07:
               return ReadXML();

            case 0x08:
               return ReadDate();

            case 0x09:
               return ReadArray();

            case 0x0A:
               return ReadObject();

            case 0x0B:
               return ReadXMLString();

            case 0x0C:
               return ReadByteArray();
         }

         throw new Exception("Unexpected AMF3 data type: " + type);
      }

      /// <summary>
      /// Reads the byte.
      /// </summary>
      /// <returns></returns>
      private static byte ReadByte()
      {
         byte ret = dataBuffer[dataPos];
         dataPos++;
         return ret;
      }

      /// <summary>
      /// Reads the byte as int.
      /// </summary>
      /// <returns></returns>
      private static int ReadByteAsInt()
      {
         int ret = ReadByte();
         if (ret < 0)
            ret += 256;
         return ret;
      }

      /// <summary>
      /// Reads the bytes.
      /// </summary>
      /// <param name="length">The length.</param>
      /// <returns></returns>
      private static byte[] ReadBytes(int length)
      {
         byte[] ret = new byte[length];
         for (int i = 0; i < length; i++)
         {
            ret[i] = dataBuffer[dataPos];
            dataPos++;
         }
         return ret;
      }

      /// <summary>
      /// Reads the int.
      /// </summary>
      /// <returns></returns>
      private static int ReadInt()
      {
         int ret = ReadByteAsInt();
         int tmp;

         if (ret < 128)
         {
            return ret;
         }
         else
         {
            ret = (ret & 0x7f) << 7;
            tmp = ReadByteAsInt();
            if (tmp < 128)
            {
               ret = ret | tmp;
            }
            else
            {
               ret = (ret | tmp & 0x7f) << 7;
               tmp = ReadByteAsInt();
               if (tmp < 128)
               {
                  ret = ret | tmp;
               }
               else
               {
                  ret = (ret | tmp & 0x7f) << 8;
                  tmp = ReadByteAsInt();
                  ret = ret | tmp;
               }
            }
         }

         // Sign extend
         int mask = 1 << 28;
         int r = -(ret & mask) | ret;
         return r;
      }

      /// <summary>
      /// Reads the double.
      /// </summary>
      /// <returns></returns>
      private static double ReadDouble()
      {
         long value = 0;
         for (int i = 0; i < 8; i++)
            value = (value << 8) + ReadByteAsInt();

         return BitConverter.Int64BitsToDouble(value);
      }

      /// <summary>
      /// Reads the string.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.Exception">Error parsing AMF3 string from  + data + '\n' + e.Message</exception>
      private static string ReadString()
      {
         int handle = ReadInt();
         bool inline = ((handle & 1) != 0);
         handle = handle >> 1;

         if (inline)
         {
            if (handle == 0)
               return "";

            byte[] data = ReadBytes(handle);

            string str;
            try
            {
               System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
               str = enc.GetString(data);
            }
            catch (Exception e)
            {
               throw new Exception("Error parsing AMF3 string from " + data + '\n' + e.Message);
            }

            stringReferences.Add(str);

            return str;
         }
         else
         {
            return stringReferences[handle];
         }
      }

      /// <summary>
      /// Reads the XML.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">Reading of XML is not implemented</exception>
      private static string ReadXML()
      {
         throw new NotImplementedException("Reading of XML is not implemented");
      }

      /// <summary>
      /// Reads the date.
      /// </summary>
      /// <returns></returns>
      private static DateTime ReadDate()
      {
         int handle = ReadInt();
         bool inline = ((handle & 1) != 0);
         handle = handle >> 1;

         if (inline)
         {
            long ms = (long)ReadDouble();
            DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            d = d.AddSeconds(ms / 1000);

            objectReferences.Add(d);

            return d;
         }
         else
         {
             return DateTime.MinValue;
         }
      }

      /// <summary>
      /// Reads the array.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">Associative arrays are not supported</exception>
      private static object[] ReadArray()
      {
         int handle = ReadInt();
         bool inline = ((handle & 1) != 0);
         handle = handle >> 1;

         if (inline)
         {
            string key = ReadString();
            if (key != null && !key.Equals(""))
               throw new NotImplementedException("Associative arrays are not supported");

            object[] ret = new object[handle];
            objectReferences.Add(ret);

            for (int i = 0; i < handle; i++)
               ret[i] = Decode();

            return ret;
         }
         else
         {
            return (object[])objectReferences[handle];
         }
      }

      /// <summary>
      /// Reads the list.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">Associative arrays are not supported</exception>
      private static List<object> ReadList()
      {
          int handle = ReadInt();
          bool inline = ((handle & 1) != 0);
          handle = handle >> 1;

          if (inline)
          {
              string key = ReadString();
              if (key != null && !key.Equals(""))
                  throw new NotImplementedException("Associative arrays are not supported");

              List<object> ret = new List<object>();
              objectReferences.Add(ret);

              for (int i = 0; i < handle; i++)
                  ret.Add(Decode());

              return ret;
          }
          else
          {
              return (List<object>)objectReferences[handle];
          }
      }

      /// <summary>
      /// Reads the object.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">Externalizable not handled for  + cd.type</exception>
      private static object ReadObject()
      {
         int handle = ReadInt();
         bool inline = ((handle & 1) != 0);
         handle = handle >> 1;

         if (inline)
         {
            bool inlineDefine = ((handle & 1) != 0);
            handle = handle >> 1;

            ClassDefinition cd;
            if (inlineDefine)
            {
               cd = new ClassDefinition();
               cd.type = ReadString();

               cd.externalizable = ((handle & 1) != 0);
               handle = handle >> 1;
               cd.dynamic = ((handle & 1) != 0);
               handle = handle >> 1;

               for (int i = 0; i < handle; i++)
                  cd.members.Add(ReadString());

               classDefinitions.Add(cd);
            }
            else
            {
               cd = classDefinitions[handle];
            }

            TypedObject ret = new TypedObject(cd.type);

            // Need to add reference here due to circular references
            objectReferences.Add(ret);

            if (cd.externalizable)
            {
               if (cd.type.Equals("DSK"))
                  ret = ReadDSK();
               else if (cd.type.Equals("DSA"))
                  ret = ReadDSA();
               else if (cd.type.Equals("flex.messaging.io.ArrayCollection"))
               {
                  object obj = Decode();
                  ret = TypedObject.MakeArrayCollection((object[])obj);
               }
               else if (cd.type.Equals("com.riotgames.platform.systemstate.ClientSystemStatesNotification") || cd.type.Equals("com.riotgames.platform.broadcast.BroadcastNotification"))
               {
                  int size = 0;
                  for (int i = 0; i < 4; i++)
                     size = size * 256 + ReadByteAsInt();

                  byte[] data = ReadBytes(size);
                  StringBuilder sb = new StringBuilder();
                  for (int i = 0; i < data.Length; i++)
                     sb.Append(Convert.ToChar(data[i]));

                  JavaScriptSerializer serializer = new JavaScriptSerializer();
                  ret = serializer.Deserialize<TypedObject>(sb.ToString());
                  ret.type = cd.type;
               }
               else
               {
                  //for (int i = dataPos; i < dataBuffer.length; i++)
                  //System.out.print(String.format("%02X", dataBuffer[i]));
                  //System.out.println();
                  throw new NotImplementedException("Externalizable not handled for " + cd.type);
               }
            }
            else
            {
               for (int i = 0; i < cd.members.Count; i++)
               {
                  String key = cd.members[i];
                  object value = Decode();
                  ret.Add(key, value);
               }

               if (cd.dynamic)
               {
                  String key;
                  while ((key = ReadString()).Length != 0)
                  {
                     object value = Decode();
                     ret.Add(key, value);
                  }
               }
            }

            return ret;
         }
         else
         {
            return objectReferences[handle];
         }
      }

      /// <summary>
      /// Reads the XML string.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">Reading of XML strings is not implemented</exception>
      private static string ReadXMLString()
      {
         throw new NotImplementedException("Reading of XML strings is not implemented");
      }

      /// <summary>
      /// Reads the byte array.
      /// </summary>
      /// <returns></returns>
      private static byte[] ReadByteArray()
      {
         int handle = ReadInt();
         bool inline = ((handle & 1) != 0);
         handle = handle >> 1;

         if (inline)
         {
            byte[] ret = ReadBytes(handle);
            objectReferences.Add(ret);
            return ret;
         }
         else
         {
            return (byte[])objectReferences[handle];
         }
      }

      /// <summary>
      /// Reads the DSA.
      /// </summary>
      /// <returns></returns>
      private static TypedObject ReadDSA()
      {
         TypedObject ret = new TypedObject("DSA");

         int flag;
         List<int> flags = ReadFlags();
         for (int i = 0; i < flags.Count; i++)
         {
            flag = flags[i];
            int bits = 0;
            if (i == 0)
            {
               if ((flag & 0x01) != 0)
                  ret.Add("body", Decode());
               if ((flag & 0x02) != 0)
                  ret.Add("clientId", Decode());
               if ((flag & 0x04) != 0)
                  ret.Add("destination", Decode());
               if ((flag & 0x08) != 0)
                  ret.Add("headers", Decode());
               if ((flag & 0x10) != 0)
                  ret.Add("messageId", Decode());
               if ((flag & 0x20) != 0)
                  ret.Add("timeStamp", Decode());
               if ((flag & 0x40) != 0)
                  ret.Add("timeToLive", Decode());
               bits = 7;
            }
            else if (i == 1)
            {
               if ((flag & 0x01) != 0)
               {
                  ReadByte();
                  byte[] temp = ReadByteArray();
                  ret.Add("clientIdBytes", temp);
                  ret.Add("clientId", ByteArrayToID(temp));
               }
               if ((flag & 0x02) != 0)
               {
                  ReadByte();
                  byte[] temp = ReadByteArray();
                  ret.Add("messageIdBytes", temp);
                  ret.Add("messageId", ByteArrayToID(temp));
               }
               bits = 2;
            }

            ReadRemaining(flag, bits);
         }

         flags = ReadFlags();
         for (int i = 0; i < flags.Count; i++)
         {
            flag = flags[i];
            int bits = 0;

            if (i == 0)
            {
               if ((flag & 0x01) != 0)
                  ret.Add("correlationId", Decode());
               if ((flag & 0x02) != 0)
               {
                  ReadByte();
                  byte[] temp = ReadByteArray();
                  ret.Add("correlationIdBytes", temp);
                  ret.Add("correlationId", ByteArrayToID(temp));
               }
               bits = 2;
            }

            ReadRemaining(flag, bits);
         }

         return ret;
      }

      /// <summary>
      /// Reads the DSK.
      /// </summary>
      /// <returns></returns>
      private static TypedObject ReadDSK()
      {
         // DSK is just a DSA + extra set of flags/objects
         TypedObject ret = ReadDSA();
         ret.type = "DSK";

         List<int> flags = ReadFlags();
         for (int i = 0; i < flags.Count; i++)
            ReadRemaining(flags[i], 0);

         return ret;
      }

      /// <summary>
      /// Reads the flags.
      /// </summary>
      /// <returns></returns>
      private static List<int> ReadFlags()
      {
         List<int> flags = new List<int>();
         int flag;
         do
         {
            flag = ReadByteAsInt();
            flags.Add(flag);
         } while ((flag & 0x80) != 0);

         return flags;
      }

      /// <summary>
      /// Reads the remaining.
      /// </summary>
      /// <param name="flag">The flag.</param>
      /// <param name="bits">The bits.</param>
      private static void ReadRemaining(int flag, int bits)
      {
         // For forwards compatibility, read in any other flagged objects to
         // preserve the integrity of the input stream...
         if ((flag >> bits) != 0)
         {
            for (int o = bits; o < 6; o++)
            {
               if (((flag >> o) & 1) != 0)
                  Decode();
            }
         }
      }

      /// <summary>
      /// Bytes the array to ID.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      private static string ByteArrayToID(byte[] data)
      {
         StringBuilder ret = new StringBuilder();
         for (int i = 0; i < data.Length; i++)
         {
            if (i == 4 || i == 6 || i == 8 || i == 10)
               ret.Append('-');
            ret.AppendFormat("{0:X2}", data[i]);
         }

         return ret.ToString();
      }

      /// <summary>
      /// Decodes the AM f0.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">AMF0 type not supported:  + type</exception>
      private static object DecodeAMF0()
      {
         int type = ReadByte();
         switch (type)
         {
            case 0x00:
               return ReadIntAMF0();

            case 0x02:
               return ReadStringAMF0();

            case 0x03:
               return ReadObjectAMF0();

            case 0x05:
               return null;

            case 0x11: // AMF3
               return Decode();
         }

         throw new NotImplementedException("AMF0 type not supported: " + type);
      }

      /// <summary>
      /// Reads the string AM f0.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.Exception">Error parsing AMF0 string from  + data + '\n' + e.Message</exception>
      private static string ReadStringAMF0()
      {
         int length = (ReadByteAsInt() << 8) + ReadByteAsInt();
         if (length == 0)
            return "";

         byte[] data = ReadBytes(length);

         // UTF-8 applicable?
         string str;
         try
         {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            str = enc.GetString(data);
         }
         catch (Exception e)
         {
            throw new Exception("Error parsing AMF0 string from " + data + '\n' + e.Message);
         }

         return str;
      }

      /// <summary>
      /// Reads the int AM f0.
      /// </summary>
      /// <returns></returns>
      private static int ReadIntAMF0()
      {
         return (int)ReadDouble();
      }

      /// <summary>
      /// Reads the object AM f0.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException">AMF0 type not supported:  + b</exception>
      private static TypedObject ReadObjectAMF0()
      {
         TypedObject body = new TypedObject("Body");
         string key;
         while (!(key = ReadStringAMF0()).Equals(""))
         {
            byte b = ReadByte();
            if (b == 0x00)
               body.Add(key, ReadDouble());
            else if (b == 0x02)
               body.Add(key, ReadStringAMF0());
            else if (b == 0x05)
               body.Add(key, null);
            else
               throw new NotImplementedException("AMF0 type not supported: " + b);
         }
         ReadByte(); // Skip object end marker

         return body;
      }
   }
}
