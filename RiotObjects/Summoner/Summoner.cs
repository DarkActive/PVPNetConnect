using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// Class with some basic public summoner information.
    /// </summary>
    public class Summoner : RiotGamesObject
    {
        #region Constructors and Callbacks

        public Summoner(TypedObject result)
        {
            base.SetFields<Summoner>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Highest ranked tier for season two.
        /// </summary>
        [InternalName("seasonTwoTier")]
        public string SeasonTwoTier { get; set; }

        /// <summary>
        /// The summoner name or in game name.
        /// </summary>
        [InternalName("internalName")]
        public string InternalName { get; set; }

        /// <summary>
        /// Account ID number.
        /// </summary>
        [InternalName("acctid")]
        public int AccountId { get; set; }

        /// <summary>
        /// Highest ranked tier for season one.
        /// </summary>
        [InternalName("seasonOneTier")]
        public string SeasonOneTier { get; set; }

        /// <summary>
        /// The summoner name or in game name (same as InternalName).
        /// </summary>
        [InternalName("name")]
        public string Name;

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("sumId")]
        public int SummonerId { get; set; }

        /// <summary>
        /// Profile icon ID number.
        /// </summary>
        [InternalName("profileIconId")]
        public int ProfileIconId { get; set; }

        [InternalName("helpFlag")]
        public bool HelpFlag { get; set; }

        [InternalName("displayEloQuestionaire")]
        public bool DisplayEloQuestionaire { get; set; }

        [InternalName("lastGameDate")]
        public DateTime LastGameDate { get; set; }

        [InternalName("advancedTutorialFlag")]
        public bool AdvancedTutorialFlag { get; set; }

        [InternalName("revisionDate")]
        public DateTime RevisionDate { get; set; }

        [InternalName("revisionId")]
        public int RevisionId { get; set; }

        [InternalName("nameChangeFlag")]
        public bool NameChangeFlag { get; set; }

        [InternalName("tutorialFlag")]
        public bool TutorialFlag { get; set; }

        [InternalName("socialNetworkUserIds")]
        public object SocialNetworkUserIds { get; set; }

        #endregion
    }
}
