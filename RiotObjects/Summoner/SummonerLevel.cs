using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines summoner level information.
    /// </summary>
    public class SummonerLevel : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerLevel"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SummonerLevel(TypedObject result)
        {
            base.SetFields<SummonerLevel>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Experience tier modification.
        /// </summary>
        [InternalName("expTierMod")]
        public int ExpTierMod { get; set; }

        /// <summary>
        /// Grant RP value.
        /// </summary>
        [InternalName("grantRp")]
        public int GrantRP { get; set; }

        /// <summary>
        /// Experience gained for loss.
        /// </summary>
        [InternalName("expForLoss")]
        public int ExpForLoss { get; set; }

        /// <summary>
        /// The summoner tier.
        /// </summary>
        [InternalName("summonerTier")]
        public int SummonerTier { get; set; }

        /// <summary>
        /// The influence points tier mod.
        /// </summary>
        [InternalName("infTierMod")]
        public int InfTierMod { get; set; }

        /// <summary>
        /// Experience needed to next level.
        /// </summary>
        [InternalName("expToNextLevel")]
        public int ExpToNextLevel { get; set; }

        /// <summary>
        /// Experience gained for win.
        /// </summary>
        [InternalName("expForWin")]
        public int ExpForWin { get; set; }

        /// <summary>
        /// The current summoner level.
        /// </summary>
        [InternalName("summonerLevel")]
        public int Level { get; set; }

        #endregion
    }
}
