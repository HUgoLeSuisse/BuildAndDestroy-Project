using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildAndDestroy.GameComponents.Texture;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class AnimBullet : Bullet
    {
        SimpleAnim anim;
        private float rotation;
        public float Rotation { get { return rotation; } }
        public AnimBullet(
            GameManager gameManager,
            E_Entity shooter,
            string textrue = "projectil/fireball",
            Point? position = null,
            Point? size = null,
            Vector2? direction = null,
            float distance = 0,
            float speed = 15
            ) : base(
                gameManager,
                shooter,
                position,
                size,
                direction,
                distance,
                speed)
        {
            anim = new SimpleAnim(DisplayUtils.GetInstance().GetByPath<Texture2D>(textrue), 30);
            
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * MathF.PI*2*2;
            if (rotation >= MathF.PI * 2 * 2)
            {
                rotation = 0;   
            }
        }

        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }
        public override Color GetCurrentColor()
        {
            return Color.White;
        }

        public override Texture2D GetCurrentTexture()
        {
            return anim.GetCurrentTexture();
        }
    }
}
