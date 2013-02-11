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
using System.IO;
using System.Linq;
using System.Text;

namespace PVPNetConnect
{
    /// <summary>
    /// The RTMPSEncoder class that encodes RTMPS packets
    /// </summary>
   public static class RTMPSEncoder
   {

       /// <summary>
       /// The start time
       /// </summary>
      [ThreadStatic]
      public static long startTime = (long)DateTime.Now.TimeOfDay.TotalMilliseconds;

      /// <summary>
      /// Adds the headers.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      public static byte[] AddHeaders(byte[] data)
      {
         List<byte> result = new List<byte>();

         // Header byte
         result.Add((byte)0x03);

         // Timestamp
         long timediff = ((long)DateTime.Now.TimeOfDay.TotalMilliseconds - startTime);
         result.Add((byte)((timediff & 0xFF0000) >> 16));
         result.Add((byte)((timediff & 0x00FF00) >> 8));
         result.Add((byte)(timediff & 0x0000FF));

         // Body size
         result.Add((byte)((data.Length & 0xFF0000) >> 16));
         result.Add((byte)((data.Length & 0x00FF00) >> 8));
         result.Add((byte)(data.Length & 0x0000FF));

         // Content type
         result.Add((byte)0x11);

         // Source ID
         result.Add((byte)0x00);
         result.Add((byte)0x00);
         result.Add((byte)0x00);
         result.Add((byte)0x00);

         // Add body
         for (int i = 0; i < data.Length; i++)
         {
            result.Add(data[i]);
            if (i % 128 == 127 && i != data.Length - 1)
               result.Add((byte)0xC3);
         }

         byte[] ret = new byte[result.Count];
         for (int i = 0; i < ret.Length; i++)
            ret[i] = result[i];

         return ret;
      }

      /// <summary>
      /// Encodes the connect.
      /// </summary>
      /// <param name="paramaters">The paramaters.</param>
      /// <returns></returns>
      public static byte[] EncodeConnect(Dictionary<string, object> paramaters)
      {
         List<Byte> result = new List<Byte>();

         WriteStringAMF0(result, "connect");
         WriteIntAMF0(result, 1); // invokeId

         // Write params
         result.Add((byte)0x11); // AMF3 object
         result.Add((byte)0x09); // Array
         WriteAssociativeArray(result, paramaters);

         // Write service call args
         result.Add((byte)0x01);
         result.Add((byte)0x00); // false
         WriteStringAMF0(result, "nil"); // "nil"
         WriteStringAMF0(result, ""); // ""

         // Set up CommandMessage
         TypedObject cm = new TypedObject("flex.messaging.messages.CommandMessage");
         cm.Add("operation", 5);
         cm.Add("correlationId", "");
         cm.Add("timestamp", 0);
         cm.Add("messageId", RandomUID());
         cm.Add("body", new TypedObject(null));
         cm.Add("destination", "");
         Dictionary<string, object> headers = new Dictionary<string, object>();
         headers.Add("DSMessagingVersion", 1.0);
         headers.Add("DSId", "my-rtmps");
         cm.Add("headers", headers);
         cm.Add("clientId", null);
         cm.Add("timeToLive", 0);

         // Write CommandMessage
         result.Add((byte)0x11); // AMF3 object
         Encode(result, cm);

         byte[] ret = new byte[result.Count];
         for (int i = 0; i < ret.Length; i++)
            ret[i] = result[i];

         ret = AddHeaders(ret);
         ret[7] = (byte)0x14; // Change message type

         return ret;
      }

      /// <summary>
      /// Encodes the invoke.
      /// </summary>
      /// <param name="id">The id.</param>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      public static byte[] EncodeInvoke(int id, object data)
      {
         List<Byte> result = new List<Byte>();

         result.Add((byte)0x00); // version
         result.Add((byte)0x05); // type?
         WriteIntAMF0(result, id); // invoke ID
         result.Add((byte)0x05); // ???

         result.Add((byte)0x11); // AMF3 object
         Encode(result, data);

         byte[] ret = new byte[result.Count];
         for (int i = 0; i < ret.Length; i++)
            ret[i] = result[i];

         ret = AddHeaders(ret);

         return ret;
      }

