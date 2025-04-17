using BuildAndDestroy.GameComponents.GameObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
    public class W_Sword : W_Weapon
    {
        public W_Sword(E_Player owner) : base(owner, "Sword")
        {
        }

        public override void Attack(E_Entity target)
        {
            target.TakeDamage(Owner.TotalDamage * (Owner.Knowledges.Force + 1), Owner);
        }
    }
}
