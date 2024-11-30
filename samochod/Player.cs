using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Player : Car
    {
        // constants
        const float topSpeed = 10f;

        Vector2 velocity;
        Vector2 velocityDirection;
        float speed;
        float friction;
        public Player(Texture2D texture) : base(texture){}
        public void init()
        {
            velocity = new Vector2();
            speed = 0;
            friction = 0.2f;
            rotation = 0.0f;
        }
        public override void Update(GameTime gametime)
        {
            rotation %= 6.28f;
            if (speed != 0)
            {
                speed -= friction;
                if (speed < 0)
                {
                    speed = 0;
                }
            }
            Debug.WriteLine(rotation);

            if (Input.accelerate) {
                if (speed < topSpeed)
                {
                    speed += 0.4f;
                }
            }
            if (Input.steerLeft)
            {
                rotation -= 0.1f;
            }
            if (Input.steerRight)
            {
                rotation += 0.1f;
            }

            //rotation = (float)Math.Atan2((double)position.Y, (double)position.X);
            velocityDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            velocity = speed * velocityDirection;
            position += velocity;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, position, textureBoundry, Color.White, rotation+3.14f/2, origin, scale, SpriteEffects.None, 0f);
            }
            else
            {
                Debug.WriteLine("Entity: {0} has no texture", ID);
            }
        }

    }
}
