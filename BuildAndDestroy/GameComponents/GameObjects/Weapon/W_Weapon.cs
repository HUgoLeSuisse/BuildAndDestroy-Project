using BuildAndDestroy.GameComponents.GameObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
    /// <summary>
    /// Classe de base pour une arme
    /// </summary>
    public abstract class W_Weapon
    {
        private E_Player owner;
        public E_Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public W_Weapon(E_Player owner, string name)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Permet d'attaquer avec l'arme
        /// </summary>
        /// <param name="target">cible à attaquer</param>
        public abstract void Attack(E_Entity target);

    }
}
