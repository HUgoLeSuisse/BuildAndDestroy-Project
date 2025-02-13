using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects
{
    public class GameManager
    {
        E_Player player;
        E_Monster monster;
        Timer Timer = new Timer ();
        UpdateEvents e = UpdateEvents.GetInstance();

        public GameManager() {
            e.PreUpdate += PerUpdate;
            e.Update += Update;

            player = new E_Player(this);

            monster = new E_Monster(
                this,
                position : new Point(500,500),
                size : new Point(150,150),
                maxHealth: 100
                );
        }

        /// <summary>
        /// Vérifie s'il y a une entité à une position donnée
        /// </summary>
        /// <param name="pos">la postion</param>
        /// <param name="who">l'entité (si il y en a une)</param>
        /// <returns></returns>
        public bool IsSomethingHere(Point pos, out E_Entity who)
        {
            if (monster.GetAbsoluteRectangle().Contains(pos))
            {
                who = monster;
                return true;
            }
            if (player.GetAbsoluteRectangle().Contains(pos)) 
            {
                who = player;
                return true;
            }
            who = null;
            return false;
        }

        private void PerUpdate(GameTime gameTime)
        {
        }

        private void Update(GameTime gameTime)
        {
        }

        public I_Visible[] GetVisibleElement()
        {
            return new I_Visible[] { player, monster };
        }
    }
}
