using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Spell.Activ;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell.Comptency
{
    public class S_CircularSlash : Skill
    {
        public S_CircularSlash( E_Player owner) : base("Circular Slash", owner, new Knowledges(2,0,0), null)
        {
            active = new CircularSlash(owner.GameManager, this);
        }
    }
}
