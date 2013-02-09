using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    public class RuneType : RiotGamesObject
    {
        #region Constructors and Callbacks

        public RuneType(TypedObject result)
        {
            base.SetFields<RuneType>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// ID number of rune type.
        /// </summary>
        [InternalName("runeTypeId")]
        public int RuneTypeID { get; set; }

        /// <summary>
        /// Name of rune type.
        /// </summary>
        [InternalName("name")]
        public string Name { get; set; }

        #endregion
    }
}
