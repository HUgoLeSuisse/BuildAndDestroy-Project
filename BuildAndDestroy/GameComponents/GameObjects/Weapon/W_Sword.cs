using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Weapon
{
    /// <summary>
    /// Permet de gérer une épée
    /// </summary>
    public class W_Sword : W_Weapon 
    {
        public W_Sword(E_Player owner) : base(owner, "Sword")
        {
        }


        /// <summary>
        /// Permet d'attaquer avec l'arme
        /// </summary>
        /// <param name="target">cible à attaquer</param>
        public override void Attack(E_Entity target)
        {
            base.Attack(target);
            Owner.Hit(target, Owner.Stats[E_Entity.DAMAGE].Total * (Owner.Knowledges.Force + 1));
        }

        public override Texture2D GetCurrentTexture()
        {
            return DisplayUtils.GetInstance().GetByPath<Texture2D>("sword");
        }
    }
}
