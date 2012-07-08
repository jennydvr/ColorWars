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
        protected int MAX_SPEED;

        #endregion

        #region Variables

        protected Kinematic origin = new Kinematic();
        protected Kinematic target = new Kinematic();
        protected Seek seek;
        protected Flee flee;
        protected Wander wander;

        #endregion

        #region Constructors

        public Particle(Signal signal)
        {
            // Init the seek and flee
            seek = new Seek();
            flee = new Flee();
            wander = new Wander();
            seek.endingRadius = 0;

            // Initialize the origin and target
            target.position = new Vector3(signal.location.X, signal.location.Y, 0);

            if (signal.origin == null)
                origin.position = target.position;
            else
                origin.position = new Vector3(signal.origin.point.X, signal.origin.point.Y, 0);

            // Now move the origin and the speed little
            int alpha = GameMode.random.Next(0, 100);
            int beta = GameMode.random.Next(0, 100);

            float x = (float)GameMode.random.NextDouble() * 2 * alpha - alpha;
            float y = (float)GameMode.random.NextDouble() * 2 * beta - beta;

            origin.position += new Vector3(x, y, 0);
            MAX_SPEED = GameMode.random.Next(30, 50);
        }

        #endregion

        #region Methods

        public void Update(GameTime time)
        {
            // If the target was reached, stop your movement
            if ((origin.position - target.position).Length() <= 10)
                origin.Update(flee.GetSteering(origin, target), MAX_SPEED, time);
            else if ((origin.position - target.position).Length() >= 25)
                origin.Update(seek.GetSteering(origin, target), MAX_SPEED, time);
            else
                origin.Update(wander.GetSteering(origin, new Kinematic()), MAX_SPEED, time);
        }

        /// <summary>
        /// Draws the propagation of a signal
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="batch">Sprite batch</param>
        public void Draw(ContentManager content, SpriteBatch batch)
        {
            Texture2D texture = content.Load<Texture2D>(ASSET);
            Rectangle dest = new Rectangle((int)origin.position.X, (int)origin.position.Y, 4, 4);
            batch.Draw(texture, dest, Color.White);
        }

        #endregion
    }
}