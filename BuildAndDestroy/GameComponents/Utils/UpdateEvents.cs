using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents.Utils
{
    /// <summary>
    /// Permet de rendre les events Update accesible à tous les classe // Singleton
    /// </summary>
    public class UpdateEvents
    {
        #region Singleton
        private static UpdateEvents instance;
        private UpdateEvents() { }
        public static UpdateEvents GetInstance()
        {
            if (instance == null)
            {
                instance = new UpdateEvents();
            }
            return instance;
        }

        #endregion

        public delegate void E_Update(GameTime gameTime);
        /// <summary>
        /// Update utiliser principalement pour la logique
        /// </summary>
        public E_Update Update;
        /// <summary>
        /// Avant l'update utiliser principalement pour les inputs
        /// </summary>
        public E_Update PreUpdate;
    }
}
