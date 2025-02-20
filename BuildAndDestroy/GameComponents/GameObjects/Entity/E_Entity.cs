using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BuildAndDestroy.GameComponents.GameObjects.Utils;

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
            float armor = 1,
            float range = 30,
            LootBox lootBox = null
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
            this.range = range;

            this.lootBox = lootBox;

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
        public virtual void Accept(I_VisibleVisitor v)
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

        private LootBox lootBox;

        /// <summary>
        /// Vitesse d'attaque 
        /// </summary>
        public virtual float AttackSpeed
        {
            get { return attackSpeed; }
        }
        /// <summary>
        /// Vie acctuelle
        /// </summary>
        public virtual float Health { get { return currentHealth; } }
        /// <summary>
        /// Vie Maximum
        /// </summary>
        public virtual float MaxHealth { get { return maxHealth; } }

        /// <summary>
        /// Armure
        /// </summary>
        public virtual float Armor
        {
            get { return armor; }
        }
        /// <summary>
        /// Proté d'attaque
        /// </summary>
        public virtual float Range
        { get { return range; } }

        /// <summary>
        /// Dégat
        /// </summary>
        public virtual float Damage { get { return damage; } }
        /// <summary>
        /// Vitesse
        /// </summary>
        public virtual float Speed
        {
            get
            {
                return speed;
            }
        }

        private Cooldown attackCooldown = null;

        /// <summary>
        /// Rectangle d'attaque
        /// </summary>
        private Circle attackArea
        {
            get
            {

                Circle r = new Circle(
                    rect.Center,
                    range + rect.Width/2
                    );
                return r;
            }
        }
        /// <summary>
        /// Peut il attaquer
        /// </summary>
        private bool CanAttack { get { return attackCooldown == null; } }
        /// <summary>
        /// le temp à attendre entre chaque attaque
        /// </summary>
        private float AttackTime { get { return 1 / AttackSpeed; } }


        protected E_Entity target;
        protected Path path;



        public delegate void OnDie(E_Entity killer, LootBox lootBox);
        public OnDie onDie;


        /// <summary>
        /// Permet d'infliger des dégat à l'entité
        /// </summary>
        /// <param name="amount">nombre de dégat</param>
        /// <param name="player">qui à envoyer les dégats</param>
        public virtual void TakeDamage(float amount, E_Entity enemy)
        {
            currentHealth -= amount - Armor;
            if (currentHealth < 0)
            {
                onDie?.Invoke(enemy,lootBox);
            }

        }

        /// <summary>
        /// Envoie une attaque sur une cible et lance le cooldown d'attaque 
        /// </summary>
        /// <param name="target">La cible a attaquer</param>
        protected virtual void Attack(E_Entity target)
        {
            if (CanAttack)
            {
                target.TakeDamage(Damage, this);
                attackCooldown = new Cooldown(AttackTime);
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
                path = new Path(rect.Center, target.rect.Center);
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
        /// Indique à l'entité ou elle doit aller et vérifie si une autre entité s'y trouve et si oui la prend pour cible
        /// </summary>
        /// <param name="pos">la position</param>
        protected void GoToPosition(Point pos)
        {
            E_Entity target;
            if (gameMananger.IsSomeThingHere(pos, out target))
            {
                this.target = target;
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
