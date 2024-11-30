using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace samochod
{
    public class Car : Entity
    {
        Rectangle textureBoundry;
        
        public Car(Texture2D texture) : base(texture)
        {
            textureBoundry = new Rectangle(69,17, 42, 90);
            position = new Vector2(100, 100);
            Debug.WriteLine("initialized car");
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
