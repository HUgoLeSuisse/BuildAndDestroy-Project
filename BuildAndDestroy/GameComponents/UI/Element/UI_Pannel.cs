using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    /// <summary>
    /// Element contenant d'autres elements
    /// </summary>
    public class UI_Pannel : UI_Element
    {
        private List<UI_Element> childrens = new List<UI_Element>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">Rectangle relatif à la Position du parent</param>
        /// <param name="color">Couleur de l'element(par defaut blanc)</param>
        /// <param name="texture">TextureBG de l'element (par defaut 1x1 blanc)</param>
        public UI_Pannel(Rectangle rect, Color? color, Texture2D? image) : base(rect, color, image)
        {
        }

        /// <summary>
        /// Obtient un tableau avec tous les enfants du panneau
        /// </summary>
        /// <returns></returns>
        public UI_Element[] GetChilds()
        {
            return childrens.ToArray();
        }

        /// <summary>
        /// Permet d'ajouter un enfant
        /// </summary>
        /// <param name="child"></param>
        public void Add(UI_Element child)
        {
            childrens.Add(child);
            child.Parent = this;
        }

        /// <summary>
        /// Permet à VisibleVistor de fonctionner
        /// </summary>
        /// <param name="v"></param>
        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }
    }
}
