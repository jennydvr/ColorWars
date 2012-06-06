using System.Collections.Generic;

namespace ColorWars
{
    /// <summary>
    /// Defines a state machine for this game
    /// There are no actions for simplicity
    /// </summary>
    class StateMachine
    {
        #region Variables

        /// <summary>
        /// Holds a list of states for the machine
        /// </summary>
        protected List<State> states;

        /// <summary>
        /// Initial state
        /// </summary>
        protected State initialState;

        /// <summary>
        /// Current state
        /// </summary>
        protected State currentState;

        /// <summary>
        /// Character of this machine
        /// </summary>
        protected Character character;

        #endregion

        #region Constructor

        public StateMachine(Character character)
        {
            this.character = character;
            this.states = new List<State>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks and applies transitions
        /// </summary>
        /// <returns>The steering generated</returns>
        public SteeringOutput Update(Kinematic character, Kinematic target)
        {
            // Asume none transition is triggered
            Transition triggeredTransition = null;

            // Check through the transitions and store the first one that triggers
            foreach (Transition transition in currentState.GetTransitions())
                if (transition.isTriggered())
                {
                    triggeredTransition = transition;
                    break;
                }

            // Check if there is a transition to fire
            if (triggeredTransition != null)
                currentState = triggeredTransition.getTargetState();

            return currentState.GetSteering(character, target);
        }

        #endregion
    }
}