using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Input;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BuildAndDestroy.GameComponents.GameObjects.Spell;
using System;
using BuildAndDestroy.GameComponents.GameObjects.Spell.Comptency;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_Player : E_Entity
    {

        public E_Player(GameManager gameMananger) : base(gameMananger, rect: new Rectangle(300, 500, 50, 50), range: 50, speed: 10)
        {
            inputManager = new InputManager();
            mouseInput = MouseInput.Instance;
            mouseInput.onRightUp += (onScreenPos, inGamePos) =>
            {
                GoToPosition(inGamePos);
            };

            skills[0] = new S_Fireball(this);
            inputManager.PutInput("FirstSpell", Keys.Q);
            inputManager.GetInputs()["FirstSpell"].onKeyUp += () =>
            {
                skills[0].Use();
            };
            

        }
        #region Display

        /// <summary>
        /// Pour visiter la classe
        /// </summary>
        /// <param name="v"></param>
        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La texture acctuel</returns>
        public override Texture2D GetAcctualTexture()
        {
            return texture;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>La couleur acctuel</returns>
        public override Color GetAcctualColor()
        {
            return Color.Green;
        }


        #endregion

        private InputManager inputManager;
        private MouseInput mouseInput;

        private Skill[] skills = new Skill[3];
        public Skill[] Skills
        {
            get { return skills; } 
        
        }



        private int level = 0;
        private float xp = 0;


        private Knowledges knowledges = new Knowledges(0,0,0);


        public Knowledges Knowledges
        {
            get
            {
                return knowledges;
            }
        }
           

        /// <summary>
        /// Niveau du personnage
        /// </summary>
        public int Level
        {
            get
            {
                return level;
            }
        }
        /// <summary>
        /// La quantité d'xp du personnage
        /// </summary>
        public float Xp
        {
            get
            {
                return xp;
            }
            set
            {
                xp += value;
                if (xp >= NextLevel)
                {
                    xp -= NextLevel;
                    level += 1;
                    levelUp?.Invoke();
                }
            }
        }

        /// <summary>
        /// Xp jusqu'au prochain niveau
        /// </summary>
        public float NextLevel
        {
            get
            {
                return (int)(MathF.Pow(xp, 0.6f) / 15);
            }
        }

        /// <summary>
        /// Vitesse d'attaque
        /// </summary>
        public override float AttackSpeed
        {
            get { return base.AttackSpeed; }
        }
        /// <summary>
        /// Vie acctuelle
        /// </summary>
        public override float Health { get { return base.Health; } }
        /// <summary>
        /// Vie Maximum
        /// </summary>
        public override float MaxHealth { get { return base.MaxHealth; } }

        /// <summary>
        /// Armure
        /// </summary>
        public override float Armor
        {
            get { return base.Armor; }
        }
        /// <summary>
        /// Proté d'attaque
        /// </summary>
        public override float Range
        { get { return base.Range; } }


        public delegate void LevelUp();
        /// <summary>
        /// Quand le personnage gagne un niveau
        /// </summary>
        public LevelUp levelUp;


        /// <summary>
        /// Permet d'attaquer une cible
        /// </summary>
        /// <param name="target">la cible</param>
        protected override void MeleeAttack(E_Entity target)
        {
            base.MeleeAttack(target);
        }

        /// <summary>
        /// Permet de recevoir une loot box
        /// </summary>
        /// <param name="loot">la loot box</param>
        public void ReciveLootBox(LootBox loot)
        {
            if (loot is not null)
            {
                Xp = loot.Xp;
            }
        }

        public Vector2 GetMouseDirection()
        {
            return mouseInput.GetMousePosition().ToVector2()-Rect.Center.ToVector2();
        }
    }
}