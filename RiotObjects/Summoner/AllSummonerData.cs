using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class AllSummonerData : RiotGamesObject
    {
        #region Constructors and Callbacks

        public AllSummonerData(Callback callback)
        {
            this.callback = callback;
        }

        public AllSummonerData(TypedObject result)
        {
            base.SetFields<AllSummonerData>(this, result);
        }


        public delegate void Callback(AllSummonerData result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<AllSummonerData>(this, result);
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

        [InternalName("masteryBook")]
        public MasteryBook MasteryBook { get; set; }

        #endregion
    }
}
