using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis
{
    /// <summary>
    /// Permet d'appliquer un modificateur sur une statisque
    /// </summary>
    public class StatModifier
    {
        private bool isFlat;
        public bool IsFlat
        {
            get { return isFlat; }
        }

        private float amount;

        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFalt">True : une valeur fix / False : un pourcentage </param>
        /// <param name="amount">Mettre en négatif si malus (mettre pourcentage en decimal Ex : 50% => 0.5f)</param>
        public StatModifier(bool isFlat, float amount)
        {
            this.isFlat = isFlat;
            this.amount = amount;
        }
    }
}
