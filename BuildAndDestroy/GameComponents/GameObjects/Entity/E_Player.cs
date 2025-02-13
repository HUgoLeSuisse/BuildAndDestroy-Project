using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.Input;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_Player : E_Entity
    {
        private InputManager inputManager;
        private MouseInput mouseInput;


        public E_Player(GameManager gameMananger) : base(gameMananger)
        {

            inputManager = new InputManager();
            mouseInput = new MouseInput();
            mouseInput.onRightUp += GoToPosition;

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La texture acctuel</returns>
        public override Texture2D GetAcctualTexture()
        {
            return texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le rectangle absolu</returns>
        public override Rectangle GetAbsoluteRectangle()
        {
            return rect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La couleur acctuel</returns>
        public override Color GetAcctualColor()
        {
            return Color.Green;
        }

    }
}