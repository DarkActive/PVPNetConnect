using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PVPNetConnect.RiotObjects;

namespace PVPNetConnect.Assets
{
    /// <summary>
    /// The CallbackData class, with information for a callback.
    /// </summary>
    public class CallbackData
    {
        /// <summary>
        /// The callback data.
        /// </summary>
        public RiotGamesObject callback { get; set; }
        /// <summary>
        /// The typed object data.
        /// </summary>
        public TypedObject messageBody { get; set;  }

        /// <summary>
        /// Constructor for CallbackData
        /// </summary>
        /// <param name="cb">The Callback object.</param>
        /// <param name="mb">The TypedObject.</param>
        public CallbackData(RiotGamesObject cb, TypedObject mb)
        {
            callback = cb;
            messageBody = mb;
        }
    }
}
