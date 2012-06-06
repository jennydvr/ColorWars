using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    /// <summary>
    /// Defines a proper structure for output the linear and angular acceleration
    /// </summary>
    class SteeringOutput
    {
        #region Variables

        /// <summary>
        /// Linear acceleration
        /// </summary>
        public Vector3 linear = Vector3.Zero;

        /// <summary>
        /// Angular acceleration
        /// </summary>
        public float angular = 0;

        #endregion

        #region Operators

        /// <summary>
        /// Defines the sum of two steering outputs
        /// </summary>
        /// <param name="first">First operand</param>
        /// <param name="second">Second operand</param>
        /// <returns>A new steering that corresponds to the sum of s1 and s2</returns>
        public static SteeringOutput operator +(SteeringOutput first, SteeringOutput second)
        {
            SteeringOutput res = new SteeringOutput();
            res.linear = first.linear + second.linear;
            res.angular = first.angular + second.angular;

            return res;
        }

        /// <summary>
        /// Defines the multiplication of a steering output with a constant
        /// </summary>
        /// <param name="steering">Operand</param>
        /// <param name="constant">Constant</param>
        /// <returns>A new steering that corresponds to the multiplication of a steering and a constant</returns>
        public static SteeringOutput operator *(SteeringOutput steering, int constant)
        {
            SteeringOutput res = new SteeringOutput();
            res.linear = steering.linear * constant;
            res.angular = steering.angular * constant;

            return res;
        }

        /// <summary>
        /// Defines the multiplication of a steering output with a constant
        /// </summary>
        /// <param name="constant">Constant</param>
        /// <param name="steering">Operand</param>
        /// <returns>A new steering that corresponds to the multiplication of a constant and a steering</returns>
        public static SteeringOutput operator *(int constant, SteeringOutput steering)
        {
            return steering * constant;
        }

        /// <summary>
        /// Defines the multiplication of a steering output with a constant
        /// </summary>
        /// <param name="steering">Operand</param>
        /// <param name="constant">Constant</param>
        /// <returns>A new steering that corresponds to the multiplication of a steering and a constant</returns>
        public static SteeringOutput operator *(SteeringOutput steering, double constant)
        {
            SteeringOutput res = new SteeringOutput();
            res.linear = steering.linear * (float)constant;
            res.angular = steering.angular * (float)constant;

            return res;
        }

        /// <summary>
        /// Defines the multiplication of a steering output with a constant
        /// </summary>
        /// <param name="constant">Constant</param>
        /// <param name="steering">Operand</param>
        /// <returns>A new steering that corresponds to the multiplication of a constant and a steering</returns>
        public static SteeringOutput operator *(double constant, SteeringOutput steering)
        {
            return steering * constant;
        }

        #endregion
    }
}