using BuildAndDestroy.GameComponents.GameObjects.Effect;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis;
using Microsoft.Xna.Framework;

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
        /// Permet d'attaquer avec l'arme
        /// </summary>
        /// <param name="target">cible à attaquer</param>
        public override void Attack(E_Entity target)
        {

            Bullet b = new Bullet(
                Owner.GameManager,
                Owner,
                size: new Point(4, 4),
                direction: Owner.GetDirectionWith(target),
                distance: Owner.Stats[E_Entity.RANGE].Total);
            b.onTouch += (entity) =>
            {
                Owner.Hit(entity);
            };
        }
    }
}
