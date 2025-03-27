using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Spell;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    public class UI_Skill : UI_Element
    {
        private Skill skill;
        public Skill Skill { get { return skill; } }
        UpdateEvents updateEvents;
        public UI_Skill(Skill skill,Rectangle rect) : base(rect, null, null)
        {
            this.skill = skill;

            updateEvents = UpdateEvents.GetInstance();
            updateEvents.Update += Update;

        }

        private void Update(GameTime gameTime)
        {
            if (skill != null)
            {
                texture = skill.Icon;
                ColorBG = skill.Color;
            }
        }

        /// <summary>
        /// Pour le visiteur
        /// </summary>
        /// <param name="v"></param>
        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }
    }
}
