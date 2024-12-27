using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace samochod
{
    public class GameScore
    {
        public string time;
        public EntityManager entityManager;
        public LevelManager levelManager;
        public GameScore()
        {
            inGameTimer = new Stopwatch();
            bestTimes = new Dictionary<int, TimeSpan> {
                {0, new TimeSpan(0,0,0) },
                {1, new TimeSpan(0,0,0) },
                {2, new TimeSpan(0,0,0) },
                {3, new TimeSpan(0,0,0) },
                {4, new TimeSpan(0,0,0) },
                {5, new TimeSpan(0,0,0) },
            };
        }
        // should maybe make a timer class
        public Stopwatch inGameTimer;
        public Dictionary<int, TimeSpan> bestTimes;

        public void Update(GameTime gt)
        {
            if (Game1.gameState == GameState.loading)
            {
                inGameTimer.Reset();
            }
            if (Game1.gameState == GameState.running)
            {
                if (entityManager.player.startedMoving && !inGameTimer.IsRunning)
                {
                    inGameTimer.Start();
                }
                time = $"Time: {inGameTimer.Elapsed:mm\\:ss\\.ff}";
            }
            if (Game1.gameState == GameState.win)
            {
                inGameTimer.Stop();
                bestTimes[LevelManager.levelID] = inGameTimer.Elapsed;

            }
        }
    }
}
