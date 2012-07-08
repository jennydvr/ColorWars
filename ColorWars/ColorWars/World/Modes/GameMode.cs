using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        static public ContentManager content;
        static public Random random = new Random();

        // Stuff for the graphs
        static public List<Polygon> polygons = new List<Polygon>();
        static public Graph movement = new Graph();
        static public Graph smells = new Graph();

        // Timer for thinner balls
        protected float timer = 0;
        protected float interval = 10000;

        #endregion

        #region Initialization

        public GameMode(ContentManager c)
        {
            // Initialize stuff
            background = new Background();
            obstacles = new List<Obstacle>();
            squorres = new List<Enemy>();

            content = c;
        }

        public virtual void LoadContent()
        {
            ReadXML();

            // Finally create the movement graph
            movement.PolygonsInitialize();

            // Now the smell graph
            smells.FileInitialize("\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\olor.xml");

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

            // Update player
            dotty.Update(time);

            // Update enemies
            foreach (Enemy squorre in squorres)
                squorre.Update(time, dotty.kinematic.Clone());

            // Update thinner balls
            foreach (Thinner ball in CollisionDetector.thinners)
                ball.Update(time);
            CollisionDetector.thinners.RemoveAll(NotActivated);

            // Put a thinner ball
            AddThinnerball(time);

            // Update the signals in the smells graph
            smells.Update(time);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            // Draw obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.Draw(batch);

            // Draw paintballs
            foreach (Paintball ball in CollisionDetector.balls)
                ball.Draw(batch);

            // Draw thinner balls
            foreach (Thinner ball in CollisionDetector.thinners)
                ball.Draw(batch);

            // Draw dotty
            dotty.Draw(batch);

            // Draw enemies
            foreach (Enemy squorre in squorres)
                squorre.Draw(batch);


            // Draw signals
            for (int i = 0; i != smells.nodes.Count; ++i)
                for (int j = 0; j != smells.nodes[i].signals.Count; ++j)
                    if (smells.nodes[i].signals[j].activated)
                        smells.nodes[i].signals[j].Draw(content, batch);

            // Draw graph and polygons
           // for (int i = 0; i != polygons.Count; ++i)
           //     polygons[i].Draw(batch, i, content);

           // movement.Draw(content, batch);
           // smells.Draw(content, batch);
        }

        #endregion

        #region Auxiliar methods
        
        protected bool NotActivated(Paintball ball)
        {
            return !ball.activated;
        }

        protected bool NotActivated(Thinner ball)
        {
            return !ball.activated;
        }

        public virtual void AddEnemy(float x, float y)
        {
            squorres.Add(new Enemy(x, y));
        }

        protected void AddThinnerball(GameTime time)
        {
            timer += (float)time.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                // Put a thinner ball
                CollisionDetector.thinners.Add(new Thinner(content));
                timer = 0;
            }
        }

        #endregion
    }
}