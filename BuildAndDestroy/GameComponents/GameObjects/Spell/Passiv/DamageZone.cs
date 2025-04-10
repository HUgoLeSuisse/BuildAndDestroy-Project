using BuildAndDestroy.GameComponents.GameObjects.Effect;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell.Passiv
{
    public class DamageZone : Passive
    {
        private float damage;
        private float range;
        private Color color;
        private DrawableCircle drawable;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range">zone de dégat</param>
        /// <param name="damage">Dégat par second</param>
        /// <param name="color">Couleur de la zone</param>
        /// <param name=""></param>
        public DamageZone(
            GameManager gameManager,
            Skill skill,
            float range = 150,
            float damage = 1,
            Color? color = null
            ) : base( gameManager, skill )
        { 
            this.range = range;
            this.damage = damage;
            this.color = color ?? Color.Transparent;
            drawable = new DrawableCircle(new Circle(skill.Owner.Position, range),this.color);
            GameManager.drawableCircles.Add(drawable);
        }
        protected override void OnConditionValide(GameTime gameTime)
        {
            base.OnConditionValide(gameTime);



            Circle circle = new Circle(skill.Owner.Position, range);

            drawable.circle = circle;

            List<E_Entity> hitteds;
            gm.IsSomeThingHere(circle, out hitteds);

            foreach (var item in hitteds)
            {
                if (item != skill.Owner)
                {
                    item.TakeDamage((float)(damage * gameTime.ElapsedGameTime.TotalSeconds), skill.Owner);
                }
            }

        }
        public override void Destroy()
        {
            base.Destroy();

            GameManager.drawableCircles.Remove(drawable);
        }
    }
}
