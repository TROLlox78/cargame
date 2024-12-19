using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace samochod
{
    public class Zone : Entity
    {
        EntityType entityType {  get; set; }
        public float pX {  get; set; }
        public float pY { get; set; }
        public int  Width { get; set; }
        public int Height { get; set; }
        [JsonConstructor]
        public Zone(EntityType entityType, float pX, float pY, int Width, int Height)
        {
            this.entityType = entityType;
            this.pX = pX;
            this.pY = pY;
            this.Width = Width;
            this.Height = Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle zoneRect = new Rectangle((int)pX, (int)pY, Width, Height);
            spriteBatch.Draw(textures[3], zoneRect, new Rectangle(336, 672, 16,16), new Color(Color.Pink,0.4f),
                0, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
