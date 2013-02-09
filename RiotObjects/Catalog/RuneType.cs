using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    public class RuneType : RiotGamesObject
    {
        #region Constructors and Callbacks

        public RuneType(Callback callback)
        {
            this.callback = callback;
        }

        public RuneType(TypedObject result)
        {
            base.SetFields<RuneType>(this, result);
        }


        public delegate void Callback(RuneType result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<RuneType>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// ID number of rune type.
        /// </summary>
        [InternalName("runeTypeId")]
        public int RuneTypeId { get; set; }

        /// <summary>
        /// Name of rune type.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        #endregion
    }
}
