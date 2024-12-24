﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        const float topSpeed = 4f;

        private float wheelRotation = 0;
        private bool hasControl = true;
        private bool startedMoving = false;
        SoundEffectInstance idle;
        SoundEffectInstance brake;

        public Player() :base() {  } 
        public void init()
        {
            hasControl = true;
            velocity = new Vector2();
            offset = new Vector2(-30, 0);
            origin.X -= 30;
            speed = 0;
            rotation = 0.0f;
            color = Color.White;
             idle = audioMan.Get(Sound.idle);
             brake = audioMan.Get(Sound.brake);
        }
        public void RemoveControl()
        {
            hasControl = false;
        }
        public override void Update(GameTime gametime)
        {
            if (idle.State != SoundState.Playing)
            {
                idle.Play();
            }
            idle.Pitch = Math.Clamp(speed/topSpeed, -0.5f, 0.6f) - 0.2f;
            rotation %= 6.28f;
            if (speed != 0)
            {
                startedMoving = true;

                speed -= friction;

                rotation += direction * wheelRotation * speed / 40;

                if (wheelRotation != 0)
                {
                    // subtract some amount speed from wheel
                    wheelRotation /= 1.1f;
                }
                
                if (speed < 0)
                {
                    speed = 0;
                    startedMoving = false;
                }
            }
            //Debug.WriteLine("Wh {0}",wheelRotation);
            if (hasControl)
            {
                if (Input.accelerate)
                {
                    if (speed < topSpeed)
                    {
                        speed += 0.4f;
                        
                    }
                }
                if (Input.steerLeft)
                {
                    if (wheelRotation > -90 * 3.14 / 180)
                        wheelRotation -= 0.05f;
                }
                if (Input.steerRight)
                {
                    if (wheelRotation < 90 * 3.14 / 180)
                        wheelRotation += 0.05f;
                }
                if (Input.brake)
                {
                    if (speed != 0)
                    {
                        Brake();
                        if (brake.State != SoundState.Playing && !Input.accelerate)
                        {
                            brake.Play();
                        }
                    }
                }
                if (Input.shiftGear)
                {
                    if (speed <= 0.2f)
                    {
                        direction *= -1;
                        audioMan.PlaySound(Sound.gear_shift);
                    }
                }
            }

            
            velocityDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            velocity =  direction*  speed * velocityDirection;
            position += velocity;

        }

        public void Brake()
        {
            if (speed > 0)
            {
                speed -= 0.4f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
