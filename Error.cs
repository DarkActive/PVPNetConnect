using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect
{
    /// <summary>
    /// Enum error type
    /// </summary>
   public enum ErrorType
   {
       /// <summary>
       /// The password
       /// </summary>
      Password,
      /// <summary>
      /// The auth key
      /// </summary>
      AuthKey,
      /// <summary>
      /// The handshake
      /// </summary>
      Handshake,
      /// <summary>
      /// The connect
      /// </summary>
      Connect,
      /// <summary>
      /// The login
      /// </summary>
      Login,
      /// <summary>
      /// The invoke
      /// </summary>
      Invoke,
      /// <summary>
      /// The receive
      /// </summary>
      Receive,
      /// <summary>
      /// The general
      /// </summary>
      General
   }

   /// <summary>
   /// Error class.
   /// </summary>
   public class Error
   {
       /// <summary>
       /// The type
       /// </summary>
      public ErrorType Type;
      /// <summary>
      /// The message
      /// </summary>
      public string Message;
   }
}
