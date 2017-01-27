using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame
{
    public delegate void FlameMessageHandler(object sender, Message m);

    public class Message
    {
        private object _sender;
        private dynamic _data;

        public Message(object sender)
        {
            _sender = sender;
        }

        public dynamic Data
        {
            get
            {
                return _data;
            }
        }

        public T Sender<T>()
        {
            if (_sender is T)
            {
                return (T)_sender;
            }
            else
            {
                throw new Exception("Unable to sender of specified Type");
            }
        }
    }
}
