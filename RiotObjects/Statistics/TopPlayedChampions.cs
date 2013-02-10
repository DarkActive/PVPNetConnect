using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    /// <summary>
    /// The class that defines top played champions.
    /// </summary>
    public class TopPlayedChampions : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="TopPlayedChampions"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public TopPlayedChampions(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopPlayedChampions"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public TopPlayedChampions(TypedObject result)
        {
            base.SetFields<TopPlayedChampions>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(TopPlayedChampions result);

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
