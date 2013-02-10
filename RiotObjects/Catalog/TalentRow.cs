using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    /// <summary>
    /// Class that defines a specific TalentRow
    /// </summary>
    public class TalentRow : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="TalentRow"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public TalentRow(TypedObject result)
        {
            base.SetFields<TalentRow>(this, result);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Index of talent group.
        /// </summary>
        [InternalName("index")]
        public int Index { get; set; }

        /// <summary>
        /// List of talent rows.
        /// </summary>
        [InternalName("talents")]
        public List<Talent> TalentList { get; set; }

        /// <summary>
        /// Talent Group ID number.
        /// </summary>
        [InternalName("tltGroupId")]
        public int TalentGroupID { get; set; }

        /// <summary>
        /// Points to activate the talent row.
        /// </summary>
        [InternalName("pointsToActivate")]
        public int PointsToActivate { get; set; }

        /// <summary>
        /// Talent Row ID number.
        /// </summary>
        [InternalName("tltRowId")]
        public int TalentRowID { get; set; }

        #endregion
    }
}
