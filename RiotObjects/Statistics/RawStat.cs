using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines a raw stats and its information.
    /// </summary>
    public class RawStat : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Create an empty Class
        /// </summary>
        public RawStat()
        {
        }

        /// <summary>
        /// Create the Class with a TypedObject result
        /// </summary>
        /// <param name="result"></param>
        public RawStat(TypedObject result)
        {
            base.SetFields<RawStat>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The string that defines the stat type.
        /// </summary>
        [InternalName("statType")]
        public string StatType { get; set; }

        /// <summary>
        /// The value of the stat.
        /// </summary>
        [InternalName("value")]
        public int Value { get; set; }

        #endregion
    }
}
