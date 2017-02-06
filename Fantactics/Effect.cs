using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantactics
{
    class Effect
    {
        public Unit Unit {get; set;}
        public int TurnsLeft {get; set;}

        public const int LASTS_FOREVER = -1000;

        public Unit(Unit unit)
        {
            Unit = unit;

            unit.OnTurnEnd += TurnEnd;
            unit.OnTurnStart += TurnStart;
        }

        public void TurnStart(object sender, Message m)
        {
            if (TurnsLeft <= 0 && TurnsLeft != LASTS_FOREVER)
            {
                UnApply();
            }
        }

        public void TurnEnd(object sender, Message m)
        {
            TurnsLeft -= 1;
        }

        public void UnApply()
        {
            Unit.OnTurnEnd -= TurnEnd;
            Unit.OnTurnStart -= TurnStart;
        }

        public void ApplyHealth(string healthMod)
        {
            Unit.Health = IntegerMod(Unit.Health, healthMod);
        }

        public void ApplyAttack(string attackMod)
        {
            Unit.Attack = IntegerMod(Unit.Attack, attackMod);
        }

        private int IntegerMod(int origVaue, string mod)
        {
            if (mod == null)
            {
                return origVaue
            }
            if (mod.Contains("CHANCE"))
            {
                // handle random effects
            }
            if (mod.Contains("+"))
            {
                return origVaue + int.Parse(mod.Replace("+", ""));
            }
            else if (mod.Contains("-"))
            {
                return origVaue - int.Parse(mod.Replace("-", ""));
            }
            else if (mod.Contains("="))
            {
                return int.Parse(mod.Replace("=", ""));
            }
        }
    }
}
