using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class AggregatedStat : RiotGamesObject
    {
        #region Constructors and Callbacks

        public AggregatedStat(Callback callback)
        {
            this.callback = callback;
        }

        public AggregatedStat(TypedObject result)
        {
            base.SetFields<AggregatedStat>(this, result);
        }

        public delegate void Callback(AggregatedStat result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<AggregatedStat>(this, result);
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
        /// Count of stat??? (usually 0)
        /// </summary>
        [InternalName("count")]
        public int Count { get; set; }

        /// <summary>
        /// The value of the stat.
        /// </summary>
        [InternalName("value")]
        public int Value { get; set; }

        /// <summary>
        /// The champion ID number.
        /// </summary>
        [InternalName("championId")]
        public int ChampionID { get; set; }

        #endregion
    }
}
