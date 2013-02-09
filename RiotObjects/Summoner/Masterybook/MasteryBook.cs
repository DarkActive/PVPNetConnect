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

        //TODO: Finish this
        [InternalName("summonerId")]
        public int SummonerId { get; set; }

        #endregion
    }
}
