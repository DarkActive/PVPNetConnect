using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines summoner default spells information.
    /// </summary>
    public class SummonerDefaultSpells : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerDefaultSpells"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SummonerDefaultSpells(TypedObject result)
        {
            base.SetFields<SummonerDefaultSpells>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Dictionary object for default spells based on game mode (Game modes: CLASSIC, ARAM, ODIN, TUTORIAL).
        /// </summary>
        [InternalName("summonerDefaultSpellMap")]
        public Dictionary<string, SummonerGameModeSpells> SummonerDefaultSpellMap { get; set; }

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int summonerId { get; set; }

        #endregion
    }
}
