using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class GameMode
    {
        #region Items of the game

        // Obstacles of the game
        protected Background background;
        static public List<Obstacle> obstacles;

        // Characters
        static public Player dotty;
        static public List<Enemy> squorres;
       
        // Other stuff
        protected ContentManager content;

        // Stuff for the graph
        static public List<Polygon> polygons = new List<Polygon>();
        static public Graph graph = new Graph();

        #endregion

        #region Initialization

        public GameMode(ContentManager content)
        {
            // Initialize stuff
            background = new Background();
            obstacles = new List<Obstacle>();
            squorres = new List<Enemy>();

            this.content = content;

        }

        public virtual void LoadContent()
        {
            ReadXML();

            // Finally create the graph
            graph = new Graph();

            // Load obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.LoadContent(content);

            // Load player
            dotty.LoadContent(content);

            // Load enemies
            foreach (Enemy squorre in squorres)
                squorre.LoadContent(content);
        }

        public void ReadXML()
        {
            // The file:
            string filename = "\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\mapa.xml";

            // Open the document
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            // game tag
            XmlNodeList game = xDoc.GetElementsByTagName("game");

            // player tag
            XmlNodeList list = ((XmlElement)game[0]).GetElementsByTagName("player");
            XmlNodeList lx = ((XmlElement)list[0]).GetElementsByTagName("x");
            XmlNodeList ly = ((XmlElement)list[0]).GetElementsByTagName("y");
            float x = System.Convert.ToSingle(lx[0].InnerText);
            float y = System.Convert.ToSingle(ly[0].InnerText);

            dotty = new Player(x, y);

            // enemies tag
            list = ((XmlElement)game[0]).GetElementsByTagName("enemies");
            list = ((XmlElement)list[0]).GetElementsByTagName("enemy");

            foreach (XmlElement nodo in list)
            {
                lx = nodo.GetElementsByTagName("x");
                ly = nodo.GetElementsByTagName("y");
                x = System.Convert.ToSingle(lx[0].InnerText);
                y = System.Convert.ToSingle(ly[0].InnerText);
                
                AddEnemy(x, y);
            }

            // obstacles tag
            list = ((XmlElement)game[0]).GetElementsByTagName("obstacles");
            list = ((XmlElement)list[0]).GetElementsByTagName("obstacle");

            foreach (XmlElement node in list)
            {
                lx = node.GetElementsByTagName("x");
                ly = node.GetElementsByTagName("y");
                x = System.Convert.ToSingle(lx[0].InnerText);
                y = System.Convert.ToSingle(ly[0].InnerText);

                obstacles.Add(new Obstacle(x, y));
            }

            // polygons tag
            list = ((XmlElement)game[0]).GetElementsByTagName("polygons");
            list = ((XmlElement)list[0]).GetElementsByTagName("polygon");

            foreach (XmlElement polygon in list)
            {
                List<Vector2> nodes = new List<Vector2>();
                XmlNodeList lnodes = polygon.GetElementsByTagName("node");

                foreach (XmlElement node in lnodes)
                {
                    lx = node.GetElementsByTagName("x");
                    ly = node.GetElementsByTagName("y");
                    x = System.Convert.ToSingle(lx[0].InnerText);
                    y = System.Convert.ToSingle(ly[0].InnerText);

                    nodes.Add(new Vector2(x, y));
                }

                polygons.Add(new Polygon(nodes));
            }

        }

        #endregion

        #region Update and Draw

        public virtual void Update(GameTime time)
        {
            // Update paintballs
            foreach (Paintball ball in CollisionDetector.balls)
                ball.Update(time);

            CollisionDetector.balls.RemoveAll(NotActivated);

            dotty.Update(time);

            foreach (Enemy squorre in squorres)
                squorre.Update(time, dotty.kinematic.Clone());
        }

        public virtual void Draw(SpriteBatch batch)
        {
            // Draw obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.Draw(batch);

            // Draw paintballs
            foreach (Paintball ball in CollisionDetector.balls)
                ball.Draw(batch);

            // Draw dotty
            dotty.Draw(batch);

            // Draw enemies
            foreach (Enemy squorre in squorres)
                squorre.Draw(batch);

            // Draw graph and polygons
            for (int i = 0; i != polygons.Count; ++i)
                polygons[i].Draw(batch, i, content);

            graph.Draw(content, batch);
        }

        protected bool NotActivated(Paintball ball)
        {
            return !ball.activated;
        }

        #endregion

        #region Auxiliar methods

        public virtual void AddEnemy(float x, float y)
        {
            squorres.Add(new Enemy(x, y));
        }

        #endregion
    }
}