using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.RiotObjects
{
    public class UnclassedObject : RiotGamesObject
    {
        public UnclassedObject(Callback callback)
        {
            this.callback = callback;
        }

        public delegate void Callback(TypedObject result);
        private Callback callback;

        public override void DoCallback(TypedObject result)
        {
            callback(result);
        }
    }
}
