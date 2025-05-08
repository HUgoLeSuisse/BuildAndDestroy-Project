using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.GameObjects.Entity
{
 
    /// <summary>
    /// Pour tout object qui peut ce déplacer 
    /// </summary>
    public interface I_Moveable
    {
        /// <summary>
        /// Déplace l'objet
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="rect"> rectangle de l'objet</param>
        /// <param name="dir">direction dans laquelle aller</param>
        /// <param name="speed">vitesse</param>
        public void Move(GameTime gameTime, ref Rectangle rect, Vector2 dir, float speed)
        {
            rect.Offset(
                dir.X * speed * gameTime.ElapsedGameTime.Milliseconds/30,
                dir.Y * speed * gameTime.ElapsedGameTime.Milliseconds/30
                );

        }
    }
}