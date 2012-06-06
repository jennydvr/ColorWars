namespace ColorWars
{
    /// <summary>
    /// Defines the wander for groups
    /// </summary>
    class GroupWander : SteeringBehavior
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
        /// <param name="owner">The character owner of this behavior</param>
        public GroupWander(Character owner)
        {
            // Initialize the blending
            blending = new BlendedSteering();

            // Add behaviors
            BehaviorAndWeight a = new BehaviorAndWeight(new LookWhereYoureGoing(), 1);
            BehaviorAndWeight b = new BehaviorAndWeight(new Wander(), 1);
            BehaviorAndWeight c = new BehaviorAndWeight(new ObstacleAvoidance(), 2);
            BehaviorAndWeight d = new BehaviorAndWeight(new Separation(owner), 1.5f);
            BehaviorAndWeight e = new BehaviorAndWeight(new FriendsAvoidance(owner), 1.5f);

            blending.behaviors.Add(a);
            blending.behaviors.Add(b);
            blending.behaviors.Add(c);
            blending.behaviors.Add(d);
            blending.behaviors.Add(e);
        }

        /// <summary>
        /// Updates the behaviors in the blending
        /// </summary>
        protected void Update()
        {
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