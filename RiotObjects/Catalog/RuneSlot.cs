using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    /// <summary>
    /// Class that defines a specific rune slot and its infromation.
    /// </summary>
    public class RuneSlot : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="RuneSlot"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public RuneSlot(TypedObject result)
        {
            base.SetFields<RuneSlot>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// ID number of rune slot.
        /// </summary>
        [InternalName("id")]
        public int RuneSlotID { get; set; }

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
