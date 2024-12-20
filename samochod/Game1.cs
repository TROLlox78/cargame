using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System.Collections.Generic;
using System.Diagnostics;

namespace samochod
{
    public class Game1 : Game
    {
        public static int ResX = 1280;
        public static int ResY = 896;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EntityManager entityManager;
        private TextManager text;
        public static GameState gameState = GameState.menu;
        Stopwatch sw;
        // maybe create a texture manager but, not useful for small game
        List<Texture2D> textures;
        LevelManager levelManager;
        MenuManager menuManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = ResX;
            _graphics.PreferredBackBufferHeight = ResY;
            _graphics.ApplyChanges();
            textures = new List<Texture2D>();
            text = new TextManager();
            text.blazed = Content.Load<SpriteFont>("Fonts/blazed");
            text.fipps  = Content.Load<SpriteFont>("Fonts/fipps");
            sw = new Stopwatch();
            levelManager = new ();
            menuManager = new MenuManager ();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // load textures
            textures.Add(Content.Load<Texture2D>("Sprites/cars"));
            textures.Add(Content.Load<Texture2D>("Sprites/steering_wheel"));
            textures.Add(new Texture2D(GraphicsDevice, 1, 1));
            textures[2].SetData(new Color[] { Color.White });
            textures.Add(Content.Load<Texture2D>("Tiles/tileset"));
            Entity.textures = textures;
            Component.textures = textures;
            Component.TextMan = text;
            levelManager.LoadLevel(levelID:0);
            // load entity manager
            entityManager = new EntityManager();
            entityManager.text = text;

            // loading levle data
            entityManager.LevelMachen( levelManager.currentLevel);
             
            LevelManager.tileSet = textures[3];
            LevelManager.txt = text;
            levelManager.entityManager = entityManager;
            // temp adding car
            //entityManager.AddCar(EntityType.car);
            //entityManager.AddCar(EntityType.car, new Vector2(300, 300));
            entityManager.AddPlayer();
        }

        protected override void Update(GameTime gameTime)
        {
            sw.Start();
            Input.Update(Mouse.GetState(), Keyboard.GetState());
            text.Write(new Text($"mX: {Input.mousePosition.X} mY: { Input.mousePosition.Y}"));
            text.Write(new Text($"pX: {entityManager.player.position.X} pY: {entityManager.player.position.Y}"));
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
                Exit();
            if (gameState == GameState.menu)
            {
                menuManager.Update(gameTime);
            }
            else if (gameState == GameState.running)
            {

                if (entityManager.player?.alive == false)
                {
                    entityManager.AddPlayer();

                }

                if (Input.IsKeyDown(Keys.P))
                {
                    if (entityManager.player != null)
                        entityManager.player.alive = false;
                }


                levelManager.Update(gameTime);
                entityManager.Update(gameTime);
            }

            if (sw.ElapsedMilliseconds > 1000) {
                sw.Restart();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            if (gameState == GameState.menu)
            {
                menuManager.Draw(_spriteBatch);
            }
            else if (gameState == GameState.running)
            {
                levelManager.Draw(_spriteBatch);
                entityManager.Draw(_spriteBatch);
                text.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
