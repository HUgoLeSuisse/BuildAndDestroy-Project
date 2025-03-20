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
        private static MouseInput instance;
        Camera camera;
        public static MouseInput Instance { 
            get 
            {
                if (instance == null)
                {
                    instance = new MouseInput();
                }
                return instance; 
            } 
        }
        private bool isLeftDown = false;
        private bool isRightDown = false;
        private MouseInput()
        {
            camera = Camera.Instance;
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
                    onLeftDown?.Invoke(mouse.Position,GetMousePosition());
                }
            }
            else if (mouse.LeftButton.HasFlag(ButtonState.Released))
            {
                if (isLeftDown)
                {
                    isLeftDown = false;
                    onLeftUp?.Invoke(mouse.Position, GetMousePosition());
                }
            }
            // Bouton Droit
            if (mouse.RightButton.HasFlag(ButtonState.Pressed))
            {
                if (!isRightDown)
                {
                    isRightDown = true;
                    onRightDown?.Invoke(mouse.Position, GetMousePosition());
                }
            }
            else if (mouse.RightButton.HasFlag(ButtonState.Released))
            {
                if (isRightDown)
                {
                    isRightDown = false;
                    onRightUp?.Invoke(mouse.Position, GetMousePosition());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>la Position acctuel de la souris</returns>
        public Point GetMousePosition()
        {
            return Mouse.GetState().Position + camera.Position;
        }

        public delegate void OnButtonChange( Point onScreenPos,Point inGamePos);
        public OnButtonChange onLeftDown;
        public OnButtonChange onLeftUp;
        public OnButtonChange onRightDown;
        public OnButtonChange onRightUp;
    }
}
