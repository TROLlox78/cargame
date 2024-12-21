using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
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
        List<Component> mainMenu = new List<Component>();
        List<Component> winScreen = new List<Component>();
        
        public MenuManager() {
            #region main menu screen
            var options = new Button(new(Game1.ResX / 2, 650), new(300, 100), "options");
            var levelSelect = new Button(new(Game1.ResX / 2, 500), new(300, 100), "level select");
            var startGame = new Button(new(Game1.ResX / 2, 350), new(300, 100), "star game");
            mainMenu.Add(options);
            mainMenu.Add(levelSelect);
            mainMenu.Add(startGame);
            mainMenu.Add(new Title());
            startGame.Click += StartGame_Click;
            options.Click += Options_Click;
            levelSelect.Click += LevelSelect_Click;
            #endregion

            #region win screen
            var backGround = new Button(new(Game1.ResX / 2, 650), new(400, 200), "");
            winScreen.Add(backGround);
            #endregion
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            Game1.gameState = GameState.loading;
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
            foreach (var component in mainMenu)
            {
                component.Update(gameTime);
            }
            if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Game1.gameState = GameState.loading;
            }
        }
        public void WinScreenDraw(SpriteBatch sb)
        {

        }
        public void MainMenuDraw(SpriteBatch sb) 
        {
            foreach(var component in mainMenu)
            {
                component.Draw(sb);
            }
        }
    }
}
