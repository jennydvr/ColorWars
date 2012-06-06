namespace ColorWars
{
    class CarefullyPursue : BlendingBehavior
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CarefullyPursue(Character owner)
            : base()
        {
            // Create the blending behaviors
            BehaviorAndWeight a = new BehaviorAndWeight(new LookWhereYoureGoing(), 1);
            BehaviorAndWeight b = new BehaviorAndWeight(new Pursue(), 1);
            BehaviorAndWeight c = new BehaviorAndWeight(new AvoidBullets(), 3);
            BehaviorAndWeight d = new BehaviorAndWeight(new ObstacleAvoidance(), 1.75f);
            BehaviorAndWeight e = new BehaviorAndWeight(new FriendsAvoidance(owner), 1.75f);
            BehaviorAndWeight f = new BehaviorAndWeight(new Arrive(), 1);

            // Add them to the list
            blending.behaviors.Add(a);
            blending.behaviors.Add(b);
            blending.behaviors.Add(c);
            blending.behaviors.Add(d);
            blending.behaviors.Add(e);
            blending.behaviors.Add(f);
        }
        
        #endregion

        #region Methods

        protected override void Update()
        {
            base.Update();
            
            // The pursue needs a target
            blending.behaviors[1].behavior.Initialize(character, target);
            blending.behaviors[5].behavior.Initialize(character, target);
        }

        #endregion
    }
}
