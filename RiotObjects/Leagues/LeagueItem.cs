using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Leagues
{
    /// <summary>
    /// Class with specific information about a league item or player entry.
    /// </summary>
    public class LeagueItem : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="LeagueItem"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public LeagueItem(TypedObject result)
        {
            base.SetFields<LeagueItem>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The league position on previous day.
        /// </summary>
        [InternalName("previousDayLeaguePosition")]
        public int PreviousDayLeaguePosition { get; set; }

        /// <summary>
        /// Is on a hot streak? (boolean)
        /// </summary>
        [InternalName("hotStreak")]
        public bool OnHotStreak { get; set; }

        /// <summary>
        /// Mini series object, look into for more info
        /// </summary>
        [InternalName("miniSeries")]
        public object MiniSeries { get; set; }

        /// <summary>
        /// Is fresh blood? (boolean)
        /// </summary>
        [InternalName("freshBlood")]
        public bool IsFreshBlood { get; set; }

        /// <summary>
        /// League tier
        /// </summary>
        [InternalName("tier")]
        public string Tier { get; set; }

        /// <summary>
        /// Last played
        /// </summary>
        [InternalName("lastPlayed")]
        public int LastPlayed { get; set; }

        /// <summary>
        /// The player or team ID number, depending on league type.
        /// </summary>
        [InternalName("playerOrTeamId")]
        public int PlayerOrTeamID { get; set; }

        /// <summary>
        /// The number of league points for player/team.
        /// </summary>
        [InternalName("leaguePoints")]
        public int LeaguePoints { get; set; }

        /// <summary>
        /// Is incative? (boolean)
        /// </summary>
        [InternalName("inactive")]
        public bool IsInactive { get; set; }

        /// <summary>
        /// League Rank (I, II, III, etc)
        /// </summary>
        [InternalName("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Is Veteran? (boolean)
        /// </summary>
        [InternalName("veteran")]
        public bool IsVeteran { get; set; }

        /// <summary>
        /// Queue type of league.
        /// </summary>
        [InternalName("queueType")]
        public string QueueType { get; set; }

        /// <summary>
        /// Number of losses, usually set to 0
        /// </summary>
        [InternalName("losses")]
        public int Losses { get; set; }

        /// <summary>
        /// Player or team name, depending on league type.
        /// </summary>
        [InternalName("playerOrTeamName")]
        public string PlayerOrTeamName { get; set; }

        /// <summary>
        /// Number of wins
        /// </summary>
        [InternalName("wins")]
        public int Wins { get; set; }

        #endregion
    }
}
