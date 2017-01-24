using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame;
using Flame.Debug;
using Flame.Geometry;

namespace Flame.Games.Modules
{
    public class Caster:Module
    {
        public Caster(Game game):base(game)
        {

        }

        public void CastRay(Line ray, Func<object,object> callback)
        {
            Game.Jobs.Add(delegate(object o)
            {
                DebugConsole.Output("Flame", "Casting ray");
                return new object();

            }, callback);
        }
    }
}
