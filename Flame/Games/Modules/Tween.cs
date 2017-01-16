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
            return new TweenSetup<T>(objectToTween);
        }
        public void CreateSpline()
        {

        }
    }

    public class TweenObject
    {

    }

    public class TweenSetup<T>
    {
        T _objectToTween;
        public TweenSetup(T objectToTween)
        {
            _objectToTween = objectToTween;
        }
        public void From(T Start, T End)
        {
            
        }
    }
}
