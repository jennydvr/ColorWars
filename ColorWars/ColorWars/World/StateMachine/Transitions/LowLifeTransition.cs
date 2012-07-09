namespace ColorWars
{
    class LowLifeTransition : Transition
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the transition</param>
        public LowLifeTransition(Character character) :
            base(character)
        {
            this.targetState = 1;
            this.condition = new LowLifeCondition(character);
        }

        #endregion
    }
}
