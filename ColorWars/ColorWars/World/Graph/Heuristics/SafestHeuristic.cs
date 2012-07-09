using Microsoft.Xna.Framework;

namespace ColorWars
{
    class SafestHeuristic : Heuristic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="goal">Goal node for this heuristic</param>
        public SafestHeuristic(Node goal) :
            base(goal)
        {
        }

        public override float estimate(Node node)
        {
            if (node.safeNode)
                return GameMode.movement.arcs[goal.id, node.id];
            else
                return GameMode.movement.arcs[goal.id, node.id] * GameMode.movement.arcs[goal.id, node.id];
        }
    }
}