using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using BuildAndDestroy.GameComponents.Utils;
using System.Collections.Generic;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class Bullet : I_Visible, I_Moveable
    {
        public static List<Bullet> Bullets = new List<Bullet>();

        private GameManager gm;
        private E_Entity shooter;
        private Point origine;
        private float speed;
        private Vector2 direction;
        private Rectangle rect;
        private float distance;

        public delegate void OnTouch(E_Entity hitted);
        public OnTouch onTouch;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">Par defaut : 0,0</param>
        /// <param name="size">Par default : 5,5</param>
        /// <param name="direction">Par default : 0,0</param>
        /// <param name="speed">Par default : 15</param>
        public Bullet(
            GameManager gameManager,
            E_Entity shooter,
            Point? position = null,
            Point? size = null,
            Vector2? direction = null,
            float distance = 0,
            float speed = 15
            )
        {
            this.gm = gameManager;
            this.shooter = shooter;
            this.origine = position == null ? new Point(0, 0) : position.Value;
            this.direction = direction == null ? new Vector2( 0, 0 ) : direction.Value;
            this.direction.Normalize();

            this.rect = new Rectangle(
                position == null ? new Point(0, 0) : position.Value,
                size == null ? new Point(15, 15) : size.Value
                );
            this.distance = distance;
            this.speed = speed;
            UpdateEvents.GetInstance().Update += Update;
            onTouch = (hit) => { 
                Destroy();
            };
            Bullets.Add( this );
        }

        public void Destroy()
        {
            onTouch = null;
            Bullets.Remove(this);
        }

        private void Update(GameTime gameTime)
        {
            float X = rect.Location.X - origine.X;
            float Y = rect.Location.Y - origine.Y;

            if (MathF.Sqrt(X * X + Y * Y)<= distance)
            {
                E_Entity hit;
                if (gm.IsSomeThingHere(rect,out hit))
                {
                    if (hit != shooter)
                    {
                        onTouch?.Invoke(hit);
                        return;
                    }
                    
                }
                    I_Moveable moveable = this;
                    moveable.Move(gameTime, ref rect, direction, speed);
            }
            else
            {
                onTouch?.Invoke(null);
            }
        }

        #region Display
        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le rectangle absolu</returns>
        public Rectangle GetAbsoluteRectangle()
        {
            Rectangle r = new Rectangle(
                rect.X - Camera.Instance.Position.X,

                rect.Y - Camera.Instance.Position.Y,
                rect.Width,
                rect.Height
                );
            return r;
        }

        public Color GetAcctualColor()
        {
            return Color.Cyan;
        }

        public Texture2D GetAcctualTexture()
        {
            return DisplayUtils.GetInstance().blank;
        }
        #endregion


    }
}
