using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class RecentGames : RiotGamesObject
    {
        #region Constructors and Callbacks

        public RecentGames(Callback callback)
        {
            this.callback = callback;
        }

        public RecentGames(TypedObject result)
        {
            base.SetFields<RecentGames>(this, result);
        }

        public delegate void Callback(RecentGames result);
        private Callback callback;
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
