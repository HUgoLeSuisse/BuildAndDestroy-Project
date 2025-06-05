using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.Utils
{
    public class DrawableCircle : I_Visible
    {
        public Color color;
        public Circle circle;
        public DrawableCircle(Circle circle, Color color)
        {
            this.circle = circle;   
            this.color = color;
        }
        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        public Rectangle GetAbsoluteRectangle()
        {
            Camera cam = Camera.Instance;
            return new Rectangle(
                circle.Center.X- (int)circle.Radius - cam.Position.X,
                circle.Center.Y - (int)circle.Radius - cam.Position.Y,
                (int)(2 *circle.Radius),
                (int)(2 * circle.Radius)

                );
        }

        public Color GetCurrentColor()
        {
            return Color.White;
        }

        public Texture2D GetCurrentTexture()
        {
            return Display.CreateCircleTexture(DisplayUtils.GetInstance().graphics, (int)(circle.Radius * 2), color);
        }
    }
}
