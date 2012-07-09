namespace ColorWars
{
    class DeadState : State
    {
        #region Constructor

        public DeadState(Character character)
            : base(character)
        {
            // Initialize the behavior
            behavior = new FleeDisappear(character);
        }

        #endregion
    }
}
