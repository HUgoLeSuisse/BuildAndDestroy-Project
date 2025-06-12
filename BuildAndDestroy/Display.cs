using BuildAndDestroy.GameComponents;
using BuildAndDestroy.GameComponents.GameObjects.Entity;
using BuildAndDestroy.GameComponents.GameObjects.Entity.StatUtlis;
using BuildAndDestroy.GameComponents.GameObjects.Environement;
using BuildAndDestroy.GameComponents.GameObjects.Weapon;
using BuildAndDestroy.GameComponents.UI;
using BuildAndDestroy.GameComponents.UI.Element;
using BuildAndDestroy.GameComponents.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
            _graphics.IsFullScreen = true;
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
            d.graphics = GraphicsDevice;
            Color[] colorData = { Color.White };
            d.blank.SetData(colorData);

            d.defaultFont = Content.LoadLocalized<SpriteFont>("DefaultFont");

            #endregion
            _graphics.PreferredBackBufferWidth = d.width;
            _graphics.PreferredBackBufferHeight = d.height;
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();
            _cam = Camera.Instance;
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
                v.GetCurrentTexture(),
                v.GetAbsoluteRectangle(),
                v.GetCurrentColor());
        }


        public void Visit(UI_Label v)
        {
            _sb.Draw(v.GetCurrentTexture(),
                new Rectangle(
                    v.GetAbsoluteRectangle().X - v.GetAbsoluteRectangle().Width / 10,
                    v.GetAbsoluteRectangle().Y - v.GetAbsoluteRectangle().Height / 10,
                    (int)(v.GetAbsoluteRectangle().Width * 1.2f),
                    (int)(v.GetAbsoluteRectangle().Height * 1.2f)
                   ), v.GetCurrentColor());

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
        public void Visit(UI_Skill v)
        {
            _sb.Draw(
                v.GetCurrentTexture(),
                v.GetAbsoluteRectangle(),
                v.GetCurrentColor());

            Vector2 pos = v.GetAbsoluteRectangle().Center.ToVector2();
            if (v.Skill != null && v.Skill.Active != null && v.Skill.Active.Cooldown != null && !v.Skill.Active.IsAvailable)
            {
                Vector2 size = d.defaultFont.MeasureString(Convert.ToInt32(v.Skill.Active.Cooldown.GetTime()).ToString()) * 0.7f / 2;
                pos -= size;
                _sb.DrawString(
                    d.defaultFont,
                    Convert.ToInt32(v.Skill.Active.Cooldown.GetTime()).ToString(),
                    pos,
                    Color.White,
                    0,
                    Vector2.Zero,
                    0.7f,
                    SpriteEffects.None,
                    1
                    );
            }

        }
        public void Visit(UI_Button v)
        {
            if (v.label.text == "0")
            {
                int rx = 0;
            }
            _sb.Draw(v.GetCurrentTexture(),
                    v.GetAbsoluteRectangle(),
                    v.GetCurrentColor());

            UI_Label label = v.label;

            Point Size = label.Absoulute.Size;
            Point c = v.Absoulute.Size;
            int x = Size.X - c.X;
            int y = Size.Y - c.Y;
            Vector2 labelPos = new Vector2(x / 2, y / 2);
            Vector2 labelTruePos = v.Absoulute.Location.ToVector2() - labelPos;
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
        public void Visit(UI_StatPannel v)
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
            foreach (var element in v.GetGameElement(new Rectangle(_cam.Position, new Point(d.width, d.height))))
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
            _sb.Draw(
                v.GetCurrentTexture(),
                v.GetAbsoluteRectangle(),
                null,
                v.GetCurrentColor(),
                0,
                Vector2.Zero,
                v.IsFilped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0);


            DrawEntityHealthBar(v, Color.Red);


        }
        public void Visit(E_Player v)
        {
            Visit((E_Entity)v);


            _sb.DrawString(d.defaultFont, v.Level.ToString() + " : " + v.Xp + "/" + v.NextLevel, new Vector2(50, 50), Color.White);
            v.Weapon?.Accept(this);

        }

        public void Visit(W_Weapon v)
        {
            Rectangle r = v.GetAbsoluteRectangle();
            _sb.Draw(
                v.GetCurrentTexture(),
                v.GetAbsoluteRectangle(),
                null,
                v.GetCurrentColor(),
                v.Direction,
                new Vector2(187.5f, 500),
                SpriteEffects.None,
                1f
                );



        }

        /// <summary>
        /// Affiche la bar de vie d'une entité
        /// </summary>
        /// <param name="v">l'entité</param>
        /// <param name="color">la couleur de la bar de vie</param>
        public void DrawEntityHealthBar(E_Entity v, Color color)
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
                (int)(((DoubleStat)v.Stats[E_Entity.HEALTH]).Percent * 100),
                10),
                color
                );

        }
        public void Visit(DrawableCircle v)
        {
            Visit((I_Visible)v);
        }

        public void Visit(Map v)
        {
            const int TILE_SIZE = 64;
            int x = _cam.Position.X;
            int y = _cam.Position.Y;

            int w = d.width;
            int h = d.height;

            Texture2D grass = v.GetCurrentTexture();


            for (int i = -1; i < w / TILE_SIZE + 1; i++)
            {
                for (int ii = -1; ii < h / TILE_SIZE + 2; ii++)
                {
                    _sb.Draw(grass,
                        new Rectangle(i * TILE_SIZE - x % TILE_SIZE, ii * TILE_SIZE - y % TILE_SIZE, TILE_SIZE, TILE_SIZE),
                        v.GetCurrentColor());
                }
            }
        }
        public void Visit(AnimBullet v)
        {
            Rectangle r = v.GetAbsoluteRectangle();
            _sb.Draw(
                v.GetCurrentTexture(),
                v.GetAbsoluteRectangle(),
                null,
                v.GetCurrentColor(),
                v.Rotation,
                new Vector2(100,100),
                SpriteEffects.None,
                1f
                );
        }

        #endregion

        /// <summary>
        /// créer une texture ciruclaire unicolor (pour débuging)
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="diameter"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int diameter, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
            Color[] data = new Color[diameter * diameter];

            int radius = diameter / 2;
            Vector2 center = new Vector2(radius, radius);

            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    Vector2 pos = new Vector2(x, y);
                    float distance = Vector2.Distance(pos, center);

                    if (distance <= radius)
                        data[y * diameter + x] = color; // Pixel dans le cercle
                    else
                        data[y * diameter + x] = Color.Transparent; // Pixel en dehors
                }
            }

            texture.SetData(data);
            return texture;
        }

    }
}
