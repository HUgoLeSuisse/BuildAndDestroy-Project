using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell
{
    public class Passive : I_SmartObject
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

        /// <summary>
        /// s'acticve quand la condition est active
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void OnConditionValide(GameTime gameTime)
        {        }

        public virtual void Destroy()
        {
            UpdateEvents.GetInstance().Update -= Update;
        }
    }
}
