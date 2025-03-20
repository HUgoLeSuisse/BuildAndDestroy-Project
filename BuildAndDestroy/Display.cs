using BuildAndDestroy.GameComponents;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.UI;
using BuildAndDestroy.GameComponents.UI.Element;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace BuildAndDestroy
{
    /// <summary>
    /// Classe de monogame permettant de géré les updates et l'affichage
    /// </summary>
    public class Display : Game, I_VisibleVisitor
    {

        private DisplayUtils d;

        private UpdateEvents events;
        private Camera _cam;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _sb;


        #region Start
        public Display()
        {
            d = DisplayUtils.GetInstance();
            d.width = 1920;
            d.height = 1080;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            events = UpdateEvents.GetInstance();
        }
        protected override void Initialize()
        {
            base.Initialize();

            #region DisplayUtils
                d.SetContent(Content);
                _sb = new SpriteBatch(GraphicsDevice);
                d.blank = new Texture2D(GraphicsDevice, 1, 1);
                Color[] colorData = { Color.White };
                d.blank.SetData(colorData);

                d.defaultFont = Content.LoadLocalized<SpriteFont>("DefaultFont");

            #endregion
            _graphics.PreferredBackBufferWidth = d.width;
            _graphics.PreferredBackBufferHeight = d.height;
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();
            _cam = new Camera();
        }

        #endregion

        #region MainLoop
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            events.PreUpdate?.Invoke(gameTime);
            events.Update?.Invoke(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _sb.Begin(samplerState: SamplerState.PointClamp);

            I_Visible ui = _cam.GetUI_Pannel();
            ui.Accept(this);

            _sb.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Visible Traitement
        public void Visit(I_Visible v)
        {
            _sb.Draw(
                v.GetAcctualTexture(),
                v.GetAbsoluteRectangle(),
                v.GetAcctualColor());
        }


        public void Visit(UI_Label v)
        {
            _sb.Draw(v.GetAcctualTexture(),
                new Rectangle(
                    v.GetAbsoluteRectangle().X - v.GetAbsoluteRectangle().Width / 10,
                    v.GetAbsoluteRectangle().Y - v.GetAbsoluteRectangle().Height / 10,
                    (int)(v.GetAbsoluteRectangle().Width * 1.2f),
                    (int)(v.GetAbsoluteRectangle().Height * 1.2f)
                   ), v.GetAcctualColor());

            Vector2 pos = v.GetAbsoluteRectangle().Location.ToVector2();
            _sb.DrawString(
                v.font,
                v.text,
                pos,
                v.fontColor,
                0,
                Vector2.Zero,
                v.GetFontSize(),
                SpriteEffects.None,
                1
                );

        }
        public void Visit(UI_Button v)
        {
            _sb.Draw(v.GetAcctualTexture(),
                    v.GetAbsoluteRectangle(),
                    v.GetAcctualColor());

            UI_Label label = v.label;
            
            Point Size = label.Absoulute.Size;
            Point c = v.Absoulute.Size;
            int x = Size.X - c.X;
            int y = Size.Y - c.Y;
            Vector2 labelPos = new Vector2 ( x/2, y/2 );
            Vector2 labelTruePos =  v.Absoulute.Location.ToVector2() - labelPos;
            _sb.DrawString(
                label.font,
                label.text,
                labelTruePos,
                label.fontColor,
                0,
                Vector2.Zero,
                label.GetFontSize(),
                SpriteEffects.None,
                1
                );

        }

        public void Visit(UI_Pannel v)
        {
            Visit((I_Visible)v);

            UI_Element[] uI_Elements = v.GetChilds();
            foreach (var item in uI_Elements)
            {
                item.Accept(this);
            }
        }
        public void Visit(UI_GamePannel v)
        {
            foreach (var element in v.GetGameElement(new Rectangle(_cam.position, new Point(d.width, d.height))))
            {
                element.Accept(this);
            }
            foreach (var element in v.GetChilds())
            {
                element.Accept(this);
            }
        }
        public void Visit(E_Entity v)
        {

            Visit((I_Visible)v);
            DrawEntityHealthBar(v, Color.Red);

        }
        public void Visit(E_Player v)
        {
            Visit((E_Entity)v);


            _sb.DrawString(d.defaultFont,v.Level.ToString(),new Vector2(50,50),Color.White);

        }

        /// <summary>
        /// Affiche la bar de vie d'une entité
        /// </summary>
        /// <param name="v">l'entité</param>
        /// <param name="color">la couleur de la bar de vie</param>
        public void DrawEntityHealthBar(E_Entity v,Color color)
        {
            _sb.Draw(
                d.blank,
                new Rectangle(
                v.GetAbsoluteRectangle().Center.X - 50,
                v.GetAbsoluteRectangle().Top - 15,
                100,
                10),
                Color.DarkGray
                );

            _sb.Draw(
                d.blank,
                new Rectangle(
                v.GetAbsoluteRectangle().Center.X - 50,
                v.GetAbsoluteRectangle().Top - 15,
                (int)(v.Health / v.MaxHealth * 100),
                10),
                color
                );
            
        }

        #endregion


    }
}
