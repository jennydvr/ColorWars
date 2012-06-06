using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Defines the kinematics for a character
    /// </summary>
    class Kinematic
    {
        #region Variables

        /// <summary>
        /// The position of the character
        /// </summary>
        public Vector3 position = Vector3.Zero;

        /// <summary>
        /// The orientation (radians) of the character
        /// </summary>
        public float orientation = 0;

        /// <summary>
        /// The velocity of the character
        /// </summary>
        public Vector3 velocity = Vector3.Zero;

        /// <summary>
        /// The angular speed of the character
        /// </summary>
        public float rotation = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Clones this kinematic
        /// </summary>
        /// <returns>A clone of this kinematic</returns>
        public Kinematic Clone() {
            Kinematic k = new Kinematic();
            k.position = new Vector3(position.X, position.Y, position.Z);
            k.velocity = new Vector3(velocity.X, velocity.Y, velocity.Z);
            k.orientation = orientation;
            k.rotation = rotation;

            return k;
        }

        /// <summary>
        /// Updates the kinematic according to the parameters
        /// </summary>
        /// <param name="steering">Angular and linear acceleration</param>
        /// <param name="maxSpeed">Maximum speed allowed</param>
        /// <param name="time">Elapsed game time</param>
        public void Update(SteeringOutput steering, float maxSpeed, GameTime time)
        {
            // Update the position and orientation
            position += velocity * (float)time.ElapsedGameTime.TotalSeconds;
            orientation += rotation * (float)time.ElapsedGameTime.TotalSeconds;

            // Now the velocity and rotation
            velocity += steering.linear * (float)time.ElapsedGameTime.TotalSeconds;
            rotation += steering.angular * (float)time.ElapsedGameTime.TotalSeconds;

            // Check for speeding and clip
            if (velocity.Length() > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }
        }

        #endregion
    }
}