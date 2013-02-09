using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PVPNetConnect.Assets
{
    public static class StringEnum
    {
        public enum GameModes
        {
            [StringValue("CLASSIC")]
            CLASSIC = 1,
            [StringValue("DOMINION")]
            DOMINION = 2,
            [StringValue("ARAM")]
            ARAM = 3
        }

        public enum Seasons
        {
            [StringValue("CURRENT")]
            CURRENT = 0,
            [StringValue("ONE")]
            ONE = 1,
            [StringValue("TWO")]
            TWO = 2,
            [StringValue("THREE")]
            THREE = 3
        }

        public enum GameTypes
        {
            [StringValue("NORMAL_GAME")]
            NORMAL_GAME = 0,
            [StringValue("RANKED_GAME")]
            RANKED_GAME = 1,
            [StringValue("PRACTICE_GAME")]
            PRACTICE_GAME = 3
        }

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

    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}
