using BuildAndDestroy.GameComponents.GameObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
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

        public abstract void Attack(E_Entity target);

    }
}
