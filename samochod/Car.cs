using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace samochod
{
    public class Car : Entity
    {
        protected Rectangle textureBoundry;
        public Car(Texture2D texture) : base(texture)
        {
            width = 42;
            height = 90;
            textureBoundry = new Rectangle(69,17, width, height);
            position = new Vector2(100, 100);
            origin = new Vector2(width/2, height/2);
            hitbox = new Shape(position, origin, scale, rotation, texture);
            Debug.WriteLine("created car model");
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
            }
            else
            {
                Debug.WriteLine("Entity: {0} has no texture", ID);
            }
        }
    }
}
