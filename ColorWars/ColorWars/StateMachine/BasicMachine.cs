namespace ColorWars
{
    /// <summary>
    /// Defines a state machine for this game
    /// There are no actions for simplicity
    /// </summary>
    class BasicMachine : StateMachine
    {
        #region Constructor

        public BasicMachine(Character character) :
            base(character)
        {
            // States
            PursueState pursue = new PursueState(character);
            EvadeState evade = new EvadeState(character);
            DeadState die = new DeadState(character);

            // Add states
            this.states.Add(pursue);
            this.states.Add(evade);
            this.states.Add(die);

            this.initialState = this.currentState = pursue;
        }

        #endregion
    }
}