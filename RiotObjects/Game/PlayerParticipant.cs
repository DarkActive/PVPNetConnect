using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Game
{
    /// <summary>
    /// Class that defines a player participant and its infromation.
    /// </summary>
    public class PlayerParticipant : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerParticipant"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlayerParticipant(TypedObject result)
        {
            base.SetFields<PlayerParticipant>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The account ID number of observer.
        /// </summary>
        [InternalName("accountId")]
        public int AccountID { get; set; }

        /// <summary>
        /// The summoner ID number of observer.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerID { get; set; }

        /// <summary>
        /// The summoner ID number of observer.
        /// </summary>
        [InternalName("timeAddedToQueue")]
        public long TimeAddedToQueue { get; set; }

        /// <summary>
        /// Index of player participant (1 to 10, [Team1,p1] = 1 [team2,p1] = 6)
        /// </summary>
        [InternalName("index")]
        public int Index { get; set; }

        /// <summary>
        /// Rating for queue Type
        /// </summary>
        [InternalName("queueRating")]
        public int QueueRating { get; set; }

        /// <summary>
        /// The difficulty of the bot (NONE,...)
        /// </summary>
        [InternalName("botDifficulty")]
        public string BotDifficulty { get; set; }

        /// <summary>
        /// The original Account ID number (probably)
        /// </summary>
        [InternalName("originalAccountNumber")]
        public int OriginalAccountNumber { get; set; }

        /// <summary>
        /// Summoner name of the observer
        /// </summary>
        [InternalName("summonerInternalName")]
        public string SummonerInternalName { get; set; }

        /// <summary>
        /// Summoner name of the observer
        /// </summary>
        [InternalName("summonerName")]
        public string SummonerName { get; set; }

        /// <summary>
        /// Boolean value of "minor" (??)
        /// </summary>
        [InternalName("minor")]
        public bool Minor { get; set; }

        /// <summary>
        /// The skin ID that was last selected by the observer (summoner).
        /// </summary>
        [InternalName("lastSelectedSkinIndex")]
        public int LastSelectedSkinID { get; set; }

        /// <summary>
        /// Partner ID (probably duo queue partner ID ??)
        /// </summary>
        [InternalName("partnerId")]
        public int PartnerID;

        /// <summary>
        /// The profile icon ID of the observer (summoner).
        /// </summary>
        [InternalName("profileIconId")]
        public int ProfileIconID { get; set; }

        /// <summary>
        /// Is this player the team owner? (boolean)
        /// </summary>
        [InternalName("teamOwner")]
        public bool TeamOwner { get; set; }

        /// <summary>
        /// Badges ??? unknown significance, seems to be int.
        /// </summary>
        [InternalName("badges")]
        public int Badges { get; set; }

        /// <summary>
        /// Pickturn of observer (probably always 0).
        /// </summary>
        [InternalName("pickTurn")]
        public int PickTurn { get; set; }

        /// <summary>
        /// PickMode of the game.
        /// </summary>
        [InternalName("pickMode")]
        public int PickMode { get; set; }

        /// <summary>
        /// Is client in synch? (boolean)
        /// </summary>
        [InternalName("clientInSynch")]
        public bool ClientInSynch { get; set; }

        /// <summary>
        /// Original platform ID (NA, EUW, EUNE,...)
        /// </summary>
        [InternalName("originalPlatformId")]
        public string OriginalPlatformID { get; set; }

        /// <summary>
        /// The ID number of this team participant
        /// </summary>
        [InternalName("teamParticipantId")]
        public long teamParticipantID { get; set; }

        #endregion
    }
}
