using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    public class SpellBookPage : RiotGamesObject
    {
        #region Constructors and Callbacks

        public SpellBookPage(TypedObject result)
        {
            base.SetFields<SpellBookPage>(this, result);
        }

        #endregion

        #region Member Properties

        [InternalName("pageId")]
        public long PageId { get; set; }

        [InternalName("name")]
        public string Name { get; set; }

        [InternalName("current")]
        public bool Current { get; set; }

        [InternalName("slotEntries")]
        public List<SlotEntry> slotEntries { get; set; }

        [InternalName("createDate")]
        public DateTime CreateDate { get; set; }

        [InternalName("summonerId")]
        public bool SummonerId { get; set; }

        #endregion
    }
}
