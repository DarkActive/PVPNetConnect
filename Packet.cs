using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect
{
   public class Packet
   {
      private byte[] dataBuffer;
      private int dataPos;
      private int dataSize;
      private int messageType;

      public void SetSize(int size)
      {
         dataSize = size;
         dataBuffer = new byte[dataSize];
      }

      public void SetType(int type)
      {
         messageType = type;
      }

      public void Add(byte b)
      {
         dataBuffer[dataPos++] = b;
      }

      public bool IsComplete()
      {
         return (dataPos == dataSize);
      }

      public int GetSize()
      {
         return dataSize;
      }

      public int GetMessageType()
      {
         return messageType;
      }

      public byte[] GetData()
      {
         return dataBuffer;
      }
   }
}
