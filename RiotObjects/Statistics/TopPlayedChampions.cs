using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class TopPlayedChampions : RiotGamesObject
    {
        #region Constructors and Callbacks

        public TopPlayedChampions(Callback callback)
        {
            this.callback = callback;
        }

        public TopPlayedChampions(TypedObject result)
        {
            base.SetFields<TopPlayedChampions>(this, result);
        }

        public delegate void Callback(TopPlayedChampions result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<TopPlayedChampions>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Total amount of games played with champion.
        /// </summary>
        [InternalName("")]
        public List<ChampionStatInfo> ChampionList { get; set; }

        #endregion
    }
}
