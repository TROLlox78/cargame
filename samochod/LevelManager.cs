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
        private static int tileSelector = 0;
        private Tile previewTile = new((TileID)tileSelector, 0);
        private Vector2 previewPos;
        private ActionType actionType = ActionType.None;
        private string actionStr;

        private static EntityType zoneSelector = EntityType.collisionZone;
        private Rectangle zoneRect = new();
        private bool drawing;
        private Point zonePos;
        public static EntityManager entityManager;
        // ediotr
        public static TextManager textMan;

        public Texture2D tileMapTexture; // only implement if perfomance bad or levels done 
        public static Level currentLevel;
        public int levelID = 0;
        private int levelsCount = 5;
        // data
        private static Dictionary<TileID, Point> tileDic; // using Point because int, never float
        public static Texture2D tileSet;
        public const int tileSize = 16;
        public static Vector2 origin = new Vector2(tileSize/2, tileSize / 2);
        public const int tileScale = 2;
        public const int mapWidth = 1280 / (tileSize * tileScale);
        public const int mapHeight = 896 / (tileSize * tileScale);

        public LevelManager()
        {
            #region Init Tile Dic
            if (tileDic == null)
            {
                tileDic = new Dictionary<TileID, Point> {
                    {TileID.sCurbManhole,new Point(16,608) },
                    {TileID.sEmpty,new Point(48,608) },

                    {TileID.sTwoLines,new Point(80,608) },
                    //{TileID.sBend,new Point(144,608) },
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
                    // grass
                    {TileID.gGrass,new Point(80,816) },
                    {TileID.gLeftTopPath,new Point(224,768) },
                    {TileID.gTopPath,new Point(256,768) },
                    {TileID.gRightTopPath,new Point(288,768) },
                    {TileID.gLeftSidePath,new Point(224,800) },
                    {TileID.gEmpty,new Point(256,800) },
                    {TileID.gRightSidePath,new Point(288,800) },
                    {TileID.gLeftBottomPath,new Point(224,832) },
                    {TileID.gBottomPath,new Point(256,832) },
                    {TileID.gRightBottomPath,new Point(288,832) },

                    // lake
                    {TileID.lLake,new Point(48,816) },
                    {TileID.lTopLeft,new Point(16,768) },
                    {TileID.lTop1,new Point(48,768) },
                    {TileID.lTop2,new Point(80,768) },
                    {TileID.lTop3,new Point(112,768) },
                    {TileID.lTopRight,new Point(148,768) },

                    {TileID.lLeftSide1,new Point(16,800) },
                    {TileID.lLeftSide2,new Point(16,832) },
                    {TileID.lLeftSide3,new Point(16,864) },
                    {TileID.lRightSide1,new Point(144,800) },
                    {TileID.lRightSide2,new Point(144,832) },
                    {TileID.lRightSide3,new Point(144,864) },
                    {TileID.lLeftBottom,new Point(16,896) },
                    {TileID.lBottom1,new Point(16+32,896) },
                    {TileID.lBottom2,new Point(16+32*2,896) },
                    {TileID.lBottom3,new Point(16+32*3,896) },
                    {TileID.lRightBottom,new Point(16+32*4,896) },

                };
            }
            #endregion
        }
        public bool reachedFinish()
        {
            // minus one because levelID starts at 0
            return levelID+1>(levelsCount-1);
        }
        public Level LoadLevel()
        {
            //makeLevel();
            LoadData(levelID);
            //currentLevel.pIntro = new PlayerIntro(66, 1000, -1.57f, 0.7f);
            LoadTexture();
            return currentLevel;
        }
        private void makeLevel()
        {
            currentLevel = new Level();
            currentLevel.tileMap = new();
            for (int i = 0; i < mapWidth * mapHeight; i++)
            {
                currentLevel.tileMap.Add(new Tile(TileID.sEmpty, 0));
            }
            currentLevel.entities = new List<EntityTranslator>();
            currentLevel.zones = new();

        }
        private void LoadTexture()
        {
            // set tileMapTexture
        }
        private void LoadData(int levelID)
        {
            string file = $"lvl\\level{levelID}.data";
            this.levelID = levelID;
            // load map data from file

            if (File.Exists(file))
            {
                byte[] bytes = File.ReadAllBytes(file);
                currentLevel = JsonSerializer.Deserialize<Level>(bytes);
            }
            else
            {
                //throw new Exception("Couldn't load level data, file not found.");
                makeLevel();
            }
        }
        private void PopulateLevel()
        {

            currentLevel.zones = new();
            // add entities
            currentLevel.entities = new();
            foreach ( Entity e in EntityManager.entities)
            {
                Debug.WriteLine($"Saving entity type {e.EntityType.ToString()}");

                if (e is Player)
                {
                    continue;
                }
                if (e is Car)
                {
                    currentLevel.entities.Add(new EntityTranslator(EntityType.car,
                        e.position.X ,
                        e.position.Y ,
                        e.rotation,((Car)e).carModel));
                }
                if (e is Zone)
                {
                    currentLevel.zones.Add(new ZoneTranslator(e.EntityType,
                    e.position.X - e.origin.X,
                    e.position.Y - e.origin.Y,
                    e.width, e.height));
                }
            }
        }
        public void SaveData()
        {
            PopulateLevel();
            string file = $"lvl\\level{levelID}.data";
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(currentLevel);
            if (File.Exists(file))
            {
                File.WriteAllBytes(file, bytes);
            }
            else
            {
                throw new Exception("no save file found");
            }
            Debug.WriteLine($"{file} saved");
        }
        public Rectangle GetTile(TileID ID)
        {
            Point tilePos = tileDic[ID];

            return new Rectangle(tilePos.X, tilePos.Y, tileSize, tileSize);
        }
        public void Update(GameTime gameTime)
        {
            Game1.canMouse = false;
            if  (actionType == ActionType.None)
            {
                actionStr = "";
                // allow for mousing things
                Game1.canMouse = true;
            }
            if (actionType == ActionType.Tile && Game1.isFocused)
            {
                actionStr = $"Placing: {((TileID)tileSelector)}";

                // Update mouse. snap preview to nearest tile
                int snapX = Input.mousePosition.X / (tileScale * tileSize) * (tileScale * tileSize);
                int snapY = Input.mousePosition.Y / (tileScale * tileSize) * (tileScale * tileSize);
                previewPos = new Vector2(snapX + origin.X * tileScale, snapY + origin.Y * tileScale);
                
                if (Input.IsKeyPressed(Keys.OemPeriod))
                {
                    tileSelector = ++tileSelector % (tileDic.Count);
                    previewTile = new((TileID)tileSelector, 0);
                }
                if (Input.IsKeyPressed(Keys.OemComma))
                {
                    int len = tileDic.Count;
                    tileSelector = (--tileSelector % len + len) % len; // tilecount - (tilecount -id)
                    previewTile = new((TileID)tileSelector, 0);

                }
                if (Input.IsKeyPressed(Keys.R))
                {
                    byte rot = (byte)(previewTile.flags & 0b0000_0011); // mask out rot bits
                    rot++; // increment rotation
                    byte masked_rot = (byte)(rot & 0b0000_0011);
                    previewTile.flags = (byte)((previewTile.flags & 0b1111_1100) | masked_rot);
                }
                if (Input.IsKeyPressed(Keys.E))
                {
                    previewTile.flags ^= (1 << 2);
                }
                if (Input.IsKeyPressed(Keys.Q) && Input.mouseInBounds)
                {
                    int idx_X = snapX / (tileScale * tileSize);
                    int idx_Y = snapY / (tileScale * tileSize);
                    var pickedTile = currentLevel.tileMap[idx_X + idx_Y * mapWidth];
                    previewTile = new Tile(pickedTile.ID, pickedTile.flags);
                }
                if (Input.IsLeftPressed() && Input.mouseInBounds)
                {
                    int idx_X = snapX / (tileScale * tileSize);
                    int idx_Y = snapY / (tileScale * tileSize);
                    currentLevel.tileMap[idx_X + idx_Y * mapWidth] = new Tile(
                        previewTile.ID, previewTile.flags);
                    Debug.WriteLine($"idx x:{idx_X}, y:{idx_Y}");
                }
                if (Input.IsRightClicked() && Input.mouseInBounds)
                {
                    int idx_X = snapX / (tileScale * tileSize);
                    int idx_Y = snapY / (tileScale * tileSize);
                    currentLevel.tileMap[idx_X + idx_Y * mapWidth] = new Tile(
                        TileID.sEmpty, 0);
                    Debug.WriteLine($"idx x:{idx_X}, y:{idx_Y}");
                }
            }
            if (actionType == ActionType.Car)
            {
                actionStr = "";
                if (Input.mouseInBounds)
                {
                    if (Input.IsLeftClicked())
                    {
                        entityManager.AddCar(EntityType.car, Input.mousePosition.ToVector2());
                    }
                }
            }
            if (actionType == ActionType.Entity) {
                actionStr = "";
            }
            if (actionType == ActionType.CollisionZone)
            {
                actionStr = $"Placing: {((EntityType)zoneSelector)}";

                
                zoneRect = new();

                if (Input.IsKeyPressed(Keys.OemPeriod)) // '.' key
                {
                    if (zoneSelector != EntityType.noParkZone)
                    {
                        zoneSelector = ++zoneSelector;
                    } else {
                        zoneSelector = EntityType.collisionZone;
                    }
                }
                if (Input.IsKeyPressed(Keys.OemComma))
                {
                    //int len = tileDic.Count;
                    //zoneSelector = (--zoneSelector % len + len) % len; // tilecount - (tilecount -id)

                }

                if (Input.mouseInBounds)
                {
                    if (Input.IsLeftClicked() && !drawing)
                    {

                        drawing = true;
                        zonePos = Input.mousePosition;
                    }
                    
                    else if (drawing)
                    {
                        zoneRect = new(zonePos, new Point(Input.mousePosition.X - zonePos.X,
                                Input.mousePosition.Y - zonePos.Y));
                        if (Input.IsLeftClicked())
                        {
                            drawing = false;

                            var XD = new Zone(zoneSelector,
                                zoneRect.X,
                                zoneRect.Y, zoneRect.Width, zoneRect.Height);
                            entityManager.AddZone(XD);
                            Debug.WriteLine($"{zoneSelector} becomse {XD.EntityType.ToString()}");
                            // abosultly perpedicular
                        }
                    }
                    
                }
            }
            if (Input.IsKeyPressed(Keys.M))
            {
                // switch action type
                int temp = (int)actionType;
                actionType = (ActionType)(++temp % Enum.GetValues(typeof(ActionType)).Length);
            }

            if (Input.IsKeyDown(Keys.LeftControl) && Input.IsKeyPressed(Keys.S))
            {
                SaveData();
            }
            
            


            textMan.Write(new Text( $"Action:{actionType.ToString()} {actionStr}"));
            textMan.Write(new Text( $"Level : {levelID}"));
        }
        public void Draw(SpriteBatch spritebatch)
        {
            Vector2 position = new Vector2(0, 0);
            if (tileMapTexture == null)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    position.Y = (j * tileSize + origin.Y)*tileScale;

                    for (int i = 0; i < mapWidth; i++)
                    {
                        position.X = (i * tileSize + origin.X) * tileScale;
                        var tile = currentLevel.tileMap[i + j * mapWidth];
                        DrawTile(spritebatch, position, tile);
                        //txt.Write(new Text(position- origin, $"{i + j * mapWidth}", 0.6f));
                    }
                }
            }
            // draw preview tile
            if (actionType == ActionType.Tile)
            {
                DrawTile(spritebatch,previewPos,previewTile);
            }
            if (actionType == ActionType.CollisionZone)
            {
                float previewRot = (previewTile.flags & 0b0000_0011) * (float)Math.PI / 2;
                spritebatch.Draw(tileSet, zoneRect, GetTile(TileID.pEmpty2), new Color(Color.Black, 0.4f),
                    0, Vector2.Zero,  SpriteEffects.None, 0f);
            }

            
        }
        public static TileID CheckTile(Vector2 pos)
        {
            int snapX = (int)pos.X / (tileScale * tileSize) * (tileScale * tileSize);
            int snapY = (int)pos.Y / (tileScale * tileSize) * (tileScale * tileSize);

            int idx_X = snapX / (tileScale * tileSize);
            int idx_Y = snapY / (tileScale * tileSize);
            var checkTile = currentLevel.tileMap[idx_X + idx_Y * mapWidth];
            
            return checkTile.ID;
        }
        private void DrawTile(SpriteBatch spritebatch,  Vector2 position, Tile tile)
        {
            float rotation = (tile.flags & 0b0000_0011) * (float)Math.PI / 2;
            bool horizontal_mirror = (tile.flags & 0b0000_0100) != 0;
            SpriteEffects effect = horizontal_mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spritebatch.Draw(tileSet, position, GetTile(tile.ID), Color.White,
                rotation, origin, tileScale, effect, 0f);
        }
    }
}
