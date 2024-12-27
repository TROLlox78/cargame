using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace samochod
{
    public class Game1 : Game
    {
        public static bool isFocused;
        public static bool debug = true;
        public static bool canMouse = false;
        public static bool drawHitbox = false;
        public static bool audioMute = false;
        public static bool difficulty = true;
        public static int ResX = 1280;
        public static int ResY = 896;
        private AudioManager audioManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EntityManager entityManager;
        private TextManager textManager;
        public static GameState gameState = GameState.menu;
        Stopwatch sw;
        
        List<Texture2D> textures;
        LevelManager levelManager;
        MenuManager menuManager;
        TimeSpan introTimer;
        GameScore gameScore;
        
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
            if (debug && false)
            {
                _graphics.SynchronizeWithVerticalRetrace = false;
                this.IsFixedTimeStep = false;
            }
            _graphics.ApplyChanges();
            textures = new List<Texture2D>();
            TextManager.blazed = Content.Load<SpriteFont>("Fonts/blazed");
            TextManager.fipps  = Content.Load<SpriteFont>("Fonts/fipps");
            textManager = new TextManager();
            sw = new Stopwatch();
            levelManager = new LevelManager();
            audioManager = new AudioManager();
            menuManager = new MenuManager ();
            gameScore = new GameScore();

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
            entityManager.textMan = textManager;
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
            sw.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            isFocused = IsActive;
            Input.Update(Mouse.GetState(), Keyboard.GetState());
            textManager.Write(new Text($"mX: {Input.mousePosition.X} mY: {Input.mousePosition.Y}"));
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
                Exit();

            audioManager.Update(gameTime);
            gameScore.entityManager = entityManager;
            gameScore.levelManager = levelManager;
            gameScore.Update(gameTime);
            if (gameState == GameState.menu)
            {
                menuManager.Update(gameTime);
            }
            if (gameState == GameState.loading)
            {
                // loading levle data
                var lvl = levelManager.LoadLevel();
                // setting up level
                entityManager.LevelMachen(lvl);
                if (lvl.pIntro == null) { lvl.pIntro = new PlayerIntro(100, 100, 0, 0); }
                entityManager.AddPlayer(lvl.pIntro);
                introTimer =  TimeSpan.FromSeconds(lvl.pIntro.time);
                entityManager.player.RemoveControl();

                textManager.hintText.Update("Find a parking spot!");
                gameState = GameState.running;
                
            }
            if (gameState == GameState.end)
            {
                menuManager.EndGame(gameScore);
                LevelManager.levelID = 0;
            }
            else if (gameRunning())
            {
                playIntro(gameTime);

                textManager.timer.Update(gameScore.time);
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

                    if (Input.IsKeyPressed(Keys.N))
                    {
                        audioManager.SwitchMute();

                    }
                }
                if (gameState == GameState.win)
                {
                    
                    entityManager.player.RemoveControl();
                    entityManager.player.Brake();
                    if (Input.IsKeyPressed(Keys.Space))
                    {
                        if (!levelManager.reachedFinish())
                        {
                            LevelManager.levelID++;
                            gameState = GameState.loading;
                        }
                        else if (levelManager.reachedFinish())
                        {
                            gameState = GameState.end;
                        }
                    }
                }
                if (Input.IsKeyPressed(Keys.F))
                {
                    gameState = GameState.loading;
                }
                if (!debug)
                {
                    if (Input.IsKeyPressed(Keys.R))
                    {
                        gameState = GameState.loading;
                    }
                    if (Input.IsKeyPressed(Keys.M))
                    {
                        audioManager.SwitchMute();
                    }
                    if (Input.IsKeyPressed(Keys.Q))
                    {
                        gameState = GameState.menu;
                    }
                }

            }
            if (sw.ElapsedMilliseconds > 1000)
            {
                sw.Restart();
            }
            textManager.Write(new($"frameTime[ms]: {sw.Elapsed.TotalMilliseconds}"));
            sw.Restart();
        }

        private void playIntro(GameTime gameTime)
        {
            if (introTimer >= TimeSpan.Zero)
            {

                textManager.Write(new Text($"{introTimer}"));
                introTimer -= gameTime.ElapsedGameTime;
                entityManager.player.FastAccelerate();
                if (introTimer <= TimeSpan.Zero)
                {
                    entityManager.player.FastBrake();
                    entityManager.player.GiveControl();
                }
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            if (gameState == GameState.menu)
            {
                menuManager.Draw(_spriteBatch);
            }
            if (gameState != GameState.menu)
            {
                levelManager.Draw(_spriteBatch);
                entityManager.Draw(_spriteBatch);
                textManager.Draw(_spriteBatch);

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
