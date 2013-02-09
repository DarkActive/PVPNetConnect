using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class Talent : RiotGamesObject
    {
        #region Constructors and Callbacks

        public Talent(TypedObject result)
        {
            base.SetFields<Talent>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Game Code ID number of talent.
        /// </summary>
        [InternalName("gameCode")]
        public int GameCode { get; set; }

        /// <summary>
        /// Talent ID number.
        /// </summary>
        [InternalName("tltId")]
        public int TalentID { get; set; }

        /// <summary>
        /// The talent row ID number, in talent tree.
        /// </summary>
        [InternalName("talentRowId")]
        public int TalentRowID { get; set; }

        /// <summary>
        /// The talent group ID number, group in talent tree.
        /// </summary>
        [InternalName("talentGroupId")]
        public int TalentGroupID { get; set; }

        /// <summary>
        /// Has to do with in game code (Unknown type, so set to string)
        /// </summary>
        [InternalName("prereqTalentGameCode")]
        public string PreReqTalentGameCode { get; set; }

        /// <summary>
        /// Index?? (unknown significance)
        /// </summary>
        [InternalName("index")]
        public int Index { get; set; }

        /// <summary>
        /// Minimum level of talent.
        /// </summary>
        [InternalName("minLevel")]
        public int MinLevel { get; set; }

        /// <summary>
        /// Minimum tier of talent.
        /// </summary>
        [InternalName("minTier")]
        public int MinTier { get; set; }

        /// <summary>
        /// Maximum rank of talent.
        /// </summary>
        [InternalName("maxRank")]
        public int MaxRank { get; set; }

        /// <summary>
        /// The talent name.
        /// </summary>
        [InternalName("name")]
        public string TalentName { get; set; }

        /// <summary>
        /// Level 1 of Talent description string.
        /// </summary>
        [InternalName("level1Desc")]
        public string Level1Desc { get; set; }

        /// <summary>
        /// Level 2 of Talent description string.
        /// </summary>
        [InternalName("level2Desc")]
        public string Level2Desc { get; set; }

        /// <summary>
        /// Level 31 of Talent description string.
        /// </summary>
        [InternalName("level3Desc")]
        public string Level3Desc { get; set; }

        /// <summary>
        /// Level 4 of Talent description string.
        /// </summary>
        [InternalName("level4Desc")]
        public string Level4Desc { get; set; }

        /// <summary>
        /// Level 5 of Talent description string.
        /// </summary>
        [InternalName("level5Desc")]
        public string Level5Desc { get; set; }

        #endregion
    }
}
