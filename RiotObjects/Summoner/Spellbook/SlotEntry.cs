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

        /// <summary>
        /// Rune ID number.
        /// </summary>
        [InternalName("runeId")]
        public int RuneID { get; set; }

        /// <summary>
        /// Rune Slot ID number.
        /// </summary>
        [InternalName("runeSlotId")]
        public int RuneSlotID { get; set; }

        /// <summary>
        /// RuneSlot class with information about Rune Slot.
        /// </summary>
        [InternalName("runeSlot")]
        public RuneSlot RuneSlot { get; set; }

        /// <summary>
        /// Rune class with information about rune.
        /// </summary>
        [InternalName("rune")]
        public Rune Rune { get; set; }

        #endregion
    }
}
