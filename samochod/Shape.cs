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
            Point x, y;
        }
        // points have to be added in a clockwise manner
        public List<Point> points;
        List<Edge> edges;
        
        public Shape(Vector2 pos, Vector2 org, float scale, float rot, Texture2D texture)
        {// rectangle shape based on texture
        // scale not taken into account, this will cause a bug in the future
            int width = texture.Width;
            int height = texture.Height;
            int pX = (int)(pos.X - org.X);
            int pY = (int)(pos.Y - org.Y);
            points = new List<Point>(4);
            // bottom left
            points.Add(new Point(pX - width, pY - height));
            // top left
            points.Add(new Point(pX - width, pY + height));
            // top right
            points.Add(new Point(pX + width, pY + height));
            // bottom right
            points.Add(new Point(pX + width, pY - height));
            

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
        public void Intersects(Shape other) 
        {
            for (int i = 0; i < points.Count-1; i++)
            {
                for (int j = 0; j < other.points.Count - 1; j++)
                {   // edging test
                    if (lineIntersect(
                        points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y, 
                        other.points[i].X, other.points[i].Y,
                        other.points[i+1].X, other.points[i + 1].Y
                        ))
                    {
                        Debug.WriteLine("true");
                    }
                        }
            }
        }
    }
}
