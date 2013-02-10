using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    /// <summary>
    /// Class that defines the item effect in game.
    /// </summary>
    public class ItemEffect : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemEffect"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ItemEffect(TypedObject result)
        {
            base.SetFields<ItemEffect>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// ID number of effect.
        /// </summary>
        [InternalName("effectId")]
        public int EffectId { get; set; }

        /// <summary>
        /// ID number of item effect.
        /// </summary>
        [InternalName("itemEffectId")]
        public int ItemEffectId { get; set; }

        /// <summary>
        /// Class with effect information.
        /// </summary>
        [InternalName("effect")]
        public Effect Effect { get; set; }

        /// <summary>
        /// Value of the item effect.
        /// </summary>
        [InternalName("value")]
        public float Value { get; set; }

        /// <summary>
        /// ID number of the item.
        /// </summary>
        [InternalName("itemId")]
        public int ItemId { get; set; }

        #endregion
    }
}
