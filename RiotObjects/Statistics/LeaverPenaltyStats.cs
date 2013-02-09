using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class LeaverPenaltyStats : RiotGamesObject
    {
        #region Constructors and Callbacks

        public LeaverPenaltyStats(TypedObject result)
        {
            base.SetFields<LeaverPenaltyStats>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The leaver level.
        /// </summary>
        [InternalName("level")]
        public int Level;

        /// <summary>
        /// Date of last level increase of leaver.
        /// </summary>
        [InternalName("lastLevelIncrease")]
        public DateTime LastLevelIncrease { get; set; }

        /// <summary>
        /// Date of last decay of leaver.
        /// </summary>
        [InternalName("lastDecay")]
        public DateTime LastDecay { get; set; }

        /// <summary>
        /// Was user informed? (boolean)
        /// </summary>
        [InternalName("userInformed")]
        public bool UserInformed { get; set; }

        /// <summary>
        /// Leaver points
        /// </summary>
        [InternalName("points")]
        public int Points { get; set; }

        #endregion

    }
}
