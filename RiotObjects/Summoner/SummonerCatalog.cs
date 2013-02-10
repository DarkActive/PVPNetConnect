using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PVPNetConnect.RiotObjects.Catalog;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines summoner catalog.
    /// </summary>
    public class SummonerCatalog : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerCatalog"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SummonerCatalog(TypedObject result)
        {
            base.SetFields<SummonerCatalog>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Experience tier modification.
        /// </summary>
        [InternalName("items")]
        public object Items { get; set; }

        /// <summary>
        /// Talent tree list.
        /// </summary>
        [InternalName("talentTree")]
        public List<TalentGroup> TalentTreeList;

        /// <summary>
        /// Spellbook/Runebook config, with all the rune slots.
        /// </summary>
        [InternalName("spellBookConfig")]
        public List<RuneSlot> SpellBookConfig { get; set; }

        #endregion
    }
}
