using Microsoft.Xna.Framework;
using BuildAndDestroy.GameComponents.UI.Element;
using BuildAndDestroy.GameComponents.Utils;

namespace BuildAndDestroy.GameComponents.UI
{
    /// <summary>
    /// Permet de géré une interface utilisateur
    /// </summary>
    public abstract class UI_Manager : I_SmartObject
    {
        protected DisplayUtils d;
        protected Camera cam;

        /// <summary>
        /// Ensemble des élments contenant l'interface
        /// </summary>
        protected UI_Pannel content;

        protected UI_Manager(Camera cam)
        {
            d = DisplayUtils.GetInstance();
            this.cam = cam;
        }

        public void Destroy()
        {
            content.Destroy();
        }

        /// <summary>
        /// Permet d'obtenir l'ensemble des élements
        /// </summary>
        /// <returns>L'ensemble des élements</returns>
        public virtual UI_Pannel GetContent()
        {
            return content;
        }
    }
}
