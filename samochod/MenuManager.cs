using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
using System;
using System.Collections.Generic;


namespace samochod
{
    public class MenuManager
    {
        public MenuState menuState = MenuState.main;
        List<Component> mainMenu = new List<Component>();
        List<Component> optionsMenu = new List<Component>();
        List<Component> levelsMenu = new List<Component>();
        List<Component> endMenu = new List<Component>();
        Text credit;
        Text endTimes;
        public MenuManager() {
            #region main menu screen
            var options = new Button(new(Game1.ResX / 2, 650), new(300, 100), "Options");
            var levelSelect = new Button(new(Game1.ResX / 2, 500), new(300, 100), "Level select");
            var startGame = new Button(new(Game1.ResX / 2, 350), new(300, 100), "Start game");
            credit = new Text(new(Game1.ResX *0.37f, Game1.ResY * 0.97f), "Made by s193162, JPWP 2024/25",0.8f);
            mainMenu.Add(options);
            mainMenu.Add(levelSelect);
            mainMenu.Add(startGame);
            mainMenu.Add(new Title("CAR GAME"));
            startGame.Click += StartGame_Click;
            options.Click += Options_Click;
            levelSelect.Click += LevelSelect_Click;
            #endregion

            #region options menu screen
            var back =       new Button(new(Game1.ResX / 2, 650), new(300, 100), "Back");
            var volumeMute = new Button(new(Game1.ResX / 2, 500), new(300, 100), "Music: On");
            var difficulty = new Button(new(Game1.ResX / 2, 350), new(300, 100), "Difficulty: hard");
            optionsMenu.Add(back);
            optionsMenu.Add(volumeMute);
            optionsMenu.Add(difficulty);
            optionsMenu.Add(new Title("OPTIONS"));
            back.Click += Back_Click;
            volumeMute.Click += VolumeMute_Click;
            difficulty.Click += Difficulty_Click;
            #endregion
            #region level select menu screen
            levelsMenu.Add(new Title("LEVELS"));
            var lvl1 = new Button(new(Game1.ResX*25/100, 300), new(300, 100), "Level 1");
            var lvl2 = new Button(new(Game1.ResX*5/10, 300), new(300, 100), "Level 2");
            var lvl3 = new Button(new(Game1.ResX*75/100, 300), new(300, 100), "Level 3");

            var lvl4 = new Button(new(Game1.ResX*25/100, 500), new(300, 100), "Level 4");
            var lvl5 = new Button(new(Game1.ResX*5/10, 500), new(300, 100), "Level 5");
            var lvl6 = new Button(new(Game1.ResX*75/100, 500), new(300, 100), "Level 6");
            levelsMenu.Add(lvl1); levelsMenu.Add(lvl2); levelsMenu.Add(lvl3);
            levelsMenu.Add(lvl4); levelsMenu.Add(lvl5); levelsMenu.Add(lvl6);
            levelsMenu.Add(back);
            lvl1.Click += Lvl1_Click;
            lvl2.Click += Lvl2_Click;
            lvl3.Click += Lvl3_Click;
            lvl4.Click += Lvl4_Click;
            lvl5.Click += Lvl5_Click;
            lvl6.Click += Lvl6_Click;
            #endregion
            #region end

            endMenu.Add(new Button(new(Game1.ResX / 2, Game1.ResY/2), new(500, 500), ""));
            endMenu.Add(new Title("THE END"));
            
            #endregion
        }

        private void Lvl1_Click(object sender, EventArgs e)
        {
            LevelManager.levelID = 0;
            Game1.gameState = GameState.loading;
        }
        private void Lvl2_Click(object sender, EventArgs e)
        {
            LevelManager.levelID = 1;
            Game1.gameState = GameState.loading;
        }
        private void Lvl3_Click(object sender, EventArgs e)
        {
            LevelManager.levelID = 2;
            Game1.gameState = GameState.loading;
        }
        private void Lvl4_Click(object sender, EventArgs e)
        {
            LevelManager.levelID = 3;
            Game1.gameState = GameState.loading;
        }
        private void Lvl5_Click(object sender, EventArgs e)
        {
            LevelManager.levelID = 4;
            Game1.gameState = GameState.loading;
        }
        private void Lvl6_Click(object sender, EventArgs e)
        {
            LevelManager.levelID = 5;
            Game1.gameState = GameState.loading;
        }

