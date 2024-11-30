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
        int width = 42;
        int height = 90;
        public Car(Texture2D texture) : base(texture)
        {
            textureBoundry = new Rectangle(69,17, width, height);
            position = new Vector2(100, 100);
            origin = new Vector2(width/2, height/2);
            Debug.WriteLine("created car model");
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
