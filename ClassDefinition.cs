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
    /// The class definition class.
    /// </summary>
   public class ClassDefinition
   {
       /// <summary>
       /// The type
       /// </summary>
      public string type;
      /// <summary>
      /// The externalizable
      /// </summary>
      public bool externalizable = false;
      /// <summary>
      /// The dynamic
      /// </summary>
      public bool dynamic = false;
      /// <summary>
      /// The members
      /// </summary>
      public List<string> members = new List<string>();
   }
}
