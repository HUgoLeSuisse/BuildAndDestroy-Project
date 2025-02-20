using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Utils
{
    public class LootBox
    {
        private float xp;

        public float Xp { get { return xp; } }
        public LootBox(float xp)
        {
            this.xp = xp;
        }
    }
}
