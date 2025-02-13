using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    /// <summary>
    /// Panneau contenant une partie et un ensemble d'UI_Element
    /// </summary>
    public class UI_GamePannel : UI_Pannel
    {
        public GameManager game { get; private set; }
        public UI_GamePannel() : base(new Rectangle(0,0,DisplayUtils.GetInstance().width, DisplayUtils.GetInstance().height), Color.Transparent, null)
        {
            game = new GameManager();
        }

        public I_Visible[] GetGameElement(Rectangle rect)
        {
            return game.GetVisibleElement();
        }
        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }
    }
}
