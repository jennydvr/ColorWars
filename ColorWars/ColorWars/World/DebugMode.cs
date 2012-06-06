using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gearset;

namespace ColorWars
{
    class DebugMode
    {
        #region Items of the game

        // Obstacles of the game
        Background background;
        List<Obstacle> obstacles;

        // Characters
        static public Player dotty;

        static public List<EnemyDebug> squorres;

        #endregion

        #region Initialization

        public DebugMode()
        {
            // Initialize stuff
            background = new Background();
            obstacles = new List<Obstacle>();
            dotty = new Player();

            squorres = new List<EnemyDebug>();
            squorres.Add(new EnemyDebug(500, 500));
        /*    squorres.Add(new EnemyDebug(500, 300));
            squorres.Add(new EnemyDebug(200, 500));
            squorres.Add(new EnemyDebug(200, 400));
            squorres.Add(new EnemyDebug(500, 200));
            squorres.Add(new EnemyDebug(600, 600));*/

            // Add some obstacles to the list
            obstacles.Add(new Obstacle(200, 200));
          //  obstacles.Add(new Obstacle(600, 400));
        }

        public void LoadContent(ContentManager content)
        {
            // Load all the contents of the characters
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.LoadContent(content);
            }

            dotty.LoadContent(content);

            foreach (EnemyDebug squorre in squorres)
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
            
            foreach (EnemyDebug squorre in squorres)
                squorre.Update(time, dotty.kinematic.Clone());
        }

        public void Draw(SpriteBatch batch)
        {
            // Draw obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Draw(batch);
            }

            // Draw paintballs
            foreach (Paintball ball in CollisionDetector.balls)
            {
                ball.Draw(batch);
            }

            // Show dotty and his debug stuff
            dotty.Draw(batch);

            foreach (EnemyDebug squorre in squorres)
                squorre.Draw(batch);
        }

        #endregion

        #region Auxiliar Methods

        protected bool NotActivated(Paintball ball) {
            return !ball.activated;
        }

        #endregion
    }
}