using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Effect
{
    /// <summary>
    /// Effet temporaire
    /// </summary>
    public abstract class F_Temporary : F_Effect
    {
        private Cooldown cooldown;

        public delegate void OnEnd();
        public OnEnd onEnd;

        public F_Temporary(E_Entity receiver,E_Entity giver, float duration) : base(receiver,giver)
        {
            cooldown = new Cooldown(duration);
            cooldown.Start();
            onEnd += base.Destroy;
            cooldown.endCooldown += () =>
            {
                onEnd?.Invoke();
            };
        }
        public override void Destroy()
        {
            base.Destroy();
        }

    }
}
