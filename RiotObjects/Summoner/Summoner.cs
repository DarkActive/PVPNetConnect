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

        /// <summary>
        /// Initializes a new instance of the <see cref="Summoner"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
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

        /// <summary>
        /// The help flag.
        /// </summary>
        [InternalName("helpFlag")]
        public bool HelpFlag { get; set; }

        /// <summary>
        /// THe display elo questionare boolean
        /// </summary>
        [InternalName("displayEloQuestionaire")]
        public bool DisplayEloQuestionaire { get; set; }

        /// <summary>
        /// The last game date.
        /// </summary>
        [InternalName("lastGameDate")]
        public DateTime LastGameDate { get; set; }

        /// <summary>
        /// THe advanced tutorial flag.
        /// </summary>
        [InternalName("advancedTutorialFlag")]
        public bool AdvancedTutorialFlag { get; set; }

        /// <summary>
        /// THe revision date.
        /// </summary>
        [InternalName("revisionDate")]
        public DateTime RevisionDate { get; set; }

        /// <summary>
        /// The revision id.
        /// </summary>
        [InternalName("revisionId")]
        public int RevisionId { get; set; }

        /// <summary>
        /// THe name change flag.
        /// </summary>
        [InternalName("nameChangeFlag")]
        public bool NameChangeFlag { get; set; }

        /// <summary>
        /// The tutorial flag.
        /// </summary>
        [InternalName("tutorialFlag")]
        public bool TutorialFlag { get; set; }

        /// <summary>
        /// The social network user ids.
        /// </summary>
        [InternalName("socialNetworkUserIds")]
        public object SocialNetworkUserIds { get; set; }

        #endregion
    }
}
