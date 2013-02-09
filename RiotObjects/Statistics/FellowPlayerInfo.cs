using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Statistics
{
    public class FellowPlayerInfo : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Create an empty Class
        /// </summary>
        public FellowPlayerInfo()
        {
        }

        /// <summary>
        /// Create the Class with a callback.
        /// </summary>
        /// <param name="callback"></param>
        public FellowPlayerInfo(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Create the Class with a TypedObject result
        /// </summary>
        /// <param name="result"></param>
        public FellowPlayerInfo(TypedObject result)
        {
            base.SetFields<FellowPlayerInfo>(this, result);
        }

        /// <summary>
        /// Delegate method for Callback
        /// </summary>
        /// <param name="result"></param>
        public delegate void Callback(FellowPlayerInfo result);

        /// <summary>
        /// The callback member.
        /// </summary>
        private Callback callback;

        /// <summary>
        /// The DOCallback method.
        /// </summary>
        /// <param name="result"></param>
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<FellowPlayerInfo>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The champion ID number.
        /// </summary>
        [InternalName("championId")]
        public int ChampionID { get; set; }

        /// <summary>
        /// The Team ID number (100 = blue, 200 = purple).
        /// </summary>
        [InternalName("teamId")]
        public int TeamID { get; set; }

        /// <summary>
        /// The summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerID { get; set; }

        #endregion
    }
}
