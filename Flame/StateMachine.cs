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

        public void AddState(string id, State<T> state)
        {
            _states.Add(id, state);
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
        public virtual void Start(T controlObject) { }
        public virtual void Update(T controlObject) { }
        public virtual void End(T controlObject) { }
    }
}
