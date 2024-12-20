using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class MenuManager
    {
        List<Component> components = new List<Component>();
        public MenuManager() {
            components.Add(new Button(new (Game1.ResX/2,650),new (300,100),"options"));
            components.Add(new Button(new (Game1.ResX/2,500),new (300,100),"level select"));
            components.Add(new Button(new (Game1.ResX/2,350),new (300,100),"star game"));
            components.Add(new Title());
        }

        public void Update(GameTime gameTime) 
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch sb) 
        {
            foreach(var component in components)
            {
                component.Draw(sb);
            }
        }
    }
}
