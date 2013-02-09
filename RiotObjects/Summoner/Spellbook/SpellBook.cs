using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SpellBook : RiotGamesObject
    {
        #region Constructors and Callbacks
        
        public SpellBook(Callback callback)
        {
            this.callback = callback;
        }

        public SpellBook(TypedObject result)
        {
            base.SetFields<SpellBook>(this, result);
        }

        public delegate void Callback(SpellBook result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<SpellBook>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Rune book pages list.
        /// </summary>
        [InternalName("bookPages")]
        public List<SpellBookPage> RunePagesList { get; set; }

        /// <summary>
        /// Create date of rune book.
        /// </summary>
        [InternalName("dateString")]
        public string DateString { get; set; }

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public long SummonerID { get; set; }

        #endregion
    }
}
