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
    public class S_Fireball : Skill
    {
        public S_Fireball( E_Player owner) : base("Fireball", owner, null, null)
        {
            active = new Fireball(owner.GameManager,this);
        }
    }
}
