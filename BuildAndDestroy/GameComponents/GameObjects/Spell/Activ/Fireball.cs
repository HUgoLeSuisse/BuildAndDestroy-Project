using BuildAndDestroy.GameComponents.GameObjects.Entity;
using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell.Activ
{
    public class Fireball : Active
    {
        const float BASE_DAMAGE = 3;
        const float MAGIC_RATIO = 0.5f;
        const float DISTANCE = 500;
        const float SPEED = 15;
        const int SIZE = 30;
        const int COOLDOWN = 3;


        public Fireball(GameManager gm, Skill skill) : base(gm,skill,COOLDOWN)
        {
            onUse += Launch;
        }

        /// <summary>
        /// Lance la boule de feu
        /// </summary>
        /// <param name="skillData"></param>
        private void Launch(SkillData skillData)
        {
            // envoyer un projectile
            Bullet fireBall = new Bullet(gm,skill.Owner,
                position: skill.Owner.Position,
                size: new Point(SIZE, SIZE),
                distance: DISTANCE,
                direction: skill.Owner.GetMouseDirection(),
                speed: SPEED
                );

            //quand le projectile touche
            fireBall.onTouch += (hitted) =>
            {
                OnTouch(skillData, hitted);
            };
        }

        /// <summary>
        /// Quand le projetcile touche
        /// </summary>
        /// <param name="skillData"></param>
        /// <param name="hitted"></param>
        private void OnTouch(SkillData skillData, E_Entity hitted)
        {
            if (hitted != null)
            {
                hitted.TakeDamage(BASE_DAMAGE + MAGIC_RATIO * skill.Owner.Knowledges.Magic, skill.Owner);
            }
        }
    }
}
