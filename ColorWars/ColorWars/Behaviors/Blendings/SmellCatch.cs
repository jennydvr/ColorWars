using Microsoft.Xna.Framework;

namespace ColorWars
{
    class SmellCatch : BlendingBehavior
    {
        #region Variables

        /// <summary>
        /// Owner of this behavior
        /// </summary>
        Character owner;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SmellCatch(Character owner)
            : base()
        {
            this.owner = owner;

            // Create the blending behaviors
            BehaviorAndWeight a = new BehaviorAndWeight(new LookWhereYoureGoing(), 1);
            BehaviorAndWeight b = new BehaviorAndWeight(new FollowPath(), 1.5f);
            BehaviorAndWeight c = new BehaviorAndWeight(new ObstacleAvoidance(), 0.5f);
            BehaviorAndWeight d = new BehaviorAndWeight(new FriendsAvoidance(owner), 2);

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
            
            // The follow needs a target
            Kinematic kinematic = new Kinematic();
            kinematic.position = new Vector3(owner.sensors[0].origin.point.X, owner.sensors[0].origin.point.Y, 0);

            blending.behaviors[1].behavior.Initialize(kinematic, target);
        }

        #endregion
    }
}
