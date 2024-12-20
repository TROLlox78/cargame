using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Component
    {
        public static TextManager TextMan;
        public Point position { get; set; }
        public Point size { get; set; }
        public static List<Texture2D> textures;
        public Component() { }

        public virtual void Update(GameTime gameTime) { 
            
        }
        public virtual void Draw(SpriteBatch sb) { }
    }
}
