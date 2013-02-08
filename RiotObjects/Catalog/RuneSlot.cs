using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    public class RuneSlot : RiotGamesObject
    {
        #region Constructors and Callbacks

        public RuneSlot(Callback callback)
        {
            this.callback = callback;
        }

        public RuneSlot(TypedObject result)
        {
            base.SetFields<RuneSlot>(this, result);
        }


        public delegate void Callback(RuneSlot result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<RuneSlot>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// ID number of rune slot.
        /// </summary>
        [InternalName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Minimum level required for rune slot.
        /// </summary>
        [InternalName("minLevel")]
        public int MinLevel { get; set; }

        /// <summary>
        /// Class that describes this slot's rune type.
        /// </summary>
        [InternalName("runeType")]
        public RuneType RuneType { get; set; }

        #endregion
    }
}
