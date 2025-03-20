using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.GameObjects.Skill
{
    public class Skill
    {
        private E_Player owner;
        private string name;


        private Active active;
        private List<Passive> passives = new List<Passive>();
        public Active Active
        {
            get { return active; }
            set { active = value; }
        }

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
            Knowledges requierd = null
            )
        {
            this.name = name;
            this.owner = owner;

            RequiredKnowledges = requierd ?? new Knowledges(0,0,0);

        }
        

    }
}
