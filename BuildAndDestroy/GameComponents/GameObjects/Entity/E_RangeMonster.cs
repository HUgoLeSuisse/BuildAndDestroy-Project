using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public class E_RangeMonster : E_Monster
    {
        public E_RangeMonster(
            GameManager gameMananger,
            Point? position = null,
            Point? size = null,
            Texture2D texture = null,
            float maxHealth = 5,
            float speed = 5,
            float damage = 1,
            float attackSpeed = 1.5f,
            float armor = 1,
            float range = 300,
            LootBox lootBox = null
            ) : base(
                gameMananger,
                position:position,
                size:size,
                maxHealth: maxHealth,
                speed: speed,
                damage: damage,
                attackSpeed: attackSpeed,
                armor: armor,
                range: range,
                lootBox: lootBox)
        {
            attack = RangeAttack;
        }

        /// <summary>
        /// Attaque à distance
        /// </summary>
        /// <param name="target"></param>
        private void RangeAttack(E_Entity target)
        {
            Bullet b = new Bullet(GameManager, this,
                size: new Point(20, 20),
                direction: GetDirectionWith(target),
                distance: Stats[RANGE].Total);
            b.onTouch += (entity) =>
            {
                Hit(entity, Stats[DAMAGE].Total);
            };
        }
    }
}
