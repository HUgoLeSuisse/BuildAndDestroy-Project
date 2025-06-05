using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Environement
{
    public class Map : I_Visible
    {
        Texture2D grassTexture;
        public Map() {
            grassTexture = DisplayUtils.GetInstance().GetByPath<Texture2D>("environement/grass");
        }

        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        public Rectangle GetAbsoluteRectangle()
        {
            return new Rectangle(0,0,500,500);
        }

        public Color GetCurrentColor()
        {
            return Color.White;
        }

        public Texture2D GetCurrentTexture()
        {
            return grassTexture;
        }
    }
}
