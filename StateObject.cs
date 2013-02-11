using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PVPNetConnect
{
    internal sealed class StateObject
    {
        private readonly ManualResetEvent _done = new ManualResetEvent(false);

        public int BytesRead { get; set; }
        public ManualResetEvent Done { get { return _done; } }
    }
}
