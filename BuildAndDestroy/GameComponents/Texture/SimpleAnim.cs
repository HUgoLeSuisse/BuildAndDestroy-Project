using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.Texture
{
    public class SimpleAnim : SimpleTexture
    {
        private float currentFrame = 0;
        private float frequency;
        private int maxFrame;

        public float Frequency
        {
            get
            {
                return frequency;
            }
            private set
            {
                frequency = value;
            }
        }

        public SimpleAnim(Texture2D texture, float frequency) : base(texture)
        {
            this.Frequency = frequency;
        }

        public SimpleAnim(string textureName, float frequency) : base(textureName)
        {
            this.Frequency = frequency;

            maxFrame = base.Texture.Width / base.Texture.Height;
            UpdateEvents.GetInstance().Update += Update;

        }

        private void Update(GameTime gameTime)
        {
            currentFrame += (float)gameTime.ElapsedGameTime.TotalSeconds * frequency;
            if (currentFrame > maxFrame)
            {
                currentFrame = 0;
            }
        }

        public Texture2D GetCurrentFrame()
        {

            Rectangle rect = new Rectangle( base.Texture.Height * (int)(currentFrame),0, base.Texture.Height, base.Texture.Height);

            // Créer un tableau pour contenir les pixels découpés
            Color[] data = new Color[rect.Width * rect.Height];

            // Extraire les pixels du rectangle depuis la texture source
            base.Texture.GetData(0, rect, data, 0, data.Length);

            // Créer une nouvelle texture avec ces pixels
            Texture2D subTexture = new Texture2D(DisplayUtils.GetInstance().graphics, rect.Width, rect.Height);
            subTexture.SetData(data);

            return subTexture;
        }

        public override Texture2D GetCurrentTexture()
        {
            return GetCurrentFrame();
        }

    }
}
