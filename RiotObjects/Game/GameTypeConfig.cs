using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Game
{
    /// <summary>
    /// Class which defines a Game type config object.
    /// </summary>
    public class GameTypeConfig : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="GameTypeConfig"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public GameTypeConfig(TypedObject result)
        {
            base.SetFields<GameTypeConfig>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Game type config ID number.
        /// </summary>
        [InternalName("id")]
        public int ID { get; set; }

        /// <summary>
        /// Allow trades? (boolean)
        /// </summary>
        [InternalName("allowTrades")]
        public bool AllowTrades { get; set; }

        /// <summary>
        /// Game type config name.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Duration in seconds of main pick timer.
        /// </summary>
        [InternalName("mainPickTimerDuration")]
        public int MainPickTimerDuration { get; set; }

        /// <summary>
        /// Is Exclusive pick? (boolean)
        /// </summary>
        [InternalName("exclusivePick")]
        public bool ExclusivePick { get; set; }

        /// <summary>
        /// Pick mode.
        /// </summary>
        [InternalName("pickMode")]
        public string PickMode { get; set; }

        /// <summary>
        /// Number of max allowable bans.
        /// </summary>
        [InternalName("maxAllowableBans")]
        public int MaxAllowableBans { get; set; }

        /// <summary>
        /// Duration in seconds of ban timer.
        /// </summary>
        [InternalName("banTimerDuration")]
        public int BanTimerDuration { get; set; }

        /// <summary>
        /// Duration in seconds of post pick timer.
        /// </summary>
        [InternalName("postPickTimerDuration")]
        public int PostPickTimerDuration { get; set; }

        #endregion
    }
}
