using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorWars
{
    /// <summary>
    /// Implements the velocity match behavior
    /// </summary>
    class VelocityMatch : SteeringBehavior
    {
        #region Constants

        protected const float maxAcceleration = 150;

        protected const float timeToTarget = 0.1f;

        #endregion Constants

        #region Methods

        protected override SteeringOutput SteeringGenerator()
        {
            SteeringOutput steering = new SteeringOutput();

            // Try to get to the target velocity
            steering.linear = target.velocity - character.velocity;
            steering.linear /= timeToTarget;

            // Check if acceleration is too fast
            if (steering.linear.Length() > maxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= maxAcceleration;
            }

            steering.angular = 0;
            return steering;
        }

        #endregion Methods
    }
}