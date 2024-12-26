using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


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
        public Title(string s) {
            text = s;
            color = new (255, 190, 0); // basic orange
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

            // offset by calculated amount 
            position = new Point(position.X - 45, position.Y + 22);
            float yellow = 1;
            for (int i = 0; i < 10; i++)
            {
                yellow *= 0.9f;
                position = new Point(position.X + i, position.Y - i / 2);
                color = new(1, yellow, 0);
                sb.DrawString(TextManager.blazed, text, position.ToVector2(), color,
                    rotation, origin, scale, SpriteEffects.None, 0f);
            }

            // main title
            position = new Point(Game1.ResX / 3 + 100, 100);

            color = new(255, 80, 0);
            color = new(90, 0, 40);
            sb.DrawString(TextManager.blazed, text, position.ToVector2(), color,
                    rotation, origin, scale, SpriteEffects.None, 0f);

        }

      

    }
}