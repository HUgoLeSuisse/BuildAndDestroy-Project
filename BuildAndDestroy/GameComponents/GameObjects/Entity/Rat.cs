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
    public class Rat : E_Monster
    {
        public Rat(GameManager gameMananger, Point? position = null) 
            : base(gameMananger,
                  position: position,
                  size: new Point(40,40),
                  maxHealth:15,
                  speed: 8,
                  damage: 2,
                  attackSpeed: 1.2f,
                  armor: 2,
                  range: 40)
        {
            AnimManager.Add(new SimpleAnim("monster/Rat", 8), "default");
        }

        public override Color GetCurrentColor()
        {
            return Color.White;
        }
    }
}
