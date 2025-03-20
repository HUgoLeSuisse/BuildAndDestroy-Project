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
    }
}
