using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents
{
    public interface I_Visible
    {
        /// <summary>
        /// Permet d'appeler la fonction correspondante au type du spécifique de l'élément sans faire de cast // nécessite I_VisibleVisitor
        /// </summary>
        /// <param name="v"></param>
        public void Accept(I_VisibleVisitor v);
        public Texture2D GetAcctualTexture();
        public Rectangle GetAbsoluteRectangle();
        public Color GetAcctualColor();
    }
}
