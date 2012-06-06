namespace ColorWars
{
    /// <summary>
    /// Checks if the character has low life
    /// </summary>
    class LowLifeCondition : Condition
    {
        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character</param>
        public LowLifeCondition(Character character) :
            base(character)
        {
        }

        public override bool test()
        {
            return character.life <= 25;
        }

        #endregion
    }
}