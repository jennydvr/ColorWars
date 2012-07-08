namespace ColorWars
{
    /// <summary>
    /// Implements the Not condition
    /// </summary>
    class NotCondition : Condition
    {
        #region Variables

        /// <summary>
        /// Condition to evaluate
        /// </summary>
        protected Condition condition;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the test</param>
        /// <param name="condition">Condition to test</param>
        public NotCondition(Character character, Condition condition) :
            base(character)
        {
            this.condition = condition;
        }

        public override bool Test()
        {
            return !condition.Test();
        }

        #endregion
    }
}