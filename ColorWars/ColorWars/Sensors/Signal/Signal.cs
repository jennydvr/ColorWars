using Microsoft.Xna.Framework;

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

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID of the node this signal belongs to</param>
        /// <param name="intensity">Intensity of the signal</param>
        /// <param name="origin">Origin of this signal</param>
        public Signal(int id, float intensity, Node origin)
        {
            this.id = id;
            this.intensity = intensity;
            this.startInterval = 500 * (1 - intensity);
            this.endInterval = MathHelper.Min(maxtime, maxtime * intensity * 3.5f);
            this.origin = origin;
            this.activated = false;
            this.timer = 0;
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the time of this signal
        /// </summary>
        /// <param name="time">Elapsed game time</param>
        public void Update(GameTime time)
        {
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
                }
            }
        }

        #endregion
    }
}