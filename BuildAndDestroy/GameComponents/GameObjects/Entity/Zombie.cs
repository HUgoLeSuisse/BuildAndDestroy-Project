using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class Zombie : E_RangeMonster
    {
        public Zombie(GameManager gameMananger, Point? position = null) : base(gameMananger, position, new Point(60, 60), null, 15, 4, 5, 1, 2, 300, new LootBox(350))
        {
            AnimManager.Add(new SimpleAnim("zombie", 8), "default");
        }

        public override Color GetCurrentColor()
        {
            return Color.White;
        }

        /// <summary>
        /// Attaque à distance
        /// </summary>
        /// <param name="target"></param>
        protected override void RangeAttack(E_Entity target)
        {
            Bullet b = new AnimBullet(GameManager, this,
                textrue: "projectil/bone",
                size: new Point(40, 40),
                direction: GetDirectionWith(target),
                distance: Stats[RANGE].Total);
            b.onTouch += (entity) =>
            {
                Hit(entity, Stats[DAMAGE].Total);
            };
        }
    }
}
