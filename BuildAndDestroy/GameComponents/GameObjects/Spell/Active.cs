﻿using BuildAndDestroy.GameComponents.Utils;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell
{
    public class Active : I_SmartObject
    {
        private bool isAvailable = true;
        private Cooldown cooldown;
        public Cooldown Cooldown { get { return cooldown; } }
        private float baseTimer;

        protected GameManager gm;
        protected Skill skill;

        public bool IsAvailable { get { return isAvailable; } }

        /// <summary>
        /// Cooldown de la comptéance
        /// </summary>
        public float Timer
        {
            get { return baseTimer; }
        }


        public Active(GameManager gm, Skill skill, int baseTimer)
        {
            this.gm = gm;
            this.skill = skill;
            this.baseTimer = baseTimer;
            onUse += () =>
            {
                isAvailable = false;
                cooldown = new Cooldown(Timer);
                cooldown.Start();

                cooldown.endCooldown += () =>
                {
                    isAvailable = true;

                    cooldown = null;
                };
            }; 

        }

        public delegate void OnUse();
        public OnUse onUse;

        public virtual void Destroy()
        {
            cooldown?.Destroy();
            onUse = null;
        }
    }
}
