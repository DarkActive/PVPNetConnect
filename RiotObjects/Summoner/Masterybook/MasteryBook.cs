using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class MasteryBook : RiotGamesObject
    {
        #region Constructors and Callbacks

        public MasteryBook(TypedObject result)
        {
            base.SetFields<MasteryBook>(this, result);
        }

        public override void DoCallback(TypedObject obj)
        {
            return;
        }

        #endregion

        #region Member Properties

        //TODO: Finish this
        [InternalName("summonerId")]
        public int SummonerId { get; set; }

        #endregion
    }
}
