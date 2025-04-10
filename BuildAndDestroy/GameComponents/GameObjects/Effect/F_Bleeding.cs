using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Effect
{
    public class F_Bleeding : F_Temporary
    {
        private float damage;


        public F_Bleeding(E_Entity receiver, E_Entity giver,
            int duration = 3,
            float damage = 2) : base(receiver,giver, duration)
        {
            this.damage = damage;
            UpdateEvents.GetInstance().Update += Update;
        }

        private void Update(GameTime gameTime)
        {
            Receiver?.TakeDamage((float)(damage*gameTime.ElapsedGameTime.TotalSeconds), Giver);
        }

        public override void Destroy()
        {
            base.Destroy();
            UpdateEvents.GetInstance().Update -= Update;
        }
    }
}
