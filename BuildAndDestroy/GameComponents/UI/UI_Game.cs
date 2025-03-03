using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.UI.Element;
using Microsoft.Xna.Framework;
namespace BuildAndDestroy.GameComponents.UI
{
    /// <summary>
    /// Permet de géré l'interface en jeu et contient le jeu
    /// </summary>
    public class UI_Game : UI_Manager
    {

        private E_Player player;

        /// <summary>
        /// La partie en cours
        /// </summary>
        public UI_Game(Camera cam) : base(cam) 
        { 
            content = new UI_GamePannel();
        }


    }
}
