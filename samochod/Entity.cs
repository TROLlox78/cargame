using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Entity
    {
        public int ID;
        public bool alive = true;
        public bool mouseable = true;

        public Vector2 position, 
            origin, // offset to the center of the texture from it's top left corner
            offset; // center of mass offset
        public float rotation;
        public float scale;

        protected Texture2D texture;
        protected Rectangle textureBoundry;
        public int width, height;
        public Shape hitbox {
            get
            { return new Shape(position, offset, scale, rotation, textureBoundry);  }
                
                }
        public Entity(Texture2D texture)
        {
            this.texture = texture;
            width = texture.Width; height = texture.Height;
            scale = 1;// READ FROM LEVEL
        }
        public virtual void UpdateMouse() 
        {
            if (!mouseable) { return; }
            if (Input.IsLeftPressed())
            {
                if (width>= DistanceTo(Input.mousePosition))
                {
                    position = toVect(Input.mousePosition);
                }
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            UpdateMouse();
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null) 
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
            else
            {
                Debug.WriteLine("Entity: {0} has no texture",ID);
            }
        }
        public virtual object Clone()
        {
            // return a clone of an object
            // otherwise copies by referenece/pointer
            return this.MemberwiseClone();
        }
        public float DistanceTo(Point point)
        {
            float distX = (point.X - position.X);
            float distY = (point.Y - position.Y);
            return (float)Math.Sqrt(distX*distX + distY*distY);
        }
        public Vector2 toVect(Point p) 
        {
            return new Vector2(p.X, p.Y);
        }
    }
}
