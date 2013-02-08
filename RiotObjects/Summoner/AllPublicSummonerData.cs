using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class AllPublicSummonerData : RiotGamesObject
    {
        #region Constructors and Callbacks

        public AllPublicSummonerData(Callback callback)
        {
            this.callback = callback;
        }

        public AllPublicSummonerData(TypedObject result)
        {
            base.SetFields<AllPublicSummonerData>(this, result);
        }


        public delegate void Callback(AllPublicSummonerData result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<AllPublicSummonerData>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        [InternalName("summoner")]
        public Summoner Summoner { get; set; }

        [InternalName("summonerLevel")]
        public SummonerLevel SummonerLevel { get; set; }

        [InternalName("summonerLevelAndPoints")]
        public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

        [InternalName("spellBook")]
        public SpellBook SpellBook { get; set; }

        [InternalName("summonerDefaultSpells")]
        public SummonerDefaultSpells SummonerDefaultSpells { get; set; }

        [InternalName("summonerTalentsAndPoints")]
        public SummonerDefaultSpells SummonerTalentsAndPoints { get; set; }

        #endregion
    }
}
