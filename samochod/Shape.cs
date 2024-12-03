using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    
    public class Shape
    {
        private class Edge
        {
            Vector2 x, y;
        }
        // points have to be added in a clockwise manner
        public Vector2 position;
        public List<Vector2> points; // points in space
        public float angle = 0;

        public void rot()
        {
            // should be normalized to zero
            for (int i = 0; i < points.Count; i++) 
            {
                float px = position.X;
                float py = position.Y;

                float x = points[i].X -px;
                float y = points[i].Y -py;
                float newX = (float)(x*Math.Cos(angle) - y*Math.Sin(angle));
                float newY = (float)(x*Math.Sin(angle) + y*Math.Cos(angle));
                points[i] = new Vector2(newX+px, newY+py);
            }
        }

        public Shape(Vector2 pos, Vector2 offset, float scale, float ang, Rectangle texture)
        {// rectangle shape based on texture
         // scale not taken into account, this will cause a bug in the future
            position = pos;
            angle += ang;
            int width = texture.Width/2;
            int height = texture.Height/2;
            float pX = (pos.X - offset.X);
            float pY = (pos.Y - offset.Y);
            points = new List<Vector2>(4);
            // bottom left
            points.Add(new Vector2(pX - width, pY - height));
            // top left
            points.Add(new Vector2(pX - width, pY + height));
            // top right
            points.Add(new Vector2(pX + width, pY + height));
            // bottom right
            points.Add(new Vector2(pX + width, pY - height));
            rot();// rotate the points after creation
        }

        public bool lineIntersect(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {

            float denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            if (denominator == 0) { return false; }

            float ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / denominator;
            float ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / denominator;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1)
            {
                return false;
            }

            return true;
        }
        public bool Intersects(Shape other) 
        {
            for (int i = 0; i < points.Count-1; i++)
            {
                for (int j = 0; j < other.points.Count - 1; j++)
                {   // edge testing
                    if (lineIntersect(
                        points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, 
                        other.points[i].X, other.points[i].Y,
                        other.points[i+1].X, other.points[i + 1].Y
                        ))
                        {
                        return true;
                        }
                        }
            }
            return false;
        }
    }
}
