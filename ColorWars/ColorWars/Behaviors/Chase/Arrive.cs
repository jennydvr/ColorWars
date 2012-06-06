using Gearset;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Implements the arrive behavior
    /// </summary>
    class Arrive : SteeringBehavior
    {
        #region Constants

        /// <summary>
        /// The maximum acceleration allowed
        /// </summary>
        public float maxAcceleration = 150;

        /// <summary>
        /// The maximum speed allowed
        /// </summary>
        public float maxSpeed = 100;

        /// <summary>
        /// Radius for arriving at the target
        /// </summary>
        public float targetRadius = 80;

        /// <summary>
        /// Radius for beginning to slow down
        /// </summary>
        public float slowRadius = 120;

        /// <summary>
        /// Time over which to achieve target speed
        /// </summary>
        protected const float timeToTarget = 0.1f;

        #endregion

        #region Methods

        public override SteeringOutput GetSteering()
        {
            // If the target was reached, stop your movement
            if ((character.position - target.position).Length() <= 70)
            {
                character.velocity = Vector3.Zero;
                character.rotation = 0;
                return new SteeringOutput();
            }

            return base.GetSteering();
        }

        public override SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            // If the target was reached, stop your movement
            if ((character.position - target.position).Length() <= 70)
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
            Vector3 direction = target.position - character.position;
            float distance = direction.Length();

            // Check if we are there
            if (distance < targetRadius)
                return steering;

            float targetSpeed = 0;

            // If we are outside slowRadius, go maxSpeed
            if (distance > slowRadius)
                targetSpeed = maxSpeed;

            // Otherwise calculate a scaled speed
            else
                targetSpeed = maxSpeed * distance / slowRadius;

            // The target velocity combines speed and direction
            Vector3 targetVelocity = direction;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            // Acceleration tries to get to the target velocity
            steering.linear = targetVelocity - character.velocity;
            steering.linear /= timeToTarget;

            // Check if acceleration is too fast
            if (steering.linear.Length() > maxAcceleration)
            {
                steering.linear.Normalize();
                steering.linear *= maxAcceleration;
            }

            steering.angular = 0;

            if (DEBUG)
                Debug(steering);

            return steering;
        }
        
        protected void Debug(SteeringOutput steering)
        {
            Vector2 position = new Vector2(character.position.X, character.position.Y);
            GS.ShowVector2("Seek", position, new Vector2(steering.linear.X, steering.linear.Y), Color.Violet);
        }
        
        #endregion
    }
}