using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Input;
using System.Collections.Generic;
using BuildAndDestroy.GameComponents.GameObjects.Effect;
using BuildAndDestroy.GameComponents.GameObjects.Weapon;
using BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis;
using System.Buffers;
using BuildAndDestroy.GameComponents.Texture;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    /// <summary>
    /// Gére une entité
    /// </summary>
    public class E_Entity : I_Visible, I_Moveable, I_SmartObject, AnimatedObject
    {

        private GameManager gameManager;
        public GameManager GameManager
        {
            get { return gameManager; }
        }

        public E_Entity(
            GameManager gameMananger,
            Rectangle? rect = null,
            float maxHealth = 10,
            float speed = 5,
            float damage = 3,
            float attackSpeed = 1,
            float armor = 1,
            float range = 30,
            LootBox lootBox = null)
        {
            d = DisplayUtils.GetInstance();
            this.rect = rect == null ? new Rectangle(0, 0, 50, 50) : rect.Value;
            this.gameManager = gameMananger;

            animManager = new AnimManager(this);

            stats.Add(SPEED, new Stat(speed));
            stats.Add(DAMAGE, new Stat(damage));
            stats.Add(RANGE, new Stat(range));
            stats.Add(ATTACK_SPEED, new Stat(attackSpeed));
            stats.Add(ARMOR, new Stat(armor));
            stats.Add(HEALTH, new DoubleStat(maxHealth));


            this.lootBox = lootBox;


            path = new Path(this.rect.Center, this.rect.Center);
            UpdateEvents e = UpdateEvents.GetInstance();
            e.Update += Update;


            attack = MeleeAttack;

            onDie += (killer, lootbox) =>
            {
                // donne la lootbox au tueur de l'entité
                if (lootbox != null)
                {
                    if (killer is E_Player)
                    {
                        E_Player p = (E_Player)killer;
                        p.ReciveLootBox(lootbox);
                    }
                }
                Destroy();
            };

        }

        #region Display
        protected Rectangle rect;
        DisplayUtils d;

        private AnimManager animManager;
        public AnimManager AnimManager
        {
            get
            {
                return animManager;
            }
        }
        public bool IsFilped
        {
            get 
            {
                return GetDirection().X <= -0.5;
            }
        }

        public virtual string GetState()
        {
            return "default";
        }

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
        public virtual Texture2D GetCurrentTexture()
        {
            return animManager.GetCurrentTexture();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le rectangle absolu</returns>
        public virtual Rectangle GetAbsoluteRectangle()
        {
            Rectangle r = new Rectangle(
                rect.X - Camera.Instance.Position.X,

                rect.Y - Camera.Instance.Position.Y,
                rect.Width,
                rect.Height
                );
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La couleur acctuel</returns>
        public virtual Color GetCurrentColor()
        {
            return Color.White;
        }
        #endregion

        #region Gameplay

        #region Champs

        // Statistique
        public const string SPEED = "speed";
        public const string ARMOR = "armor";
        public const string DAMAGE = "damage";
        public const string HEALTH = "health";
        public const string ATTACK_SPEED = "attackSpeed";
        public const string RANGE = "range";

        public bool isMoving = false;

        private Dictionary<string, Stat> stats = new Dictionary<string, Stat> ();

        public Dictionary<string, Stat> Stats { get { return stats; } }


        private List<F_Effect> effects = new List<F_Effect>();

        private LootBox lootBox;
        #endregion

        #region Propriété

        /// <summary>
        /// Ensemble d'effet du personnage
        /// </summary>
        public List<F_Effect> Effects { get { return effects; } }

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
                    Stats[RANGE].Total - 10 + rect.Width / 2
                    );
                return r;
            }
        }
        /// <summary>
        /// Peut il attaquer
        /// </summary>
        private bool CanAttack { get { return attackCooldown == null; } }

        /// <summary>
        /// calcule la réduction de dégat en fonction de l'armure
        /// </summary>
        private float DamageReduction
        {
            get
            {
                const float C = 2;
                return C / (C + Stats[ARMOR].Total);
            }
        }

        /// <summary>
        /// le temp à attendre entre chaque attaque
        /// </summary>
        public float AttackTime { get { return 1 / Stats[ATTACK_SPEED].Total; } }


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
        public delegate void onAttack(E_Entity target);
        /// <summary>
        /// Fait attaquer l'entity
        /// </summary>
        public onAttack attack;

        #endregion

        #region Methode

        /// <summary>
        /// Permet d'infliger des dégat à l'entité
        /// </summary>
        /// <param name="amount">nombre de dégat</param>
        /// <param name="enemy">qui à envoyer les dégats</param>
        /// <returns>Retourne si l'entité a été tuer par les dégats</returns>
        public virtual bool TakeDamage(float amount, E_Entity enemy)
        {
            ((DoubleStat)Stats[HEALTH]).CurrentAmount -= amount * DamageReduction;

            if (((DoubleStat)Stats[HEALTH]).IsZero)
            {
                onDie?.Invoke(enemy, lootBox);
                return true;
            }
            return false;

        }

        /// <summary>
        /// Envoie une attaque sur une cible et lance le cooldown d'attaque 
        /// </summary>
        /// <param name="target">La cible a attaquer</param>
        protected virtual void Attack(E_Entity target)
        {
            if (CanAttack)
            {
                attack?.Invoke(target);
                attackCooldown = new Cooldown(AttackTime);
                attackCooldown.endCooldown += resetAttack;
                attackCooldown.Start();
            }
        }

        /// <summary>
        /// Envoie une attaque sur une cible et lance le cooldown d'attaque 
        /// </summary>
        /// <param name="target">La cible a attaquer</param>
        protected void MeleeAttack(E_Entity target)
        {
            Hit(target, Stats[DAMAGE].Total);

        }
        /// <summary>
        /// Quand on tape
        /// </summary>
        /// <param name="hited"></param>
        public virtual void Hit(E_Entity hit, float damage)
        {
            if (hit is not null)
            {
                if (hit.TakeDamage(damage, this))
                {
                    target = null;
                }
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
                    isMoving = true;
                    I_Moveable moveable = this;
                    moveable.Move(gameTime, ref rect, path.GetDirection(), Stats[SPEED].Total);
                }
                else
                {
                    isMoving = false;
                    Attack(target);
                }
            }
            else
            {
                if (!rect.Contains(path.Destination))
                {
                    isMoving = true;
                    I_Moveable moveable = this;
                    moveable.Move(gameTime, ref rect, path.GetDirection(), Stats[SPEED].Total);
                }
                else
                {

                    isMoving = false;
                }
            }
        }

        /// <summary>
        /// Indique à l'entité ou elle doit aller et vérifie si une autre entité s'y trouve et si oui la prend pour cible
        /// </summary>
        /// <param name="pos">la Position</param>
        protected void GoToPosition(Point pos)
        {
            E_Entity target;
            if (gameManager.IsSomeThingHere(pos, out target))
            {
                this.target = target;
            }
            else
            {
                this.target = null;
                path = new Path(rect.Center, pos);
            }
        }

        public virtual void Destroy()
        {
            var effectsArray = effects.ToArray();
            foreach (var item in effectsArray)
            {
                item.Destroy();
            }
            GameManager.DeleteEntity(this);
            UpdateEvents.GetInstance().Update -= Update;
        }

        public Vector2 GetDirectionWith(E_Entity entity)
        {
            Vector2 direction = (entity.Position - Position).ToVector2();
            direction.Normalize();
            return direction;
        }

        public Vector2 GetDirection()
        {
            return path?.GetDirection() ?? new Vector2();
        }

        #endregion

        #endregion
    }
}
