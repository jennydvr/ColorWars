using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gearset;

namespace ColorWars
{
    /// <summary>
    /// Implements the friends avoidance behavior
    /// An Obstacle Avoidance with a twist to avoid friends
    /// </summary>
    class FriendsAvoidance : ObstacleAvoidance
    {
        #region Variables

        /// <summary>
        /// Owner of this behavior
        /// </summary>
        protected Character owner;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">Owner of this behavior</param>
        public FriendsAvoidance(Character owner)
        {
            this.owner = owner;

            // Redefine some values
            avoidDistance = 150;
            lookahead = 75;
            maxTime = 200;
        }

        protected override Collision GetCollisions(Vector3 position, Vector3 ray)
        {
            // Calculate two more rays
            double alpha = Math.Atan2(ray.Y, ray.X);

            Vector3 aRay = ray.Length() * new Vector3((float)Math.Cos(alpha + Math.PI / 4), (float)Math.Sin(alpha + Math.PI / 4), 0);
            Vector3 bRay = ray.Length() * new Vector3((float)Math.Cos(alpha - Math.PI / 4), (float)Math.Sin(alpha - Math.PI / 4), 0);

            // Now try to find a collision with any ray
            Collision collision = CollisionDetector.GetCollision(position, ray, owner);

            if (collision.position == Vector3.Zero)
            {
                collision = CollisionDetector.GetCollision(character.position, aRay, owner);

                if (collision.position == Vector3.Zero)
                    collision = CollisionDetector.GetCollision(character.position, bRay, owner);
            }

            if (DEBUG)
            {
                GS.ShowVector2("FR 1", new Vector2(position.X, position.Y), new Vector2(ray.X, ray.Y), Color.Gray);
                GS.ShowVector2("FR 2", new Vector2(position.X, position.Y), new Vector2(aRay.X, aRay.Y), Color.Gray);
                GS.ShowVector2("FR 3", new Vector2(position.X, position.Y), new Vector2(bRay.X, bRay.Y), Color.Gray);
            }

            return collision;
        }

        protected new void Debug(SteeringOutput steering)
        {
            GS.ShowMark("Friends Avoidance target", new Vector2(base.target.position.X, base.target.position.Y));

            Vector2 position = new Vector2(character.position.X, character.position.Y);
            GS.ShowVector2("Friends Avoidance", position, new Vector2(steering.linear.X, steering.linear.Y), Color.Yellow);
        }

        #endregion
    }
}