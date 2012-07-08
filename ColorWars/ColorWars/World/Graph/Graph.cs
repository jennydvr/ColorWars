using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Globalization;

namespace ColorWars
{
    class Graph
    {
        #region Variables

        /// <summary>
        /// Nodes list
        /// </summary>
        public List<Node> nodes;

        /// <summary>
        /// Matrix of distances
        /// </summary>
        public float[,] arcs;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Graph()
        {
            nodes = new List<Node>();
            arcs = new float[0, 0];
        }

        /// <summary>
        /// Builds the graph according to the polygons in the game
        /// </summary>
        public void PolygonsInitialize()
        {
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

        /// <summary>
        /// Builds the graph according to a filename
        /// </summary>
        /// <param name="filename">File</param>
        public void FileInitialize(string filename)
        {
            // First, fill the nodes
            for (int i = 0; i != GameMode.polygons.Count; ++i)
                this.nodes.Add(new Node(GameMode.polygons[i].center, i));

            // Fill the initial arcs
            arcs = new float[nodes.Count, nodes.Count];

            for (int i = 0; i != nodes.Count; ++i)
            {
                arcs[i, i] = float.PositiveInfinity;

                for (int j = i + 1; j < nodes.Count; ++j)
                    arcs[i, j] = arcs[j, i] = float.PositiveInfinity;
            }

            // Open the document
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            // graph tag
            XmlNodeList list = xDoc.GetElementsByTagName("graph");

            // arcs tag
            list = ((XmlElement)list[0]).GetElementsByTagName("arcs");
            list = ((XmlElement)list[0]).GetElementsByTagName("arc");

            foreach (XmlElement arc in list)
            {
                XmlNodeList lx = arc.GetElementsByTagName("i");
                XmlNodeList ly = arc.GetElementsByTagName("j");
                XmlNodeList lz = arc.GetElementsByTagName("cost");

                if (lz[0].InnerText == "INF")
                    continue;

                int x = System.Convert.ToInt32(lx[0].InnerText);
                int y = System.Convert.ToInt32(ly[0].InnerText);
                float z = (float)double.Parse(lz[0].InnerText, CultureInfo.InvariantCulture);

                arcs[x, y] = arcs[y, x] = z;
            }
        }

        #endregion

        #region Drawing

        /// <summary>
        /// Updates the signals in every node
        /// </summary>
        /// <param name="time">Elapsed game time</param>
        public void Update(GameTime time)
        {
            // Updates every node
            foreach (Node node in nodes)
                node.Update(time);
        }

        /// <summary>
        /// Draws the nodes and the arcs of this graph
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="batch">Sprite batch</param>
        public void Draw(ContentManager content, SpriteBatch batch)
        {
            // Draw nodes
            foreach (Node node in nodes)
                node.Draw(content, batch);
            
            // Draw connections if the cost is not empty
            for (int i = 0; i != nodes.Count; ++i)
                for (int j = i + 1; j < nodes.Count; ++j)
                    if (!float.IsPositiveInfinity(arcs[i,j]))
                        Gearset.GS.ShowLine("a" + i + j, nodes[i].point, nodes[j].point, Color.Green);
        }

        #endregion
    }
}