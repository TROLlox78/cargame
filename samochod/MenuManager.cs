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
            var options = new Button(new(Game1.ResX / 2, 650), new(300, 100), "options");
            var levelSelect = new Button(new(Game1.ResX / 2, 500), new(300, 100), "level select");
            var startGame = new Button(new(Game1.ResX / 2, 350), new(300, 100), "star game");
            components.Add(options);
            components.Add(levelSelect);
            components.Add(startGame);
            components.Add(new Title());
            startGame.Click += StartGame_Click;
            options.Click += Options_Click;
            levelSelect.Click += LevelSelect_Click;
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            Game1.gameState = GameState.running;
        }
        private void Options_Click(object sender, EventArgs e)
        {
            Game1.gameState = GameState.options;

            throw new NotImplementedException();
        }
        private void LevelSelect_Click(object sender, EventArgs e)
        {
            Game1.gameState = GameState.levelSelect;

            throw new NotImplementedException();
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
