namespace ColorWars
{
    class SmartEvade : BlendingBehavior
    {
        #region Variables

        /// <summary>
        /// Owner of this behavior
        /// </summary>
        protected Character owner;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmartEvade(Character owner)
            : base()
        {
            this.owner = owner;

            // Create the blending behaviors
            BehaviorAndWeight a = new BehaviorAndWeight(new LookWhereYoureGoing(), 1);
            BehaviorAndWeight b = new BehaviorAndWeight(new Evade(), 1);
            BehaviorAndWeight c = new BehaviorAndWeight(new ObstacleAvoidance(), 1.75f);
            BehaviorAndWeight d = new BehaviorAndWeight(new FriendsAvoidance(owner), 1.75f);

            // Add them to the list
            blending.behaviors.Add(a);
            blending.behaviors.Add(b);
            blending.behaviors.Add(c);
            blending.behaviors.Add(d);
        }

        #endregion

        #region Methods

        protected override void Update()
        {
            base.Update();

            // The evade needs a target
            blending.behaviors[1].behavior.Initialize(character, target);
        }

        #endregion
    }
}