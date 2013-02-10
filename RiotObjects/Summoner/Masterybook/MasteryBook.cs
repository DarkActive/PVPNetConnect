using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects.Summoner
{
    /// <summary>
    /// The class that defines a mastery book.
    /// </summary>
    public class MasteryBook : RiotGamesObject
    {
        #region Constructors and Callbacks

        /// <summary>
        /// Initializes a new instance of the <see cref="MasteryBook"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public MasteryBook(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MasteryBook"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public MasteryBook(TypedObject result)
        {
            base.SetFields<MasteryBook>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(MasteryBook result);

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
            base.SetFields<MasteryBook>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        /// <summary>
        /// Summoner ID number.
        /// </summary>
        [InternalName("summonerId")]
        public int SummonerID { get; set; }

        /// <summary>
        /// Believe this is date entire mastery book was created.
        /// </summary>
        [InternalName("dateString")]
        public DateTime DateString { get; set; }

        /// <summary>
        /// List of all the Mastery book pages.
        /// </summary>
        [InternalName("bookPages")]
        public List<MasteryBookPage> MasteryPagesList { get; set; }

        #endregion
    }
}
