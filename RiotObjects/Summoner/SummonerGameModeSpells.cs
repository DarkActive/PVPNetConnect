using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SummonerGameModeSpells : RiotGamesObject
    {
        #region Constructors and Callbacks

        public SummonerGameModeSpells(Callback callback)
        {
            this.callback = callback;
        }

        public SummonerGameModeSpells(TypedObject result)
        {
            base.SetFields<SummonerGameModeSpells>(this, result);
        }

        public delegate void Callback(SummonerGameModeSpells result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields <SummonerGameModeSpells>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Summoner spell ID in D slot.
        /// </summary>
        [InternalName("spell1Id")]
        public int Spell1Id { get; set; }

        /// <summary>
        /// Summoner spell ID in F slot.
        /// </summary>
        [InternalName("spell2Id")]
        public int Spell2Id { get; set; }

        #endregion
    }
}
