using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Games;

namespace Flame
{
    public class GameThing: IDisposable, IComparable<GameThing>
    {
        private Game _game;
        private Dictionary<string, List< Func<Message, Message>  > > _messageListeners;
        private int _uid = 0;
        private int _layerIndex = 0;
        private bool _trashed = false;


        public GameThing()
        {
            _messageListeners = new Dictionary<string, List<Func<Message, Message>>>();
        }

        public virtual void Update()
        {

        }
        public virtual void Draw()
        {
            
        }
        public void Trash()
        {
            _trashed = true;
            Emit("Trash", new Message(this));
        }
        public void Dispose()
        {
            // this is were we need to clear up some things
            Emit("Dispose", new Message(this));
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

        public int CompareTo(GameThing thing)
        {
            if (thing == null)
            {
                return 1;
            }
            else if (LayerIndex == thing.LayerIndex)
            {
                return Uid.CompareTo(thing.Uid);
            }
            else
            {
                return LayerIndex.CompareTo(thing.LayerIndex);
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

        public bool Trashed
        {
            get
            {
                return _trashed;
            }
        }

        public int LayerIndex
        {
            get
            {
                return _layerIndex;
            }
            set
            {
                _layerIndex = value;
            }
        }

        public static bool operator== (GameThing thing1, GameThing thing2)
        {
            if (thing2 == null || thing1 == null)
            {
                return false;
            }
            return thing1.Uid == thing2.Uid;
        }

        public static bool operator !=(GameThing thing1, GameThing thing2)
        {
            return thing1.Uid != thing2.Uid;
        }
    }
}
