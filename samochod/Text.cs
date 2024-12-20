﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
    }
    public  class TextManager
    {
        public SpriteFont blazed, fipps;
        public  List<Text> texts;
        public TextManager()
        {
            texts = new List<Text>();
        }
        public void Write(Text text)
        {// don't pass a text object next time..
            texts.Add(text);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
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
