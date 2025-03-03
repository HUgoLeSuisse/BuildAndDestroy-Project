using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BuildAndDestroy.GameComponents.Utils;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    /// <summary>
    /// Element d'interface avec un event quand on clique dessus
    /// </summary>
    public class UI_Button : UI_Label
    {
        private Timer timePressed;
        private bool isOver;
        private bool isPressed;
        private Color overColor;
        private Color pressedColor;

        public UI_Button(
            Point position = new Point(),
            string text = "",
            float fontSize = 1,
            Color color = new Color(),
            Texture2D? image = null,
            Color? fontColor = null,
            SpriteFont? font = null,
            Color overColor = new Color(),
            Color pressedColor = new Color()
            ) : base(position, text, fontSize, color, image, fontColor, font)
        {
            this.overColor = overColor;
            this.pressedColor = pressedColor;
            UpdateEvents.GetInstance().Update += Update;
        }
        public delegate void MouseAction();
        /// <summary>
        /// quand la souris entre
        /// </summary>
        public MouseAction onMouseOver;
        /// <summary>
        /// quand la souris sort
        /// </summary>
        public MouseAction onMouseUnover;
        /// <summary>
        /// quand la souris est enfoncé
        /// </summary>
        public MouseAction onMouseDown;
        /// <summary>
        /// quand la souris est relâché
        /// </summary>
        public MouseAction onMouseUp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Durée du clique</param>
        public delegate void OnClick(float time);
        /// <summary>
        /// S'appele quand on clique sur le bouton
        /// </summary>
        public OnClick onClick;

        /// <summary>
        /// Vérifie l'état de la souris et appele les events correspondant
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            if (GetAbsoluteRectangle().Contains(mouse.Position))
            {
                if (!isOver)
                {
                    onMouseOver?.Invoke();
                    isOver = true;
                }

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (!isPressed)
                    {
                        onMouseDown?.Invoke();
                        timePressed = new Timer();

                        isPressed = true;
                    }
                }
                else
                {
                    if (isPressed)
                    {
                        onMouseUp?.Invoke();
                        onClick?.Invoke(timePressed.GetTime());
                        timePressed = null;
                        isPressed = false;
                    }
                }
            }
            else
            {
                if (isOver)
                {
                    onMouseOver?.Invoke();
                }
                isOver = false;

                if (mouse.LeftButton == ButtonState.Released)
                {
                    if (isPressed)
                    {
                        onMouseDown?.Invoke();
                        isPressed = false;
                    }
                }
            }
        }

        /// <summary>
        /// La couleur change en fonction de si le buton est appuyer, survoler ou rien du tout
        /// </summary>
        /// <returns>La couleur acctuelle</returns>
        public override Color GetAcctualColor()
        {
            if (isPressed)
            {
                return pressedColor;
            }
            if (isOver)
            {
                return overColor;
            }
            return ColorBG.Value;
        }
    }
}
