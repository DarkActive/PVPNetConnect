using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines player stats.
    /// </summary>
    public class PlayerStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStats"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlayerStats(TypedObject result)
        {
            base.SetFields<PlayerStats>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// List of time tracked stats.
        /// </summary>
        [InternalName("timeTrackedStats")]
        public List<TimeTrackedStat> TimeTrackedStats;

        /// <summary>
        /// Number of promotional games played.
        /// </summary>
        [InternalName("promoGamesPlayed")]
        public int PromoGamesPlayed { get; set; }

        /// <summary>
        /// Date of promotional games played last updated.
        /// </summary>
        [InternalName("promoGamesPlayedLastUpdated")]
        public DateTime PromoGamesPlayedLastUpdated { get; set; }

        #endregion

    }
}
