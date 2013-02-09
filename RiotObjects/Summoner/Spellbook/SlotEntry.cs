using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PVPNetConnect.RiotObjects.Catalog;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SlotEntry : RiotGamesObject
    {
        #region Constructors and Callbacks

        public SlotEntry(TypedObject result)
        {
            base.SetFields<SlotEntry>(this, result);
        }

        #endregion

        #region Member Properties

        [InternalName("runeId")]
        public int RuneId { get; set; }

        [InternalName("runeSlotId")]
        public int RuneSlotId { get; set; }

        [InternalName("runeSlot")]
        public RuneSlot RuneSlot { get; set; }

        [InternalName("rune")]
        public Rune Rune { get; set; }

        #endregion
    }
}
