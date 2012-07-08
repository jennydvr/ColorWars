namespace ColorWars
{
    /// <summary>
    /// Defines a state machine for this game
    /// There are no actions for simplicity
    /// </summary>
    class BasicStarMachine : BasicMachine
    {
        #region Constructor

        public BasicStarMachine(Character character) :
            base(character)
        {
        }

        #endregion
    }
}