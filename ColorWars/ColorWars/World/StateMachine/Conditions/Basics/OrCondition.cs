﻿namespace ColorWars
{
    /// <summary>
    /// Implements the Or condition
    /// </summary>
    class OrCondition : Condition
    {
        #region Variables

        /// <summary>
        /// First condition to evaluate
        /// </summary>
        protected Condition first;

        /// <summary>
        /// Second condition to evaluate
        /// </summary>
        protected Condition second;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character to test</param>
        /// <param name="first">First condition to test</param>
        /// <param name="second">Second condition to test</param>
        public OrCondition(Character character, Condition first, Condition second) :
            base(character)
        {
            this.first = first;
            this.second = second;
        }

        public override bool Test()
        {
            return first.Test() | second.Test();
        }

        #endregion
    }
}