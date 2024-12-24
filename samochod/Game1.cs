using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System.Collections.Generic;
using System.Diagnostics;

namespace samochod
{
    public class Game1 : Game
    {
        public static bool isFocused;
        public static bool debug = true;
        public static bool canMouse = false;
        public static bool drawHitbox = false;
        public static int ResX = 1280;
        public static int ResY = 896;
        private AudioManager audioManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EntityManager entityManager;
        private TextManager textManager;
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
            textManager = new TextManager();
            textManager.blazed = Content.Load<SpriteFont>("Fonts/blazed");
            textManager.fipps  = Content.Load<SpriteFont>("Fonts/fipps");
            sw = new Stopwatch();
            levelManager = new LevelManager();
            audioManager = new AudioManager();
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
            Component.TextMan = textManager;
            // load entity manager
            entityManager = new EntityManager();
            entityManager.text = textManager;
            entityManager.audio = audioManager;
            Entity.audioMan = audioManager;
            LevelManager.tileSet = textures[3];
            LevelManager.textMan = textManager;
            LevelManager.entityManager = entityManager;
            audioManager.sounds.Add(Sound.hit, Content.Load<SoundEffect>("Sounds/hit_8bit"));
            audioManager.sounds.Add(Sound.gear_shift, Content.Load<SoundEffect>("Sounds/gear_shift"));
            audioManager.sounds.Add(Sound.idle, Content.Load<SoundEffect>("Sounds/idle"));
            audioManager.sounds.Add(Sound.brake, Content.Load<SoundEffect>("Sounds/brake"));
            audioManager.sounds.Add(Sound.theme, Content.Load<SoundEffect>("Sounds/theme_car"));

            audioManager.StartMusic(Sound.theme);

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            sw.Start();
            isFocused = IsActive;
            Input.Update(Mouse.GetState(), Keyboard.GetState());
            textManager.Write(new Text($"mX: {Input.mousePosition.X} mY: {Input.mousePosition.Y}"));
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
                Exit();

            audioManager.Update(gameTime);

            if (gameState == GameState.menu)
            {
                
                menuManager.Update(gameTime);
            }
            if (gameState == GameState.loading)
            {
                // loading levle data
                levelManager.LoadLevel();
                entityManager.LevelMachen(levelManager.currentLevel);
                entityManager.AddPlayer();
                
                gameState = GameState.running;
            }
            else if (gameRunning())
            {
                levelManager.Update(gameTime);
                entityManager.Update(gameTime);
                if (debug)
                {
                    if (entityManager.player?.alive == false)
                    {
                        entityManager.AddPlayer();

                    }

                    if (Input.IsKeyPressed(Keys.P))
                    {
                        if (entityManager.player != null)
                            entityManager.player.alive = false;
                    }
                    if (Input.IsKeyPressed(Keys.Enter))
                    {
                        // auto win
                        gameState = GameState.win;
                    }
                }
                if (gameState == GameState.win)
                {
                    entityManager.player.RemoveControl();
                    entityManager.player.Brake();
                    if (Input.IsKeyPressed(Keys.Space))
                    {
                        if (!levelManager.reachedFinish())
                            levelManager.levelID++;
                        gameState = GameState.loading;
                    }
                }
                if (Input.IsKeyPressed(Keys.F)) {
                    gameState = GameState.loading;
                   
                }
                if (Input.IsKeyPressed(Keys.N))
                {
                    audioManager.ChangeMusicVolume(0.1f);

                }

            }

            if (sw.ElapsedMilliseconds > 1000)
            {
                sw.Restart();
            }
        }

       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            if (gameState == GameState.menu)
            {
                menuManager.MainMenuDraw(_spriteBatch);
            }
            if (gameRunning())
            {
                levelManager.Draw(_spriteBatch);
                entityManager.Draw(_spriteBatch);
                textManager.Draw(_spriteBatch);

                if (gameState == GameState.win)
                {
                    
                    menuManager.WinScreenDraw(_spriteBatch);

                }

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private static bool gameRunning()
        {
            return gameState == GameState.running || gameState == GameState.win;
        }
    }
}
