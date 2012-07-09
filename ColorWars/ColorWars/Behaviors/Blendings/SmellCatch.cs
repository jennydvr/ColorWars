using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            BehaviorAndWeight b = new BehaviorAndWeight(new ObstacleAvoidance(), 0.5f);
            BehaviorAndWeight c = new BehaviorAndWeight(new FriendsAvoidance(owner), 2);

            FollowPath f = new FollowPath();
            f.heuristic = 's';
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
            
            // The follow path needs a target
            Kinematic kinematic = owner.sensors[0].origin;

            Vector2 point = GetNearestNode(kinematic).point;
            kinematic.position = new Vector3(point.X, point.Y, 0);

            blending.behaviors[3].behavior.Initialize(character, kinematic);
        }

        #endregion

        #region Auxiliars

        /// <summary>
        /// Calculates the node of this character
        /// </summary>
        /// <param name="character">Kinematic of the character</param>
        /// <returns>The node of the character</returns>
        protected Node GetNearestNode(Kinematic character)
        {
            Vector2 pos = GetPolygon(character).center;

            foreach (Node node in GameMode.smells.nodes)
                if (pos == node.point)
                    return node;

            return new Node(Vector2.Zero, -1);
        }

        /// <summary>
        /// Calculates the character's polygon
        /// </summary>
        /// <param name="character">Kinematic of the character</param>
        /// <returns>The polygon of the character</returns>
        protected Polygon GetPolygon(Kinematic character)
        {
            Vector2 vect = new Vector2(character.position.X, character.position.Y);
            float min = float.PositiveInfinity;
            Polygon closest = new Polygon(new List<Vector2>());

            foreach (Polygon poly in GameMode.polygons)
                if (poly.Contains(vect))
                {
                    return poly;
                }
                else if ((poly.center - vect).Length() < min)
                {
                    min = (poly.center - vect).Length();
                    closest = poly;
                }

            return closest;
        }

        #endregion
    }
}
