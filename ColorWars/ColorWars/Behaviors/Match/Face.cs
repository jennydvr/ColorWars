using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Gearset;

namespace ColorWars
{
    /// <summary>
    /// Implements the align behavior
    /// </summary>
    class Face : Align
    {
        #region Methods
        
        protected override SteeringOutput SteeringGenerator()
        {   
            // Work out the direction to the target
            Vector3 direction = target.position - character.position;

            if (direction.Length() == 0)
            {
                return new SteeringOutput();
            }

            // Put the target together
            base.target = target;
            base.target.orientation = (float)Math.Atan2(-direction.X, direction.Y);

            return base.SteeringGenerator();
        }

        #endregion
    }
}