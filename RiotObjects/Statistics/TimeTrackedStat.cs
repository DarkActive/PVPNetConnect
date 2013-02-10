using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The calss that defines time tracked stats.
    /// </summary>
    public class TimeTrackedStat : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTrackedStat"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public TimeTrackedStat(TypedObject result)
        {
            base.SetFields<TimeTrackedStat>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Date of the time tracked stat.
        /// </summary>
        [InternalName("timestamp")]
        public DateTime Timestamp;

        /// <summary>
        /// Type of the time tracked stat.
        /// </summary>
        [InternalName("type")]
        public string Type { get; set; }

        #endregion

    }
}
