using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// Class that defines an aggregated stat and its information.
    /// </summary>
    public class AggregatedStat : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregatedStat"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public AggregatedStat(TypedObject result)
        {
            base.SetFields<AggregatedStat>(this, result);
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
