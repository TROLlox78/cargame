using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Button
    {
        public Point position {  get; set; }
        public Point size { get; set; }
        public Point cornerPos { get; set; }

        public static Text text;
        public Rectangle hitbox
        {
            get
            {
                return new Rectangle(position, size);
            }
        }
        public bool hovered { get; set; }

        public Button() { }
        public Button(Point position, Point size, string text) { }


    }
}
