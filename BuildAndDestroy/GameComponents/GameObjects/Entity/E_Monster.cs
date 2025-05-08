using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_Monster : E_Entity
    {

        public E_Monster(
            GameManager gameMananger,
            Point? position = null,
            Point? size = null,
            Texture2D texture = null,
            float maxHealth = 10,
            float speed = 5,
            float damage = 4,
            float attackSpeed = 0.8f,
            float armor = 1,
            float range = 30,
            bool isRange = false,
            LootBox lootBox = null
            ) : base(gameMananger,
                new Rectangle
                (position == null ? new Point(0,0): position.Value,
                    size == null ? new Point(0, 0) : size.Value),
                texture,
                maxHealth: maxHealth,
                speed: speed,
                attackSpeed: attackSpeed,
                armor: armor,
                range: range,
                lootBox: lootBox
                )
        {
        }
        protected override void Update(GameTime gameTime)
        { 
            base.Update(gameTime);
        }

        /// <summary>
        /// retourne la couleur acctuelle de l'enemie
        /// </summary>
        /// <returns></returns>
        public override Color GetAcctualColor()
        {
            return Color.Red;
        }

        /// <summary>
        /// Subit des dégats et réagis en conséquence
        /// </summary>
        /// <param name="amount">Quantité de dégat</param>
        /// <param name="enemy">Enemy qui inflige les dégats</param>
        public override bool TakeDamage(float amount, E_Entity enemy)
        {
            bool value = base.TakeDamage(amount,enemy);
            SetTarget(enemy);
            return value;
        }

        /// <summary>
        /// Permet de ciblé un enemy
        /// </summary>
        /// <param name="enemy">La cible</param>
        private void SetTarget(E_Entity enemy)
        {
            target = enemy;
        }

    }
}
