using BuildAndDestroy.GameComponents.GameObjects;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

            UI_Pannel basePannel = new UI_Pannel(new Rectangle(0, 0, 1000, 150), Color.White, null);
            UI_Pannel weapon = new UI_Pannel(new Rectangle(0, 0, 500, 150), Color.Brown, null);
            UI_Pannel stats = new UI_Pannel(new Rectangle(500, 0, 500, 150), Color.SaddleBrown, null);
            basePannel.Add(weapon);
            basePannel.Add(stats);
            UI_Label uI_Label = new UI_Label();
            Add(basePannel);
            /*
            basePannel.xAlign = Horizontal.M;
            basePannel.yAlign = Vertical.B;*/
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
