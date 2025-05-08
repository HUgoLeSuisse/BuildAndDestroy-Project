using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.UI;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildAndDestroy.GameComponents.GameObjects
{
    public class GameManager : I_SmartObject
    {
        public static List<DrawableCircle> drawableCircles = new List<DrawableCircle>();

        UpdateEvents e = UpdateEvents.GetInstance();

        E_Player player;
        List<E_Monster> monsters = new List<E_Monster>();

        Timer Timer = new Timer ();


        public GameManager() {
            e.Update += Update;

            player = new E_Player(this);
            var m1 = new E_Monster(
                this,
                position: new Point(800, 500),
                size: new Point(150, 150),
                lootBox: new LootBox(100)
                );
            var m2 = new E_RangeMonster(
                this,
                position: new Point(1600, 500),
                size: new Point(75, 75),
                lootBox: new LootBox(100)
                );
            var m3 = new E_Monster(
                this,
                maxHealth: 200,
                damage: 6,
                position: new Point(24000, 500),
                size: new Point(200, 200),
                lootBox: new LootBox(300)
                );
            monsters.Add(m1 );
            monsters.Add(m2);
            monsters.Add(m3);
        }

        /// <summary>
        /// Destroy An Entity
        /// </summary>
        /// <param name="killer"></param>
        /// <param name="lootBox"></param>
        public void DeleteEntity(E_Entity killed)
        {
            if (killed is E_Monster)
            {
                monsters.Remove((E_Monster)killed);
            }
        }

        /// <summary>
        /// Vérifie s'il y a une entité à une Position donnée
        /// </summary>
        /// <param name="pos">la postion</param>
        /// <param name="who">l'entité (si il y en a une)</param>
        /// <returns></returns>
        public bool IsSomeThingHere(Point pos, out E_Entity who)
        {
            if (player.Rect.Contains(pos))
            {
                who = player;
                return true;
            }
            foreach (var monster in monsters)
            {
                if (monster.Rect.Contains(pos))
                {
                    who = monster;
                    return true;
                }
            }
            who = null;
            return false;
        }
        /// <summary>
        /// Vérifie s'il y a une entité qui collision avec le rectangle donné
        /// </summary>
        /// <param name="rect">le rectangle</param>
        /// <param name="who">l'entité (si il y en a une)</param>
        /// <returns></returns>
        public bool IsSomeThingHere(Rectangle rect, out List<E_Entity> who)
        {
            who = new List<E_Entity>();
            if (player.Rect.Intersects(rect))
            {
                who.Add(player);
            }
            foreach (var monster in monsters)
            {
                if (monster.Rect.Intersects(rect))
                {
                    who.Add(monster);
                }
            }
            if (who.Count >= 1) { return true; }
            return false;
        }
        /// <summary>
        /// Vérifie s'il y a une entité qui collision avec le rectangle donné
        /// </summary>
        /// <param name="rect">le rectangle</param>
        /// <param name="who">l'entité (si il y en a une)</param>
        /// <returns></returns>
        public bool IsSomeThingHere(Circle circ, out List<E_Entity> who)
        {
            who = new List<E_Entity>();
            if (circ.Intersects(player.Rect))
            {
                who.Add(player);
            }
            foreach (var monster in monsters)
            {
                if (circ.Intersects(monster.Rect))
                {
                    who.Add(monster);
                }
            }
            if (who.Count >= 1) { return true; }
            return false;
        }


        private void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Retourne les éléments à afficher
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public I_Visible[] GetVisibleElement(Rectangle rect)
        {
            List<I_Visible> visibles = drawableCircles.Cast<I_Visible>().ToList();
            List<E_Entity> entities;
            IsSomeThingHere(rect, out entities);
            foreach (var entity in entities)
            {
                visibles.Add(entity);
            }
            foreach (var bullet in Bullet.Bullets)
            {
                visibles.Add(bullet);
            }
            return visibles.ToArray();
        }

        /// <summary>
        /// Retourne le joueur
        /// </summary>
        /// <returns></returns>
        public E_Player GetPlayer()
        {
            return player;
        }

        public void Destroy()
        {
            drawableCircles.Clear();
            player?.Destroy();
            player = null;
            foreach (var item in monsters)
            {
                item.Destroy();
            }
            e.Update -= Update;
            Timer?.Destroy();
            Timer = null;
        }
    }
}
