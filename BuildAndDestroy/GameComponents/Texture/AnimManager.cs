using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.Texture
{
    public class AnimManager
    {
        Dictionary<string, SimpleTexture> states;

        private AnimatedObject owner;

        public AnimManager(AnimatedObject owner)
        { 
            this.owner = owner;
            states = new Dictionary<string, SimpleTexture>();
            Add(new SimpleTexture(DisplayUtils.GetInstance().blank), "blank");
        }

        public void Add(SimpleTexture state,string name)
        {
            states.Add( name, state);
        }

        public Texture2D GetCurrentTexture()
        {

            string state = owner.GetState();

            if (states.ContainsKey(state))
            {
                return states[state].GetCurrentTexture();
            }


            return states["blank"].GetCurrentTexture();
        }
    }
}
