using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class Node : Point
    {
        #region Variables

        /// <summary>
        /// ID of the node
        /// </summary>
        public int id;

        /// <summary>
        /// Signals in this node
        /// </summary>
        public List<Signal> signals = new List<Signal>();

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="point">Position of the node</param>
        /// <param name="id">ID of the node</param>
        public Node(Vector2 point, int id)
            : base(point)
        {
            this.id = id;
            this.color = Color.Green;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the signals in this node
        /// </summary>
        /// <param name="time">Elapsed game time</param>
        public void Update(GameTime time)
        {
            // Update every signal
            foreach (Signal signal in signals)
                signal.Update(time);

            // Remove the not activated signals
            signals.RemoveAll(NotActivated);

            // Update this node color
            this.color = Color.Green;
            foreach (Signal signal in signals)
                if (signal.activated)
                    this.color = Color.Gray;
        }

        /// <summary>
        /// Adds a signal to the list
        /// </summary>
        /// <param name="intensity">Intensity of the signal</param>
        public void AddSignal(Signal signal)
        {
            signals.Add(signal);
        }

        /// <summary>
        /// Calculates the ditance between two nodes
        /// </summary>
        /// <param name="first">First node</param>
        /// <param name="second">Second node</param>
        /// <returns>Float that represents the distance</returns>
        static public float Distance(Node first, Node second)
        {
            return Vector2.Distance(first.point, second.point);
        }

        #endregion

        #region Auxiliar Methods

        /// <summary>
        /// Checks whether a signal is activatedd or not
        /// </summary>
        /// <param name="signal">Signal to check</param>
        /// <returns>True if not activated, false otherwise</returns>
        protected bool NotActivated(Signal signal)
        {
            return signal.endInterval == -1;
        }

        #endregion
    }
}
