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

        public override void DoCallback(TypedObject obj)
        {
            return;
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The string that defines the stat type.
        /// </summary>
        [InternalName("gameMode")]
        public string GameMode { get; set; }

        /// <summary>
        /// Count of stat??? (usually 0)
        /// </summary>
        [InternalName("userId")]
        public int AccountID { get; set; }

        /// <summary>
        /// The value of the stat.
        /// </summary>
        [InternalName("gameModeString")]
        public string GameModeString { get; set; }

        #endregion
    }
}
