using BuildAndDestroy.GameComponents.GameObjects.Pathfinding;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Input;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_Player : E_Entity
    {

        
        private InputManager inputManager;
        private MouseInput mouseInput;

        private float xp;

        /// <summary>
        /// Niveau du personnage
        /// </summary>
        public int Level { get {
                return (int)(MathF.Pow(xp, 0.6f) / 15);
            } }

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


        public E_Player(GameManager gameMananger) : base(gameMananger, rect: new Rectangle(300, 500, 50, 50), range: 50, speed: 10)
        {

            inputManager = new InputManager();
            mouseInput = new MouseInput();
            mouseInput.onRightUp += GoToPosition;

        }
        #region Display
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
        /// <returns>Le rectangle absolu</returns>
        public override Rectangle GetAbsoluteRectangle()
        {
            return rect;
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

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        protected override void Attack(E_Entity target)
        {
            target.onDie += (me, loot) =>
            {
            };
            base.Attack(target);
        }
        public void ReciveLootBox(LootBox loot)
        {

        }
        /// <summary>
        /// Fait gagner de l'xp au personnage
        /// </summary>
        /// <param name="amount"></param>
        public void EarnXP(int amount)
        {
            int level = Level;
            xp += amount;
            if (level < Level)
            {
                levelUp?.Invoke();
            }
        }

    }
}