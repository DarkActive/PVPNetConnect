using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Leagues
{
    /// <summary>
    /// Class with specific information about a summoner's leagues
    /// </summary>
    public class SummonerLeagues : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerLeagues"/> class.
        /// </summary>
        /// <param name="callback">The callback method.</param>
        public SummonerLeagues(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerLeagues"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SummonerLeagues(TypedObject result)
        {
            base.SetFields<SummonerLeagues>(this, result);
        }

        /// <summary>
        /// Delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(SummonerLeagues result);

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
            base.SetFields<SummonerLeagues>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// List of all leagues summoner is part of.
        /// </summary>
        [InternalName("summonerLeagues")] 
        public List<League> Leagues { get; set; }

        #endregion
    }
}
