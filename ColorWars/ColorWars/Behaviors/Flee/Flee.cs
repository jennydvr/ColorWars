using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorWars
{
    /// <summary>
    /// Implements the flee behavior
    /// </summary>
    class Flee : SteeringBehavior
    {
        #region Constants

        /// <summary>
        /// The maximum acceleration of the character
        /// </summary>
        public float maxAcceleration = 150;

        #endregion

        #region Methods

        protected override SteeringOutput SteeringGenerator()
        {
            SteeringOutput steering = new SteeringOutput();

            // Get the direction to the target
            steering.linear = character.position - target.position;

            // Give full acceleration along this direction
            steering.linear.Normalize();
            steering.linear *= maxAcceleration;

            steering.angular = 0;
            return steering;
        }

        #endregion
    }
}