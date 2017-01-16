using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Flame.Games.Modules
{
    public enum Easing
    {
        Smooth,
        Linear
    }
    public class Tween: Module
    {
        public Tween(Game game): base(game)
        {

        }
        public void Update()
        {

        }
        public TweenSetup<T> CreateTween<T>(T objectToTween, double time, Easing easing = Easing.Smooth)
        {
            return new TweenSetup<T>(objectToTween, Game, time, easing);
        }
        public void CreateSpline()
        {

        }
    }

    public class TweenObject<T>: GameThing
    {
        T _objectThatIsTweening;
        T _objectToTweenTo;
        double _finalTime = 0;
        double _elapsedTime = 0;
        List<PropertyInfo> _tweenProperties = new List<PropertyInfo>();
        Easing _easing;
        public TweenObject(T objectThatIsTweening, T objectToTweenTo, Game game, double finalTime, Easing easing): base()
        {
            Game = game;
            _objectThatIsTweening = objectThatIsTweening;
            _objectToTweenTo = objectToTweenTo;
            _finalTime = finalTime * 2;
            _easing = easing;
            SetUpProperties();
            game.Add(this);
        }
        public override void Update()
        {
            _elapsedTime += Game.Delta;
            double normalizedTime = _elapsedTime / _finalTime;
            double easedTime = Ease(normalizedTime);

            foreach (PropertyInfo p in _tweenProperties)
            {
                double? prop = p.GetValue(_objectToTweenTo) as double?;
                double? origProp = p.GetValue(_objectThatIsTweening) as double?;
                // can it be converted to a number...
                if (prop == null || origProp == null)
                {
                    continue;
                }
                double normalizedProp = (double)origProp / (double)prop;
                double tweenedProp = ((double)prop * easedTime) + ((double)origProp * (1 - easedTime));
                p.SetValue(_objectThatIsTweening, tweenedProp);
            }

            if (_elapsedTime >= _finalTime)
            {
                Trash();
            }
        }
        double Ease(double normalizedTime)
        {
            switch(_easing)
            {
                case Easing.Linear:
                    return normalizedTime;
                case Easing.Smooth:
                    return normalizedTime * normalizedTime * (3 - 2 * normalizedTime);
                default:
                    return normalizedTime;
            }
        }
        void SetUpProperties()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in properties)
            {
                double? prop = p.GetValue(_objectToTweenTo) as double?;
                // can it be converted to a number...
                if (prop == null)
                {
                    continue;
                }

                if (p.GetSetMethod(false) == null) { continue; }
                if (p.GetGetMethod(false) == null) { continue; }

                _tweenProperties.Add(p);
                
            }
        }
    }

    public class TweenSetup<T>
    {
        T _objectToTween;
        Game _game;
        double _time;
        Easing _easing;

        public TweenSetup(T objectToTween, Game game, double time, Easing easing)
        {
            _objectToTween = objectToTween;
            _game = game;
            _time = time;
            _easing = easing;
        }
        public TweenSetup<T> From(T Start)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(PropertyInfo p in properties)
            {
                double? prop = p.GetValue(Start) as double?;
                // can it be converted to a number...
                if (prop == null)
                {
                    continue;
                }

                if (p.GetSetMethod(false) == null) { continue; }
                if (p.GetGetMethod(false) == null) { continue; }


                p.SetValue(_objectToTween, prop);
            }
            return this;
        }

        public TweenObject<T> To(T End)
        {
            return new TweenObject<T>(_objectToTween, End, _game, _time, _easing);
        }
    }
}
