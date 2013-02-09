using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SummonerLevel : RiotGamesObject
    {
        #region Constructors and Callbacks

        public SummonerLevel(TypedObject result)
        {
            base.SetFields<SummonerLevel>(this, result);
        }

        #endregion

        #region Member Properties

        [InternalName("expTierMod")]
        public int ExpTierMod { get; set; }

        [InternalName("grantRp")]
        public int GrantRP { get; set; }

        [InternalName("expForLoss")]
        public int ExpForLoss { get; set; }

        [InternalName("summonerTier")]
        public int SummonerTier { get; set; }

        [InternalName("infTierMod")]
        public int InfTierMod { get; set; }

        [InternalName("expToNextLevel")]
        public int ExpToNextLevel { get; set; }

        [InternalName("expForWin")]
        public int ExpForWin { get; set; }

        [InternalName("summonerLevel")]
        public int Level { get; set; }

        #endregion
    }
}
