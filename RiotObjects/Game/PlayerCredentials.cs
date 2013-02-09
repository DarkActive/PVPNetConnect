using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Game
{
    /// <summary>
    /// Class with playercredentials info to spectate a game.
    /// </summary>
    public class PlayerCredentials : RiotGamesObject
    {
        #region Constructors and Callbacks

        public PlayerCredentials(TypedObject result)
        {
            base.SetFields<PlayerCredentials>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Encryption key.
        /// </summary>
        [InternalName("encryptionKey")]
        public string EncryptionKey { get; set; }

        /// <summary>
        /// Game ID number.
        /// </summary>
        [InternalName("gameId")]
        public int GameId { get; set; }

        /// <summary>
        /// Game server IP.
        /// </summary>
        [InternalName("serverIp")]
        public string ServerIP { get; set; }

        /// <summary>
        /// Are there observers?
        /// </summary>
        [InternalName("observer")]
        public bool Observer { get; set; }

        /// <summary>
        /// Observer server IP.
        /// </summary>
        [InternalName("observerServerIp")]
        public string ObserverServerIP { get; set; }

        /// <summary>
        /// HandshakeToken ???
        /// </summary>
        [InternalName("handshakeToken")]
        public string HandshakeToken { get; set; }

        /// <summary>
        /// Player ID (of observer???)
        /// </summary>
        [InternalName("playerId")]
        public int PlayerID { get; set; }

        /// <summary>
        /// Game server port.
        /// </summary>
        [InternalName("serverPort")]
        public int ServerPort { get; set; }

        /// <summary>
        /// Observer server port.
        /// </summary>
        [InternalName("observerServerPort")]
        public int ObserverServerPort { get; set; }

        /// <summary>
        /// Summoner name (of observer???)
        /// </summary>
        [InternalName("summonerName")]
        public string SummonerName { get; set; }

        /// <summary>
        /// Observer encryption key.
        /// </summary>
        [InternalName("observerEncryptionKey")]
        public string ObserverEncryptionKey { get; set; }

        #endregion
    }
}
