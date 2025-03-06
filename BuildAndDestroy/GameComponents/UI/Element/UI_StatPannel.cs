using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildAndDestroy.GameComponents.UI.Element
{
    public class UI_StatPannel : UI_Pannel
    {
        private E_Player player;
        private UI_Label health;
        private UI_Label level;

        private UI_Button force;
        private UI_Button magic;
        private UI_Button sicence;

        public UI_StatPannel(Rectangle rect, Color? color, Texture2D image, E_Player player) : base(rect, color, image)
        {
            this.player = player;

            UI_Pannel weapon = new UI_Pannel(new Rectangle(0, 0, 500, 150), Color.Brown, null);
            Add(weapon);
            UI_Pannel stats = new UI_Pannel(new Rectangle(0, 0, 500, 150), Color.SaddleBrown, null);
            Add(stats);
            stats.xAlign = Horizontal.R;

            health = new UI_Label(position : new Point(0,0), text:"xxxx",color:Color.Transparent, fontColor : Color.White);
            stats.Add(health);
            health.xAlign = Horizontal.R;

            force = new UI_Button(
                text : "0",
                fontSize: 0.4f,
                color: new Color(0xffffff),
                overColor: new Color(0xffffff),
                pressedColor: new Color(0xffffff),
                fontColor: Color.White
                );
            stats.Add(force);
            force.yAlign = Vertical.T;

            magic = new UI_Button(

                text: "0",
                fontSize: 0.4f,
                color: new Color(0x7029fb),
                overColor: new Color(0x5800ff),
                pressedColor: new Color(0x9565f5 ),
                fontColor: Color.White
                );
            stats.Add(magic);
            magic.yAlign = Vertical.M;

            sicence = new UI_Button(
                text: "0",
                fontSize: 0.4f,
                color: new Color(0xffffff),
                overColor: new Color(0xffffff),
                pressedColor: new Color(0xffffff),
                fontColor: Color.White
                );
            stats.Add(sicence);
            sicence.yAlign = Vertical.B;


            UpdateEvents.GetInstance().PreUpdate += Update;

        }

        private void Update(GameTime gameTime)
        {
            health.text = player.Health.ToString(); 
        }
    }
}
