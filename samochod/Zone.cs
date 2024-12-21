using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace samochod
{
    public class Zone : Entity
    {
        EntityType entityType;
        public Rectangle rect { 
            get 
            { return new Rectangle((int)(position.X-origin.X), (int)(position.Y-origin.Y),width,height); } 
        }
        public bool touching = false;
        
        public Zone(EntityType entityType, float pX, float pY, int Width, int Height)
        {
            this.entityType = entityType;
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
            spriteBatch.Draw(textures[3], rect, textureBoundry, new Color(Color.Pink,0.4f),
                0, Vector2.Zero, SpriteEffects.None, 0f);
            if (Game1.drawHitbox) { DrawHitbox(spriteBatch); }
        }
    }
}
