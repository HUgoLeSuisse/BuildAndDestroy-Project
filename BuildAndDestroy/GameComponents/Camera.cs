using BuildAndDestroy.GameComponents.UI;
using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents
{
    /// <summary>
    /// S'occupe de ce qu'il faut afficher
    /// </summary>
    public class Camera
    {
        private UI_Manager UI;
        public Point position {  get; private set; }
        public Camera() 
        { 
            UI = new UI_Menu(this);
        }

        /// <summary>
        /// Changer d'interface utilisateur
        /// </summary>
        /// <param name="_ui"></param>
        public void ChangeUI(UI_Manager _ui)
        {
            UI = _ui;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Le pannau d'affichage contenant tous les elements à afficher</returns>
        public I_Visible GetUI_Pannel() 
        { 
            return UI.GetContent();
        }

    }
}
