namespace ColorWars
{
    /// <summary>
    /// Defines a state machine for this game
    /// There are no actions for simplicity
    /// </summary>
    class BasicPursueMachine : BasicMachine
    {
        #region Constructor

        public BasicPursueMachine(Character character) :
            base(character)
        {
            // States
            PursueState pursue = new PursueState(character);

            // Add states
            this.states.Add(pursue);

            this.initialState = this.currentState = pursue;

            // Remove star
            this.states.RemoveAt(0);
        }

        #endregion
    }
}