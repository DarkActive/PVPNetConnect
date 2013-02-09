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

        [InternalName("bookPages")]
        public List<SpellBookPage> BookPages { get; set; }

        [InternalName("dateString")]
        public string DateString { get; set; }

        [InternalName("summonerId")]
        public long SummonerId { get; set; }

        #endregion
    }
}
