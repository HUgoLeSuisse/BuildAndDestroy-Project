using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.UI.Element;
using Microsoft.Xna.Framework;
using System;
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
            UI_GamePannel gamePannel = new UI_GamePannel();
            content = gamePannel;
            player = gamePannel.GetPlayer();
        }

        public E_Player GetPlayer()
        {
            return player;
        }
        public override UI_Pannel GetContent()
        {
            return content;
        }
    }
}
