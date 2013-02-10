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

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformGameLifecycle"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public PlatformGameLifecycle(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformGameLifecycle"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PlatformGameLifecycle(TypedObject result)
        {
            base.SetFields<PlatformGameLifecycle>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(PlatformGameLifecycle result);

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
