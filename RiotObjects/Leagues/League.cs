using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Leagues
{
    /// <summary>
    /// Class with specific information about a league.
    /// </summary>
    public class League : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="League"/> class.
        /// </summary>
        /// <param name="callback">The callback method.</param>
        public League(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="League"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public League(TypedObject result)
        {
            base.SetFields<League>(this, result);
        }

        /// <summary>
        /// Delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(League result);

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
            base.SetFields<League>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Queue name that this league is for.
        /// </summary>
        [InternalName("queue")]
        public string Queue { get; set; }

        /// <summary>
        /// Name of the league.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The tier of the league.
        /// </summary>
        [InternalName("tier")]
        public string Tier { get; set; }

        /// <summary>
        /// The league Rank of requestor (I, II, etc..)
        /// </summary>
        [InternalName("requestorsRank")]
        public string RequestorsRank { get; set; }

        /// <summary>
        /// League Entries or summoners
        /// </summary>
        [InternalName("entries")]
        public List<LeagueItem> EntriesList { get; set; }

        /// <summary>
        /// The summoner name of the requestor.
        /// </summary>
        [InternalName("requestorsName")]
        public string RequestorsName { get; set; }

        #endregion
    }
}
