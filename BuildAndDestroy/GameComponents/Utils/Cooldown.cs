﻿using Microsoft.Xna.Framework;
using System;

namespace BuildAndDestroy.GameComponents.Utils
{
    /// <summary>
    /// Permet lancer un minuteur qui active un event quand il a fini
    /// </summary>
    public class Cooldown : I_SmartObject
    {

        private float maxTime;
        private float currentTime;
        private bool isEnable = false;

        /// <summary>
        /// Temps de fin du cooldown
        /// </summary>
        public float MaxTime
        {
            get { return maxTime; }
        }

        /// <summary>
        /// Temps écoulé depuis le depuis du cooldown
        /// </summary>
        public float CurrentTime
        {
            get { return currentTime; }
        }


        /// <summary>
        /// Est ce que le cooldown est actif
        /// </summary>
        public bool IsEnable
        {
            get { return isEnable; }
        }

        public delegate void EndTimer();
        /// <summary>
        /// s'Active quand le temps voulu s'est écoulé
        /// </summary>
        public EndTimer endCooldown;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Temps à écouler</param>
        public Cooldown(float time)
        {
            maxTime = time;
            currentTime = time;
            UpdateEvents.GetInstance().Update += Update;
        }

        private void Update(GameTime gameTime)
        {
            if (isEnable) {
                currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentTime < 0) {
                if (isEnable)
                {
                    endCooldown?.Invoke();
                    Stop();
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Temps acctuel</returns>
        public float GetTime()
        {
            return currentTime;
        }
        /// <summary>
        /// Arrete le minuteur
        /// </summary>
        public void Stop()
        {
            isEnable = false;
        }
        /// <summary>
        /// Démarre le minuteur
        /// </summary>
        public void Start()
        {
            isEnable = true;
        }

        public void Destroy()
        {
            UpdateEvents.GetInstance().Update -= Update;
            endCooldown = null;
        }
    }
}
