using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Graph
    {
        #region Variables

        public List<Node> nodes;
        public float[,] arcs;

        #endregion

        #region Constructor

        /// <summary>
        /// Builds the graph according to the polygons in the game
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();

            // Fill the nodes
            for (int i = 0; i != GameMode.polygons.Count; ++i)
                this.nodes.Add(new Node(GameMode.polygons[i].center, i));

            // Find the arcs
            arcs = new float[nodes.Count, nodes.Count];

            for (int i = 0; i != GameMode.polygons.Count; ++i)
            {
                // Choose a polygon
                Polygon first = GameMode.polygons[i];
                arcs[i, i] = float.PositiveInfinity;

                for (int j = i + 1; j < GameMode.polygons.Count; ++j)
                {
                    // Choose another polygon and add a connection
                    Polygon second = GameMode.polygons[j];

                    if (Polygon.AreAdjacent(first, second))
                        arcs[i, j] = arcs[j, i] = Vector2.Distance(first.center, second.center);
                    else
                        arcs[i, j] = arcs[j, i] = float.PositiveInfinity;
                }
            }
        }

        #endregion

        #region Drawing

        public void Draw(ContentManager content, SpriteBatch batch)
        {
            // Draw nodes
            foreach (Node node in nodes)
                node.Draw(content, batch);
            
            // Draw connections if the cost is empty
            for (int i = 0; i != nodes.Count; ++i)
                for (int j = i + 1; j < nodes.Count; ++j)
                    if (!float.IsPositiveInfinity(arcs[i,j]))
                        Gearset.GS.ShowLine("a" + i + j, nodes[i].point, nodes[j].point, Color.Green);
        }

        #endregion
    }
}