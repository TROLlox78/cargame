using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace samochod
{
    public class Car : Entity
    {
        public Car(Texture2D texture) : base(texture)
        {
            width = 91;
            height = 43;
            textureBoundry = new Rectangle(452,69, width, height);
            position = new Vector2(100, 100);
            origin = new Vector2(width/2, height/2);
            Debug.WriteLine("created car model");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHitbox(spriteBatch);
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
