using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace samochod
{
    public class Text
    {
        public Vector2 position;
        public string text;
        public float scale = 1;
        public Color color = Color.Black;
        private static int x = 10, y = 900;
        public static void reset()
        {
            x = 10; y = 800;
        }
        public Text(Vector2 pos, string text)
        {
            position = pos;
            this.text = text;
        }
        public Text(Vector2 pos, string text, float scale)
        {
            position = pos;
            this.text = text;
            this.scale = scale;
        }
        public Text( string text)
        {
            // debug only
            position = new Vector2(x,y);
            y -= 20;
            this.text = text;
        }
        public void Update(string text)
        {
            this.text = text;
        }
    }
    public  class TextManager
    {
        string mesage = "WSAD to move\nSpace to shift gears";
        public Text hintText;
        public SpriteFont blazed, fipps;
        public  List<Text> texts;
        public TextManager()
        {
            texts = new List<Text>();
            hintText = new(new Vector2(Game1.ResX/2 - 100,Game1.ResY*0.91f),"");
    }
        public void Write(Text text)
        {// don't pass a text object next time..
            texts.Add(text);
        }
        
        

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw hitnt
            spriteBatch.DrawString(fipps, hintText.text, hintText.position, hintText.color,
            0, Vector2.Zero, hintText.scale, SpriteEffects.None, 0f);

            foreach (Text text in texts)
            {
                spriteBatch.DrawString(fipps, text.text, text.position, text.color,
                    0,Vector2.Zero,text.scale,SpriteEffects.None,0f);
            }
            texts.Clear();
            Text.reset();
        }

    }
}
