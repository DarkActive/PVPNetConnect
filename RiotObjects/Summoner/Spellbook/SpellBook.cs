using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines a spellbook.
    /// </summary>
    public class SpellBook : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellBook"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public SpellBook(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellBook"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public SpellBook(TypedObject result)
        {
            base.SetFields<SpellBook>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(SpellBook result);

        /// <summary>
        /// The callback method.
        /// </summary>
        private Callback callback;

        /// <summary>
        /// The DoCallback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<SpellBook>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Rune book pages list.
        /// </summary>
        [InternalName("bookPages")]
        public List<SpellBookPage> RunePagesList { get; set; }

        /// <summary>
        /// Create date of rune book.
        /// </summary>
        [InternalName("dateString")]
        public string DateString { get; set; }

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public long SummonerID { get; set; }

        #endregion
    }
}
