﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Entity
    {
        public int ID;
        public bool alive = true;
        public bool mouseable = true;
        Vector2 p2;
        public Vector2 position, 
            origin, // offset to the center of the texture from it's top left corner
            offset; // center of mass offset
        public float rotation;
        public float scale;

        public static List<Texture2D> textures;
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
                p2 = toVect(Input.mousePosition);
                if (width>= DistanceTo(Input.mousePosition))
                {
                    position = toVect(Input.mousePosition);
                }
            }
            if (Input.IsRightPressed())
            {
                rotation += 0.1f;
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            rotation %= 6.28f;
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

        private float lerp(Vector2 p1, Vector2 p2, float inc)
        {
            return  p1.Y + (inc - p1.X)*(p2.Y-p1.Y)/(p2.X-p1.X);
        }
        protected List<Vector2> Interpolate(Vector2 p1, Vector2 p2)
        {

            List<Vector2> pointSet = new List<Vector2>();
            float dist = Math.Abs(p2.X - p1.X);


            if (p1.X> p2.X)
            {
                Vector2 temp = p1;
                p1 = p2;
                p2 = temp;
            }
            for (int x = (int)p1.X; x < p2.X; x++)
            {
                Vector2 vector2 = new Vector2();
                //float increment = (x - p1.X) / (dist);
                vector2.Y = lerp(p1, p2, x);
                //Debug.WriteLine(vector2.Y);
                vector2.X = x;
                pointSet.Add(vector2);
            }
            return pointSet;
        }
        protected void DrawHitbox(SpriteBatch sprite)
        {
            float scale = 2;
            Vector2 p1 = new Vector2(200, 200);

            for (int i = 0; i < hitbox.points.Count-1; i++)
            {
                List<Vector2> edge = Interpolate(hitbox.points[i], hitbox.points[i+1]);
                foreach(Vector2 point in edge)
                {
                    sprite.Draw(textures[2], point, null,Color.Red,0,Vector2.Zero,scale,SpriteEffects.None,0);
                    //sprite.Draw(textures[2], point, Color.Red);
                }
            }

            List<Vector2> edge2 = Interpolate(hitbox.points[hitbox.points.Count-1], hitbox.points[0]);
            foreach (Vector2 point in edge2)
            {
                sprite.Draw(textures[2], point, null, Color.Red, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                //sprite.Draw(textures[2], point, Color.Red);
            }
        }
    }
}
