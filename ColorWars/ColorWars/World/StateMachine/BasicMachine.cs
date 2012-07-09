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
            StarState star = new StarState(character);
            EvadeState evade = new EvadeState(character);
            DeadState die = new DeadState(character);
            ChaseLifeState chase = new ChaseLifeState(character);

            // Add states
            this.states.Add(star);
            this.states.Add(evade);
            this.states.Add(die);
            this.states.Add(chase);

            this.initialState = this.currentState = star;
        }

        #endregion
    }
}