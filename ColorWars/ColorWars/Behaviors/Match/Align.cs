using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Implements the align behavior
    /// IMPORTANT: All the constants are adjusted according to my eye...
    /// </summary>
    class Align : SteeringBehavior
    {
        #region Constants

        /// <summary>
        /// Maximum angular acceleration allowed
        /// </summary>
        protected const float maxAngularAcceleration = 25;

        /// <summary>
        /// Maximum rotation allowed
        /// </summary>
        protected const float maxRotation = (float)Math.PI * 0.75f;

        /// <summary>
        /// Radius for arriving at the target
        /// </summary>
        protected const float targetRadius = 0.05f;

        /// <summary>
        /// Radius for beginning to slow down
        /// </summary>
        protected const float slowRadius = 0.75f;

        /// <summary>
        /// Time over which to achieve target speed
        /// </summary>
        protected const float timeToTarget = 0.1f;

        #endregion

        #region Methods

        /// <summary>
        /// Transforms a given number into its equivalent closest to zero
        /// </summary>
        /// <param name="rads">Number to transform</param>
        /// <returns>The closest number's transformation to zero</returns>
        protected float MapRadians(float rads)
        {
            // Convert the radians to the interval [0, 2pi)
            while (rads >= Math.PI * 2)
            {
                rads -= (float)Math.PI * 2;
            }

            while (rads < 0)
            {
                rads += (float)Math.PI * 2;
            }

            // Now choose the value closest to 0
            if (rads < Math.Abs(rads - (float)Math.PI * 2))
            {
                return rads;
            }
            else
            {
                return rads - (float)Math.PI * 2;
            }
        }

        protected override SteeringOutput SteeringGenerator()
        {
            SteeringOutput steering = new SteeringOutput();

            // Get the naive direction to the target
            float rotation = target.orientation - character.orientation;

            // Map the result to the (-pi, pi) interval
            rotation = MapRadians(rotation);
            float rotationSize = Math.Abs(rotation);

            // Check if we are there
            if (rotationSize < targetRadius)
                return steering;

            float targetRotation = 0;

            // If we are outside slowRadius, go full speed
            if (rotationSize > slowRadius)
            {
                targetRotation = maxRotation;
            }
            // Otherwise calculate a scaled rotation
            else
            {
                targetRotation = maxRotation * rotationSize / slowRadius;
            }

            // Now combine the speed with the direction
            targetRotation *= rotation / rotationSize;

            // Acceleration tries to get to the target rotation
            steering.angular = targetRotation - character.rotation;
            steering.angular /= timeToTarget;

            float angularAcceleration = Math.Abs(steering.angular);

            // Check if acceleration is too big
            if (angularAcceleration > maxAngularAcceleration)
            {
                steering.angular /= angularAcceleration;
                steering.angular *= maxAngularAcceleration;
            }

            steering.linear = Vector3.Zero;
            return steering;
        }
        
        #endregion
    }
}