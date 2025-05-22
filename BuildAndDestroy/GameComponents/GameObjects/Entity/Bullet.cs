using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using BuildAndDestroy.GameComponents.Utils;
using System.Collections.Generic;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class Bullet : I_Visible, I_Moveable, I_SmartObject
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
            this.origine = position == null ? shooter.Position : position.Value;
            this.direction = direction == null ? new Vector2( 0, 0 ) : direction.Value;

            this.rect = new Rectangle(
                position == null ? shooter.Position- (size.Value.ToVector2()/2).ToPoint() : position.Value,
                size == null ? new Point(15, 15) : size.Value
                );
            this.distance = distance + MathF.Sqrt(rect.Width* rect.Width + rect.Height * rect.Height);
            this.speed = speed;
            UpdateEvents.GetInstance().Update += Update;
            onTouch = (hit) => { 
                Destroy();
            };
            Bullets.Add( this );
        }

        /// <summary>
        /// Supprime le projectile
        /// </summary>
        public void Destroy()
        {
            Bullets.Remove(this);
            UpdateEvents.GetInstance().Update -= Update;
            onTouch = null;
        }

        private void Update(GameTime gameTime)
        {
            float X = rect.Center.X - origine.X;
            float Y = rect.Center.Y - origine.Y;

            if (MathF.Sqrt(X * X + Y * Y)< distance )
            {
                List<E_Entity> hitteds;
                if (gm.IsSomeThingHere(rect,out hitteds))
                {
                    foreach (var item in hitteds)
                    {
                        if (item != shooter)
                        {
                            onTouch?.Invoke(item);
                            return;
                        }
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

        public Texture2D GetCurrentTexture()
        {
            return DisplayUtils.GetInstance().blank;
        }
        #endregion


    }
}
