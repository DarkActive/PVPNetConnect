using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines player stat summaries
    /// </summary>
    public class PlayerStatSummaries : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStatSummaries"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlayerStatSummaries(TypedObject result)
        {
            base.SetFields<PlayerStatSummaries>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The list of all player stat summaries for each queue type (ranked, normal, etc)
        /// </summary>
        [InternalName("playerStatSummarySet")]
        public List<PlayerStatSummary> PlayerStatSummaryList;

        /// <summary>
        /// The Acount ID number.
        /// </summary>
        [InternalName("userId")]
        public int AccountID { get; set; }

        #endregion

    }
}
