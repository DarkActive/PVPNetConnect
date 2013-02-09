using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class ChampionStatInfo : RiotGamesObject
    {
        #region Constructors and Callbacks

        public ChampionStatInfo(TypedObject result)
        {
            base.SetFields<ChampionStatInfo>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Total amount of games played with champion.
        /// </summary>
        [InternalName("totalGamesPlayed")]
        public int TotalGamesPlayed { get; set; }

        /// <summary>
        /// Account ID number of summoner.
        /// </summary>
        [InternalName("accountId")]
        public int AccountID { get; set; }

        /// <summary>
        /// List of different aggregated stats with champion
        /// </summary>
        [InternalName("stats")]
        public List<AggregatedStat> Stats { get; set; }

        /// <summary>
        /// The champion ID number.
        /// </summary>
        [InternalName("championId")]
        public int ChampionID { get; set; }

        #endregion
    }
}
