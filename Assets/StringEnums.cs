using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PVPNetConnect.Assets
{
    /// <summary>
    /// Game Modes enumerator.
    /// </summary>
    public enum GameModes
    {
        /// <summary>
        /// CLASSIC enum
        /// </summary>
        [StringValue("CLASSIC")]
        CLASSIC = 1,
        /// <summary>
        /// DOMINION enum
        /// </summary>
        [StringValue("DOMINION")]
        DOMINION = 2,
        /// <summary>
        /// ARAM enum
        /// </summary>
        [StringValue("ARAM")]
        ARAM = 3
    }

    /// <summary>
    /// Seasons enumerator.
    /// </summary>
    public enum Seasons
    {
        /// <summary>
        /// Current Season Enum
        /// </summary>
        [StringValue("CURRENT")]
        CURRENT = 0,
        /// <summary>
        /// Season 1 Enum
        /// </summary>
        [StringValue("ONE")]
        ONE = 1,
        /// <summary>
        /// Season 2 Enum
        /// </summary>
        [StringValue("TWO")]
        TWO = 2,
        /// <summary>
        /// Season 3 Enum
        /// </summary>
        [StringValue("THREE")]
        THREE = 3
    }

    /// <summary>
    /// Game types enumerator.
    /// </summary>
    public enum GameTypes
    {
        /// <summary>
        /// Normal game enum.
        /// </summary>
        [StringValue("NORMAL_GAME")]
        NORMAL_GAME = 0,
        /// <summary>
        /// Ranked game enum.
        /// </summary>
        [StringValue("RANKED_GAME")]
        RANKED_GAME = 1,
        /// <summary>
        /// Matched game enum.
        /// </summary>
        [StringValue("MATCHED_GAME")]
        MATCHED_GAME = 2,
        /// <summary>
        /// Practice game enum.
        /// </summary>
        [StringValue("PRACTICE_GAME")]
        PRACTICE_GAME = 3
    }

    /// <summary>
    /// Queue types Enumeartor.
    /// </summary>
    public enum QueueTypes
    {
        /// <summary>
        /// Normal 5x5 queue enum.
        /// </summary>
        [StringValue("NORMAL_5x5")]
        NORMAL_5x5 = 0,
        /// <summary>
        /// Ranked Solo 5x5 queue enum.
        /// </summary>
        [StringValue("RANKED_SOLO_5x5")]
        RANKED_SOLO_5x5 = 1,
        /// <summary>
        /// Ranked team 5x5 queue enum.
        /// </summary>
        [StringValue("RANKED_TEAM_5x5")]
        RANKED_TEAM_5x5 = 2,
        /// <summary>
        /// Ranked team 3x3 queue enum.
        /// </summary>
        [StringValue("RANKED_TEAM_3x3")]
        RANKED_TEAM_3x3 = 3
    }

    /// <summary>
    /// The StringEnum value with GetStringValue method
    /// </summary>
    public static class StringEnum
    {
        /// <summary>
        /// Gets the string value from Atrribute.
        /// </summary>
        /// <param name="value">Enum value.</param>
        /// <returns></returns>
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

    /// <summary>
    /// StringValue class for System.Attribute
    /// </summary>
    public class StringValue : System.Attribute
    {
        /// <summary>
        /// The value of StringValue
        /// </summary>
        private string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringValue(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The stringvalue value
        /// </value>
        public string Value
        {
            get { return _value; }
        }
    }
}
