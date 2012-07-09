namespace ColorWars
{
    class SmellTransition : Transition
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="character">Character of the transition</param>
        public SmellTransition(Character character) :
            base(character)
        {
            this.targetState = 3;
            this.condition = character.sensors[0];
        }

        #endregion
    }
}
