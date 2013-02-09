using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class AggregatedStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        public AggregatedStats(Callback callback)
        {
            this.callback = callback;
        }

        public AggregatedStats(TypedObject result)
        {
            base.SetFields<AggregatedStats>(this, result);
        }

        public delegate void Callback(AggregatedStats result);
        private Callback callback;
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
