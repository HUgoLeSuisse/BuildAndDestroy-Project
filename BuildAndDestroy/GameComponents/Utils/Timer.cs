﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuildAndDestroy.GameComponents.Utils.Cooldown;

namespace BuildAndDestroy.GameComponents.Utils
{
    /// <summary>
    /// Permet de compter le temps passé depuis la création de l'instance
    /// </summary>
    public class Timer : I_SmartObject
    {
        private float time;
        public float Time { get { return time; } set { time = value; } }
        public Timer()
        {
            UpdateEvents.GetInstance().Update += Update;
        }
        private void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Destroy()
        {
            UpdateEvents.GetInstance().Update -= Update;
        }
    }
}
