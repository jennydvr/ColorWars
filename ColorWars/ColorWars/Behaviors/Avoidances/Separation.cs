using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gearset;

namespace ColorWars
{
    /// <summary>
    /// Keeps the characters from getting too close
    /// Works good when the targets are moving in roughly the same direction
    /// </summary>
    class Separation : SteeringBehavior
    {
        #region Variables

        /// <summary>
        /// Potential targets to avoid
        /// </summary>
        public List<Kinematic> targets = new List<Kinematic>();

        /// <summary>
        /// Owner of this behavior
        /// </summary>
        public Character owner;

        #endregion

        #region Constants

        /// <summary>
        /// Distance in which this behavior can act
        /// </summary>
        protected float threshold = 150;

        /// <summary>
        /// Coefficient that graduates the repulsion force of the targets
        /// </summary>
        protected float decayCoefficient = 500;

        /// <summary>
        /// Maximum acceleration
        /// </summary>
        protected float maxAcceleration = 150;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">The character who owns this behavior</param>
        public Separation(Character owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Updates the targets list
        /// </summary>
        protected void Update()
        {
            foreach (Enemy squorre in DebugMode.squorres)
                if (squorre != owner)
                    targets.Add(squorre.kinematic.Clone());
        }

        protected override SteeringOutput SteeringGenerator()
        {
            SteeringOutput steering = new SteeringOutput();

            // First, update the targets list
            Update();

            foreach (Kinematic t in targets)
            {
                // Check if the target is close
                Vector3 direction = character.position - t.position;
                float distance = direction.Length();

                if (distance < threshold)
                {
                    // Calculate strength of repulsion
                    float strength = Math.Min(decayCoefficient / (distance * distance), maxAcceleration);

                    // Add acceleration
                    direction.Normalize();
                    steering.linear += strength * direction;
                }
            }

            if (DEBUG)
                Debug(steering);

            return steering;
        }

        protected void Debug(SteeringOutput steering)
        {
            Vector2 position = new Vector2(character.position.X, character.position.Y);
            GS.ShowVector2("Separation", position, new Vector2(steering.linear.X, steering.linear.Y), Color.Violet);
        }

        #endregion
    }
}