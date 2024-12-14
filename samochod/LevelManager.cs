﻿using Microsoft.Xna.Framework;
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
        private static int ID_selector = 0;//22;
        private Tile previewTile = new((TileID)ID_selector, 0);
        private Vector2 previewPos;
        private string ActionType = "none";
        // 
        public static TextManager txt;

        public Texture2D tileMapTexture; // only implement if perfomance bad or levels done 
        public Level currentLevel;
        public int levelID;
        // data
        private static Dictionary<TileID, Point> tileDic; // using Point because int, never float
        public static Texture2D tileSet;
        public static int tileSize = 16;
        public static Vector2 origin = new Vector2(tileSize/2, tileSize / 2);
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

                    {TileID.sTwoLines,new Point(80,608) },
                    {TileID.sBend,new Point(144,608) },
                    {TileID.sYellow,new Point(16,640) },
                    {TileID.sStripSide,new Point(80,640) },
                    {TileID.sDoubleStripSide,new Point(112,640) },
                    {TileID.sManhole,new Point(16,672) },
                    {TileID.sCrossing,new Point(48,672) },
                    {TileID.sStraigthLine,new Point(80,672) },
                    // pavement
                    {TileID.pTopCorner,new Point(208,608) },
                    {TileID.pTop1,new Point(240,608) },
                    {TileID.pTop2,new Point(272,608) },
                    {TileID.pBalls,new Point(336,608) },
                    {TileID.pLeftSide,new Point(208,640) },
                    {TileID.pTopShaded,new Point(240,640) },
                    {TileID.pBottomShaded,new Point(272,640) },
                    {TileID.pRightSide,new Point(304,640) },
                    {TileID.pEmpty,new Point(336,640) },
                    {TileID.pLeftSide2,new Point(208,672) },
                    {TileID.pLeftShaded,new Point(240,672) },
                    {TileID.pRightShaded,new Point(272,672) },
                    {TileID.pRightSide2,new Point(304,672) },
                    {TileID.pEmpty2,new Point(336,672) },
                    {TileID.pBottomCorner,new Point(208,704) },
                    {TileID.pBottom1,new Point(240,704) },
                    {TileID.pBottom2,new Point(272,704) },
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
            // Update mouse. snap preview to nearest tile
            int snapX = Input.mousePosition.X / (tileScale * tileSize) * (tileScale * tileSize);
            int snapY = Input.mousePosition.Y / (tileScale * tileSize) * (tileScale * tileSize);
            previewPos = new Vector2(snapX + origin.X * tileScale, snapY + origin.Y * tileScale);
            if (Input.IsKeyDown(Keys.LeftControl) && Input.IsKeyPressed(Keys.S))
            {
                SaveData();
            }
            if (Input.IsKeyPressed(Keys.M))
            {
                // switch action type
            }
            if (Input.IsKeyPressed(Keys.OemPeriod))
            {
                Debug.WriteLine("key oem period");
                ID_selector = ++ID_selector % (tileDic.Count-1);
                previewTile = new((TileID)ID_selector, 0);
                ActionType = $"Placing: {((TileID)ID_selector)}";

    }
            if (Input.IsKeyPressed(Keys.OemComma))
            {
                Debug.WriteLine("key less");
                int len = tileDic.Count ;
                ID_selector = (--ID_selector% len + len) % len; // tilecount - (tilecount -id)
                previewTile = new((TileID)ID_selector, 0);
                ActionType = $"Placing: {((TileID)ID_selector)}";

            }
            if (Input.IsKeyPressed(Keys.R)) 
            {
                // TODO: FIX THIS, ONLY 2 BITS FOR ROTATION
                //byte rot = (byte)(previewTile.flags & 0b0000_0011);
                //previewTile.flags = (byte)(previewTile.flags | (++rot & 0b0000_0011));
                previewTile.flags++;
            }
            if (Input.IsLeftClicked())
            {
                int idx_X = snapX / (tileScale * tileSize);
                int idx_Y = snapY / (tileScale * tileSize);
                currentLevel.tileMap[idx_X + idx_Y * mapWidth] = new Tile(
                    previewTile.ID, previewTile.flags);
                Debug.WriteLine($"idx x:{idx_X}, y:{idx_Y}");
            }
            if (Input.IsKeyPressed(Keys.K))
            {
                Debug.WriteLine($"map size {currentLevel.tileMap.Count}");
            }

            txt.Write(new Text( ActionType));
        }
        public void Draw(SpriteBatch spritebatch)
        {
            int scale = tileScale;
            Vector2 position = new Vector2(0, 0);
            if (tileMapTexture == null)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    position.Y = (j * tileSize + origin.Y)*scale;

                    for (int i = 0; i < mapWidth; i++)
                    {
                        position.X = (i * tileSize + origin.X)*scale;
                        var tile = currentLevel.tileMap[i + j * mapWidth];
                        float rotation = (tile.flags & 0b0000_0011) * (float)Math.PI/2;
                        
                        spritebatch.Draw(tileSet, position, GetTile(tile.ID), Color.White,
                            rotation, origin, scale, SpriteEffects.None, 0f);
                        //txt.Write(new Text(position- origin, $"{i + j * mapWidth}", 0.6f));
                    }
                }
            }
            // draw preview tile
            float previewRot = (previewTile.flags & 0b0000_0011) * (float)Math.PI / 2;
            spritebatch.Draw(tileSet, previewPos, GetTile(previewTile.ID), Color.White,
                previewRot, origin, scale, SpriteEffects.None, 0f);
            
        }
    }
}