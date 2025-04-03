using BuildAndDestroy.GameComponents.GameObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Effect
{
    public class F_ArmorReduction : F_Temporary
    {
        private float amount;
        public F_ArmorReduction(E_Entity receiver, E_Entity giver, float duration, float amount = 2) : base(receiver, giver, duration)
        {
            this.amount = amount;
        }

        public override void ApplyStatBonus()
        {
            base.ApplyStatBonus();
            Receiver.BonusArmor = -amount;
        }
    }
}
