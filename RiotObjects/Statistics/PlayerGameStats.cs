using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines player game stats.
    /// </summary>
    public class PlayerGameStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerGameStats"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlayerGameStats(TypedObject result)
        {
            base.SetFields<PlayerGameStats>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Name of champion (unreliable).
        /// </summary>
        [InternalName("skinName")]
        public string ChampionName { get; set; }

        /// <summary>
        /// Is Ranked game? (boolean)
        /// </summary>
        [InternalName("ranked")]
        public bool IsRanked { get; set; }

        /// <summary>
        /// ID number of champion skin used
        /// </summary>
        public int SkinIndex { get; set; }

        /// <summary>
        /// List of players in game.
        /// </summary>
        [InternalName("fellowPlayers")]
        public List<FellowPlayerInfo> FellowPlayers { get; set; }

        /// <summary>
        /// Type of game.
        /// </summary>
        [InternalName("gameType")]
        public string GameType { get; set; }

        /// <summary>
        /// XP earned from game.
        /// </summary>
        [InternalName("experienceEarned")]
        public int ExperienceEarned { get; set; }

        /// <summary>
        /// Was eligible for First win of day? (boolean)
        /// </summary>
        [InternalName("eligibleFirstWinOfDay")]
        public bool IsFirstWinOfDay { get; set; }

        /// <summary>
        /// Difficulty (unreliable)
        /// </summary>
        [InternalName("difficulty")]
        public object Difficulty { get; set; }

        /// <summary>
        /// ID of Map.
        /// </summary>
        [InternalName("gameMapId")]
        public int GameMapID { get; set; }

        /// <summary>
        /// Marked as a leaver? (boolean)
        /// </summary>
        [InternalName("leaver")]
        public bool IsLeaver { get; set; }

        /// <summary>
        /// Summoner Spell 1 (1) ID number.
        /// </summary>
        [InternalName("spell1")]
        public int SummSpellD { get; set; }

        /// <summary>
        /// Summoner Spell F (2) ID number.
        /// </summary>
        [InternalName("spell2")]
        public int SummSpellF { get; set; }

        /// <summary>
        /// Game Type enum (usually same as gameType).
        /// </summary>
        [InternalName("gameTypeEnum")]
        public string GameTypeEnum { get; set; }

        /// <summary>
        /// The team ID number (100 = Blue, 200 = Purple)
        /// </summary>
        [InternalName("teamId")]
        public int TeamID { get; set; }

        /// <summary>
        /// Summoner ID number of player.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerId { get; set; }

        /// <summary>
        /// Player stats like kills, deaths, minions, etc...
        /// </summary>
        [InternalName("statistics")]
        public List<RawStat> PlayerStats { get; set; }

        /// <summary>
        /// Is marked as AFK? (boolean)
        /// </summary>
        [InternalName("afk")]
        public bool IsAFK { get; set; }

        /// <summary>
        /// IS NOT GAME ID!!! Some type of ID involving the game (maybe match history ID?).
        /// </summary>
        [InternalName("id")]
        public long HistoryID { get; set; }

        /// <summary>
        /// Amount of boost XP earned.
        /// </summary>
        [InternalName("boostXpEarned")]
        public int BoostXPEarned { get; set; }

        /// <summary>
        /// Summoner level after the game.
        /// </summary>
        [InternalName("level")]
        public int Level { get; set; }

        /// <summary>
        /// Invalid game? (boolean)
        /// </summary>
        [InternalName("invalid")]
        public bool IsInvalid { get; set; }

        /// <summary>
        /// Account ID of player
        /// </summary>
        [InternalName("userId")]
        public int AccountID { get; set; }

        /// <summary>
        /// Date/Time of game creation
        /// </summary>
        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Average Ping of user to server
        /// </summary>
        [InternalName("userServerPing")]
        public int UserServerPing { get; set; }

        /// <summary>
        /// Adjusted Rating (Riot hides this value)
        /// </summary>
        [InternalName("adjustedRating")]
        public int AdjustedRating { get; set; }

        /// <summary>
        /// Size of premade team, 1 if not premade team.
        /// </summary>
        [InternalName("premadeSize")]
        public int PremadeSize { get; set; }

        /// <summary>
        /// Boost IP earned.
        /// </summary>
        [InternalName("boostIpEarned")]
        public int BoostIPEarned { get; set; }

        /// <summary>
        /// ID number of the game.
        /// </summary>
        [InternalName("gameId")]
        public int GameId { get; set; }

        /// <summary>
        /// Time in Queue in SECONDS.
        /// </summary>
        [InternalName("timeInQueue")]
        public int TimeInQueue { get; set; }

        /// <summary>
        /// IP earned from game.
        /// </summary>
        [InternalName("ipEarned")]
        public int IPEarned { get; set; }

        /// <summary>
        /// ELO Change (Riot hides this Value)
        /// </summary>
        [InternalName("eloChange")]
        public int EloChange { get; set; }

        /// <summary>
        /// Game Mode: (CLASSIC, DOMINION, etc)
        /// </summary>
        [InternalName("gameMode")]
        public string GameMode { get; set; }

        /// <summary>
        /// Difficulty String (unreliable)
        /// </summary>
        [InternalName("difficultyString")]
        public string DifficultyString { get; set; }

        /// <summary>
        /// Think has to do with ELO (riot hides this value)
        /// </summary>
        [InternalName("KCoefficient")]
        public int KCoefficient { get; set; }

        /// <summary>
        /// TEAM Rating based on ELO (riot hides this value)
        /// </summary>
        [InternalName("teamRating")]
        public int TeamRating { get; set; }

        /// <summary>
        /// SubType of game (same as QueueType USUALLY)
        /// </summary>
        public string SubType { get; set; }

        /// <summary>
        /// Queue Type of game
        /// </summary>
        [InternalName("queueType")]
        public string QueueType { get; set; }

        /// <summary>
        /// Premade Team (seems to only mean full premade = 5 players)
        /// </summary>
        [InternalName("premadeTeam")]
        public bool IsPremadeTeam { get; set; }

        /// <summary>
        /// Predicted outcome based on elo? (Riot hides this value)
        /// </summary>
        [InternalName("predictedWinPct")]
        public double PredictedWinPercent { get; set; }

        /// <summary>
        /// ELO after game (Riot hides this value)
        /// </summary>
        [InternalName("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// ID of champion used.
        /// </summary>
        [InternalName("championId")]
        public int ChampionID { get; set; }             

        #endregion
    }
}
