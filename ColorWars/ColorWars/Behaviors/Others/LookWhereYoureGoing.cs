using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorWars
{
    /// <summary>
    /// Implements the Look Where You're Going behavior
    /// </summary>
    class LookWhereYoureGoing : Align
    {
        #region Methods
        
        protected override SteeringOutput SteeringGenerator()
        {
            // First calculate the target to delegate to Align
            SteeringOutput steering = new SteeringOutput();

            // Check for a zero direction and make no change if so
            if (character.velocity.Length() == 0)
            {
                return steering;
            }

            // Otherwise set the target based on the velocity
            target.orientation = (float) Math.Atan2(-character.velocity.X, character.velocity.Y);

            // Delegate to Align
            return base.SteeringGenerator();
        }
        
        #endregion
    }
}