using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
    public interface I_Moveable
    {
        public void Move(GameTime gameTime, ref Rectangle rect, Vector2 dir, float speed)
        {
            rect.Offset(
                dir.X * speed * gameTime.ElapsedGameTime.Milliseconds/30,
                dir.Y * speed * gameTime.ElapsedGameTime.Milliseconds/30
                );

        }
    }
}