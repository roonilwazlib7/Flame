using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame
{
    public class GameThing
    {
        private Game _game;
        private Dictionary<string, List< Func<Message, Message>  > > _messageListeners;
        private int _uid;
        private bool _trash = false;

        public void Update()
        {

        }
        public void Draw()
        {
            
        }

        public void On(string messageKey, Func<Message, Message> delagte)
        {
            if (_messageListeners.ContainsKey(messageKey))
            {
                _messageListeners[messageKey].Add(delagte);
            }
            else
            {
                _messageListeners.Add(messageKey, new List< Func<Message, Message> >() { delagte });
            }
        }

        public void Emit(string messageKey, Message message)
        {
            if (_messageListeners.ContainsKey(messageKey))
            {
                foreach (Func<Message, Message> listener in _messageListeners[messageKey])
                {
                    listener(message);
                }
            }
        }

        public int Uid
        {
            get
            {
                return _uid;
            }
            set
            {
                _uid = value;
            }
        }

        public Game Game
        {
            get
            {
                return _game;
            }
            set
            {
                _game = value;
            }
        }
    }
}
