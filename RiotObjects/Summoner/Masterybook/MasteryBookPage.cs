using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines the MasteryBookPage
    /// </summary>
    public class MasteryBookPage : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="MasteryBookPage"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public MasteryBookPage(TypedObject result)
        {
            base.SetFields<MasteryBookPage>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerID { get; set; }

        /// <summary>
        /// Mastery book page ID number.
        /// </summary>
        [InternalName("pageId")]
        public int PageID { get; set; }

        /// <summary>
        /// Mastery book page name.
        /// </summary>
        [InternalName("name")]
        public string PageName { get; set; }

        /// <summary>
        /// Creation date of mastery book page (seems to always be NULL).
        /// </summary>
        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Is current mastery book page? (boolean)
        /// </summary>
        [InternalName("current")]
        public bool IsCurrent { get; set; }

        /// <summary>
        /// List of all the Mastery book pages.
        /// </summary>
        [InternalName("talentEntries")]
        public List<TalentEntry> TalentEntriesList { get; set; }

        #endregion
    }
}