      /// <summary>
      /// Encodes the specified obj.
      /// </summary>
      /// <param name="obj">The obj.</param>
      /// <returns></returns>
      public static byte[] Encode(object obj)
      {
         List<byte> result = new List<byte>();
         Encode(result, obj);

         byte[] ret = new byte[result.Count];
         for (int i = 0; i < ret.Length; i++)
            ret[i] = result[i];

         return ret;
      }

      /// <summary>
      /// Encodes the specified ret.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="obj">The obj.</param>
      /// <exception cref="System.Exception">Unexpected object type:  + obj.GetType().FullName</exception>
      public static void Encode(List<byte> ret, object obj)
      {
         if (obj == null)
         {
            ret.Add((byte)0x01);
         }
         else if (obj is bool)
         {
            bool val = (bool)obj;
            if (val)
               ret.Add((byte)0x03);
            else
               ret.Add((byte)0x02);
         }
         else if (obj is int)
         {
            ret.Add((byte)0x04);
            WriteInt(ret, (int)obj);
         }
         else if (obj is double)
         {
            ret.Add((byte)0x05);
            WriteDouble(ret, (double)obj);
         }
         // added for long objects to be added as double, not sure will work
         else if (obj is long)
         {
             ret.Add((byte)0x05);
             WriteDouble(ret, (double)obj);
         }
         else if (obj is string)
         {
            ret.Add((byte)0x06);
            WriteString(ret, (string)obj);
         }
         else if (obj is DateTime)
         {
            ret.Add((byte)0x08);
            WriteDate(ret, (DateTime)obj);
         }
         // Must precede Object[] check
         else if (obj is byte[])
         {
            ret.Add((byte)0x0C);
            WriteByteArray(ret, (byte[])obj);
         }
         else if (obj is object[])
         {
            ret.Add((byte)0x09);
            WriteArray(ret, (object[])obj);
         }
         // Must precede Map check
         else if (obj is TypedObject)
         {
            ret.Add((byte)0x0A);
            WriteObject(ret, (TypedObject)obj);
         }
         else if (obj is Dictionary<string, object>)
         {
            ret.Add((byte)0x09);
            WriteAssociativeArray(ret, (Dictionary<string, object>)obj);
         }
         else
         {
            throw new Exception("Unexpected object type: " + obj.GetType().FullName);
         }
      }

      /// <summary>
      /// Writes the int.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteInt(List<Byte> ret, int val)
      {
         if (val < 0 || val >= 0x200000)
         {
            ret.Add((byte)(((val >> 22) & 0x7f) | 0x80));
            ret.Add((byte)(((val >> 15) & 0x7f) | 0x80));
            ret.Add((byte)(((val >> 8) & 0x7f) | 0x80));
            ret.Add((byte)(val & 0xff));
         }
         else
         {
            if (val >= 0x4000)
            {
               ret.Add((byte)(((val >> 14) & 0x7f) | 0x80));
            }
            if (val >= 0x80)
            {
               ret.Add((byte)(((val >> 7) & 0x7f) | 0x80));
            }
            ret.Add((byte)(val & 0x7f));
         }
      }

      /// <summary>
      /// Writes the double.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteDouble(List<byte> ret, double val)
      {
         if (Double.IsNaN(val))
         {
            ret.Add((byte)0x7F);
            ret.Add((byte)0xFF);
            ret.Add((byte)0xFF);
            ret.Add((byte)0xFF);
            ret.Add((byte)0xE0);
            ret.Add((byte)0x00);
            ret.Add((byte)0x00);
            ret.Add((byte)0x00);
         }
         else
         {
            byte[] temp = BitConverter.GetBytes((double)val);

            for (int i = temp.Length - 1; i >= 0; i--)
               ret.Add(temp[i]);
         }
      }

      /// <summary>
      /// Writes the string.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      /// <exception cref="System.Exception">Unable to encode string as UTF-8:  + val + '\n' + e.Message</exception>
      private static void WriteString(List<byte> ret, string val)
      {
         byte[] temp = null;
         try
         {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            temp = encoding.GetBytes(val);
         }
         catch (Exception e)
         {
            throw new Exception("Unable to encode string as UTF-8: " + val + '\n' + e.Message);
         }

         WriteInt(ret, (temp.Length << 1) | 1);

         foreach (byte b in temp)
            ret.Add(b);
      }

