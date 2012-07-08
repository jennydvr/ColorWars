using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ColorWars
{
    class Signal
    {
        #region Constants

        /// <summary>
        /// Maximum time for a signal
        /// </summary>
        static public float maxtime = 10000;

        #endregion

        #region Variables

        /// <summary>
        /// Graph that follows this signal, according to the sensor it activates
        /// </summary>
        protected Graph graph;

        /// <summary>
        /// ID of the node this signal belongs to
        /// </summary>
        protected int id;

        /// <summary>
        /// Parent node of this signal
        /// </summary>
        public Node origin;

        /// <summary>
        /// Location of this signal
        /// </summary>
        public Vector2 location;

        /// <summary>
        /// Intensity of the signal
        /// </summary>
        public float intensity;

        /// <summary>
        /// Factor to propagate the signal
        /// </summary>
        protected float factor;

        /// <summary>
        /// Timer for this signal
        /// </summary>
        public float timer;

        /// <summary>
        /// Time in that the signal shall begin
        /// </summary>
        public float startInterval;       
        
        /// <summary>
        /// Time in that the signal shall end
        /// </summary>
        public float endInterval;

        /// <summary>
        /// Checks whether this signal is activated or not
        /// </summary>
        public bool activated;

        /// <summary>
        /// List of points to draw, surrounding the signal
        /// </summary>
        public List<Particle> points = new List<Particle>();

        /// <summary>
        /// Order of calling of the signal
        /// </summary>
        protected int order;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID of the node this signal belongs to</param>
        /// <param name="intensity">Intensity of the signal</param>
        /// <param name="origin">Origin of this signal</param>
        /// <param name="order">When was this parameter called</param>
        public Signal(int id, float intensity, Node origin, int order)
        {
            // Set propagation stuff
            this.id = id;
            this.intensity = intensity;
            this.activated = false;
            this.order = order;
            this.origin = origin;

            // Set timers
            this.startInterval = 50 * order * (1 - intensity);
            this.endInterval = MathHelper.Min(maxtime, maxtime * intensity * 3.5f);
            this.timer = 0;
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the time of this signal
        /// </summary>
        /// <param name="time">Elapsed game time</param>
        public void Update(GameTime time)
        {
            // Only update if the parent signal is still alive, else, kill this signal
            if (origin.signals.Count > 0)
            {
                // Update timer
                timer += (float)time.ElapsedGameTime.TotalMilliseconds;

                if (startInterval != -1)
                {
                    // Add the signal
                    if (timer > startInterval)
                    {
                        activated = true;
                        startInterval = -1;
                        timer = 0;
                    }
                }
                else if (endInterval != -1)
                {
                    // Remove the signal
                    if (timer > endInterval)
                    {
                        activated = false;
                        endInterval = -1;
                        timer = 0;

                        // Remove particles
                        points.Clear();
                    }
                }
            }
            else
            {
                activated = false;
                endInterval = -1;
                timer = 0;

                // Remove particles
                points.Clear();
            }

            // Update the particles
            if (activated)
            {
                // Add a little more
                for (int i = 0; i != (int)(10 * intensity); ++i)
                    points.Add(new Particle(this));

                // Update each
                foreach (Particle point in points)
                    point.Update(time);

                // Remove dead-particles
                points.RemoveAll(NotActivated);
            }
        }

        /// <summary>
        /// Draws the propagation of a signal
        /// </summary>
        /// <param name="content">Content manager</param>
        /// <param name="batch">Sprite batch</param>
        public void Draw(ContentManager content, SpriteBatch batch)
        {
            foreach (Particle point in points)
                point.Draw(content, batch);
        }

        #endregion

        #region Auxiliars

        /// <summary>
        /// Checks if a particle is activated
        /// </summary>
        /// <param name="particle">Particle</param>
        /// <returns>True if activated, false otherwise</returns>
        protected bool NotActivated(Particle particle)
        {
            return particle.done;
        }

        #endregion
    }
}