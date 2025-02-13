using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BuildAndDestroy.GameComponents.Input
{
    /// <summary>
    /// Permet de connaitre les actions de la souris utile au personnage
    /// </summary>
    public class MouseInput 
    {
        private bool isLeftDown = false;
        private bool isRightDown = false;
        public MouseInput()
        {
            UpdateEvents.GetInstance().PreUpdate += PreUpdate;
        }

        protected virtual void PreUpdate(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            // Bouton Gauche
            if (mouse.LeftButton.HasFlag(ButtonState.Pressed))
            {
                if (!isLeftDown)
                {
                    isLeftDown = true;
                    onLeftDown?.Invoke(mouse.Position);
                }
            }
            else if (mouse.LeftButton.HasFlag(ButtonState.Released))
            {
                if (isLeftDown)
                {
                    isLeftDown = false;
                    onLeftUp?.Invoke(mouse.Position);
                }
            }
            // Bouton Droit
            if (mouse.RightButton.HasFlag(ButtonState.Pressed))
            {
                if (!isRightDown)
                {
                    isRightDown = true;
                    onRightDown?.Invoke(mouse.Position);
                }
            }
            else if (mouse.RightButton.HasFlag(ButtonState.Released))
            {
                if (isRightDown)
                {
                    isRightDown = false;
                    onRightUp?.Invoke(mouse.Position);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>la position acctuel de la souris</returns>
        public Point GetMousePosition()
        {
            return Mouse.GetState().Position;
        }

        public delegate void OnButtonChange( Point pos);
        public OnButtonChange onLeftDown;
        public OnButtonChange onLeftUp;
        public OnButtonChange onRightDown;
        public OnButtonChange onRightUp;
    }
}
