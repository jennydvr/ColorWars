using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Particle
    {
        #region Constants

        protected const string ASSET = "ball";
        protected const int WIDTH = 5;
        protected const int HEIGHT = 5;
        protected int MAX_SPEED = 50;

        #endregion

        #region Variables

        /// <summary>
        /// Origin of the signal
        /// </summary>
        public Kinematic origin = new Kinematic();

        /// <summary>
        /// Target node of the signal
        /// </summary>
        public Kinematic target = new Kinematic();

        /// <summary>
        /// Behavior
        /// </summary>
        protected Seek seek;

        /// <summary>
        /// True if the particle has reached the target
        /// </summary>
        public bool done;

        #endregion

        #region Constructors

        public Particle(Signal signal)
        {
            this.done = false;

            // Init the seek and flee
            seek = new Seek();
            seek.endingRadius = 0;

            // Initialize the origin and target
            origin.position = new Vector3(signal.origin.point.X, signal.origin.point.Y, 0);
            target.position = new Vector3(signal.location.X, signal.location.Y, 0);

            // Now move the origin a little
            int alpha = GameMode.random.Next(10, 50);
            int beta = GameMode.random.Next(10, 50);

            float x = (float)GameMode.random.NextDouble() * 2 * alpha - alpha;
            float y = (float)GameMode.random.NextDouble() * 2 * beta - beta;

            origin.position += new Vector3(x, y, 0);

            // And now the target
            alpha = GameMode.random.Next(0, 100);
            beta = GameMode.random.Next(0, 100);

            x = (float)GameMode.random.NextDouble() * 2 * alpha - alpha;
            y = (float)GameMode.random.NextDouble() * 2 * beta - beta;

            target.position += new Vector3(x, y, 0);
        }

        #endregion

        #region Methods

        public void Update(GameTime time)
        {
            origin.Update(seek.GetSteering(origin, target), MAX_SPEED, time);

            // If the target was reached, stop your movement
            if ((origin.position - target.position).Length() <= 10)
                done = true;
        }

        /// <summary>
        /// Draws the propagation of a signal
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="batch">Sprite batch</param>
        public void Draw(ContentManager content, SpriteBatch batch)
        {
            Texture2D texture = content.Load<Texture2D>(ASSET);
            Rectangle dest = new Rectangle((int)origin.position.X, (int)origin.position.Y, 1, 1);
            batch.Draw(texture, dest, Color.White);
        }

        #endregion
    }
}