using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    public class ItemEffect : RiotGamesObject
    {
        #region Constructors and Callbacks

        public ItemEffect(Callback callback)
        {
            this.callback = callback;
        }

        public ItemEffect(TypedObject result)
        {
            base.SetFields<ItemEffect>(this, result);
        }


        public delegate void Callback(ItemEffect result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields <ItemEffect>(this, result);
            callback(this);
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
