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
        protected DisplayUtils d;

        private UI_Pannel parent;

        private Rectangle rect;
        private Color color;
        public Texture2D texture;

        /// <summary>
        /// Pannaux dans lequel il est contenu
        /// </summary>
        public UI_Pannel Parent { get { return parent; } set { parent = value; } }

        /// <summary>
        /// Rectangle absolue à la position du parent
        /// </summary>
        public Rectangle Absoulute
        {
            get
            {
                Rectangle abs = rect;
                if (parent != null)
                {
                    abs.Location += parent.GetAbsoluteRectangle().Location;
                }
                return abs;
            }

            set
            {
                Rectangle abs = value;
                if (parent != null)
                {
                    abs.Location -= parent.GetAbsoluteRectangle().Location;
                }
                rect = abs;
            }
        }

        /// <summary>
        /// Rectangle relatif à la position du parent
        /// </summary>
        public Rectangle Relative
        {
            get
            {
                return rect;
            }

            set
            {
                rect = value;
            }
        }


        #region SetRelativePos

        /// <summary>
        /// Alignement sur l'axe horizontal
        /// </summary>
        public virtual Horizontal xAlign
        {
            set
            {
                if (parent != null)
                {
                    int x = 0;
                    switch (value)
                    {
                        case Horizontal.L:
                            x = 0;
                            break;
                        case Horizontal.M:
                            x = parent.rect.Width / 2 - this.rect.Width / 2;
                            break;
                        case Horizontal.R:
                            x = parent.rect.Width - this.rect.Width;
                            break;
                    }
                    Rectangle rect = new Rectangle(new Point(x, this.rect.Y), this.rect.Size);
                    this.rect = rect;
                }
            }

        }
        /// <summary>
        /// Alignement sur l'axe vertical
        /// </summary>
        public virtual Vertical yAlign
        {
            set
            {
                if (parent != null)
                {
                    int y = 0;
                    switch (value)
                    {
                        case Vertical.T:
                            y = 0;
                            break;
                        case Vertical.M:
                            y = parent.rect.Height / 2 - this.rect.Height / 2;
                            break;
                        case Vertical.B:
                            y = parent.rect.Height - this.rect.Height;
                            break;
                    }
                    Rectangle rect = new Rectangle(new Point(this.rect.X, y), this.rect.Size);
                    this.rect = rect;
                }
            }
        }

        #endregion
        /// <summary>
        /// Couleur de l'element
        /// </summary>
        public Color? ColorBG
        {
            get
            {
                return color;
            }

            set
            {
                color = value ?? Color.Transparent;
            }
        }

        /// <summary>
        /// Texture de fond de l'element
        /// </summary>
        public Texture2D TextureBG
        {
            get { return texture; }
            set
            {
                texture = value ?? DisplayUtils.GetInstance().blank;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">Rectangle relatif à la position du parent</param>
        /// <param name="color">Couleur de l'element</param>
        /// <param name="texture">TextureBG de l'element (par defaut 1x1 blanc)</param>
        public UI_Element(Rectangle rect, Color? color, Texture2D texture)
        {
            d = DisplayUtils.GetInstance();
            Relative = rect;
            ColorBG = color;
            TextureBG = texture;
        }



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
        /// <returns>TextureBG acctuelle</returns>
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
            return Absoulute;
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
