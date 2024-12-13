using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace samochod
{
    public class LevelManager
    {
        // for editor
        private static int ID_selector = 22;
        private Tile previewTile = new((TileID)ID_selector, 0);
        private Vector2 previewPos;
        // 

        public Texture2D tileMapTexture; // only implement if perfomance bad or levels done 
        public Level currentLevel;
        public int levelID;
        // data
        private static Dictionary<TileID, Point> tileDic; // using Point because int, never float
        public static Texture2D tileSet;
        public static int tileSize = 16;
        public static int tileScale = 2;
        public static int mapWidth = 1280 / (tileSize * tileScale);
        public static int mapHeight = 896 / (tileSize * tileScale);
        public LevelManager()
        {
            #region Init Tile Dic
            if (tileDic == null)
            {
                tileDic = new Dictionary<TileID, Point> {
                    {TileID.sCurbManhole,new Point(16,608) },
                    {TileID.sEmpty,new Point(48,608) },
                    {TileID.sYellowH,new Point(48,640) },
                    {TileID.pEmpty,new Point(336,640) },
                };
            }
            #endregion
            LoadLevel(0);
        }
        public void LoadLevel(int levelID)
        {
            //makeLevel();
            LoadData(levelID);
            LoadTexture();
        }
        private void makeLevel()
        {
            currentLevel = new Level();
            currentLevel.tileMap = new();
            for (int i = 0; i < mapWidth * mapHeight; i++)
            {
                currentLevel.tileMap.Add(new Tile(TileID.sEmpty, 0));
            }

        }
        private void LoadTexture()
        {
            // set tileMapTexture
        }
        private void LoadData(int levelID)
        {
            string file = $"level{levelID}.data";
            this.levelID = levelID;
            // load map data from file

            if (File.Exists(file))
            {
                byte[] bytes = File.ReadAllBytes(file);
                currentLevel = JsonSerializer.Deserialize<Level>(bytes);
            }
            else
            {
                throw new Exception("Couldn't load level data, file not found.");
            }
        }
        public void SaveData()
        {
            string file = $"level{levelID}.data";
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(currentLevel);
            File.WriteAllBytes(file, bytes);
            Debug.WriteLine($"{file} saved");
        }
        public Rectangle GetTile(TileID ID)
        {
            Point tilePos = tileDic[ID];

            return new Rectangle(tilePos.X, tilePos.Y, tileSize, tileSize);
        }
        public void Update(GameTime gameTime)
        {
            if (Input.IsKeyDown(Keys.LeftControl) && Input.IsKeyPressed(Keys.S))
            {
                SaveData();
            }

            // snap preview to nearest tile
            int snapX = Input.mousePosition.X/(tileScale*tileSize)    * (tileScale*tileSize);
            int snapY = Input.mousePosition.Y/ (tileScale * tileSize) * (tileScale * tileSize);
            previewPos = new Vector2(snapX, snapY);

        }
        public void Draw(SpriteBatch spritebatch)
        {
            int scale = tileScale;
            Vector2 position = new Vector2(0, 0);
            if (tileMapTexture == null)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    position.Y = j * scale * tileSize;
                    position.X = 0;

                    for (int i = 0; i < mapWidth; i++)
                    {
                        var tile = currentLevel.tileMap[i + j * mapHeight];
                        position.X = i * tileSize * scale;
                        spritebatch.Draw(tileSet, position, GetTile(tile.ID), Color.White,
                            tile.flags, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        
                    }
                }
            }
            // draw preview tile
            spritebatch.Draw(tileSet, previewPos, GetTile(previewTile.ID), Color.White,
                previewTile.flags, Vector2.Zero, scale, SpriteEffects.None, 0f);
            
        }
    }
}
