using System;
using BuildAndDestroy.GameComponents.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Environement
{
    public class Prop : I_Visible
    {
        private Rectangle rect;

        private SimpleTexture texture;
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        public Prop(Point position, Point size,string texture) 
        {
            rect = new Rectangle(position, size);
            this.texture = new SimpleTexture(texture);
        }

        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        public Rectangle GetAbsoluteRectangle()
        {
            Rectangle r = new Rectangle(
                Rect.X - Camera.Instance.Position.X,

                Rect.Y - Camera.Instance.Position.Y,
                Rect.Width,
                Rect.Height
                );
            return r;
        }

        public Color GetCurrentColor()
        {
            return Color.White;
        }

        public Texture2D GetCurrentTexture()
        {
            return texture.GetCurrentTexture();
        }
    }
}
