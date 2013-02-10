using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    /// <summary>
    /// Class that defines a specific Rune and its information.
    /// </summary>
    public class Rune : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="Rune"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public Rune(TypedObject result)
        {
            base.SetFields<Rune>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// String to image path (usually set to null).
        /// </summary>
        [InternalName("imagePath")]
        public string ImagePath { get; set; }

        /// <summary>
        /// Tool tip for rune (usually set to null).
        /// </summary>
        [InternalName("toolTip")]
        public string ToolTip { get; set; }

        /// <summary>
        /// Tier of rune (1, 2, or 3).
        /// </summary>
        [InternalName("tier")]
        public int Tier { get; set; }

        /// <summary>
        /// Item ID of the rune.
        /// </summary>
        [InternalName("itemId")]
        public int ItemID { get; set; }

        /// <summary>
        /// Class that specifies the rune type.
        /// </summary>
        [InternalName("runeType")]
        public int RuneType { get; set; }

        /// <summary>
        /// Duration of rune (probably only affected for temporary runes).
        /// </summary>
        [InternalName("duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Game code of the rune (game client code).
        /// </summary>
        [InternalName("gameCode")]
        public int GameCode { get; set; }

        /// <summary>
        /// List of Item Effects.
        /// </summary>
        [InternalName("itemEffects")]
        public List<ItemEffect> ItemEffects { get; set; }

        /// <summary>
        /// Type of this object (set to "RUNE" in this class.
        /// </summary>
        [InternalName("baseType")]
        public string BaseType { get; set; }

        /// <summary>
        /// Description of the rune.
        /// </summary>
        [InternalName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Name of the rune.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Unknown significance (set to null).
        /// </summary>
        [InternalName("uses")]
        public string Uses { get; set; }

        #endregion
    }
}
