using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Player : Car
    {
        // constants
        const float topSpeed = 5f;

        Vector2 velocity;
        Vector2 velocityDirection;
        float speed;
        float friction;
        float direction = 1;
        float wheelRotation = 0;

        //temp
        int skin=0;
        public Player() :base() { init(); } 
        public void init()
        {
            velocity = new Vector2();
            offset = new Vector2(-30, 0);
            origin.X -= 30;
            speed = 0;
            friction = 0.2f;
            rotation = 0.0f;
        }
        public override void Update(GameTime gametime)
        {
            if (Input.IsKeyPressed(Keys.C)) {
                skin = ++skin % models.Count;
                textureBoundry = models[(Model)skin];
            }

            rotation %= 6.28f;
            if (speed != 0)
            {
                speed -= friction;

                rotation += direction * wheelRotation * speed / 40;

                if (wheelRotation != 0)
                {
                    //int sign = wheelRotation > 0 ? 1 : -1; 
                    // subtract some amount speed from wheel
                    wheelRotation /= 1.2f;
                }
                
                if (speed < 0)
                {
                    speed = 0;
                }
            }
            //Debug.WriteLine("Wh {0}",wheelRotation);

            if (Input.accelerate) {
                if (speed < topSpeed)
                {
                    speed += 0.4f;
                }
            }
            if (Input.steerLeft)
            {
                if (wheelRotation > - 90* 3.14 / 180) 
                wheelRotation -= 0.05f;
            }
            if (Input.steerRight)
            {
                if (wheelRotation < 90 * 3.14 / 180)
                    wheelRotation += 0.05f;
            }
            if (Input.brake)
            {
                if (speed > 0)
                {
                    speed -= 0.2f;
                }
            }
            if (Input.shiftGear)
            {
                if (speed <= 0.2f)
                {
                    direction *= -1;
                }
            }

            
            velocityDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            velocity =  direction*  speed * velocityDirection;
            position += velocity;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
