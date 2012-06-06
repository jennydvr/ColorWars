namespace ColorWars
{
    class BlendingBehavior : SteeringBehavior
    {
        #region Variables

        /// <summary>
        /// Saves the behaviors
        /// </summary>
        protected BlendedSteering blending;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public BlendingBehavior()
        {
            // Initialize the blending
            blending = new BlendedSteering();
        }

        public override SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            this.character = character;
            this.target = target;

            return GetSteering();
        }

        /// <summary>
        /// Updates the behaviors in the blending
        /// </summary>
        protected virtual void Update()
        {
            // Update all behaviors by giving them a character and an empty target
            foreach (BehaviorAndWeight behavior in blending.behaviors)
                behavior.behavior.Initialize(character, new Kinematic());
        }

        protected override SteeringOutput SteeringGenerator()
        {
            // First, update the behaviors in the blending
            Update();

            // Now return the steering
            return blending.GetSteering();
        }

        #endregion
    }
}