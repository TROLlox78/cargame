using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace samochod
{
    public class Title : Component
    {
        float rotation = 0;
        float maxRot = 0.1f;
        float rotSpeed = 0.001f;
        float scale = 4;
        Vector2 origin;
        string text;
        Color color;
        public Title() {
            text = "CAR GAME";
            color = new (255, 165, 0); // basic orange
            position = new Point(Game1.ResX / 3 + 100, 100);
            origin = new Vector2(4*scale*text.Length / 2, 1*scale);
        }

        public override void Update(GameTime gameTime)
        {
            if (rotation > maxRot)
            {
                rotSpeed *= -1;
            }
            if (rotation < -maxRot)
            {
                rotSpeed *= -1;
            }
            rotation += rotSpeed;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(TextMan.blazed, "CAR GAME", position.ToVector2(), color,
                rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}