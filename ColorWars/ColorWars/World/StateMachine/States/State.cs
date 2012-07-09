using System.Collections.Generic;

namespace ColorWars
{
    /// <summary>
    /// Defines a superclass for the states of a game
    /// There are no actions for simplicity; in this case,
    /// the behaviors are called directly since one state
    /// corresponds to one behavior (probably a blending)
    /// </summary>
    class State
    {
        #region Variables

        /// <summary>
        /// Behavior executed in the state
        /// </summary>
        protected SteeringBehavior behavior;

        /// <summary>
        /// List of transitions outgoing from this state
        /// </summary>
        protected List<Transition> transitions;

        /// <summary>
        /// Character using the state
        /// </summary>
        protected Character character;

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public State(Character character)
        {
            transitions = new List<Transition>();
            this.character = character;
        }

        /// <summary>
        /// Generates a steering output
        /// </summary>
        /// <param name="character">Character for the behavior</param>
        /// <param name="target">Target for the behavior</param>
        /// <returns>Generated steering</returns>
        public SteeringOutput GetSteering(Kinematic character, Kinematic target)
        {
            return behavior.GetSteering(character, target);
        }

        /// <summary>
        /// Gets the transition list
        /// </summary>
        /// <returns>The transitions list</returns>
        public List<Transition> GetTransitions()
        {
            return transitions;
        }

        #endregion
    }
}