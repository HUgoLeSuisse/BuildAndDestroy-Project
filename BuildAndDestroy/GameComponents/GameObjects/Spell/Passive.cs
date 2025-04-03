using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell
{
    public class Passive
    {
        protected GameManager gm;
        protected Skill skill;

        /// <summary>
        /// Condition a remplir pour l'activation du passif
        /// </summary>
        public virtual bool Condition { get { return true; } }


        public Passive(GameManager gm, Skill skill) {
            UpdateEvents.GetInstance().Update += Update;
            this.gm = gm;
            this.skill = skill;

        }


        protected virtual void Update(GameTime gameTime)
        {
            if (Condition)
            {
                OnConditionValide(gameTime);
            }
        }

        protected virtual void OnConditionValide(GameTime gameTime)
        {

        }
    }
}
