using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class PlayerLifetimeStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        public PlayerLifetimeStats(Callback callback)
        {
            this.callback = callback;
        }

        public PlayerLifetimeStats(TypedObject result)
        {
            base.SetFields<PlayerLifetimeStats>(this, result);
        }

        public delegate void Callback(PlayerLifetimeStats result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<PlayerLifetimeStats>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// List of all the player stat summaries (summaries of Solo_ranked, normal, etc)
        /// </summary>
        [InternalName("playerStatSummaries")]
        public List<PlayerStatSummaries> PlayerStatSummaries { get; set; }

        /// <summary>
        /// The leaver penalty stats.
        /// </summary>
        [InternalName("leaverPenaltyStats")]
        public LeaverPenaltyStats LeaverPenaltyStats { get; set; }

        /// <summary>
        /// The Account ID number.
        /// </summary>
        [InternalName("userId")]
        public int AccountID { get; set; }

        /// <summary>
        /// The leaver penalty stats.
        /// </summary>
        [InternalName("dodgeStreak")]
        public int DodgeStreak { get; set; }

        /// <summary>
        /// Dodge Penalty Date
        /// </summary>
        [InternalName("dodgePenaltyDate")]
        public DateTime DodgePenaltyDate { get; set; }

        /// <summary>
        /// Player stats like time tracked stats.
        /// </summary>
        [InternalName("leaverPenaltyStats")]
        public PlayerStats PlayerStats { get; set; }

        #endregion
    }
}
