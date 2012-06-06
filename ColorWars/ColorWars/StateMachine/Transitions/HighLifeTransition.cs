namespace ColorWars
{
    class HighLifeTransition : Transition
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the transition</param>
        public HighLifeTransition(Character character) :
            base(character)
        {
            this.targetState = new EvadeState(character);
            this.condition = new NotCondition(character, new LowLifeCondition(character));
        }

        #endregion
    }
}
