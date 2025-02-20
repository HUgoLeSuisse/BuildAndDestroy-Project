using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Utils
{
    /// <summary>
    /// Un ensemble de récompense qui peut être donnée à un joueur
    /// </summary>
    public class LootBox
    {
        private float xp;

        /// <summary>
        /// la quantité d'xp a recevoir
        /// </summary>
        public float Xp { get { return xp; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xp">la quantité d'xp a recevoir</param>
        public LootBox(float xp)
        {
            this.xp = xp;
        }
    }
}
