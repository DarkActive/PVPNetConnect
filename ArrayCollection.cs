using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace PVPNetConnect
{
    /// <summary>
    /// The Array collection clas.
    /// </summary>
   public class ArrayCollection
   {
       /// <summary>
       /// Initializes a new instance of the <see cref="ArrayCollection"/> class.
       /// </summary>
       /// <param name="items">The items.</param>
      public ArrayCollection(object[] items)
      {
         this.Items = items;
      }

      /// <summary>
      /// The items in collection.
      /// </summary>
      public object[] Items;
   }
}
