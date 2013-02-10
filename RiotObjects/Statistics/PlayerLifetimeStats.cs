using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines player lifetime stats.
    /// </summary>
    public class PlayerLifetimeStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerLifetimeStats"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public PlayerLifetimeStats(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerLifetimeStats"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlayerLifetimeStats(TypedObject result)
        {
            base.SetFields<PlayerLifetimeStats>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(PlayerLifetimeStats result);

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
