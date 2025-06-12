using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Texture;
using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class Slim : E_Monster
    {
        public Slim(GameManager gameMananger, Point? position = null)
            : base(gameMananger,
                  position: position,
                  size: new Point(200, 200),
                  maxHealth: 50,
                  speed: 4,
                  damage: 6,
                  attackSpeed: 0.3f,
                  armor: 4,
                  range: 50,
                  lootBox: new LootBox(550))
        {
            AnimManager.Add(new SimpleAnim("monster/slim", 8), "default");
        }

        public override Color GetCurrentColor()
        {
            return Color.White;
        }
    }
}
