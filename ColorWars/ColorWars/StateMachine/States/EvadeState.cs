namespace ColorWars
{
    class EvadeState : State
    {
        #region Constructor

        public EvadeState(Character character)
            : base(character)
        {
            // Initialize the behavior
            behavior = new SmartEvade(character);

            // And now initialize the transitions
            transitions.Add(new DeadTransition(character));
            transitions.Add(new SmellTransition(character));
            transitions.Add(new HighLifeTransition(character));
        }

        #endregion
    }
}
