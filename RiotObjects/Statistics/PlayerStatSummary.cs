using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines player stat summary.
    /// </summary>
    public class PlayerStatSummary : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStatSummary"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlayerStatSummary(TypedObject result)
        {
            base.SetFields<PlayerStatSummary>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The queue type of stat summary
        /// </summary>
        [InternalName("playerStatSummaryType")]
        public string QueueType;

        /// <summary>
        /// Date last modified.
        /// </summary>
        [InternalName("modifyDate")]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Number of leaves from games.
        /// </summary>
        [InternalName("leaves")]
        public int Leaves { get; set; }

        /// <summary>
        /// Number of wins.
        /// </summary>
        [InternalName("wins")]
        public int Wins { get; set; }

        /// <summary>
        /// Account ID number of stats' user
        /// </summary>
        [InternalName("userId")]
        public int AccountID { get; set; }

        #endregion

    }
}
