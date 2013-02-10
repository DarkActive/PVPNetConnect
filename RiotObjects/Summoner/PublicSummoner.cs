using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// Class with public summoner information.
    /// </summary>
    public class PublicSummoner : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicSummoner"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public PublicSummoner(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicSummoner"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PublicSummoner(TypedObject result)
        {
            base.SetFields<PublicSummoner>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(PublicSummoner result);

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
            base.SetFields<PublicSummoner>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// The name of the summoner.
        /// </summary>
        [InternalName("InternalName")]
        public string InternalName { get; set; }

        /// <summary>
        /// The name of the summoner.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The account ID number.
        /// </summary>
        [InternalName("acctId")]
        public int AccountId { get; set; }

        /// <summary>
        /// The ID number of summoner's profile icon.
        /// </summary>
        [InternalName("profileIconId")]
        public int ProfileIconId { get; set; }

        /// <summary>
        /// Date/time this information was revised.
        /// </summary>
        [InternalName("revisionDate")]
        public DateTime RevisionDate { get; set; }

        /// <summary>
        /// NOT IMPLEMENTED.
        /// </summary>
        [InternalName("revisionId")]
        public int RevisionId { get; set; }

        /// <summary>
        /// The current level of the summoner.
        /// </summary>
        [InternalName("summonerLevel")]
        public int SummonerLevel { get; set; }

        /// <summary>
        /// The summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerId { get; set; }

        #endregion
    }
}
