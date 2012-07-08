using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Implements the evade behavior
    /// </summary>
    class Evade : Flee
    {
        #region Constants

        /// <summary>
        /// Maximum prediction time
        /// </summary>
        public float maxPrediction = 100;

        #endregion

        #region Methods

        protected override SteeringOutput SteeringGenerator()
        {
            SteeringOutput steering = new SteeringOutput();

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

            // Call flee
            base.target = target;
            base.target.position += base.target.velocity * prediction;

            return base.SteeringGenerator();
        }

        #endregion
    }
}