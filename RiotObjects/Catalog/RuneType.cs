using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    /// <summary>
    /// Class that defines a specific rune type and its infromation.
    /// </summary>
    public class RuneType : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="RuneType"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
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
