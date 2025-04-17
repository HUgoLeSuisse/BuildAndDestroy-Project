using BuildAndDestroy.GameComponents.GameObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Effect
{
    public class F_BonusRange : F_Effect
    {
        int amount;
        public F_BonusRange(E_Entity receiver, E_Entity giver, int amount) : base(receiver, giver)
        {
            this.amount = amount;
        }

        public override void ApplyStatBonus()
        {
            Receiver.BonusRange = amount;
        }
    }
}
