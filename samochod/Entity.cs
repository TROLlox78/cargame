using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public Vector2 position, origin;
        public float rotation;
        protected Texture2D texture;
        public float scale = 1;
        public Entity(Texture2D texture)
        {
            this.texture = texture;
  
        }

        public virtual void Update(GameTime gameTime)
        {
            
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
    }
}
