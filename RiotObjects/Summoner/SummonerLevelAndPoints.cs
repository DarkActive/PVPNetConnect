using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SummonerLevelAndPoints : RiotGamesObject
    {
        #region Constructors and Callbacks

        public SummonerLevelAndPoints(Callback callback)
        {
            this.callback = callback;
        }

        public SummonerLevelAndPoints(TypedObject result)
        {
            base.SetFields<SummonerLevelAndPoints>(this, result);
        }


        public delegate void Callback(SummonerLevelAndPoints result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<SummonerLevelAndPoints>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Total influence points (IP) earned in lifetime.
        /// </summary>
        [InternalName("infPoints")]
        public int InfPoints { get; set; }

        /// <summary>
        /// Amount of experience points (default = 10 if summoner level is 30).
        /// </summary>
        [InternalName("expPoints")]
        public int ExpPoints { get; set; }

        /// <summary>
        /// Current level of summoner.
        /// </summary>
        [InternalName("summonerLevel")]
        public int SummonerLevel { get; set; }

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerId { get; set; }

        #endregion
    }
}
