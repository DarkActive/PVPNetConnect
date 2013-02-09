using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Game
{
    /// <summary>
    /// Class with in progress game and spectator info.
    /// </summary>
    public class PlayerChampionSelection : RiotGamesObject
    {
        #region Constructors and Callbacks

        public PlayerChampionSelection(TypedObject result)
        {
            base.SetFields<PlayerChampionSelection>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Summoner name of the player.
        /// </summary>
        [InternalName("summonerInternalName")]
        public string SummonerInternalName { get; set; }

        /// <summary>
        /// The skin ID that is selected by player.
        /// </summary>
        [InternalName("selectedSkinIndex")]
        public int SelectedSkinID { get; set; }

        /// <summary>
        /// The selected summoner spell 1 (D spell).
        /// </summary>
        [InternalName("spell1Id")]
        public int Spell1ID { get; set; }

        /// <summary>
        /// The ID number of the selected summoner spell 2 (F spell).
        /// </summary>
        [InternalName("spell2Id")]
        public int Spell2ID { get; set; }

        /// <summary>
        /// The ID number of the selected champion.
        /// </summary>
        [InternalName("championId")]
        public int ChampionID { get; set; }

        #endregion
    }
}
