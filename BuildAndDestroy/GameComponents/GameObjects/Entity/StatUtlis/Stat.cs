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
        public List<StatModifier> Modifiers
        {
            get
            {
                return mods;
            }
        }

        private float baseValue;
        public float BaseValue
        {
            get
            {
                return baseValue;
            }
        }
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
