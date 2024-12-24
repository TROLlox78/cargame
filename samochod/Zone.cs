using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
using System;
using System.Diagnostics;


namespace samochod
{
    public class Zone : Entity
    {
        public Rectangle rect { 
            get 
            { return new Rectangle((int)(position.X-origin.X), (int)(position.Y-origin.Y),width,height); } 
        }
        public bool touching = false;
        
        public Zone(EntityType entityType, float pX, float pY, int Width, int Height)
        {
            EntityType = entityType;
            Debug.WriteLine(EntityType);
            Debug.WriteLine(entityType);
            width = Width;
            height = Height;
            origin = new Vector2(width/2, height/2);
            position = new Vector2( pX + origin.X, pY + origin.Y);
            //rect = new Rectangle((int)pX,(int)pY, Width, Height);
            textureBoundry = new Rectangle(336, 672, 16, 16);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Game1.debug)
            { return; }
            switch (EntityType)
            {
                case EntityType.collisionZone:
                    spriteBatch.Draw(textures[3], rect, textureBoundry,
                        new Color(Color.Pink, 0.4f),
                        0, Vector2.Zero, SpriteEffects.None, 0f);
                    break;
                case EntityType.winZone:
                    spriteBatch.Draw(textures[3], rect, textureBoundry,
                        new Color(Color.Green, 0.4f),
                        0, Vector2.Zero, SpriteEffects.None, 0f);
                    break;
                case EntityType.noParkZone:
                    spriteBatch.Draw(textures[3], rect, textureBoundry,
                        new Color(Color.Red, 0.4f),
                        0, Vector2.Zero, SpriteEffects.None, 0f);
                    break;
                    //default:
                    //throw new Exception($"incorrect type assigned to zone: {entityType.ToString()}");

            }
            if (Game1.drawHitbox) { DrawHitbox(spriteBatch); }
            
        }
    }
}
