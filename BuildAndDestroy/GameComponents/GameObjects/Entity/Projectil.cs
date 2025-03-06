using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using BuildAndDestroy.GameComponents.Utils;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class Projectil : I_Visible, I_Moveable
    {
        private float speed;
        private Point destination;
        private Rectangle rect;

        public delegate void OnTouch(E_Entity hited);
        public OnTouch onTouch;

        public Projectil(
            Point? position = null,
            Point? size = null,
            Point? destination = null,
            float speed = 15
            )
        {
            this.destination = destination == null ? new Point( 0, 0 ) : destination.Value;
            this.rect = new Rectangle(
                position == null ? new Point(0, 0) : position.Value,
                size == null ? new Point(0, 0) : size.Value
                );
            this.speed = speed;
            UpdateEvents.GetInstance().Update += Update;
        }

        private void Update(GameTime gameTime)
        {
            if (!rect.Contains(destination))
            {
                I_Moveable moveable = this;
                Point dir = (destination - rect.Location);
                moveable.Move(gameTime, ref rect, dir.ToVector2().Normalize(), speed);
            }
        }

        #region Display
        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        public Rectangle GetAbsoluteRectangle()
        {
            return rect;
        }

        public Color GetAcctualColor()
        {
            return Color.Blue;
        }

        public Texture2D GetAcctualTexture()
        {
            return DisplayUtils.GetInstance().blank;
        }
        #endregion


    }
}