        private void Difficulty_Click(object sender, EventArgs e)
        {
            var but = (Button)sender;
            if (but.text == "Difficulty: hard")
            {
                Game1.difficulty = false;
                but.text = "Difficulty: easy";
            }
            else if (but.text == "Difficulty: easy")
            {
                Game1.difficulty = true;
                but.text = "Difficulty: hard";
            }
        }

        private void VolumeMute_Click(object sender, EventArgs e)
        {
            var but = (Button)sender;
            if (Game1.audioMute)
            {
                but.text = "Music: On";
                Game1.audioMute = false;
            }
            else if (!Game1.audioMute)
            {
                but.text = "Music: Off";
                Game1.audioMute = true;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            menuState = MenuState.main;
        }
        private void StartGame_Click(object sender, EventArgs e)
        {
            Game1.gameState = GameState.loading;
        }
        private void Options_Click(object sender, EventArgs e)
        {
            menuState = MenuState.options;
        }
        private void LevelSelect_Click(object sender, EventArgs e)
        {
            menuState = MenuState.levelSelect;
        }

        public void EndGame(GameScore score)
        {
            Game1.gameState = GameState.menu;
            menuState = MenuState.end;
            string x = "";
            int i = 1;
            TimeSpan total = TimeSpan.Zero;
            foreach (var time in score.bestTimes)
            {
                x += $"Level {i++}: {time.Value:mm\\:ss\\.fff}\n";
                total += time.Value;
            }
            x += $"Total Time: {total:mm\\:ss\\.fff}";
            endTimes = new Text(new Vector2(Game1.ResX / 2 - 100, Game1.ResY * 0.3f), x);

        }
        public void Update(GameTime gameTime) 
        {
            if (menuState == MenuState.main)
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
            else if (menuState == MenuState.options)
            {
                foreach (var component in optionsMenu)
                {
                    component.Update(gameTime);
                }
                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                {
                    menuState = MenuState.main;
                }
            }
             else if (menuState == MenuState.levelSelect) {
                foreach (var component in levelsMenu)
                {
                    component.Update(gameTime);
                }
                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                {
                    menuState = MenuState.main;
                }
            }
            if (menuState == MenuState.end) {
                if (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                {
                    menuState = MenuState.main;
                    Game1.gameState = GameState.menu;
                }
            }
        }


        public void EndDraw(SpriteBatch sb)
        {
            foreach (var component in endMenu)
            {
                component.Draw(sb);
            }
            endTimes.Draw(sb);
        }

        public void WinScreenDraw(SpriteBatch sb)
        {

        }
        public void LevelSelectDraw(SpriteBatch sb)
        {
            foreach (var component in levelsMenu)
            {
                component.Draw(sb);
            }
        }
        public void OptionsMenuDraw(SpriteBatch sb)
        {
            foreach (var component in optionsMenu)
            {
                component.Draw(sb);
            }
        }
        public void Draw(SpriteBatch sb) 
        {
            if (menuState == MenuState.main)
            {
                MainMenuDraw(sb);
            }
            else if (menuState == MenuState.levelSelect)
            {
                LevelSelectDraw(sb);
            }
            else if (menuState == MenuState.options)
            {
                OptionsMenuDraw(sb);   
            }
            else if (menuState == MenuState.end)
            {
                EndDraw(sb);
            }
            credit.Draw(sb);
        }

        private void MainMenuDraw(SpriteBatch sb)
        {
            foreach (var component in mainMenu)
            {
                component.Draw(sb);
            }
        }
    }
}
