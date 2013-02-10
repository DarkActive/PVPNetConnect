using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines summoner game mode spells information.
    /// </summary>
    public class SummonerGameModeSpells : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerGameModeSpells"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SummonerGameModeSpells(TypedObject result)
        {
            base.SetFields<SummonerGameModeSpells>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Summoner spell ID in D slot.
        /// </summary>
        [InternalName("spell1Id")]
        public int Spell1Id { get; set; }

        /// <summary>
        /// Summoner spell ID in F slot.
        /// </summary>
        [InternalName("spell2Id")]
        public int Spell2Id { get; set; }

        #endregion
    }
}
