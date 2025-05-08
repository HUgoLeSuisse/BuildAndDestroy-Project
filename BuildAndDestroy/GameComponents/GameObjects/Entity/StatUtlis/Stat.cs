using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis
{
    /// <summary>
    /// Permet de gérer une statistique
    /// </summary>
    public class Stat
    {
        private List<StatModifier> mods = new List<StatModifier>();

        /// <summary>
        /// vous modifier la statistique (bonus / malus)
        /// </summary>
        public List<StatModifier> Modifiers
        {
            get
            {
                return mods;
            }
        }

        private float baseValue;

        /// <summary>
        /// valeur de base
        /// </summary>
        public float BaseValue
        {
            get
            {
                return baseValue;
            }
        }

        /// <summary>
        /// total des bonus simples
        /// </summary>
        public float FlatBonus
        {
            get
            {
                float amount = 0;
                foreach (var item in Modifiers)
                {
                    if (item.IsFlat)
                    {
                        amount += item.Amount;
                    }
                }
                return amount;
            }
        }

        /// <summary>
        /// total des bonus en pourcent
        /// </summary>
        public float PercentBonus
        {
            get
            {
                float amount = 0;
                foreach (var item in Modifiers)
                {
                    if (!item.IsFlat)
                    {
                        amount += item.Amount; 
                    }
                }
                return amount;
            }
        }

        /// <summary>
        /// Valeur après application des bonus
        /// </summary>
        public float Total
        {
            get
            {
                if (PercentBonus <= -1)
                {
                    return 0; 
                }
                return (baseValue+FlatBonus)*(PercentBonus+1);
            }
        }


        public Stat(float baseValue) 
        {
            this.baseValue = baseValue;
        }

    }
}
