using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis
{
    /// <summary>
    /// Cette classe permet de géré une statistique qui à une valeur maximal et une valeur acctuel (ex : point de vie)
    /// </summary>
    public class DoubleStat : Stat
    {
        private float current;

        /// <summary>
        /// valeur acctuelle
        /// </summary>
        public float CurrentAmount
        {
            get { return current; }
            set
            {
                current = value;
                if (current <= 0)
                {
                    current = 0;
                }
                if (current > Total)
                {
                    current = Total;
                }
            }

        }
        /// <summary>
        /// ratio entre la valeur acctuelle et la valeur maximale 
        /// </summary>
        public float Percent
        {
            get
            {
                return CurrentAmount / Total;
            }
        }
        /// <summary>
        /// est ce que la valeur acctuelle est en dessous de zero
        /// </summary>
        public bool IsZero
        {
            get
            {
                return CurrentAmount <= 0;
            }
        }

        public DoubleStat(float baseValue) : base(baseValue)
        {
            current = baseValue;
        }

    }
}
