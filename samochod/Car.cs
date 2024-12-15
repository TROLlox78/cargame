using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;



namespace samochod
{
    public class Car : Entity
    {
        protected static Dictionary<Model, Rectangle> models;
        public Model model;
        public Car() :base()
        {
            if (models == null) {  models = new Dictionary<Model, Rectangle> {
                {Model.None, new  (452,69, 91, 43)},
            }; }
            this.texture = textures[0];
            width = 91;
            height = 43;
            textureBoundry = new Rectangle(452,69, width, height);
            position = new Vector2(100, 100);
            origin = new Vector2(width/2, height/2);
            Debug.WriteLine("created car model");
        }
        [JsonConstructor]
        public Car(Model carModel, Vector2 position, float rotation) : base()
        {
            if (models == null)
            {
                models = new Dictionary<Model, Rectangle> {
                {Model.None, new  (452,69, 91, 43)},
            };
            }
            this.texture = textures[0];
            textureBoundry = models[carModel];// maybe not allowed with json constructor
            width = models[carModel].Width;
            height = models[carModel].Height;
            origin = new Vector2(width / 2, height / 2);
            this.position = position;
            this.rotation = rotation;
            Debug.WriteLine($"created car{ID}");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, position, textureBoundry, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
                //DrawHitbox(spriteBatch);
            }
            else
            {
                Debug.WriteLine("Entity: {0} has no texture", ID);
            }
        }
    }
}
