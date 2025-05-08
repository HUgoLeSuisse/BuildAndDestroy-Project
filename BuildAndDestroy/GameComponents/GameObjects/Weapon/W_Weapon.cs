using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
    /// <summary>
    /// Classe de base pour une arme
    /// </summary>
    public abstract class W_Weapon : I_Visible, I_SmartObject
    {
        private E_Player owner;
        public E_Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected private bool isAttacking = false;
        protected Cooldown attackTimer;

        /// <summary>
        /// Gére la direction de l'arme par default et quand on attaque
        /// </summary>
        public virtual float Direction
        {
            get
            {
                return isAttacking ?
                    // Quand il attaque
                    MathF.Atan2(Owner.GetDirection().Y, Owner.GetDirection().X) + MathF.PI / 4
                    + (attackTimer.MaxTime - attackTimer.CurrentTime) / attackTimer.MaxTime * MathF.PI / 2 :
                    // Quand il attaque pas
                    MathF.Atan2(Owner.GetMouseDirection().Y, Owner.GetMouseDirection().X) + MathF.PI / 2;
            }
        }

        public W_Weapon(E_Player owner, string name)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Permet d'attaquer avec l'arme
        /// </summary>
        /// <param name="target">cible à attaquer</param>
        public virtual void Attack(E_Entity target)
        {
            // Gère l'annimation
            StartAttackTimer();
        }
        public virtual void StartAttackTimer()
        {
            isAttacking = true;
            attackTimer = new Cooldown(Owner.AttackTime * 0.5f);
            attackTimer.Start();
            attackTimer.endCooldown += () =>
            {
                isAttacking = false;
            };
        }

        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        public abstract Texture2D GetAcctualTexture();

        public Rectangle GetAbsoluteRectangle()
        {

            return new Rectangle(Owner.GetAbsoluteRectangle().Center.X, Owner.GetAbsoluteRectangle().Center.Y, 50, 50);
        }

        public Color GetAcctualColor()
        {
            return Color.White;
        }

        public void Destroy()
        {
            
            attackTimer.Stop();
            attackTimer.Destroy();
            Owner.Weapon = null;
            owner = null;
        }
    }
}
