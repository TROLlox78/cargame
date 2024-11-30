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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EntityManager entityManager;
        Stopwatch sw;
        // maybe create a texture manager but, not useful for small game
        List<Texture2D> textures;
        // temp

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 1024;
            _graphics.ApplyChanges();
            textures = new List<Texture2D>();
            sw = new Stopwatch();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // load textures
            textures.Add(Content.Load<Texture2D>("Sprites/cars"));

            // load entity manager
            entityManager = new EntityManager(textures);

            // temp adding car
            entityManager.AddCar(EntityType.car);
        }

        protected override void Update(GameTime gameTime)
        {
            sw.Start();
            Input.Update(Mouse.GetState(), Keyboard.GetState());
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
                Exit();

            entityManager.Update(gameTime);

            if (sw.ElapsedMilliseconds > 1000) {
                sw.Restart();
                Debug.Write(entityManager.entities.Count);
                Debug.Write('\n');

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            entityManager.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
