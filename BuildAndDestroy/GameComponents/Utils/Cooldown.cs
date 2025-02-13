using Microsoft.Xna.Framework;
using System;

namespace BuildAndDestroy.GameComponents.Utils
{
    /// <summary>
    /// Permet lancer un minuteur qui active un event quand il a fini
    /// </summary>
    public class Cooldown
    {

        private float maxTime;
        private float currentTime;
        private bool isEnable = false;

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
            this.maxTime = time;
            currentTime = time;
            UpdateEvents.GetInstance().Update += Update;
        }

        private void Update(GameTime gameTime)
        {
            if (isEnable) {
                currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (currentTime < 0) {
                endCooldown?.Invoke();
                Stop();
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
    }
}
