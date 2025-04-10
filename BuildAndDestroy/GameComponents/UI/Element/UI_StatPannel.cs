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
        private int levelAvailable = 1;
        public int LevelAvailable
        {
            get
            {
                return levelAvailable;
            }
            set
            {
                levelAvailable = value;
                if (levelAvailable <= 0)
                {
                    force.IsEnabled = false;
                    magic.IsEnabled = false;
                    sicence.IsEnabled = false;
                }
                if (levelAvailable > 0)
                {
                    force.IsEnabled = true;
                    magic.IsEnabled = true;
                    sicence.IsEnabled = true;
                }
            }
        }
        private E_Player player;
        private UI_Label health;
        private UI_Label level;

        private UI_Button force;
        private UI_Button magic;
        private UI_Button sicence;

        public UI_StatPannel(Rectangle rect, Color? color, Texture2D image, E_Player player) : base(rect, color, image)
        {
            UpdateEvents.GetInstance().PreUpdate += Update;
            this.player = player;
            player.levelUp += () =>
            {
                LevelAvailable++;
            };
            UI_Pannel weapon = new UI_Pannel(
                new Rectangle(0, 0, 500, 150),
                Color.Brown, null);
            Add(weapon);

            UI_Pannel stats = new UI_Pannel(
                new Rectangle(0, 0, 500, 150),
                Color.SaddleBrown, null);
            Add(stats);
            stats.xAlign = Horizontal.R;

            health = new UI_Label(
                position : new Point(0,0),
                text:"xxxx",
                color:Color.Transparent,
                fontColor : Color.White);
            stats.Add(health);
            health.xAlign = Horizontal.R;


            //FORCE
            force = new UI_Button(
                text : "0",
                size: new Point(40,40),
                fontSize: 0.4f,
                color: Color.Red,
                overColor: Color.DarkRed,
                pressedColor: Color.IndianRed,
                disabledColor: Color.PaleVioletRed,
                fontColor: Color.White
                );
            stats.Add(force);
            force.yAlign = Vertical.T;


            // MAGIC
            magic = new UI_Button(

                text: "0",
                size: new Point(40,40),
                fontSize: 0.4f,
                color: Color.Purple,
                overColor: Color.Indigo,
                pressedColor: Color.MediumPurple,
                disabledColor: Color.DarkViolet,
                fontColor: Color.White
                );
            stats.Add(magic);
            magic.yAlign = Vertical.M;

            // SCIENCE
            sicence = new UI_Button(
                text: "0",
                size: new Point(40,40),
                fontSize: 0.4f,
                color: Color.Cyan,
                overColor: Color.DarkCyan,
                pressedColor: Color.LightCyan,
                disabledColor: Color.DeepSkyBlue,
                fontColor: Color.White
                );
            stats.Add(sicence);
            sicence.yAlign = Vertical.B;

            // Increasing Event
            force.onClick += (x) => {
                player.Knowledges.Force += 1;
                force.label.text = player.Knowledges.Force + "";
                LevelAvailable -= 1;
            };
            magic.onClick += (x) => {
                player.Knowledges.Magic += 1;
                magic.label.text = player.Knowledges.Magic + "";
                LevelAvailable -= 1;
            };
            sicence.onClick += (x) => {
                player.Knowledges.Science += 1;
                sicence.label.text = player.Knowledges.Science + "";
                LevelAvailable -= 1;
            };
            // Skills
            for (int i = 0; i < player.Skills.Length; i++)
            {
                UI_Skill skill = new UI_Skill(player.Skills[i],new Rectangle(150+70*i,10,60,60));
                weapon.Add(skill);
                
            }

        }

        private void Update(GameTime gameTime)
        {
            health.text = player.Health.ToString();
        }


        /// <summary>
        /// Permet à VisibleVistor de fonctionner
        /// </summary>
        /// <param name="v"></param>
        public override void Accept(I_VisibleVisitor v)
        {
            v.Visit(this);
        }

        public override void Destroy()
        {
            base.Destroy();
            UpdateEvents.GetInstance().PreUpdate -= Update;

        }
    }
}
