﻿namespace ColorWars
{
    class DeadTransition : Transition
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the transition</param>
        public DeadTransition(Character character) :
            base(character)
        {
            this.targetState = new DeadState(character);
            this.condition = new DeadCondition(character);
        }

        #endregion
    }
}