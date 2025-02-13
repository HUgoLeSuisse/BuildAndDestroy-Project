using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    /// <summary>
    /// Position horizontal relative possible
    ///     L : tout à gauche
    ///     M : au centre
    ///     R : tout à droite
    /// </summary>
    public enum Horizontal
    {
        L,
        M,
        R
    }
    /// <summary>
    /// Position vertical relative possible
    ///     T : tout en haut
    ///     M : au centre
    ///     B : tout en bas
    /// </summary>
    public enum Vertical
    {
        T,
        M,
        B
    }
    /// <summary>
    /// Element d'interface utilisateur
    /// </summary>
    public class UI_Element : I_Visible
    {
        /// <summary>
        /// Pannaux dans lequel il est contenu
        /// </summary>
        public UI_Pannel parent;

        /// <summary>
        /// Rectangle relatif à la position du parent
        /// </summary>
        public Rectangle rectRelative { get; set; }
        /// <summary>
        /// Couleur de l'element
        /// </summary>
        public Color color { get; set; }
        /// <summary>
        /// Texture de l'element
        /// </summary>
        public Texture2D texture { get; set; }

        protected DisplayUtils d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">Rectangle relatif à la position du parent</param>
        /// <param name="color">Couleur de l'element</param>
        /// <param name="texture">Texture de l'element (par defaut 1x1 blanc)</param>
        public UI_Element(Rectangle rect, Color? color, Texture2D? texture)
        {
            d = DisplayUtils.GetInstance();
            this.rectRelative = rect;
            this.color = color == null ? Color.White : color.Value; 
            this.texture = texture == null ? DisplayUtils.GetInstance().blank : texture;
        }


        #region SetRelativePos

        /// <summary>
        /// Positionner sur l'axe horizontal
        /// </summary>
        /// <param name="h"></param>
        public void SetRelativePos(Horizontal h)
        {
            if (parent != null)
            {
                int x = 0;
                switch (h)
                {
                    case Horizontal.L:
                        x = 0;
                        break;
                    case Horizontal.M:
                        x = parent.rectRelative.Width / 2 - rectRelative.Width / 2;
                        break;
                    case Horizontal.R:
                        x = parent.rectRelative.Width - rectRelative.Width;
                        break;
                }
                Rectangle rect = new Rectangle(new Point(x, rectRelative.Y), rectRelative.Size);
                rectRelative = rect;
            }
        }
        /// <summary>
        /// Positionner sur l'axe verticale
        /// </summary>
        /// <param name="h"></param>
        public void SetRelativePos(Vertical v)
        {
            if (parent != null)
            {
                int y = 0;
                switch (v)
                {
                    case Vertical.T:
                        y = 0;
                        break;
                    case Vertical.M:
                        y = parent.rectRelative.Height / 2 - rectRelative.Height / 2;
                        break;
                    case Vertical.B:
                        y = parent.rectRelative.Height - rectRelative.Height;
                        break;
                }
                Rectangle rect = new Rectangle(new Point(rectRelative.X, y), rectRelative.Size);
                rectRelative = rect;
            }
        }
        /// <summary>
        /// Positionner sur l'axe horizontal et verticale
        /// </summary>
        /// <param name="h"></param>
        public void SetRelativePos(Horizontal h, Vertical v)
        {
            SetRelativePos(h);
            SetRelativePos(v);
        }

        #endregion

        /// <summary>
        /// Permet à VisibleVistor de fonctionner
        /// </summary>
        /// <param name="v"></param>
        public virtual void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Texture acctuelle</returns>
        public virtual Texture2D GetAcctualTexture()
        {
            return texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le Rectangle absolue</returns>
        public virtual Rectangle GetAbsoluteRectangle()
        {
            Rectangle abs = rectRelative;
            if (parent != null)
            {
                abs.Location += parent.GetAbsoluteRectangle().Location;
            }
            return abs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Couleur acctuellle</returns>
        public virtual Color GetAcctualColor()
        {
            return color;
        }
    }
}
