using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects
{
    /// <summary>
    /// Unclassed Riot Object
    /// </summary>
    public class UnclassedObject : RiotGamesObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnclassedObject"/> class.
        /// </summary>
        /// <param name="callback">The callback method.</param>
        public UnclassedObject(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result">The TypedObject result or return packet result.</param>
        public delegate void Callback(TypedObject result);
        /// <summary>
        /// The callback method.
        /// </summary>
        private Callback callback;

        /// <summary>
        /// Does the callback.
        /// </summary>
        /// <param name="result">The TypedObject result or return packet result.</param>
        public override void DoCallback(TypedObject result)
        {
            callback(result);
        }
    }
}
