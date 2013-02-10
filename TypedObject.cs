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

namespace PVPNetConnect
{
    /// <summary>
    /// The class that defines a TypedObject and packet object
    /// </summary>
   public class TypedObject : Dictionary<string, object>
   {
      /// <summary>
      /// The type
      /// </summary>
      public string type;

      /// <summary>
      /// Initializes a new instance of the <see cref="TypedObject"/> class.
      /// </summary>
      public TypedObject()
      {
         this.type = null;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="TypedObject"/> class.
      /// </summary>
      /// <param name="type">The type.</param>
      public TypedObject(string type)
      {
         this.type = type;
      }

      /// <summary>
      /// Makes the array collection.
      /// </summary>
      /// <param name="data">The data.</param>
      /// <returns></returns>
      public static TypedObject MakeArrayCollection(object[] data)
      {
         TypedObject ret = new TypedObject("flex.messaging.io.ArrayCollection");
         ret.Add("array", data);
         return ret;
      }

      /// <summary>
      /// Gets the TO.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      public TypedObject GetTO(string key)
      {
         if(this[key] is TypedObject)
            return (TypedObject)this[key];

         return null;
      }

      /// <summary>
      /// Gets the string.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      public string GetString(string key)
      {
         return (string)this[key];
      }

      /// <summary>
      /// Gets the int.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      public int? GetInt(string key)
      {
         object val = this[key];
         if (val == null)
            return null;
         else if (val is int)
            return (int)val;
         else
            return Convert.ToInt32((double)val);
      }

      /// <summary>
      /// Gets the double.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      public double? GetDouble(string key)
      {
         object val = this[key];
         if (val == null)
            return null;
         else if (val is double)
            return (double)val;
         else
            return Convert.ToDouble((int)val);
      }

      /// <summary>
      /// Gets the bool.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      public bool GetBool(string key)
      {
         return (bool)this[key];
      }

      /// <summary>
      /// Gets the array.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <returns></returns>
      public object[] GetArray(string key)
      {
         if (this[key] is TypedObject && GetTO(key).type.Equals("flex.messaging.io.ArrayCollection"))
            return (object[])GetTO(key)["array"];
         else
            return (object[])this[key];
      }

      /// <summary>
      /// Returns a <see cref="System.String" /> that represents this instance.
      /// </summary>
      /// <returns>
      /// A <see cref="System.String" /> that represents this instance.
      /// </returns>
      public override string ToString()
      {
         if (type == null)
            return base.ToString();
         else if (type.Equals("flex.messaging.io.ArrayCollection"))
         {
            StringBuilder sb = new StringBuilder();
            object[] data = (object[])this["array"];
            sb.Append("ArrayCollection[");
            for (int i = 0; i < data.Length; i++)
            {
               sb.Append(data[i]);
               if (i < data.Length - 1)
                  sb.Append(", ");
            }
            sb.Append(']');
            return sb.ToString();
         }
         else
            return type + ":" + base.ToString();
      }
   }
}
