using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    /// <summary>
    /// Element d'une interface utilisateur avec du text
    /// </summary>
    public class UI_Label : UI_Element
    {
        public string text { get; set; }
        public SpriteFont font { get; set; }
        public Color fontColor { get; set; }
        public float fontSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">Position de l'élement (par defaut (0,0))</param>
        /// <param name="text">Text dans l'element</param>
        /// <param name="fontSize">Taille de la police (par defaut 1)</param>
        /// <param name="color">Couleur du fond (par defaut blanc)</param>
        /// <param name="image">Image du fond (par defaut 1x1 blanc)</param>
        /// <param name="fontColor">Couleur du text (par defaut transparent)</param>
        /// <param name="font">Police </param>
        public UI_Label(
            Point position = new Point(),
            string text = "",
            float fontSize = 1,
            Color? color = null,
            Texture2D? image = null,
            Color? fontColor = null,
            SpriteFont? font = null
            )  
            : base(
                  new Rectangle(
                      position,
                      ((font == null ? DisplayUtils.GetInstance().defaultFont : font)
                       .MeasureString(text) * fontSize).ToPoint()),
                  color,
                  image)
        {

            this.text = text;
            this.fontColor = fontColor == null ? Color.Transparent : fontColor.Value;
            this.font = font == null? d.defaultFont: font;
            this.fontSize = fontSize;

        }

        /// <summary>
        /// Permet à VisibleVistor de fonctionner
        /// </summary>
        /// <param name="v"></param>
        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le Rectangle absolue</returns>
        public override Rectangle GetAbsoluteRectangle()
        {

            return Absoulute;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La taille de la police</returns>
        public float GetFontSize()
        {
            return fontSize;
        }

    }
}
