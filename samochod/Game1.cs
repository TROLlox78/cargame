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
        private TextManager text;
        Stopwatch sw;
        // maybe create a texture manager but, not useful for small game
        List<Texture2D> textures;
        // temp
        Level tempLevel;

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
            _graphics.PreferredBackBufferHeight = 896;
            _graphics.ApplyChanges();
            textures = new List<Texture2D>();
            text = new TextManager();
            text.blazed = Content.Load<SpriteFont>("Fonts/blazed");
            text.fipps  = Content.Load<SpriteFont>("Fonts/fipps");
            sw = new Stopwatch();
            tempLevel = new Level();
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
            // load entity manager
            entityManager = new EntityManager(textures);
            entityManager.text = text;
            Level.tileSet = textures[3];
            // temp adding car
            entityManager.AddCar(EntityType.car);
            entityManager.AddCar(EntityType.car, new Vector2(300, 300));
            entityManager.AddPlayer();
        }

        protected override void Update(GameTime gameTime)
        {
            sw.Start();
            Input.Update(Mouse.GetState(), Keyboard.GetState());
            text.Write(new Text(string.Format("mX: {0} mY: {1}", Input.mousePosition.X, Input.mousePosition.Y)));
            text.Write(new Text(string.Format("pX: {0} pY: {1}", entityManager.player.position.X, entityManager.player.position.Y)));
            text.Write(new Text(string.Format("pX: {0} pY: {1}", entityManager.entities[0].position.X, entityManager.entities[0].position.Y)));
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.IsKeyDown(Keys.Escape))
                Exit();


            if (entityManager.player?.alive == false)
            {
                entityManager.AddPlayer();

            }

            if (Input.IsKeyDown(Keys.R))
            {
                if (entityManager.player != null)
                entityManager.player.alive = false;
            }
            if (Input.IsKeyPressed(Keys.T))
            {
                List<Vector2> x = entityManager.player.hitbox.points;
                for (int i = 0; i < x.Count; i++) 
                    Debug.WriteLine("point {0} x:{1} y:{2}", i, x[i].X, x[i].Y);
                
            }
            

            entityManager.Update(gameTime);

            if (sw.ElapsedMilliseconds > 1000) {
                sw.Restart();
                //Debug.WriteLine("{0}", entityManager.entities.Count);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            
            tempLevel.Draw(_spriteBatch);
            entityManager.Draw(_spriteBatch);
            text.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
