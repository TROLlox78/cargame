using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace samochod
{
    public class Player : Car
    {
        // constants
        const float topSpeed = 4f;
        public List<Vector2> wheelPositionNormal;
        public List<Vector2> _wheelPosition = new List<Vector2>();
        public List<Vector2> wheelPosition
        {
            get
            {
                _wheelPosition.Clear();
                foreach (var wheel in wheelPositionNormal)
                {
                    var pointNormal = Shape.rotationMatrix(rotation, wheel);
                    Vector2 pointInSpace = new Vector2(pointNormal.X + position.X, pointNormal.Y + position.Y);
                    _wheelPosition.Add(pointInSpace);
                }
                return _wheelPosition;
            }
            }

        private float wheelRotation = 0;
        private bool hasControl = true;
        public bool startedMoving = false;
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

            wheelPositionNormal = new List<Vector2> {
                // top left
                new Vector2(+width / 2 + 15, -height / 2 + 5),
                // top right
                 new Vector2(+width / 2 + 15, +height / 2 - 5),
                // bottom left
                new Vector2(-width / 2 + 30 + 10, -height / 2 + 5),
                //bottom right
                new Vector2(-width / 2 + 30 + 10, +height / 2 - 5),
            };

        }
        public void RemoveControl()
        {
            hasControl = false;
        }
        public void GiveControl()
        {
            hasControl = true;
        }
        public override void Update(GameTime gametime)
        {
            UpdateMouse();
            if (idle.State != SoundState.Playing)
            {
                idle.Play();
            }
            idle.Pitch = Math.Clamp(speed/topSpeed, -0.5f, 0.6f) - 0.2f;
            rotation %= 6.28f;
            if (speed != 0)
            {
                if (hasControl)
                {
                    startedMoving = true;
                }
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
                    Accelerate();
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


        double easeInOutQuint(int x){
            return x< 0.5 ? 16 * x* x* x* x* x : 1 - Math.Pow(-2 * x + 2, 5) / 2;
        }

    public bool checkForIllegalBlocks()
        {
            foreach (var wheel in wheelPosition)
            {
                if (!isInBounds(wheel)) { return false; }
                // TODO: fix so that all illegal blocks are checked for,
                // not just grass tile, have an array of illegal blocks
                if (LevelManager.CheckTile(wheel) == TileID.gGrass)
                {
                    return true;
                }
            }
            return false;
        }
        public void Accelerate()
        {
            if (speed < topSpeed)
            {
                speed += 0.4f;
            }
        }
        public void FastAccelerate()
        {
            if (speed < 6f)
            {
                speed += 0.4f;
            }
        }
        public void Brake()
        {
            if (speed > 0)
            {
                speed -= 0.4f;
            }
        }public void FastBrake()
        {
            if (speed > 0)
            {
                speed -= 0.7f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Game1.drawHitbox)
            {
                spriteBatch.Draw(textures[2], new Rectangle((int)position.X, (int)position.Y, 4, 4), Color.Yellow);

                foreach (var wheel in wheelPosition)
                {

                    spriteBatch.Draw(textures[2],
                        new Rectangle(
                            wheel.ToPoint()
                        , new Point(4, 4)), Color.Yellow);
                }
            }
        }

    }

    public class PlayerIntro
    {
        public int px {  get; set; }
        public int py { get; set; }
        public float rot { get; set; }
        public float time { get; set; }
        [JsonConstructor]
        public PlayerIntro(int px, int py, float rot, float time)
        {
            this.px = px;
            this.py = py;
            this.rot = rot;
            this.time = time;
        }
    }
}
