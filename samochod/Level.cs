using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Level
    {
        // using Point because int, never float
        static protected Dictionary<TileID,Point> tilePosition;
        List<Tile> tileMap;
        public const int tileSize = 16;
        public const int tileScale = 2;
        public Texture2D tileMapTexture;
        
        public const int mapWidth  = 1280 / (tileSize*tileScale);
        public const int mapHeight = 896 / (tileSize*tileScale);
        static public Texture2D tileSet;
        // entityList
        // collisionZones

        public Level() {
            if (tilePosition == null)
            {
                tilePosition = new Dictionary<TileID, Point> {
                    {TileID.sCurbManhole,new Point(16,608) },
                    {TileID.sEmpty,new Point(48,608) },
                    {TileID.sYellowH,new Point(48,640) },
                };
            }

            tileMap = new List<Tile> 
            {
                    
            };
        }

        public Rectangle GetTile(TileID ID)
        {
            Point tilePos = tilePosition[ID];

            return new Rectangle(tilePos.X,tilePos.Y ,tileSize,tileSize);
        }
        public void Load()
        {
            // create a texture out of tiles
        }
        public void Draw(SpriteBatch spritebatch)
        {
            int scale = tileScale;
            Vector2 position = new Vector2(0,0);
            if (tileMapTexture == null) 
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    position.Y = j * scale*tileSize;
                    position.X = 0;

                    for (int i = 0; i < mapWidth; i++)
                    {
                        if (i + j * 32 < 896)
                        {
                            position.X = i * tileSize * scale;
                            spritebatch.Draw(tileSet, position, GetTile(tileMap[i + j * 32].ID), Color.White,
                                0, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
        }
    }
}
