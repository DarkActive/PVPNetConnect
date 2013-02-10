using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// Class that defines all the aggregated stats.
    /// </summary>
    public class AggregatedStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregatedStats"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public AggregatedStats(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregatedStats"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public AggregatedStats(TypedObject result)
        {
            base.SetFields<AggregatedStats>(this, result);
        }

        /// <summary>
        /// Delegate for callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(AggregatedStats result);

        /// <summary>
        /// The callback method.
        /// </summary>
        private Callback callback;

        /// <summary>
        /// The DoCallback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<AggregatedStats>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The lifetime statistics that make up aggregaged stat.
        /// </summary>
        [InternalName("lifetimeStatistics")]
        public List<AggregatedStat> LifetimeStatistics { get; set; }

        /// <summary>
        /// The date that aggregated stats was modified.
        /// </summary>
        [InternalName("modifyDate")]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// The Aggregated stat key with general info
        /// </summary>
        [InternalName("key")]
        public AggregatedStatKey Key { get; set; }

        #endregion
    }
}
