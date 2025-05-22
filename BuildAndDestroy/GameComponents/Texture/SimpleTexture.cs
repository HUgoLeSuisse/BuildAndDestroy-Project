using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework.Graphics;


namespace BuildAndDestroy.GameComponents.Texture
{
    /// <summary>
    /// Permet de géré une texture unique
    /// </summary>
    public class SimpleTexture
    {
        private Texture2D texture;
        public Texture2D Texture { get { return texture; } }

        public SimpleTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        public SimpleTexture(string textureName)
        {
            this.texture = DisplayUtils.GetInstance().GetByPath<Texture2D>(textureName);
        }

        public virtual Texture2D GetCurrentTexture()
        {
            return Texture;
        }
    }
}
