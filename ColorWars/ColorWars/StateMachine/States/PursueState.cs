namespace ColorWars
{
    class PursueState : State
    {
        #region Constructor

        public PursueState(Character character)
            : base(character)
        {
            // Initialize the behavior
            behavior = new CarefullyPursue(character);

            // And now initialize the transitions
            transitions.Add(new LowLifeTransition(character));
        }

        #endregion
    }
}
