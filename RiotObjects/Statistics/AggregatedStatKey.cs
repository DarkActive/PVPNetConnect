using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class AggregatedStatKey : RiotGamesObject
    {
        #region Constructors and Callbacks

        public AggregatedStatKey(TypedObject result)
        {
            base.SetFields<AggregatedStatKey>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The game mode that this aggregated stat is for.
        /// </summary>
        [InternalName("gameMode")]
        public string GameMode { get; set; }

        /// <summary>
        /// The account ID number that this aggregated stat is for.
        /// </summary>
        [InternalName("userId")]
        public int AccountID { get; set; }

        /// <summary>
        /// The game mode string that this aggregated stat is for.
        /// </summary>
        [InternalName("gameModeString")]
        public string GameModeString { get; set; }

        #endregion
    }
}
