using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    /// <summary>
    /// Gére une entité
    /// </summary>
    public class E_Entity : I_Visible
    {
        protected GameManager gameMananger;
        public E_Entity(
            GameManager gameMananger,
            Rectangle? rect = null,
            Texture2D texture = null,
            float maxHealth = 10,
            float speed = 5,
            float damage = 3,
            float attackSpeed = 1,
            float armor = 1
            )
        {

            d = DisplayUtils.GetInstance();
            this.rect = rect == null ? new Rectangle(0, 0, 50, 50) : rect.Value;
            this.texture = texture == null ? d.blank : texture;
            this.gameMananger = gameMananger;

            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.speed = speed;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.armor = armor;

            path = new Path(this.rect.Center, this.rect.Center);
            UpdateEvents e = UpdateEvents.GetInstance();
            e.Update += Update;
        }

        #region Display
        protected Rectangle rect;
        protected Texture2D texture;
        DisplayUtils d;

        /// <summary>
        /// Pour le visiteur
        /// </summary>
        /// <param name="v"></param>
        public void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La texture acctuel</returns>
        public virtual Texture2D GetAcctualTexture()
        {
            return texture;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le rectangle absolu</returns>
        public virtual Rectangle GetAbsoluteRectangle()
        {
            return rect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La couleur acctuel</returns>
        public virtual Color GetAcctualColor()
        {
            return Color.White;
        }
        #endregion

        #region Gameplay

        // Statistique
        private float attackSpeed;
        private float armor;
        private float damage;
        private float currentHealth;
        private float maxHealth;
        private float speed;
        private float range;

        private Cooldown attackCooldown = null;
        private Rectangle attackArea
        {
            get
            {
                Rectangle r = new Rectangle(
                    (int)(rect.X - range),
                    (int)(rect.Y - range),
                    (int)(rect.Width + 2 * range),
                    (int)(rect.Height + 2 * range)
                    );
                return r;
            }
        }
        private bool CanAttack { get { return attackCooldown == null; } }
        private float AttackCooldown { get { return 1 / attackSpeed; } }


        protected E_Entity target;
        protected Path path;

        public float Health { get { return currentHealth; } }
        public float MaxHealth { get { return maxHealth; } }

        public delegate void OnDie(E_Entity killer);
        public OnDie onDie;


        /// <summary>
        /// Permet d'infliger des dégat à l'entité
        /// </summary>
        /// <param name="amount">nombre de dégat</param>
        /// <param name="player">qui à envoyer les dégats</param>
        public void TakeDamage(float amount, E_Entity enemy)
        {
            currentHealth -= amount - armor;
            if (currentHealth < 0)
            {
                onDie?.Invoke(enemy);
            }

        }

        /// <summary>
        /// Envoie une attaque sur une cible et lance le cooldown d'attaque 
        /// </summary>
        /// <param name="target">La cible a attaquer</param>
        private void Attack(E_Entity target)
        {
            if (CanAttack)
            {
                target.TakeDamage(damage, this);
                attackCooldown = new Cooldown(AttackCooldown);
                attackCooldown.endCooldown += resetAttack;
                attackCooldown.Start();
            }
        }
        /// <summary>
        /// permet d'attaquer à nouveau
        /// </summary>
        public void resetAttack()
        {
            attackCooldown.endCooldown -= resetAttack;
            attackCooldown = null;
        }
        protected virtual void Update(GameTime gameTime)
        {
            path.UpdateCurrentPos(rect.Center);
            if (target != null)
            {
                if (!attackArea.Intersects(target.rect))
                {
                    Vector2 dir = path.GetDirection();
                    rect.Offset(dir.X * speed, dir.Y * speed);
                }
                else
                {
                    Attack(target);
                }
            }
            else
            {
                if (!rect.Contains(path.Destination))
                {
                    Vector2 dir = path.GetDirection();
                    rect.Offset(dir.X * speed, dir.Y * speed);
                }
            }
        }
        /// <summary>
        /// Indique à l'entité ou elle doit aller
        /// </summary>
        /// <param name="pos">la position</param>
        protected void GoToPosition(Point pos)
        {
            E_Entity target;
            if (gameMananger.IsSomeThingHere(pos, out target))
            {
                this.target = target;
                path = new Path(rect.Center, target.rect.Center);
            }
            else
            {
                this.target = null;
                path = new Path(rect.Center, pos);
            }
        }


        #endregion
    }
}
