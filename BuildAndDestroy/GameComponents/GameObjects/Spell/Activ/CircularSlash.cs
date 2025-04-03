using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell.Activ
{
    public class CircularSlash : Active
    {
        const int RANGE = 250;
        const float BASE_DAMAGE = 3;
        const float FORCE_RATIO = 80;
        const float CHARGE_TIME = 0.8f;

        Circle range;
        DrawableCircle drawable;
        Cooldown charge;
        public CircularSlash(GameManager gm, Skill skill) : base(gm, skill, 5)
        {
            onUse += Charge;
            UpdateEvents.GetInstance().PreUpdate += PreUpdate;
        }

        /// <summary>
        /// Charge le coup
        /// </summary>
        /// <param name="skillData"></param>
        private void Charge(SkillData skillData)
        {
            charge = new Cooldown(CHARGE_TIME);
            charge.Start();

            range = new Circle(skill.Owner.Position, RANGE);

            drawable = new DrawableCircle(range, new Color(155,0,0,127));

            GameManager.drawableCircles.Add(drawable);

            charge.endCooldown += () => {
                Slash(drawable);
                GameManager.drawableCircles.Remove(drawable);
            };
        }

        private void PreUpdate(GameTime gameTime)
        {
            if (charge != null)
            {
                drawable.circle.Center = skill.Owner.Position;

                if (charge.CurrentTime <= 0.15)
                {
                    drawable.color = new Color(200, 50, 50, 127);
                }
            }
        }

        /// <summary>
        /// Envoye le coup
        /// </summary>
        /// <param name="range"></param>
        private void Slash(DrawableCircle range)
        {
            List<E_Entity> hitteds;
            skill.Owner.GameManager.IsSomeThingHere(range.circle, out hitteds);
            foreach (var item in hitteds)
            {
                if (item != skill.Owner)
                {
                    item.TakeDamage(BASE_DAMAGE + FORCE_RATIO * skill.Owner.Knowledges.Force, skill.Owner);
                }
            }
        }
    }
}
