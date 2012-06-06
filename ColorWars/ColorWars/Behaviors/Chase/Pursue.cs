using Gearset;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Implements the pursue behavior
    /// Unlike the book, it is subclass of arrive.
    /// This is because arrive is more accurate than seek.
    /// </summary>
    class Pursue : Arrive
    {
        #region Variables

        protected Vector3 lastPosition = Vector3.Zero;

        #endregion

        #region Constants

        /// <summary>
        /// Maximum prediction time
        /// </summary>
        public float maxPrediction = 100;

        #endregion

        #region Methods
        
        protected override SteeringOutput SteeringGenerator()
        {
            // If the target was reached, return nothing
            if ((target.position - character.position).Length() < 80)
            {
                SteeringOutput negative = new SteeringOutput();
                negative.linear = character.velocity * -1;

                return negative;
            }

            // If your target hasn't moved in a while, go seek him
            if (lastPosition == target.position)
            {
                base.target = target;
                return base.SteeringGenerator();
            }

            lastPosition = target.position;

            // Work out the distance to the target
            Vector3 direction = target.position - character.position;
            float distance = direction.Length();

            // Work out current speed
            float speed = character.velocity.Length();

            // Calculate prediction time
            float prediction = 0;

            if (speed <= distance / maxPrediction)
                prediction = maxPrediction;
            else
                prediction = distance / speed;

            // Call arrive
            base.target = target;
            base.target.position += base.target.velocity * prediction;

            SteeringOutput steering = base.SteeringGenerator();

            if (DEBUG)
                Debug(steering);

            return steering;
        }

        protected new void Debug(SteeringOutput steering)
        {
            Vector2 position = new Vector2(character.position.X, character.position.Y);
            GS.ShowVector2("Pursue", position, new Vector2(steering.linear.X, steering.linear.Y), Color.BlanchedAlmond);
        }

        #endregion
    }
}