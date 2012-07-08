namespace ColorWars
{
    class ChaseLifeState : State
    {
        #region Constructor

        public ChaseLifeState(Character character)
            : base(character)
        {
            // Initialize the behavior
            behavior = new SmellCatch(character);

            // And now initialize the transitions
            transitions.Add(new NotSmellTransition(character));
        }

        #endregion
    }
}