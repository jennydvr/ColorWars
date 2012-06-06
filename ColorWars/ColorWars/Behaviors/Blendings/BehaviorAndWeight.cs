using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorWars
{
    /// <summary>
    /// Struct that holds a behavior and an associated weight to it
    /// </summary>
    class BehaviorAndWeight
    {
        #region Variables

        /// <summary>
        /// Behavior to control
        /// </summary>
        public SteeringBehavior behavior;

        /// <summary>
        /// Weight of the behavior
        /// </summary>
        public double weight = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="b">Behavior</param>
        /// <param name="w">Weight</param>
        public BehaviorAndWeight(SteeringBehavior b, double w)
        {
            behavior = b;
            weight = w;
        }

        #endregion
    }
}