using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Effect
{
    /// <summary>
    /// Effet qui modifie l'armure
    /// </summary>
    public class F_ArmorMods : F_Temporary
    {
        private StatModifier mod;
        public F_ArmorMods(E_Entity receiver, E_Entity giver, float duration, float amount = 2) : base(receiver, giver, duration)
        {
            mod = new StatModifier(true, amount);
            Receiver.Stats[E_Entity.ARMOR].Modifiers.Add(mod);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
