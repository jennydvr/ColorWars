using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        static SpriteFont font;
        static int score = 0;
        static int state = 0;

        // Stuff for the graphs
        static public List<Polygon> polygons = new List<Polygon>();
        static public List<Waypoint> waypoints = new List<Waypoint>();
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
            font = content.Load<SpriteFont>("Fonts/gameFont");

            ReadMovement();
            ReadWaypoints();

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

        public void ReadMovement()
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

        public void ReadWaypoints()
        {
            // The file:
            string filename = "\\\\psf\\Home\\Documents\\Lenguajes\\C#\\ColorWars\\ColorWars\\ColorWarsContent\\waypoints.xml";

            // Open the document
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            // waypoints tag
            XmlNodeList list = xDoc.GetElementsByTagName("waypoints");
            list = ((XmlElement)list[0]).GetElementsByTagName("waypoint");

            foreach (XmlElement nodo in list)
            {
                XmlNodeList lx = nodo.GetElementsByTagName("x");
                XmlNodeList ly = nodo.GetElementsByTagName("y");
                float x = System.Convert.ToSingle(lx[0].InnerText);
                float y = System.Convert.ToSingle(ly[0].InnerText);

                waypoints.Add(new Waypoint(new Vector2(x, y)));
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

            squorres.RemoveAll(Dead);

            // Update thinner balls
            foreach (Thinner ball in CollisionDetector.thinners)
                ball.Update(time);
            CollisionDetector.thinners.RemoveAll(NotActivated);

            // Put a thinner ball
            AddThinnerball(time);

            // Update the signals in the smells graph
            smells.Update(time);

            // Update what to show
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.D1))
                state = 1;  // Show movement graph and waypoints
            else if (keyboard.IsKeyDown(Keys.D2))
                state = 2;  // Show smell graph
            else if (keyboard.IsKeyDown(Keys.D0))
                state = 0;  // Show nothing
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

            if (state == 1)
            {
                int id = 0;
                foreach (Waypoint w in waypoints)
                    w.Draw(content, batch, ++id);

                movement.Draw(content, batch);
            }
            else if (state == 2)
            {
                smells.Draw(content, batch);
            }

            // Write score
            int s = 0;
            foreach (Enemy enemy in squorres)
                s += 100 - (int)enemy.life;

            score = (int) MathHelper.Max(s, score);
            batch.DrawString(font, "Score: " + score, new Vector2(800, 50), Color.Black);
        }

        #endregion

        #region Auxiliar methods

        protected bool Dead(Character character)
        {
            return character.spriteColor.A <= 0;
        }

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