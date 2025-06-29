﻿using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Input;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BuildAndDestroy.GameComponents.GameObjects.Spell;
using System;
using BuildAndDestroy.GameComponents.GameObjects.Spell.Comptency;
using BuildAndDestroy.GameComponents.GameObjects.Weapon;
using BuildAndDestroy.GameComponents.Texture;
using System.Security.Principal;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_Player : E_Entity
    {

        public E_Player(GameManager gameMananger) : base(gameMananger, rect: new Rectangle(300, 500, 50, 50), range: 50, speed: 10, armor:3)
        {
            inputManager = new InputManager();
            mouseInput = MouseInput.Instance;
            mouseInput.onRightUp += (onScreenPos, inGamePos) =>
            {
                GoToPosition(inGamePos);
            };

            // Asigne la boule de feu sur le premier sort (test a supprimer par la suite)
            skills[0] = new S_Fireball(this);
            inputManager.PutInput("FirstSpell", Keys.Q);
            inputManager.GetInputs()["FirstSpell"].onKeyUp += () =>
            {
                skills[0].Use();
            };
            // Asigne le coup circulaire sur le deuxième sort (test a supprimer par la suite)
            skills[1] = new S_CircularSlash(this);
            inputManager.PutInput("SecondSpell", Keys.W);
            inputManager.GetInputs()["SecondSpell"].onKeyUp += () =>
            {
                skills[1].Use();
            };

            // Asigne une arme par defaut (test a supprimer par la suite)
            Weapon = new W_Sword(this); // attaque au corp à corp
            
            //Weapon = new W_MagicStaff(this); // attaque à distance

            AnimManager.Add(new SimpleAnim("character_1/idle", 8), "idle");
            AnimManager.Add(new SimpleAnim("character_1/front",8),"front");
            AnimManager.Add(new SimpleAnim("character_1/side", 8), "side");
            AnimManager.Add(new SimpleAnim("character_1/back", 8), "back");
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

        public override string GetState()
        {
            Vector2 dir = GetDirection();
            if (dir.Y > 0.5f)
            {
                return "front";
            }
            if (dir.Y < -0.5f)
            {
                return "back";
            }
            if (dir.X + dir.Y == 0)
            {
                return "idle";
            }
            return "side";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>La couleur acctuel</returns>
        public override Color GetCurrentColor()
        {
            return Color.White;
        }


        #endregion


        private InputManager inputManager;
        private MouseInput mouseInput;

        private Skill[] skills = new Skill[3];
        private W_Weapon weapon;


        private int level = 1;
        private float xp = 0;

        private Knowledges knowledges = new Knowledges(0, 0, 0);


        /// <summary>
        /// Compétance du personnage
        /// </summary>
        public Skill[] Skills
        {
            get { return skills; }

        }


        /// <summary>
        /// Arme équipé du personnage
        /// </summary>
        public W_Weapon Weapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                if (weapon != null)
                {
                    attack = weapon.Attack;
                }
                else
                {
                    attack = MeleeAttack;
                }
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
        /// (Pour en ajouté il faut faire Xp = QTE_A_AJOUTEE)
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
                while (xp >= NextLevel)
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
                return (int)(MathF.Pow(Level, 0.6f) * 100);
            }
        }


        /// <summary>
        /// Connaissances du personnage
        /// </summary>
        public Knowledges Knowledges
        {
            get
            {
                return knowledges;
            }
        }


        public delegate void LevelUp();
        /// <summary>
        /// Quand le personnage gagne un niveau
        /// </summary>
        public LevelUp levelUp;


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

        /// <summary>
        /// Obtient la direction entre l'entité et la souris
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMouseDirection()
        {
            Vector2 dir = mouseInput.GetMousePosition().ToVector2() - Rect.Center.ToVector2();
            dir.Normalize();
            return dir;
        }


        /// <summary>
        /// Supprime le joueur de manière deffinitive
        /// </summary>
        public override void Destroy()
        {
            base.Destroy();
            inputManager.Destroy();
            foreach (var item in skills)
            {
                item?.Destroy();
            }

        }

        public void RemoveSkill(Skill skill)
        {
            for (int i = 0; i < Skills.Length; i++)
            {
                if (Skills[i] == skill)
                {
                    skills[i] = null;
                }
            }
        }
    }
}