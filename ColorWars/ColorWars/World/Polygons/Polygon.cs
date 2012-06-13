using System.Collections.Generic;
using Gearset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Polygon
    {
        #region Variables

        /// <summary>
        /// List of nodes
        /// </summary>
        public List<Vector2> nodes;

        /// <summary>
        /// Center of the polygon
        /// </summary>
        public Vector2 center;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Polygon(List<Vector2> nodes)
        {
            this.nodes = new List<Vector2>();
            this.center = Vector2.Zero;

            foreach (Vector2 node in nodes)
            {
                this.nodes.Add(new Vector2(node.X, node.Y));
                this.center += new Vector2(node.X, node.Y);
            }

            this.center /= this.nodes.Count;
        }

        #endregion

        #region Graph

        public bool Contains(Vector2 node)
        {
            Vector2 p1 = node;
            Vector2 p2 = node + 500 * Vector2.UnitX;
            int count = 0;

            nodes.Add(nodes[0]);

            for (int i = 0; i != nodes.Count - 1; ++i)
            {
                Vector2 p3 = nodes[i];
                Vector2 p4 = nodes[i + 1];

                float d = (p1.X - p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X - p4.X);

                // If the denominator is zero the lines are parallel
                if (d == 0)
                    continue;

                float ua = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X)) / d;
                float ub = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X)) / d;

                // The intersection point should be within the two segments
                if (0 <= ub && ub <= 1 && 0 <= ua && ua <= 1)
                    ++count;
            }

            nodes.RemoveAt(nodes.Count - 1);

            return count % 2 != 0;
        }

        static public bool AreAdjacent(Polygon first, Polygon second)
        {
            int counter = 0;

            foreach (Vector2 point in first.nodes)
            {
                if (second.nodes.Contains(point))
                {
                    ++counter;

                    if (counter == 2)
                        break;
                }
            }

            return counter == 2;
        }

        #endregion

        #region Output

        /// <summary>
        /// Draws the polygon
        /// </summary>
        /// <param name="batch">Sprite batch</param>
        /// <param name="id">Id of the polygon</param>
        /// <param name="content">Content manager</param>
        public void Draw(SpriteBatch batch, int id, ContentManager content)
        {
            Texture2D cuadro = content.Load<Texture2D>("cuadrito");

            // Draws the line
            for (int i = 0; i != nodes.Count - 1; ++i)
                GS.ShowLine("P" + id + i, nodes[i], nodes[i + 1], Color.Red);

            GS.ShowLine("P" + id + nodes.Count, nodes[nodes.Count - 1], nodes[0], Color.Red);
        }

        #endregion
    }
}