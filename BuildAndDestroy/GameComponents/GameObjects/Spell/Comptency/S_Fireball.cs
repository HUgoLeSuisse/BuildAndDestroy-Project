using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Spell.Activ;
using BuildAndDestroy.GameComponents.GameObjects.Spell.Passiv;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell.Comptency
{
    /// <summary>
    /// Permet de lancer une comptéance boule de feux et possède une comptéance passive
    /// </summary>
    public class S_Fireball : Skill
    {
        public S_Fireball( E_Player owner) : base("Fireball", owner, new Knowledges(0,1,0), null)
        {
            active = new Fireball(owner.GameManager,this);
            
            //passives.Add(new DamageZone(owner.GameManager, this, color: new Color(120, 170, 190, 127)));
        }
    }
}
