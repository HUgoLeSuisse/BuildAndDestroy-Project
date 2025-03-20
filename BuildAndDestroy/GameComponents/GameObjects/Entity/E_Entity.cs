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
    public class E_Entity : I_Visible, I_Moveable
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
            bool isRange = false,
            LootBox lootBox = null)
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
            this.isRange = isRange;
            this.lootBox = lootBox;

            path = new Path(this.rect.Center, this.rect.Center);
            UpdateEvents e = UpdateEvents.GetInstance();
            e.Update += Update;


            attack += isRange? RangeAttack : MeleeAttack;
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

        #region Champs
           
        // Statistique
        private float attackSpeed;
        private float armor;
        private float damage;
        private float currentHealth;
        private float maxHealth;
        private float speed;
        private float range;
        private bool isRange;

        private LootBox lootBox;
        #endregion

        #region Propriété

        /// <summary>
        /// Position de l'entité
        /// </summary>
        public virtual Point Position
        {
            get
            {
                return rect.Center;
            }
        }
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
        /// Portée d'attaque
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

        /// <summary>
        /// Rectangel de l'entité
        /// </summary>
        public Rectangle Rect { get { return rect; } }

        /// <summary>
        /// Zone d'attaque
        /// </summary>
        private Circle AttackArea
        {
            get
            {
                Circle r = new Circle(
                    rect.Center,
                    Range-10 + rect.Width / 2
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

        /// <summary>
        /// Si l'entité lance des projectile
        /// </summary>
        public bool IsRange
        {
            get
            {
                return isRange;
            }
        }


        #endregion

        #region ValeurLogique
        private Cooldown attackCooldown = null;
        protected E_Entity target;
        protected Path path;
        #endregion

        #region Event
        /// <summary>
        /// Event de mort
        /// </summary>
        /// <param name="killer">le tueur</param>
        /// <param name="lootBox">la butin qu'a récupèrer le tueur</param>
        public delegate void OnDie(E_Entity killer, LootBox lootBox);

        /// <summary>
        /// Quand le personnage meurt
        /// </summary>
        public OnDie onDie;


        /// <summary>
        /// Event d'attaque 
        /// </summary>
        /// <param name="target">la cible</param>
        public delegate void Attack(E_Entity target);
        /// <summary>
        /// Fait attaquer l'entity
        /// </summary>
        public Attack attack;

        #endregion

        #region Methode

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
                onDie?.Invoke(enemy, lootBox);
            }

        }

        /// <summary>
        /// Envoie une attaque sur une cible et lance le cooldown d'attaque 
        /// </summary>
        /// <param name="target">La cible a attaquer</param>
        protected virtual void MeleeAttack(E_Entity target)
        {
            if (CanAttack)
            {
                Hit(target);
                attackCooldown = new Cooldown(AttackTime);
                attackCooldown.endCooldown += resetAttack;
                attackCooldown.Start();
            }
        }

        /// <summary>
        /// Envoie une attaque à distance sur une cible et lance le cooldown d'attaque 
        /// </summary>
        /// <param name="target">La cible a attaquer</param>
        protected virtual void RangeAttack(E_Entity target)
        {
            if (CanAttack)
            {
                Bullet b = new Bullet(gameMananger,this,position: Position, distance: Range+50, direction: (target.Position -Position).ToVector2());
                
                b.onTouch += Hit;

                attackCooldown = new Cooldown(AttackTime);
                attackCooldown.endCooldown += resetAttack;
                attackCooldown.Start();
            }
        }
        /// <summary>
        /// Quand on tape
        /// </summary>
        /// <param name="hited"></param>
        protected virtual void Hit(E_Entity hit)
        {
            if (hit is not null)
            {
                hit.TakeDamage(Damage, this);
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
            FollowTarget(gameTime);
        }

        /// <summary>
        /// Suit la cilbe et l'attque si elle est a porté
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void FollowTarget(GameTime gameTime)
        {
            if (target != null)
            {
                path = new Path(rect.Center, target.rect.Center);
                if (!AttackArea.Intersects(target.rect))
                {
                    I_Moveable moveable = this;
                    moveable.Move(gameTime, ref rect, path.GetDirection(), Speed);
                }
                else
                {
                    attack?.Invoke(target);
                }
            }
            else
            {
                if (!rect.Contains(path.Destination))
                {
                    I_Moveable moveable = this;
                    moveable.Move(gameTime, ref rect, path.GetDirection(), Speed);
                }
            }
        }
        /*
        public override void Move(GameTime gameTime, ref Rectangle rect,Vector2 direction, float speed)
        {

        }
        */

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

        #endregion
    }
}
