using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines recent games.
    /// </summary>
    public class RecentGames : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentGames"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public RecentGames(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentGames"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public RecentGames(TypedObject result)
        {
            base.SetFields<RecentGames>(this, result);
        }

        /// <summary>
        /// The delegate for the call back method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(RecentGames result);

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
            base.SetFields<RecentGames>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The account ID of the user whose recent games list is.
        /// </summary>
        [InternalName("userId")]
        public string AccountID { get; set; }

        /// <summary>
        /// List of the recent games and their stats.
        /// </summary>
        [InternalName("gameStatistics")]
        public List<PlayerGameStats> GameStatsList { get; set; }

        #endregion
    }
}
