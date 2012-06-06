namespace ColorWars
{
    /// <summary>
    /// Defines the transitions of a state
    /// </summary>
    class Transition
    {
        #region Variables

        /// <summary>
        /// Target state of this transition
        /// </summary>
        protected State targetState;

        /// <summary>
        /// Condition to check
        /// </summary>
        protected Condition condition;

        /// <summary>
        /// Character checking this transition
        /// </summary>
        protected Character character;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the transition</param>
        public Transition(Character character)
        {
            this.character = character;
        }

        /// <summary>
        /// Gets the target state
        /// </summary>
        /// <returns>The target state of this transition</returns>
        public State getTargetState()
        {
            return targetState;
        }

        /// <summary>
        /// Returns whether this transition shall be activated or not
        /// </summary>
        /// <returns>True if activated, false otherwise</returns>
        public bool isTriggered()
        {
            return condition.test();
        }

        #endregion
    }
}