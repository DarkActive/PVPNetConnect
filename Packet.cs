using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect
{
    /// <summary>
    /// The packet class.
    /// </summary>
   public class Packet
   {
       /// <summary>
       /// The data buffer
       /// </summary>
      private byte[] dataBuffer;
      /// <summary>
      /// The data pos
      /// </summary>
      private int dataPos;
      /// <summary>
      /// The data size
      /// </summary>
      private int dataSize;
      /// <summary>
      /// The message type
      /// </summary>
      private int messageType;

      /// <summary>
      /// Sets the size.
      /// </summary>
      /// <param name="size">The size.</param>
      public void SetSize(int size)
      {
         dataSize = size;
         dataBuffer = new byte[dataSize];
      }

      /// <summary>
      /// Sets the type.
      /// </summary>
      /// <param name="type">The type.</param>
      public void SetType(int type)
      {
         messageType = type;
      }

      /// <summary>
      /// Adds the specified b.
      /// </summary>
      /// <param name="b">The b.</param>
      public void Add(byte b)
      {
         dataBuffer[dataPos++] = b;
      }

      /// <summary>
      /// Determines whether this instance is complete.
      /// </summary>
      /// <returns>
      ///   <c>true</c> if this instance is complete; otherwise, <c>false</c>.
      /// </returns>
      public bool IsComplete()
      {
         return (dataPos == dataSize);
      }

      /// <summary>
      /// Gets the size.
      /// </summary>
      /// <returns></returns>
      public int GetSize()
      {
         return dataSize;
      }

      /// <summary>
      /// Gets the type of the message.
      /// </summary>
      /// <returns></returns>
      public int GetMessageType()
      {
         return messageType;
      }

      /// <summary>
      /// Gets the data.
      /// </summary>
      /// <returns></returns>
      public byte[] GetData()
      {
         return dataBuffer;
      }
   }
}
