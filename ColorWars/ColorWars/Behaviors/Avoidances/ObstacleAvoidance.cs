using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gearset;

namespace ColorWars
{
    /// <summary>
    /// Implements the obstacle avoidance behavior
    /// </summary>
    class ObstacleAvoidance : Seek
    {
        #region Variables

        /// <summary>
        /// True if it was found a collision
        /// </summary>
        private bool flag = false;

        /// <summary>
        /// Counter of the time that a steering should be returned
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Last steering found
        /// </summary>
        private SteeringOutput lastSteering;

        #endregion

        #region Constants

        /// <summary>
        /// How far to avoid a collision. Should be greater than the character's radius
        /// </summary>
        protected float avoidDistance = 100;

        /// <summary>
        /// Distance to look ahead for a collision
        /// </summary>
        protected float lookahead = 180;

        /// <summary>
        /// Maximum time for an steering
        /// </summary>
        protected float maxTime = 100;

        #endregion

        #region Methods

        /// <summary>
        /// Calculates posible collisions
        /// </summary>
        /// <param name="position">Initial position</param>
        /// <param name="ray">Ending position</param>
        /// <returns></returns>
        protected virtual Collision GetCollisions(Vector3 position, Vector3 ray)
        {
            // Calculate two more rays
            double alpha = Math.Atan2(ray.Y, ray.X);

            Vector3 aRay = ray.Length() * 0.5f * new Vector3((float)Math.Cos(alpha + Math.PI / 4), (float)Math.Sin(alpha + Math.PI / 4), 0);
            Vector3 bRay = ray.Length() * 0.5f * new Vector3((float)Math.Cos(alpha - Math.PI / 4), (float)Math.Sin(alpha - Math.PI / 4), 0);

            // Now try to find a collision with any ray
            Collision collision = CollisionDetector.GetCollision(position, ray);
            
            if (collision.position == Vector3.Zero)
            {
                collision = CollisionDetector.GetCollision(character.position, aRay);

                if (collision.position == Vector3.Zero)
                    collision = CollisionDetector.GetCollision(character.position, bRay);
            }

            if (DEBUG)
            {
                GS.ShowVector2("Ray 1", new Vector2(position.X, position.Y), new Vector2(ray.X, ray.Y), Color.Black);
                GS.ShowVector2("Ray 2", new Vector2(position.X, position.Y), new Vector2(aRay.X, aRay.Y), Color.Black);
                GS.ShowVector2("Ray 3", new Vector2(position.X, position.Y), new Vector2(bRay.X, bRay.Y), Color.Black);
            }

            return collision;
        }

        protected override SteeringOutput SteeringGenerator()
        {
            // Calculate the first collision ray vector
            Vector3 rayVector = character.velocity;
            rayVector.Normalize();
            rayVector *= lookahead;

            // Find the collision
            Vector3 position = character.position + new Vector3(20, 20, 0) * -rayVector / lookahead;

            Collision collision = GetCollisions(position, rayVector);

            // If there is no collision, check if another acceleration should act
            if (collision.position == Vector3.Zero)
            {
                // Check for the flag
                if (flag)
                    if (counter < maxTime)
                    {
                        if (DEBUG)
                        {
                            Debug(lastSteering);
                            GS.Show("Collision", true);
                        }

                        counter += 1;
                        return lastSteering;
                    }
                    else
                    {
                        counter = 0;
                        flag = false;
                    }

                return new SteeringOutput();
            }

            // Otherwise create a target and delegate to seek
            base.target = target;
            base.target.position = collision.position + collision.normal * avoidDistance;

            SteeringOutput steering = base.SteeringGenerator();

            if (DEBUG)
                Debug(steering);

            flag = true;
            lastSteering = steering;

            return steering;
        }

        protected new void Debug(SteeringOutput steering)
        {
            GS.ShowMark("Avoidance target", new Vector2(base.target.position.X, base.target.position.Y));

            Vector2 position = new Vector2(character.position.X, character.position.Y);
            GS.ShowVector2("Avoidance", position, new Vector2(steering.linear.X, steering.linear.Y), Color.Coral);
        }

        #endregion
    }
}