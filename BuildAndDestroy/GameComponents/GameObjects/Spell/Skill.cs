using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell
{
    public class Skill 
    {
        private E_Player owner;

        public E_Player Owner
        {
            get { return owner; }
        }

        private string name;
        private Texture2D icon;
        public Texture2D Icon { get { return icon; } }
        public Color Color
        {
            get
            {

                if (active != null && !active.IsAvailable)
                {
                    return Color.Gray;
                }
                return Color.White;
            }
        }


        protected Active active;
        public Active Active
        {
            get { return active; }
        }
        protected List<Passive> passives = new List<Passive>();

        public List<Passive> Passives
        {
            get
            {
                return passives;
            }
        }

        public Knowledges RequiredKnowledges{ get; private set; }

        public Skill
            (
            string name,
            E_Player owner,
            Knowledges requierd = null,
            Texture2D icon = null
            )
        {
            this.name = name;
            this.owner = owner;

            this.icon = icon ?? DisplayUtils.GetInstance().blank;
            RequiredKnowledges = requierd ?? new Knowledges(0,0,0);

        }
        public bool Use()
        {
            if (Owner.Knowledges.IsEnough(RequiredKnowledges))
            {
                if (active.IsAvailable)
                {
                    active.onUse?.Invoke(new SkillData());
                    return true;
                }
            }
            return false;
        }

    }
}
