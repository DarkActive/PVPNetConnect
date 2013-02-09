using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class RawStat : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Create an empty Class
        /// </summary>
        public RawStat()
        {
        }

        /// <summary>
        /// Create the Class with a callback.
        /// </summary>
        /// <param name="callback"></param>
        public RawStat(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Create the Class with a TypedObject result
        /// </summary>
        /// <param name="result"></param>
        public RawStat(TypedObject result)
        {
            base.SetFields<RawStat>(this, result);
        }

        /// <summary>
        /// Delegate method for Callback
        /// </summary>
        /// <param name="result"></param>
        public delegate void Callback(RawStat result);

        /// <summary>
        /// The callback member.
        /// </summary>
        private Callback callback;

        /// <summary>
        /// The DOCallback method.
        /// </summary>
        /// <param name="result"></param>
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<RawStat>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The string that defines the stat type.
        /// </summary>
        [InternalName("statType")]
        public string StatType { get; set; }

        /// <summary>
        /// The value of the stat.
        /// </summary>
        [InternalName("value")]
        public int Value { get; set; }

        #endregion
    }
}
