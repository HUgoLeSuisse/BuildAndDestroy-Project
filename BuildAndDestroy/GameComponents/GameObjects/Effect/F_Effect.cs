using BuildAndDestroy.GameComponents.GameObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Effect
{
    public class F_Effect
    {
        private E_Entity receiver;
        /// <summary>
        /// Celui à qui l'effet a été appliqué
        /// </summary>
        public E_Entity Receiver { get { return receiver; } set { receiver = value; } }

        private E_Entity giver;
        /// <summary>
        /// Celui qui a appliqué l'effet
        /// </summary>
        public E_Entity Giver { get { return giver; } set { giver = value; } }


        public F_Effect(E_Entity receiver, E_Entity giver) 
        { 
            this.receiver = receiver; this.giver = giver;
        }

        public virtual void ApplyStatBonus()
        {

        }
    }
}
