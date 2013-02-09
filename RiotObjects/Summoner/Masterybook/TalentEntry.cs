using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class TalentEntry : RiotGamesObject
    {
        #region Constructors and Callbacks

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
