using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PVPNetConnect.RiotObjects.Catalog;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines a talent entry.
    /// </summary>
    public class TalentEntry : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="TalentEntry"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public TalentEntry(TypedObject result)
        {
            base.SetFields<TalentEntry>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Talent ID number.
        /// </summary>
        [InternalName("talentId")]
        public int TalentID { get; set; }

        /// <summary>
        /// Talent rank number.
        /// </summary>
        [InternalName("rank")]
        public int TalentRank { get; set; }

        /// <summary>
        /// Talent class with information about talent.
        /// </summary>
        [InternalName("talent")]
        public Talent Talent { get; set; }

        #endregion
    }
}
