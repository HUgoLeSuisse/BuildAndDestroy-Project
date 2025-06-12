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
    internal class Bat : E_Monster
    {

        public Bat(GameManager gameMananger, Point? position = null)
            : base(gameMananger,
                  position: position,
                  size: new Point(45, 45),
                  maxHealth: 6,
                  speed: 11,
                  damage: 2,
                  attackSpeed: 1.2f,
                  armor: 0,
                  range: 40,
                  lootBox: new LootBox(50))
        {
            AnimManager.Add(new SimpleAnim("monster/Bat", 8), "default");
        }

        public override Color GetCurrentColor()
        {
            return Color.White;
        }
    }
}
