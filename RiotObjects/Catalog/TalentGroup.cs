using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Catalog
{
    /// <summary>
    /// Class that defines a specific TalentGroup
    /// </summary>
    public class TalentGroup : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="TalentGroup"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public TalentGroup(TypedObject result)
        {
            base.SetFields<TalentGroup>(this, result);
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
        [InternalName("talentRows")]
        public List<TalentRow> TalentRowList { get; set; }

        /// <summary>
        /// Talent group name.
        /// </summary>
        [InternalName("name")]
        public string TalentGroupName { get; set; }

        /// <summary>
        /// Talent Group ID number.
        /// </summary>
        [InternalName("tltGroupId")]
        public int TalentGroupID { get; set; }

        #endregion
    }
}
