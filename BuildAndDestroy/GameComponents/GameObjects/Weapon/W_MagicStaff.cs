using System;
using BuildAndDestroy.GameComponents.GameObjects.Effect;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
    /// <summary>
    /// Arme à distance : Permet de gérer une baguette magique
    /// </summary>
    public class W_MagicStaff : W_Weapon
    {

        public W_MagicStaff(E_Player owner) : base(owner, "Magic Staff")
        {
            // augmenter la portée du joueur 
            Owner.Stats[E_Entity.RANGE].Modifiers.Add(new StatModifier(true,500));
        }

        /// <summary>
        /// Gére la direction de l'arme par default et quand on attaque
        /// </summary>
        public override float Direction
        {
            get
            {
                return isAttacking ?
                    // Quand il attaque
                    MathF.Atan2(Owner.GetDirection().Y, Owner.GetDirection().X) + MathF.PI / 2 :
                    // Quand il attaque pas
                    MathF.Atan2(Owner.GetMouseDirection().Y, Owner.GetMouseDirection().X) + MathF.PI / 2;
            }
        }


        /// <summary>
        /// Permet d'attaquer avec l'arme
        /// </summary>
        /// <param name="target">cible à attaquer</param>
        public override void Attack(E_Entity target)
        {
            base.Attack(target);
            Vector2 dir = Owner.GetDirectionWith(target);
            Bullet b = new Bullet(
                Owner.GameManager,
                Owner,
                position: Owner.Position + (dir*50).ToPoint(),
                size: new Point(16, 16),

                direction: dir,
                distance: Owner.Stats[E_Entity.RANGE].Total);
            b.onTouch += (entity) =>
            {
                Owner.Hit(entity, Owner.Stats[E_Entity.DAMAGE].Total);
            };
        }

        public override Texture2D GetCurrentTexture()
        {
            return DisplayUtils.GetInstance().GetByPath<Texture2D>("magic_staff");
        }
    }
}
