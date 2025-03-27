using BuildAndDestroy.GameComponents.GameObjects.Entity;
using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.GameObjects.Spell.Activ
{
    public class Fireball : Active
    {
        const float BASE_DAMAGE = 3;
        const float SCALING_RATIO = 0.5f;
        const float DISTANCE = 500;
        const float SPEED = 15;
        const int SIZE = 30;
        public Fireball(GameManager gm, Skill skill) : base(gm,skill,5)
        {
            onUse += Launch;
        }

        private void Launch(SkillData skillData)
        {
            Bullet fireBall = new Bullet(gm,skill.Owner,
                position: skill.Owner.Position,
                size: new Point(SIZE, SIZE),
                distance: DISTANCE,
                direction: skill.Owner.GetMouseDirection(),
                speed: SPEED
                );

            fireBall.onTouch += (hitted) =>
            {
                OnTouch(skillData, hitted);
            };
        }

        private void OnTouch(SkillData skillData, E_Entity hitted)
        {
            if (hitted != null)
            {
                hitted.TakeDamage(BASE_DAMAGE + SCALING_RATIO * skill.Owner.Knowledges.Magic, skill.Owner);
            }
        }
    }
}
