using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_Monster : E_Entity
    {
        public E_Monster(
            GameManager gameMananger,
            Rectangle? rect = null,
            Texture2D texture = null,
            float maxHealth = 10
            ) : base(gameMananger, rect, texture,maxHealth)
        {

        }
        public E_Monster(
            GameManager gameMananger,
            Point? position = null,
            Point? size = null,
            Texture2D texture = null,
            float maxHealth = 10
            ) : base(gameMananger,new Rectangle(position == null ? new Point(0,0): position.Value, size == null ? new Point(0, 0) : size.Value), texture, maxHealth)
        {
            
        }

        public override Color GetAcctualColor()
        {
            return Color.Red;
        }
    }
}
