using BuildAndDestroy.GameComponents.GameObjects.Effect;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
    public class W_MagicStaff : W_Weapon
    {

        public W_MagicStaff(E_Player owner) : base(owner, "Magic Staff")
        {
            Owner.Effects.Add(new F_BonusRange(owner, owner, 500));
        }

        public override void Attack(E_Entity target)
        {

            Bullet b = new Bullet(
                Owner.GameManager,
                Owner,
                size: new Point(4, 4),
                direction: Owner.GetDirectionWith(target),
                distance: Owner.TotalRange);
            b.onTouch += (entity) =>
            {
                Owner.Hit(entity);
            };
        }
    }
}
