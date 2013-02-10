using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// Class to handle riot return of summonerName array.
    /// </summary>
    public class SummonerNames : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SummonerNames"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public SummonerNames(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(object[] result);

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
            callback(result.GetArray("array"));
        }

        #endregion
    }
}