      /// <summary>
      /// Writes the date.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteDate(List<Byte> ret, DateTime val)
      {
         ret.Add((byte)0x01);
         WriteDouble(ret, (double)val.TimeOfDay.TotalMilliseconds);
      }

      /// <summary>
      /// Writes the array.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteArray(List<byte> ret, object[] val)
      {
         WriteInt(ret, (val.Length << 1) | 1);
         ret.Add((byte)0x01);
         foreach (object obj in val)
            Encode(ret, obj);
      }

      /// <summary>
      /// Writes the associative array.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteAssociativeArray(List<Byte> ret, Dictionary<string, object> val)
      {
         ret.Add((byte)0x01);
         foreach (string key in val.Keys)
         {
            WriteString(ret, key);
            Encode(ret, val[key]);
         }
         ret.Add((byte)0x01);
      }

      /// <summary>
      /// Writes the object.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteObject(List<byte> ret, TypedObject val)
      {
         if (val.type == null || val.type.Equals(""))
         {
            ret.Add((byte)0x0B); // Dynamic class

            ret.Add((byte)0x01); // No class name
            foreach (string key in val.Keys)
            {
               WriteString(ret, key);
               Encode(ret, val[key]);
            }
            ret.Add((byte)0x01); // End of dynamic
         }
         else if (val.type.Equals("flex.messaging.io.ArrayCollection"))
         {
            ret.Add((byte)0x07); // Externalizable
            WriteString(ret, val.type);

            Encode(ret, val["array"]);
         }
         else
         {
            WriteInt(ret, (val.Count << 4) | 3); // Inline + member count
            WriteString(ret, val.type);

            List<String> keyOrder = new List<String>();
            foreach (string key in val.Keys)
            {
               WriteString(ret, key);
               keyOrder.Add(key);
            }

            foreach (string key in keyOrder)
               Encode(ret, val[key]);
         }
      }

      /// <summary>
      /// Writes the byte array.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      /// <exception cref="System.NotImplementedException">Encoding byte arrays is not implemented</exception>
      private static void WriteByteArray(List<byte> ret, byte[] val)
      {
         throw new NotImplementedException("Encoding byte arrays is not implemented");
      }

      /// <summary>
      /// Writes the int AM f0.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      private static void WriteIntAMF0(List<byte> ret, int val)
      {
         ret.Add((byte)0x00);

         byte[] temp = BitConverter.GetBytes((double)val);

         for (int i = temp.Length - 1; i >= 0; i--)
            ret.Add(temp[i]);
         //foreach (byte b in temp)
            //ret.Add(b);
      }

      /// <summary>
      /// Writes the string AM f0.
      /// </summary>
      /// <param name="ret">The ret.</param>
      /// <param name="val">The val.</param>
      /// <exception cref="System.Exception">Unable to encode string as UTF-8:  + val + '\n' + e.Message</exception>
      private static void WriteStringAMF0(List<byte> ret, string val)
      {
         byte[] temp = null;
         try
         {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            temp = encoding.GetBytes(val);
         }
         catch (Exception e)
         {
            throw new Exception("Unable to encode string as UTF-8: " + val + '\n' + e.Message);
         }

         ret.Add((byte)0x02);

         ret.Add((byte)((temp.Length & 0xFF00) >> 8));
         ret.Add((byte)(temp.Length & 0x00FF));

         foreach (byte b in temp)
            ret.Add(b);
      }

      /// <summary>
      /// Randoms the UID.
      /// </summary>
      /// <returns></returns>
      public static string RandomUID()
      {
         Random rand = new Random();

         byte[] bytes = new byte[16];
         rand.NextBytes(bytes);

         StringBuilder ret = new StringBuilder();
         for (int i = 0; i < bytes.Length; i++)
         {
            if (i == 4 || i == 6 || i == 8 || i == 10)
               ret.Append('-');
            ret.AppendFormat("{0:X2}", bytes[i]);
         }

         return ret.ToString();
      }

   }
}
