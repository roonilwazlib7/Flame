using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flame
{
    public class StateMachine<T>
    {
        private Dictionary<string, State<T>> _states = new Dictionary<string, State<T>>();
        private State<T> _currentState;

        public static StateMachine<T> Create(T controlObject)
        {
            return new StateMachine<T>(controlObject);
        }

        public StateMachine(T controlObject)
        {
            ControlObject = controlObject;
        }

        public T ControlObject { get; set; }
        public State<T> CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public void AddState(string id, State<T> state)
        {
            _states.Add(id, state);
            state.StateMachine = this;
            state.Name = id;
        }

        public void Switch(string id)
        {
            if (_currentState != null)
            {
                _currentState.End(ControlObject);
            }

            _currentState = _states[id];

            _currentState.Start(ControlObject);
        }
        
        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.Update(ControlObject);
            }
        }
    }

    public class State<T>
    {
        private Dictionary<string, List<Func<Message, Message>>> _messageListeners;
        public State()
        {
            _messageListeners = new Dictionary<string, List<Func<Message, Message>>>();
        }
        public string Name { get; set; }
        public StateMachine<T> StateMachine { get; set; }
        public virtual void Start(T controlObject) { }
        public virtual void Update(T controlObject) { }
        public virtual void End(T controlObject) { }

        public void On(string messageKey, Func<Message, Message> delagte)
        {
            if (_messageListeners.ContainsKey(messageKey))
            {
                _messageListeners[messageKey].Add(delagte);
            }
            else
            {
                _messageListeners.Add(messageKey, new List<Func<Message, Message>>() { delagte });
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
    }
}
