using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Game
{
    /// <summary>
    /// Class with specific information about a game (inProgress, preGame, and endGame)
    /// </summary>
    public class GameDTO : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDTO"/> class.
        /// </summary>
        /// <param name="callback">The callback method.</param>
        public GameDTO(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDTO"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public GameDTO(TypedObject result)
        {
            base.SetFields<GameDTO>(this, result);
        }

        /// <summary>
        /// Delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(GameDTO result);

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
            base.SetFields<GameDTO>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// ID of game.
        /// </summary>
        [InternalName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Game name.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Chat room name.
        /// </summary>
        [InternalName("roomName")]
        public string RoomName { get; set; }

        /// <summary>
        /// If there is a game password.
        /// </summary>
        [InternalName("passwordSet")]
        public bool PasswordSet { get; set; }

        /// <summary>
        /// Spectators allowed setting.
        /// </summary>
        [InternalName("spectatorsAllowed")]
        public string SpectatorsAllowed { get; set; }

        /// <summary>
        /// Spectator delay in seconds.
        /// </summary>
        [InternalName("spectatorDelay")]
        public int SpectatorDelay { get; set; }

        /// <summary>
        /// Type of game.
        /// </summary>
        [InternalName("gameType")]
        public string GameType { get; set; }

        /// <summary>
        /// The game type config ID.
        /// </summary>
        [InternalName("gameTypeConfigId")]
        public int GameTypeConfigId { get; set; }

        /// <summary>
        /// Current State of game, (TEAM_SELECT, CHAMP_SELECT), etc..
        /// </summary>
        [InternalName("gameState")]
        public string GameState { get; set; }

        /// <summary>
        /// Game Mode (CLASSIC, DOMINION).
        /// </summary>
        [InternalName("gameMode")]
        public string GameMode { get; set; }

        /// <summary>
        /// ID of the map.
        /// </summary>
        [InternalName("mapId")]
        public int MapId { get; set; }

        /// <summary>
        /// Maximum number of players allowed
        /// </summary>
        [InternalName("maxNumPlayers")]
        public int MaxNumPlayers { get; set; }

        /// <summary>
        /// Expiration time of lobby.
        /// </summary>
        [InternalName("expiryTime")]
        public double ExpiryTime { get; set; }

        /// <summary>
        /// The name of the queue type
        /// </summary>
        [InternalName("queueTypeName")]
        public string QueueTypeName { get; set; }

        /// <summary>
        /// Position in the queue.
        /// </summary>
        [InternalName("queuePosition")]
        public int QueuePosition { get; set; }

        /// <summary>
        /// Pick turn number to pick champion.
        /// </summary>
        [InternalName("pickTurn")]
        public int PickTurn { get; set; }

        /// <summary>
        /// Summary of game owner/creator (PlayerParticipant).
        /// </summary>
        [InternalName("ownerSummary")]
        public PlayerParticipant OwnerSummary { get; set; }

        /// <summary>
        /// List of Players (PlayerParticipant) on Team One (A).
        /// </summary>
        [InternalName("teamOne")]
        public List<PlayerParticipant> TeamOne { get; set; }

        /// <summary>
        /// List of Players (PlayerParticipant) on Team Two (B).
        /// </summary>
        [InternalName("teamTwo")]
        public List<PlayerParticipant> TeamTwo { get; set; }

        /// <summary>
        /// List of Players and the champs they selected.
        /// </summary>
        [InternalName("playerChampionSelections")]
        public List<PlayerChampionSelection> PlayerChampionSelections { get; set; }

        /// <summary>
        /// List of observers.
        /// </summary>
        [InternalName("observers")]
        public List<GameObserver> Observers { get; set; }

        /// <summary>
        /// Ban order (team number array)
        /// </summary>
        [InternalName("banOrder")]
        public List<int> BanOrder { get; set; }

        /// <summary>
        /// List of banned champions.
        /// </summary>
        [InternalName("bannedChampions")]
        public List<BannedChampion> BannedChampions { get; set; }

        /// <summary>
        /// Optimistic lock ???
        /// </summary>
        [InternalName("optimisticLock")]
        public double OptimisticLock { get; set; }

        /// <summary>
        /// Status of participants, 0s and 1s
        /// </summary>
        [InternalName("statusOfParticipants")]
        public string StatusOfParticipants { get; set; }

        /// <summary>
        /// Condition if game is terminated or done.
        /// </summary>
        [InternalName("terminatedCondition")]
        public string TerminatedCondition { get; set; }

        /// <summary>
        /// Condition if game is terminated or done.
        /// </summary>
        [InternalName("roomPassword")]
        public string RoomPassword { get; set; }

        #endregion
    }
}
