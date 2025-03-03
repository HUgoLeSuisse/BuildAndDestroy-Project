using Microsoft.Xna.Framework;
using BuildAndDestroy.GameComponents.UI.Element;
using Microsoft.Xna.Framework.Graphics;
using BuildAndDestroy.GameComponents.Utils;

namespace BuildAndDestroy.GameComponents.UI
{
    /// <summary>
    /// Créer un menu (interface utilisateur)
    /// </summary>
    public class UI_Menu : UI_Manager
    {
        public UI_Menu(Camera cam) : base(cam)
        {
            CreateMenu();
        }

        /// <summary>
        /// Permet de créer le menu 
        /// </summary>
        private void CreateMenu()
        {
            content =
                new UI_Pannel(
                    new Rectangle(0, 0, d.width, d.height),
                    new Color(125, 125, 125, 0), null);


            UI_Label label = new UI_Label(
                    position: new Point(0, 50),
                    text: "Build And Destroy",
                    fontSize: 0.7f,
                    color: Color.Tomato,
                    fontColor: Color.Black,
                    image: d.GetByPath<Texture2D>("Cadre")
                    );



            content.Add(label);
            label.xAlign = Horizontal.M;


            UI_Button playButton = new UI_Button(
                position: new Point(0, (int)(d.height*0.7f)),
                text: "Play",
                fontSize: 2,
                fontColor: Color.Black,
                color: Color.DarkCyan,
                overColor: Color.Cyan,
                pressedColor: Color.LightCyan,
                image: d.GetByPath<Texture2D>("Cadre")

                );
            content.Add(playButton);
            playButton.xAlign = Horizontal.M;
            playButton.onClick += Play;
        }

        /// <summary>
        /// s'active quand on appuie sur play // Lance la partie
        /// </summary>
        /// <param name="time"></param>
        private void Play(float time)
        {
            cam.ChangeUI(new UI_Game(cam));
        }

    }
}
