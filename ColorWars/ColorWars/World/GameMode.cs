using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class GameMode
    {
        #region Items of the game

        // Obstacles of the game
        Background background;
        List<Obstacle> obstacles;

        // Characters
        static public Player dotty;
        static public List<Enemy> squorres;

        #endregion

        #region Initialization

        public GameMode()
        {
            // Initialize stuff
            background = new Background();
            obstacles = new List<Obstacle>();
            dotty = new Player();

            squorres = new List<Enemy>();
            squorres.Add(new Enemy(500, 500));
            squorres.Add(new Enemy(600, 600));

            // Add some obstacles to the list
            obstacles.Add(new Obstacle(200, 200));
            obstacles.Add(new Obstacle(600, 300));
            obstacles.Add(new Obstacle(300, 500));
        }

        public void LoadContent(ContentManager content)
        {
            // Load all the contents of the characters
            foreach (Obstacle obstacle in obstacles)
                obstacle.LoadContent(content);

            dotty.LoadContent(content);

            foreach (Enemy squorre in squorres)
                squorre.LoadContent(content);
        }

        #endregion

        #region Update and Drawing

        public void Update(GameTime time)
        {
            // Update paintballs
            foreach (Paintball ball in CollisionDetector.balls)
                ball.Update(time);

            CollisionDetector.balls.RemoveAll(NotActivated);

            dotty.Update(time);

            foreach (Enemy squorre in squorres)
                squorre.Update(time, dotty.kinematic.Clone());
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw obstacles
            foreach (Obstacle obstacle in obstacles)
                obstacle.Draw(batch);

            // Draw paintballs
            foreach (Paintball ball in CollisionDetector.balls)
                ball.Draw(batch);

            // Show dotty and his debug stuff
            dotty.Draw(batch);

            foreach (Enemy squorre in squorres)
                squorre.Draw(batch);
        }

        #endregion

        #region Auxiliar Methods

        protected bool NotActivated(Paintball ball)
        {
            return !ball.activated;
        }

        #endregion
    }
}