using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SummonerTalentsAndPoints : RiotGamesObject
    {
        #region Constructors and Callbacks

        public SummonerTalentsAndPoints(Callback callback)
        {
            this.callback = callback;
        }

        public SummonerTalentsAndPoints(TypedObject result)
        {
            base.SetFields<SummonerTalentsAndPoints>(this, result);
        }

        public delegate void Callback(SummonerTalentsAndPoints result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<SummonerTalentsAndPoints>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Number of total talent points.
        /// </summary>
        [InternalName("talentPoints")]
        public int TalentPoints { get; set; }

        /// <summary>
        /// Date/time since modified.
        /// </summary>
        [InternalName("modifyDate")]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Date/time when created.
        /// </summary>
        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Date/time since modified.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerId { get; set; }

        #endregion
    }
}
