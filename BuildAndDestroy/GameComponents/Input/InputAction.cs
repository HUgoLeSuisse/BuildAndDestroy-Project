using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildAndDestroy.GameComponents.Input.InputAction;

namespace BuildAndDestroy.GameComponents.Input
{
    public class InputAction
    {
        public bool isDown { get; private set; }
        public Keys key { get; set; }
        Timer timer;
        public InputAction(Keys key) 
        { 
            this.key = key;
            UpdateEvents.GetInstance().PreUpdate += PreUpdate;
            isDown = false;
        }

        protected virtual void PreUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(key))
            {
                isDown = true;
                onKeyDown?.Invoke();
                timer = new Timer();
            }
            if (Keyboard.GetState().IsKeyUp(key) && isDown)
            {
                isDown = false;
                onKeyUp?.Invoke();
                onKeyPressed(timer.GetTime());
                timer = null;
            }
        }
        public delegate void UpDown();
        /// <summary>
        /// quand la souris est enfoncé
        /// </summary>
        public UpDown onKeyDown;
        /// <summary>
        /// quand la souris est relâché
        /// </summary>
        public UpDown onKeyUp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Durée du clique</param>
        public delegate void OnAction(float time);
        /// <summary>
        /// S'appele quand on clique sur le bouton
        /// </summary>
        public OnAction onKeyPressed;
    }
}
