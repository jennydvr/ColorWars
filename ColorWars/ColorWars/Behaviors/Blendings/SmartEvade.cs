using Microsoft.Xna.Framework;
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
            BehaviorAndWeight b = new BehaviorAndWeight(new ObstacleAvoidance(), 0.5f);
            BehaviorAndWeight c = new BehaviorAndWeight(new FriendsAvoidance(owner), 2);

            FollowPath f = new FollowPath();
            f.heuristic = 'c';
            BehaviorAndWeight d = new BehaviorAndWeight(f, 1.5f);

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

            // Choose the waypoint with the highest value
            float max = 0;
            Kinematic w = new Kinematic();

            foreach (Waypoint waypoint in GameMode.waypoints)
            {
                float val = waypoint.Value(GameMode.dotty.kinematic, character);

                if (val > max)
                {
                    max = val;
                    w = waypoint.kinematic.Clone();
                }
            }

            Gearset.GS.ShowMark("waypoint", new Microsoft.Xna.Framework.Vector2(w.position.X, w.position.Y));

            // The evade needs a target
            blending.behaviors[3].behavior.Initialize(character, w);
        }

        #endregion
    }
}