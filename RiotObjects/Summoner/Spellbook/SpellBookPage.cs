using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines the spellbook page.
    /// </summary>
    public class SpellBookPage : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellBookPage"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SpellBookPage(TypedObject result)
        {
            base.SetFields<SpellBookPage>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Rune page ID number.
        /// </summary>
        [InternalName("pageId")]
        public long PageID { get; set; }

        /// <summary>
        /// Rune page name.
        /// </summary>
        [InternalName("name")]
        public string PageName { get; set; }

        /// <summary>
        /// Is current rune page? (boolean)
        /// </summary>
        [InternalName("current")]
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Rune page slots entries.
        /// </summary>
        [InternalName("slotEntries")]
        public List<SlotEntry> SlotEntriesList { get; set; }

        /// <summary>
        /// Create date of rune page.
        /// </summary>
        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public bool SummonerID { get; set; }

        #endregion
    }
}
