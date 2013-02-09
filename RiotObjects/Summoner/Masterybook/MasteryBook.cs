using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class MasteryBook : RiotGamesObject
    {
        #region Constructors and Callbacks

        public MasteryBook(Callback callback)
        {
            this.callback = callback;
        }

        public MasteryBook(TypedObject result)
        {
            base.SetFields<MasteryBook>(this, result);
        }

        public delegate void Callback(MasteryBook result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<MasteryBook>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerID { get; set; }

        /// <summary>
        /// Believe this is date entire mastery book was created.
        /// </summary>
        [InternalName("dateString")]
        public DateTime DateString { get; set; }

        /// <summary>
        /// List of all the Mastery book pages.
        /// </summary>
        [InternalName("bookPages")]
        public List<MasteryBookPage> MasteryPagesList { get; set; }

        #endregion
    }
}
