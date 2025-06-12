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
        Random rnd = new Random();
        Texture2D grassTexture;

        List<Prop> props = new List<Prop>();
        public List<Prop> Props { get { return props; } }

        public Map() {
            grassTexture = DisplayUtils.GetInstance().GetByPath<Texture2D>("environement/grass");
            GenerateEnvironement(new Point(-1600,-600), new Point(2600, 1600), density:0.2f);
        }

        private void GenerateEnvironement(Point start, Point end, float density = 1)
        {
            int width = Math.Abs(end.X - start.X);
            int height = Math.Abs(end.Y - start.Y);

            int size = width * height /1000;

            float bushQuantity = size * density / 25;
            float rockQuantity = size * density / 50;
            float mossyRockQuantity = size * density / 50;

            for (int i = 0; i < bushQuantity; i++)
            {
                GenerateProp( start,  end,  "environement/bush_"+rnd.Next(1,3));
            }

            for (int i = 0; i < rockQuantity; i++)
            {
                GenerateProp(start, end, "environement/rock");
            }
            for (int i = 0; i < mossyRockQuantity; i++)
            {

                GenerateProp(start, end, "environement/mossy_rock");
            }
            props.Add(new Prop(start, new Point(65, 65), "environement/tree"));
            props.Add(new Prop(end, new Point(65, 65), "environement/tree"));
        }
        private void GenerateProp(Point start, Point end, string texture)
        {
            while (true)
            {
                bool isSomthingHere = false;
                Prop n = new Prop(new Point(rnd.Next(start.X, end.X), rnd.Next(start.Y, end.Y)), new Point(60, 60), texture);

                foreach (var item in props)
                {
                    if (n.Rect.Intersects(item.Rect))
                    {
                        isSomthingHere = true;
                    }
                }
                if (!isSomthingHere)
                {
                    props.Add(n);
                    break;
                }
            }
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
