using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Utils
{
    public class Knowledges
    {
        private int force = 0;
        private int magic = 0;
        private int science = 0;

        public int Force
        {
            get
            {
                return force;
            }
            set
            {
                force = value;
            }
        }
        public int Magic
        {
            get
            {
                return magic;
            }
            set
            {
                magic = value;
            }
        }
        public int Science
        {
            get
            {
                return science;
            }
            set
            {
                science = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f">Force</param>
        /// <param name="m">Magie</param>
        /// <param name="s">Science</param>
        public Knowledges(int f, int m, int s)
        {
            Force = f;
            Magic = m;
            Science = s;
        }

        /// <summary>
        /// Compare si la connaissance d'origne (this) et plus grand ou égale sur tous les points que celle en paramètre
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public bool IsEnough (Knowledges k)
        {
            if (k.Force > Force)
            {
                return false;
            }
            if (k.Magic > Magic)
            {
                return false;
            }
            if (k.Science > Science)
            {
                return false;
            }
            return true;
        }
    }
}
