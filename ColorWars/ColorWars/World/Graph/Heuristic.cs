﻿using Microsoft.Xna.Framework;

namespace ColorWars
{
    class Heuristic
    {
        /// <summary>
        /// Goal that this heuristic is estimating for
        /// </summary>
        private Node goal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="goal">Goal node for this heuristic</param>
        public Heuristic(Node goal)
        {
            this.goal = goal;
        }

        /// <summary>
        /// Estimates the heuristic value for this node
        /// </summary>
        /// <param name="node">Node to estimate for</param>
        /// <returns>Float estimated</returns>
        public float estimate(Node node)
        {
            return GameMode.graph.arcs[goal.id, node.id];
        }
    }
}