namespace ColorWars
{
    /// <summary>
    /// Parent class for all the steering behaviors of the game.
    /// </summary>
    abstract class SteeringBehavior
    {
        #region Flags

        public static bool DEBUG = false;

        #endregion

        #region Variables

        /// <summary>
        /// The kinematic data for the character
        /// </summary>
        public Kinematic character;
         
        /// <summary>
        /// The kinematic data for the target
        /// </summary>
        public Kinematic target;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the parameters for the steering generation
        /// </summary>
        /// <param name="character">Kinematic of the character</param>
        /// <param name="target">Kinematic of the target</param>
        public void Initialize(Kinematic character, Kinematic target)
        {
            this.character = character;
            this.target = target;
        }

        /// <summary>
        /// Gives back the resulting steering
        /// </summary>
        /// <returns>The resulting steering of the behavior</returns>
        public virtual SteeringOutput GetSteering()
        {
            return SteeringGenerator();
        }

        /// <summary>
        /// Initializes the parameters for the steering generation and gives back the resulting steering
        /// </summary>
        /// <param name="character">Kinematic of the character</param>
        /// <param name="target">Kinematic of the target</param>
        /// <returns>The resulting steering of the behavior</returns>
        public virtual SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            this.character = character.Clone();
            this.target = target.Clone();

            return GetSteering();
        }

        /// <summary>
        /// Applies the algorithm to calculate the steering of the given behavior.
        /// </summary>
        /// <returns>The resulting steering of the behavior</returns>
        abstract protected SteeringOutput SteeringGenerator();

        #endregion
    }
}