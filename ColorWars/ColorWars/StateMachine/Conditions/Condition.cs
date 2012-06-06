namespace ColorWars
{
    /// <summary>
    /// Defines different kinds of conditions of this game
    /// </summary>
    abstract class Condition
    {
        #region Variable

        /// <summary>
        /// Character testing this condition
        /// </summary>
        protected Character character;

        #endregion

        #region Constructor

        public Condition(Character character)
        {
            this.character = character;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks whether this condition evaluates to true or not
        /// </summary>
        /// <returns>True if applies, false otherwise</returns>
        abstract public bool test();

        #endregion
    }
}