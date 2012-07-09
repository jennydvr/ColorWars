namespace ColorWars
{
    class StarState : State
    {
        #region Constructor

        public StarState(Character character)
            : base(character)
        {
            // Initialize the behavior
            behavior = new StarCatch(character);

            // And now initialize the transitions
            transitions.Add(new LowLifeTransition(character));
        }

        #endregion
    }
}