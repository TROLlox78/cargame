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
        public class Edge
        {
            public Vector2 p1, p2;
            public Edge (Vector2 x, Vector2 y)
            {
                this.p1 = x;
                this.p2 = y;
            }
        }
        // points have to be added in a clockwise manner
        public Vector2 position;
        public List<Vector2> points;    // points in space
        public List<Edge> edges;        // edges made from points
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


        public void makeEdges()
        {
           
            edges = new List<Edge>(points.Count);
            for (int i = 0; i < points.Count; i++)
            {
                edges.Add( new Edge(points[i], points[(i+1)%points.Count]));
            }
        }

        public Shape(Vector2 pos, Vector2 offset, float scale, float ang, int width, int height)
        {// rectangle shape based on texture
         // scale not taken into account, this will cause a bug in the future
            position = pos;
            angle += ang;
            //int width = texture.Width/2;
            //int height = texture.Height/2;
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
            makeEdges();
        }

        public bool lineIntersect(Edge e1, Edge e2)
        {
            ///
            /// algorithm taken from: https://paulbourke.net/geometry/pointlineplane/
            /// 

            // edge 1 line points
            float x1 = e1.p1.X;
            float y1 = e1.p1.Y;

            float x2 = e1.p2.X;
            float y2 = e1.p2.Y;
             // edge 2 line points
            float x3 = e2.p1.X;
            float y3 = e2.p1.Y;

            float x4 = e2.p2.X;
            float y4 = e2.p2.Y;

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
            for (int i = 0; i < edges.Count; i++)
            {
                for (int j = 0; j < other.edges.Count; j++)
                {   // edge testing
                    if (lineIntersect(edges[i], other.edges[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
