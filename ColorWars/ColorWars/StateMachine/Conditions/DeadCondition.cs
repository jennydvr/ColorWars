namespace ColorWars
{
    /// <summary>
    /// Checks if the character is dead
    /// </summary>
    class DeadCondition : Condition
    {
        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character</param>
        public DeadCondition(Character character) :
            base(character)
        {
        }

        public override bool Test()
        {
            return character.life <= 0;
        }

        #endregion
    }
}