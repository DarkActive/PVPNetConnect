using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines all public summoner data.
    /// </summary>
    public class AllPublicSummonerData : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="AllPublicSummonerData"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public AllPublicSummonerData(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllPublicSummonerData"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public AllPublicSummonerData(TypedObject result)
        {
            base.SetFields<AllPublicSummonerData>(this, result);
        }


        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(AllPublicSummonerData result);

        /// <summary>
        /// The callback method.
        /// </summary>
        private Callback callback;

        /// <summary>
        /// The DoCallback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<AllPublicSummonerData>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The Summoner information.
        /// </summary>
        [InternalName("summoner")]
        public Summoner Summoner { get; set; }

        /// <summary>
        /// The summoner level information.
        /// </summary>
        [InternalName("summonerLevel")]
        public SummonerLevel SummonerLevel { get; set; }

        /// <summary>
        /// The summoner level and points information.
        /// </summary>
        [InternalName("summonerLevelAndPoints")]
        public SummonerLevelAndPoints SummonerLevelAndPoints { get; set; }

        /// <summary>
        /// The spellbook or rune book.
        /// </summary>
        [InternalName("spellBook")]
        public SpellBook SpellBook { get; set; }

        /// <summary>
        /// The default summoner spells information.
        /// </summary>
        [InternalName("summonerDefaultSpells")]
        public SummonerDefaultSpells SummonerDefaultSpells { get; set; }

        /// <summary>
        /// The summoner talents and points information.
        /// </summary>
        [InternalName("summonerTalentsAndPoints")]
        public SummonerTalentsAndPoints SummonerTalentsAndPoints { get; set; }

        #endregion
    }
}
