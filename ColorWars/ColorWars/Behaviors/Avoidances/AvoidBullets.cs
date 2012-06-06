using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Behavior that allows a character to avoid bullets
    /// </summary>
    class AvoidBullets : Evade
    {
        #region Constants

        /// <summary>
        /// Sets the radius in which a bullet has to be fleed
        /// </summary>
        protected const float characterRadius = 80;

        #endregion

        #region Methods

        protected override SteeringOutput SteeringGenerator()
        {
            // Calculate if there is a bullet close enough to me
            Vector3 minDistance = new Vector3(float.PositiveInfinity, 0, 0);

            foreach (Paintball ball in CollisionDetector.balls)
            {
                Vector3 position = new Vector3(ball.kinematic.position.X, ball.kinematic.position.Y, 0);
                float distance = (position - character.position).Length();

                if (distance < minDistance.Length() & distance < characterRadius)
                    minDistance = position;
            }

            // If there is no bullet close enough, don't do anything
            if (minDistance.Length() == float.PositiveInfinity)
                return new SteeringOutput();

            // If there is, avoid it
            base.target = target;
            base.target.position = minDistance;

            return base.SteeringGenerator();
        }

        #endregion
    }
}
