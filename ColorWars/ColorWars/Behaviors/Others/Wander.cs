using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gearset;
using Microsoft.Xna.Framework.Graphics;

namespace ColorWars
{
    class Wander : Face
    {
        #region Constants

        /// <summary>
        /// Offset of the wander circle
        /// </summary>
        protected const float wanderOffset = 75;

        /// <summary>
        /// Radius of the wander circle
        /// </summary>
        protected const float wanderRadius = 25;

        /// <summary>
        /// Maximum rate at which the wander orientation can change
        /// </summary>
        protected const float wanderRate = (float) Math.PI / 2;

        /// <summary>
        /// Current orientation of the wander target
        /// </summary>
        protected float wanderOrientation = 0;

        /// <summary>
        /// Maximum acceleration of the character
        /// </summary>
        protected const float maxAcceleration = 100;

        #endregion

        #region Methods

        /// <summary>
        /// Calculates a random number
        /// </summary>
        /// <returns>Random number in [-1, 1]</returns>
        protected double randomBinomial()
        {
            Random r = new Random();
            return (float)(r.NextDouble() - r.NextDouble()); 
        }
        
        protected override SteeringOutput SteeringGenerator()
        {
            // Update the wander orientation
            wanderOrientation += (float)randomBinomial() * wanderRate;

            // Calculate the combined target orientation
            float targetOrientation = wanderOrientation + character.orientation;

            // Calculate the center of the wander circle
            Vector3 circleLocation = new Vector3((float)Math.Cos(character.orientation + Math.PI / 2), (float)Math.Sin(character.orientation + Math.PI / 2), 0);
            circleLocation.Normalize();

            target.position = character.position + circleLocation * wanderOffset;

            // Calculate the center of the wander circle
            Vector3 targetOrientationVector = new Vector3((float)Math.Cos(targetOrientation), (float)Math.Sin(targetOrientation), 0);
            targetOrientationVector.Normalize();

            target.position += wanderRadius * targetOrientationVector;

            // Delegate to face
            base.target = target;

            SteeringOutput steering = base.SteeringGenerator();

            // Set full linear acceleration
            Vector3 orientation = new Vector3((float)Math.Cos(character.orientation + Math.PI / 2), (float)Math.Sin(character.orientation + Math.PI / 2), 0);
            orientation.Normalize();
            steering.linear = maxAcceleration * orientation;

            if (DEBUG)
                Debug();

            return steering;
        }

        protected void Debug()
        {
            Vector2 offset = new Vector2((float)Math.Cos(character.orientation + Math.PI / 2), (float)Math.Sin(character.orientation + Math.PI / 2));
            Vector2 position = new Vector2(character.position.X, character.position.Y);
            offset.Normalize();
            offset *= wanderOffset;

            GS.ShowVector2("Offset", position, offset, Color.Cyan);
            GS.ShowVector2("Target", position + offset, new Vector2(target.position.X, target.position.Y) - position, Color.Cyan);
        }

        #endregion
    }
}