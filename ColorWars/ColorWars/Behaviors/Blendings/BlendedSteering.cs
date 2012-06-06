using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gearset;

namespace ColorWars
{
    /// <summary>
    /// Calculates the resulting steering from a group of behaviors
    /// </summary>
    class BlendedSteering
    {
        #region Variables

        /// <summary>
        /// Holds a list of BehaviorAndWeight instances
        /// </summary>
        public List<BehaviorAndWeight> behaviors = new List<BehaviorAndWeight>();

        #endregion

        #region Constants

        /// <summary>
        /// The maximum acceleration allowed
        /// </summary>
        protected const float maxAcceleration = 300;

        /// <summary>
        /// The maximum rotation allowed
        /// </summary>
        protected const float maxRotation = 100;

        #endregion

        #region Methods

        public SteeringOutput GetSteering()
        {
            SteeringOutput steering = new SteeringOutput();

            // Accumulate all accelerations
            foreach (BehaviorAndWeight behavior in behaviors)
                steering += behavior.weight * behavior.behavior.GetSteering();

            // Crop the result and return
            if (steering.linear.Length() > maxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= maxAcceleration;
            }

            steering.angular = Math.Min(steering.angular, maxRotation);

            return steering;
        }

        #endregion
    }
}