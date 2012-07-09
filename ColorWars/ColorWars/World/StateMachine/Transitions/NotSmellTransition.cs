namespace ColorWars
{
    class NotSmellTransition : Transition
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the transition</param>
        public NotSmellTransition(Character character) :
            base(character)
        {
            this.targetState = 1;
            this.condition = new NotCondition(character, character.sensors[0]);
        }

        #endregion
    }
}
