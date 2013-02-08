using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Game
{
    /// <summary>
    /// Class with in progress game and spectator info.
    /// </summary>
    public class PlatformGameLifecycle : RiotGamesObject
    {
        #region Constructors and Callbacks

        public PlatformGameLifecycle(Callback callback)
        {
            this.callback = callback;
        }

        public PlatformGameLifecycle(TypedObject result)
        {
            base.SetFields<PlatformGameLifecycle>(this, result);
        }

        public delegate void Callback(PlatformGameLifecycle result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            if (result == null)
            {
                callback(null);
                return;
            }

            base.SetFields<PlatformGameLifecycle>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Reconnect delay.
        /// </summary>
        [InternalName("reconnectDelay")]
        public int ReconnectDelay { get; set; }

        /// <summary>
        /// Last date modified.
        /// </summary>
        [InternalName("lastModifiedDate")]
        public double LastModifiedDate { get; set; }

        /// <summary>
        /// Game information of in progress.
        /// </summary>
        [InternalName("game")]
        public GameDTO Game { get; set; }

        /// <summary>
        /// The player credentials for spectating the game.
        /// </summary>
        [InternalName("playerCredentials")]
        public PlayerCredentials PlayerCredentials { get; set; }

        /// <summary>
        /// The name of the game.
        /// </summary>
        [InternalName("gameName")]
        public string GameName { get; set; }

        /// <summary>
        /// Connectivity state enum ???
        /// </summary>
        [InternalName("connectivityStateEnum")]
        public string ConnectivityStateEnum { get; set; }

        #endregion
    }
}
