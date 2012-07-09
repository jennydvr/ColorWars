using Microsoft.Xna.Framework;

namespace ColorWars
{
    class ClosestHeuristic : Heuristic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="goal">Goal node for this heuristic</param>
        public ClosestHeuristic(Node goal) :
            base(goal)
        {
        }

        public override float estimate(Node node)
        {
            return GameMode.movement.arcs[goal.id, node.id];
        }
    }
}