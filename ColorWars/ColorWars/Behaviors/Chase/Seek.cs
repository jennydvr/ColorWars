using Gearset;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Implements the seek behavior
    /// </summary>
    class Seek : SteeringBehavior
    {
        #region Constants

        /// <summary>
        /// The maximum acceleration of the character
        /// </summary>
        protected const float maxAcceleration = 150;

        /// <summary>
        /// Radius for stopping the movement
        /// </summary>
        public int endingRadius = 70;

        #endregion

        #region Methods
        
        public override SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            // If the target was reached, stop your movement
            if ((character.position - target.position).Length() <= endingRadius)
            {
                character.velocity = Vector3.Zero;
                character.rotation = 0;
                return new SteeringOutput();
            }

            return base.GetSteering(character, target);
        }

        protected override SteeringOutput SteeringGenerator()
        {
            SteeringOutput steering = new SteeringOutput();

            // Get the direction to the target
            steering.linear = target.position - character.position;

            // Give full acceleration along this direction
            steering.linear.Normalize();
            steering.linear *= maxAcceleration;

            steering.angular = 0;

            if (DEBUG)
                Debug(steering);

            return steering;
        }

        protected void Debug(SteeringOutput steering)
        {
            Vector2 position = new Vector2(character.position.X, character.position.Y);
            GS.ShowVector2("Seek", position, new Vector2(steering.linear.X, steering.linear.Y), Color.LawnGreen);
        }

        #endregion
    }
}