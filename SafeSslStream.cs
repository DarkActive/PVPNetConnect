using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Security;

namespace PVPNetConnect
{
    internal sealed class SafeSslStream
    {
        private readonly object _streamLock = new object();
        private readonly SslStream _stream;

        public SafeSslStream(SslStream stream)
        {
            _stream = stream;
        }

        public IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState)
        {
            return _stream.BeginAuthenticateAsClient(targetHost, asyncCallback, asyncState);
        }

        public void EndAuthenticateAsClient(IAsyncResult asyncResult)
        {
            _stream.EndAuthenticateAsClient(asyncResult);
        }

        public int Read(byte[] buffer)
        {
            return Read(buffer, 0, buffer.Length);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            var state = new StateObject();
            lock (_streamLock)
            {
                _stream.BeginRead(buffer, offset, count, ReadCallback, state);
            }
            state.Done.WaitOne();
            return state.BytesRead;
        }

        public int ReadByte()
        {
            byte[] buffer = new byte[1];

            var state = new StateObject();
            lock (_streamLock)
            {
                _stream.BeginRead(buffer, 0, 1, ReadCallback, state);
            }
            state.Done.WaitOne();
            return buffer[0];
        }

        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            var state = new StateObject();
            lock (_streamLock)
            {
                _stream.BeginWrite(buffer, offset, count, WriteCallback, state);
            }
            state.Done.WaitOne();
        }

        private void ReadCallback(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;
            lock (_streamLock)
            {
                state.BytesRead = _stream.EndRead(ar);
            }
            state.Done.Set();
        }

        private void WriteCallback(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;
            lock (_streamLock)
            {
                _stream.EndWrite(ar);
            }
            state.Done.Set();
        }
    }
}
