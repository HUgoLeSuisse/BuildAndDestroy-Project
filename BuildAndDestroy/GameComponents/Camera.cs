using BuildAndDestroy.GameComponents.UI;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;

namespace BuildAndDestroy.GameComponents
{
    /// <summary>
    /// S'occupe de ce qu'il faut afficher
    /// </summary>
    public class Camera
    {
        private static Camera instance;
        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }
        }
        private DisplayUtils d;
        private UI_Manager UI;
        public Point Position { get; private set; }
        private Camera()
        {
            d = DisplayUtils.GetInstance();
            UI = new UI_Menu(this);
            UpdateEvents.GetInstance().Update += Update;
        }
        public void Update(GameTime gameTime)
        {
            if (UI is UI_Game)
            {
                UI_Game game = UI as UI_Game;
                Position = game.GetPlayer().Rect.Center - new Point(d.width / 2, d.height / 2);
            }
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
